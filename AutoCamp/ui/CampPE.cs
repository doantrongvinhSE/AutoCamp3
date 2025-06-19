using AutoCamp.domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Newtonsoft.Json;

namespace AutoCamp.ui
{

    public partial class CampPE : Form
    {
        private List<string> _idTkqcList;
        private string _cookie;
        private string _token;
        private string _proxy;
        private const string CONFIG_FILE = "pe_form_config.txt";

        private class PeFormConfig
        {
            public string RichDataDraftPe { get; set; }
            public bool ImportDraft { get; set; }
            public bool PublicCampPE { get; set; }
            public string TxtAdsSet { get; set; }
            public string TxtAd { get; set; }
        }

        public CampPE(List<string> idTkqcList, string cookie, string token, string proxy)
        {
            InitializeComponent();
            _idTkqcList = idTkqcList;
            _cookie = cookie;
            _token = token;
            _proxy = proxy;
            LoadPageData();
            LoadFormData();

            // Đăng ký sự kiện FormClosing để lưu dữ liệu
            this.FormClosing += CampPE_FormClosing;
        }

        private void CampPE_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveFormData();
        }

        private void SaveFormData()
        {
            try
            {
                var config = new PeFormConfig
                {
                    RichDataDraftPe = richDataDraftPe.Text,
                    ImportDraft = cbxImportDraft.Checked,
                    PublicCampPE = cbxPublicCampPE.Checked,
                    TxtAdsSet = txtAdsSet.Text,
                    TxtAd = txtAd.Text
                };
                var json = JsonConvert.SerializeObject(config, Formatting.Indented);
                File.WriteAllText(CONFIG_FILE, json);
            }
            catch (Exception ex)
            {
                // Có thể log lỗi nếu cần
                Console.WriteLine($"Lỗi khi lưu dữ liệu form: {ex.Message}");
            }
        }

        private void LoadFormData()
        {
            try
            {
                if (File.Exists(CONFIG_FILE))
                {
                    var json = File.ReadAllText(CONFIG_FILE);
                    var config = JsonConvert.DeserializeObject<PeFormConfig>(json);
                    if (config != null)
                    {
                        richDataDraftPe.Text = config.RichDataDraftPe;
                        cbxImportDraft.Checked = config.ImportDraft;
                        cbxPublicCampPE.Checked = config.PublicCampPE;
                        txtAdsSet.Text = config.TxtAdsSet;
                        txtAd.Text = config.TxtAd;
                    }
                }
            }
            catch (Exception ex)
            {
                // Có thể log lỗi nếu cần
                Console.WriteLine($"Lỗi khi load dữ liệu form: {ex.Message}");
            }
        }

        private void LoadPageData()
        {
            // Thêm dữ liệu vào comboBox2 (PAGE ID)
            foreach (string idTkqc in _idTkqcList)
            {
                ListIdTkqcTable.Rows.Add(idTkqc, "");
            }

        }

