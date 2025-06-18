namespace AutoCamp.ui
{
    partial class CampPE
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CampPE));
            panel1 = new Panel();
            label3 = new Label();
            ListIdTkqcTable = new DataGridView();
            id = new DataGridViewTextBoxColumn();
            process = new DataGridViewTextBoxColumn();
            cbxPublicCampPE = new CheckBox();
            label2 = new Label();
            richDataDraftPe = new RichTextBox();
            cbxImportDraft = new CheckBox();
            label1 = new Label();
            btnRun = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ListIdTkqcTable).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(btnRun);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(ListIdTkqcTable);
            panel1.Controls.Add(cbxPublicCampPE);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(richDataDraftPe);
            panel1.Controls.Add(cbxImportDraft);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(1201, 590);
            panel1.TabIndex = 0;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(697, 14);
            label3.Name = "label3";
            label3.Size = new Size(57, 15);
            label3.TabIndex = 6;
            label3.Text = "Bản nháp";
            // 
            // ListIdTkqcTable
            // 
            ListIdTkqcTable.AllowUserToAddRows = false;
            ListIdTkqcTable.AllowUserToResizeRows = false;
            ListIdTkqcTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ListIdTkqcTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ListIdTkqcTable.Columns.AddRange(new DataGridViewColumn[] { id, process });
            ListIdTkqcTable.Location = new Point(5, 14);
            ListIdTkqcTable.Name = "ListIdTkqcTable";
            ListIdTkqcTable.RowHeadersVisible = false;
            ListIdTkqcTable.Size = new Size(508, 562);
            ListIdTkqcTable.TabIndex = 5;
            // 
            // id
            // 
            id.FillWeight = 134.771576F;
            id.HeaderText = "ID";
            id.Name = "id";
            // 
            // process
            // 
            process.FillWeight = 134.771576F;
            process.HeaderText = "Trạng thái";
            process.Name = "process";
            process.ReadOnly = true;
            // 
            // cbxPublicCampPE
            // 
            cbxPublicCampPE.AutoSize = true;
            cbxPublicCampPE.Location = new Point(533, 67);
            cbxPublicCampPE.Name = "cbxPublicCampPE";
            cbxPublicCampPE.Size = new Size(92, 19);
            cbxPublicCampPE.TabIndex = 4;
            cbxPublicCampPE.Text = "Public camp";
            cbxPublicCampPE.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(533, 14);
            label2.Name = "label2";
            label2.Size = new Size(55, 15);
            label2.TabIndex = 2;
            label2.Text = "Cấu hình";
            // 
            // richDataDraftPe
            // 
            richDataDraftPe.Location = new Point(697, 42);
            richDataDraftPe.Name = "richDataDraftPe";
            richDataDraftPe.Size = new Size(486, 390);
            richDataDraftPe.TabIndex = 1;
            richDataDraftPe.Text = "";
            // 
            // cbxImportDraft
            // 
            cbxImportDraft.AutoSize = true;
            cbxImportDraft.Location = new Point(533, 42);
            cbxImportDraft.Name = "cbxImportDraft";
            cbxImportDraft.Size = new Size(124, 19);
            cbxImportDraft.TabIndex = 0;
            cbxImportDraft.Text = "Nhập bản nháp PE";
            cbxImportDraft.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 6);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 0;
            label1.Text = "Camp";
            // 
            // btnRun
            // 
            btnRun.Location = new Point(1094, 447);
            btnRun.Name = "btnRun";
            btnRun.Size = new Size(89, 44);
            btnRun.TabIndex = 7;
            btnRun.Text = "Run";
            btnRun.UseVisualStyleBackColor = true;
            btnRun.Click += btnRun_Click;
            // 
            // CampPE
            // 
            AutoScaleMode = AutoScaleMode.None;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(1225, 614);
            Controls.Add(label1);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            Name = "CampPE";
            StartPosition = FormStartPosition.CenterParent;
            Text = "CampPE";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ListIdTkqcTable).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private CheckBox cbxImportDraft;
        private RichTextBox richDataDraftPe;
        private Label label2;
        private CheckBox cbxPublicCampPE;
        private DataGridView ListIdTkqcTable;
        private Label label3;
        private DataGridViewTextBoxColumn id;
        private DataGridViewTextBoxColumn process;
        private Button btnRun;
    }
}