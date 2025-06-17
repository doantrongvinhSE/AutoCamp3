using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoCamp.domain;
using AutoCamp.models;
using Newtonsoft.Json;

namespace AutoCamp.ui
{
    public partial class CampBPOptionsForm : Form
    {
        private List<PageInfoModel> _pages;
        private string _cookie;
        private string _token;
        private string _proxy;

        public CampBPOptionsForm(List<PageInfoModel> pages, string cookie, string token, string? proxy = null)
        {
            InitializeComponent();
            _pages = pages;
            _cookie = cookie;
            _token = token;
            _proxy = proxy;
            LoadPageData();
        }

        private void LoadPageData()
        {
            // Thêm dữ liệu vào comboBox2 (PAGE ID)
            foreach (var page in _pages)
            {
                cbbPageID.Items.Add($"{page.Id} - {page.Name}");
            }

            // Mặc định chọn item đầu tiên nếu có
            if (cbbPageID.Items.Count > 0)
            {
                cbbPageID.SelectedIndex = 0;
            }
        }

        private async void cbbPageID_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbPostId.Items.Clear();

            string selectedItem = cbbPageID.SelectedItem.ToString();

            List<string> result = await PageDomain.getAllPostPage(_cookie, _token, selectedItem.Split('-')[0].Trim(), _proxy);

            foreach (var item in result)
            {
                cbbPostId.Items.Add(item);
            }

        }

        private void btnSaveConfigBP_Click(object sender, EventArgs e)
        {
            try
            {
                string optionBp = cbbBpScreenOptions.SelectedItem.ToString();
                string pageId = cbbPageID.SelectedItem.ToString().Split('-')[0].Trim();
                string postId = cbbPostId.SelectedItem.ToString();

                // Tạo object để serialize thành JSON
                var config = new
                {
                    OptionBP = optionBp,
                    PageId = pageId,
                    PostId = postId
                };

                // Chuyển đổi object thành JSON
                string jsonContent = JsonConvert.SerializeObject(config, Formatting.Indented);

                // Lưu vào file txt
                string filePath = "bp_config.txt";
                System.IO.File.WriteAllText(filePath, jsonContent);

                MessageBox.Show("Đã lưu cấu hình thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            catch
            {
                MessageBox.Show("Vui lòng chọn đầy đủ các trường trước khi lưu cấu hình.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
