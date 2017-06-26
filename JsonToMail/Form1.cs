using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeTool;

namespace JsonToMail
{
    public partial class Form1 : Form
    {
        private Config config;

        private Config Config
        {
            get { return config; }
            set
            {
                config = value;
                UpdateConfigInfo(config);
            }
        }

        private IEnumerable<MailInfo> mailInfos;

        private IEnumerable<MailInfo> MailInfos
        {
            get { return mailInfos; }
            set
            {
                mailInfos = value;
                gridControl1.DataSource = mailInfos;
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void sb_SendMail_Click(object sender, EventArgs e)
        {
            if (MailInfos == null)
            {
                MessageBox.Show("請先匯入資料。");
                return;
            }
            bool isSuccessful = true;
            foreach (var mailInfo in MailInfos)
            {
                isSuccessful &= OutLookTool.ComposeMail(mailInfo, true);
            }

            if (isSuccessful)
                MessageBox.Show("已全數寄信成功，請到寄件備份查看");
        }

        private void sb_ImportData_Click(object sender, EventArgs e)
        {
            try
            {
                MailInfos = JsonHelper.GetMailInfos(Config);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("json檔案格式有誤，請確認。\r\nFile path:{0}\r\n{1}", "D:\\modified\\config.json", ex.Message));
                return;
            }
        }

        private void UpdateConfigInfo(Config config)
        {
            me_ConfigInfo.Text = config.ToString();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            try
            {
                Config = JsonHelper.GetConfig("D:\\modified\\config.json");
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("json檔案格式有誤，請確認。\r\nFile path:{0}\r\n{1}", "D:\\modified\\config.json", ex.Message));
                return;
            }

            try
            {
                MailInfos = JsonHelper.GetMailInfos(Config);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("資料來源有誤，請確認。\r\nFile path:{0}\r\n{1}", configPath, ex.Message));
                return;
            }

            gridControl1.DataSource = MailInfos;

            me_ConfigInfo.ReadOnly = true;
        }
    }
}