        private async void btnRun_Click(object sender, EventArgs e)
        {
            try
            {
                string cookie = _cookie;
                string token = _token;
                string proxy = _proxy;

                // Đọc toàn bộ giá trị từ richDataDraftPe.Text trước khi vào Task
                string[] draftParts = richDataDraftPe.Text.Split('|');
                if (draftParts.Length < 3)
                {
                    MessageBox.Show("Dữ liệu draft không hợp lệ!");
                    return;
                }
                string item1 = draftParts[0];
                string item2 = draftParts[1];
                string item3 = draftParts[2];
                string valueCampainTemplate = draftParts[3];
                string valueGroupTemplate = draftParts[4];
                string valueAdTemplate = draftParts[5];

                int delayMs = 1000; // Delay giữa các lần chạy (ms)

                if (cbxImportDraft.Checked)
                {
                    var tasks = new List<Task>();
                    // Lấy dữ liệu idTkqc cho từng row trước khi vào Task
                    var rowData = new List<(DataGridViewRow row, string idTkqc)>();
                    foreach (DataGridViewRow row in ListIdTkqcTable.Rows)
                    {
                        if (row.Cells["id"].Value != null)
                        {
                            rowData.Add((row, row.Cells["id"].Value.ToString()));
                        }
                    }
                    foreach (var (row, idTkqc) in rowData)
                    {
                        tasks.Add(Task.Run(async () =>
                        {
                            try
                            {
                                this.Invoke((Action)(() => row.Cells["process"].Value = "Đang lấy idDraft..."));
                                string valueCampain = valueCampainTemplate.Replace(item1, idTkqc);
                                string valueGroup = valueGroupTemplate;
                                string valueAd = valueAdTemplate;

                                string idDarft = null;
                                for (int attempt = 0; attempt < 2; attempt++)
                                {
                                    try
                                    {
                                        idDarft = await CampPEDomain.getIdAddraft(cookie, token, idTkqc, proxy);
                                        if (idDarft != null && !idDarft.Contains("Lỗi"))
                                            break;
                                        else
                                        {
                                            string againId = await CampPEDomain.getIdAddraftAgain(cookie, idTkqc, proxy);
                                        }
                                    }
                                    catch { }
                                }




                                if (string.IsNullOrEmpty(idDarft) || (idDarft != null && idDarft.Contains("Lỗi")))
                                {
                                    this.Invoke((Action)(() => row.Cells["process"].Value = "Lỗi lấy idDarft"));
                                    return;
                                }

                                this.Invoke((Action)(() => row.Cells["process"].Value = "Lấy idDraft ok..."));



                                string result = await CampPEDomain.pushDraftCampainPe(cookie, token, idTkqc, idDarft, valueCampain, proxy);
                                if (result.Contains("Lỗi"))
                                {
                                    this.Invoke((Action)(() => row.Cells["process"].Value = "Lỗi"));
                                    return;
                                }
                                else
                                {
                                    this.Invoke((Action)(() => row.Cells["process"].Value = "Chạy campain ok..."));
                                }

                                valueGroup = valueGroup.Replace(item2, result);
                                valueGroup = valueGroup.Replace(item1, idTkqc);
                                string result2 = await CampPEDomain.pushDraftAdsetPe(cookie, token, idTkqc, idDarft, valueGroup, result, proxy);
                                if (result2.Contains("Lỗi"))
                                {
                                    this.Invoke((Action)(() => row.Cells["process"].Value = "Lỗi"));
                                    return;
                                }
                                else
                                {
                                    this.Invoke((Action)(() => row.Cells["process"].Value = "Chạy adset ok..."));
                                }

                                valueAd = valueAd.Replace(item1, idTkqc);
                                valueAd = valueAd.Replace(item2, result);
                                valueAd = valueAd.Replace(item3, result2);
                                string result3 = await CampPEDomain.pushDraftAdPe(cookie, token, idTkqc, idDarft, valueAd, result2, proxy);
                                if (result3.Contains("Lỗi"))
                                {
                                    this.Invoke((Action)(() => row.Cells["process"].Value = "Lỗi"));
                                    return;
                                }
                                else
                                {
                                    this.Invoke((Action)(() => row.Cells["process"].Value = "Chạy ad ok..."));
                                }

                                this.Invoke((Action)(() => row.Cells["process"].Value = "Đã chạy xong"));
                            }
                            catch (Exception ex)
                            {
                                this.Invoke((Action)(() => row.Cells["process"].Value = "Lỗi: " + ex.Message));
                            }
                        }));
                    }
                    await Task.WhenAll(tasks);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }


            try
            {
                string cookie = _cookie;
                string token = _token;
                string proxy = _proxy;

                if (cbxPublicCampPE.Checked)
                {
                    var tasks = new List<Task>();
                    foreach (DataGridViewRow row in ListIdTkqcTable.Rows)
                    {
                        if (row.Cells["id"].Value != null)
                        {
                            this.Invoke((Action)(() => row.Cells["process"].Value = "Đang public camp..."));
                            tasks.Add(Task.Run(async () =>
                            {
                                string result = await CampPEDomain.publicCampPeFromDraft(cookie, token, row.Cells["id"].Value.ToString(), proxy);
                                this.Invoke((Action)(() => row.Cells["process"].Value = result));
                            }));
                        }
                    }
                    await Task.WhenAll(tasks);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }

            if (cbxDeleteDraftPe.Checked)
            {
                deleteDraftCamp(ListIdTkqcTable, _cookie, _token, _proxy);
            }
        }


        public async void deleteDraftCamp(DataGridView adsAccountTable, string cookie, string token, string? proxy = null)
        {
            List<Task> tasks = new List<Task>();

            foreach (DataGridViewRow row in adsAccountTable.Rows)
            {
                if (row.Selected)
                {
                    // Copy row để dùng trong Task tránh closure issues
                    var currentRow = row;

                    var task = Task.Run(async () =>
                    {
                        try
                        {
                            string idTkqc = currentRow.Cells["id"].Value.ToString();

                            // Cập nhật UI phải gọi từ thread chính
                            adsAccountTable.Invoke((MethodInvoker)delegate
                            {
                                currentRow.Cells["process"].Value = "Đang xoá bản nháp...";
                            });

                            string result = await Task.Run(() => CampPEDomain.DeleteAllDraftPe(cookie, token, idTkqc, proxy));

                            // Cập nhật UI phải gọi từ thread chính
                            adsAccountTable.Invoke((MethodInvoker)delegate
                            {
                                currentRow.Cells["process"].Value = result;
                            });
                        }
                        catch (Exception ex)
                        {
                            // Xử lý lỗi và cập nhật UI
                            adsAccountTable.Invoke((MethodInvoker)delegate
                            {
                                currentRow.Cells["process"].Value = "Lỗi: " + ex.Message;
                            });
                        }
                    });

                    tasks.Add(task);
                }
            }

            // Đợi tất cả các task hoàn thành
            await Task.WhenAll(tasks);
        }

        private void cbxEditAllCamp_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxEditAllCamp.Checked)
            {
                panel5.Enabled = true;
            }
            else
            {
                panel5.Enabled = false;
            }
        }

