namespace AutoCamp.ui
{
    partial class CampBPOptionsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CampBPOptionsForm));
            panel1 = new Panel();
            btnSaveConfigBP = new Button();
            label1 = new Label();
            panel2 = new Panel();
            cbbPostId = new ComboBox();
            cbbPageID = new ComboBox();
            label4 = new Label();
            label3 = new Label();
            cbbBpScreenOptions = new ComboBox();
            label2 = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnSaveConfigBP);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(629, 252);
            panel1.TabIndex = 0;
            // 
            // btnSaveConfigBP
            // 
            btnSaveConfigBP.Location = new Point(528, 207);
            btnSaveConfigBP.Name = "btnSaveConfigBP";
            btnSaveConfigBP.Size = new Size(89, 33);
            btnSaveConfigBP.TabIndex = 51;
            btnSaveConfigBP.Text = "OK";
            btnSaveConfigBP.UseVisualStyleBackColor = true;
            btnSaveConfigBP.Click += btnSaveConfigBP_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(52, 15);
            label1.TabIndex = 50;
            label1.Text = "Tham số";
            // 
            // panel2
            // 
            panel2.Controls.Add(cbbPostId);
            panel2.Controls.Add(cbbPageID);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(cbbBpScreenOptions);
            panel2.Controls.Add(label2);
            panel2.Location = new Point(12, 27);
            panel2.Name = "panel2";
            panel2.Size = new Size(605, 164);
            panel2.TabIndex = 0;
            // 
            // cbbPostId
            // 
            cbbPostId.FormattingEnabled = true;
            cbbPostId.Location = new Point(75, 100);
            cbbPostId.Name = "cbbPostId";
            cbbPostId.Size = new Size(515, 23);
            cbbPostId.TabIndex = 3;
            // 
            // cbbPageID
            // 
            cbbPageID.FormattingEnabled = true;
            cbbPageID.Location = new Point(75, 55);
            cbbPageID.Name = "cbbPageID";
            cbbPageID.Size = new Size(515, 23);
            cbbPageID.TabIndex = 3;
            cbbPageID.SelectedIndexChanged += cbbPageID_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(14, 103);
            label4.Name = "label4";
            label4.Size = new Size(55, 15);
            label4.TabIndex = 2;
            label4.Text = "POST ID: ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(14, 58);
            label3.Name = "label3";
            label3.Size = new Size(55, 15);
            label3.TabIndex = 2;
            label3.Text = "PAGE ID: ";
            // 
            // cbbBpScreenOptions
            // 
            cbbBpScreenOptions.FormattingEnabled = true;
            cbbBpScreenOptions.Items.AddRange(new object[] { "Page", "Suite" });
            cbbBpScreenOptions.Location = new Point(78, 13);
            cbbBpScreenOptions.Name = "cbbBpScreenOptions";
            cbbBpScreenOptions.Size = new Size(167, 23);
            cbbBpScreenOptions.TabIndex = 1;
            cbbBpScreenOptions.Text = "None";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 16);
            label2.Name = "label2";
            label2.Size = new Size(58, 15);
            label2.TabIndex = 0;
            label2.Text = "Màn hình";
            // 
            // CampBPOptionsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(629, 252);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "CampBPOptionsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "BP Options";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Label label1;
        private ComboBox cbbBpScreenOptions;
        private Label label2;
        private ComboBox cbbPageID;
        private Label label3;
        private ComboBox cbbPostId;
        private Label label4;
        private Button btnSaveConfigBP;
    }
}