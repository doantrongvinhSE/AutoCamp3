using AutoCamp.domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AutoCamp.ui
{

    public partial class CampPE : Form
    {
        private List<string> _idTkqcList;
        private string _cookie;
        private string _token;
        private string _proxy;



        public CampPE(List<string> idTkqcList, string cookie, string token, string proxy)
        {
            InitializeComponent();
            _idTkqcList = idTkqcList;
            _cookie = cookie;
            _token = token;
            _proxy = proxy;
            LoadPageData();
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
                                for (int attempt = 0; attempt < 5; attempt++)
                                {
                                    try
                                    {
                                        idDarft = await CampPEDomain.getIdAddraft(cookie, token, idTkqc, proxy);
                                        if (idDarft != null && !idDarft.Contains("Lỗi"))
                                            break;
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
                                } else {
                                    this.Invoke((Action)(() => row.Cells["process"].Value = "Chạy campain ok..."));
                                }

                                valueGroup = valueGroup.Replace(item2, result);
                                valueGroup = valueGroup.Replace(item1, idTkqc);
                                string result2 = await CampPEDomain.pushDraftAdsetPe(cookie, token, idTkqc, idDarft, valueGroup, result, proxy);
                                if (result2.Contains("Lỗi"))
                                {
                                    this.Invoke((Action)(() => row.Cells["process"].Value = "Lỗi"));
                                    return;
                                } else {
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
                                } else {
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
        }
    }


}