        private void cbxEditNumCamp_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxEditNumCamp.Checked)
            {
                panel5.Enabled = true;
            }
            else
            {
                panel5.Enabled = false;
            }
        }

        private async void btnGetEditDraft_Click(object sender, EventArgs e)
        {
            btnGetEditDraft.Enabled = false;
            try
            {
                int selectedCount = 0;
                DataGridViewRow selectedRow = null;
                foreach (DataGridViewRow row in ListIdTkqcTable.Rows)
                {
                    if (row.Selected)
                    {
                        selectedCount++;
                        selectedRow = row;
                    }
                }
                if (selectedCount == 0)
                {
                    MessageBox.Show("Vui lòng chọn 1 tài khoản.");
                    return;
                }
                if (selectedCount > 1)
                {
                    MessageBox.Show("Chỉ được chọn 1 tài khoản!");
                    return;
                }
                string idTkqc = selectedRow.Cells["id"].Value.ToString();
                string result = await CampPEDomain.getEditTargetDraft(_cookie, _token, idTkqc, _proxy);
                if (result.Contains("Lỗi"))
                {
                    MessageBox.Show("Lấy thất bại!");
                    return;
                }
                else {
                    txtAdsSet.Text = result.Split('|')[0];
                    txtAd.Text = result.Split('|')[1];
                }
                MessageBox.Show("Lấy dữ liệu thành công");
            }
            finally
            {
                btnGetEditDraft.Enabled = true;
            }
        }
    }


}
