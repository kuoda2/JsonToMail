namespace JsonToMail
{
    partial class Form1
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.sb_SendMail = new DevExpress.XtraEditors.SimpleButton();
            this.sb_ImportData = new DevExpress.XtraEditors.SimpleButton();
            this.me_ConfigInfo = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.me_ConfigInfo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gridControl1.Location = new System.Drawing.Point(0, 163);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(708, 313);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // sb_SendMail
            // 
            this.sb_SendMail.Location = new System.Drawing.Point(200, 38);
            this.sb_SendMail.Name = "sb_SendMail";
            this.sb_SendMail.Size = new System.Drawing.Size(157, 97);
            this.sb_SendMail.TabIndex = 1;
            this.sb_SendMail.Text = "寄送工作郵件";
            this.sb_SendMail.Click += new System.EventHandler(this.sb_SendMail_Click);
            // 
            // sb_ImportData
            // 
            this.sb_ImportData.Location = new System.Drawing.Point(12, 38);
            this.sb_ImportData.Name = "sb_ImportData";
            this.sb_ImportData.Size = new System.Drawing.Size(157, 97);
            this.sb_ImportData.TabIndex = 2;
            this.sb_ImportData.Text = "匯入資料";
            this.sb_ImportData.Click += new System.EventHandler(this.sb_ImportData_Click);
            // 
            // me_ConfigInfo
            // 
            this.me_ConfigInfo.Dock = System.Windows.Forms.DockStyle.Right;
            this.me_ConfigInfo.Location = new System.Drawing.Point(376, 0);
            this.me_ConfigInfo.Name = "me_ConfigInfo";
            this.me_ConfigInfo.Size = new System.Drawing.Size(332, 163);
            this.me_ConfigInfo.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 476);
            this.Controls.Add(this.me_ConfigInfo);
            this.Controls.Add(this.sb_ImportData);
            this.Controls.Add(this.sb_SendMail);
            this.Controls.Add(this.gridControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.me_ConfigInfo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton sb_SendMail;
        private DevExpress.XtraEditors.SimpleButton sb_ImportData;
        private DevExpress.XtraEditors.MemoEdit me_ConfigInfo;
    }
}

