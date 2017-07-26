using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OfficeTool;

namespace JsonToMail
{
    public partial class Form1 : Form
    {
        private readonly string configPath = System.IO.Directory.GetCurrentDirectory() + @"/config.json";

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
                dataGridView1.DataSource = mailInfos;
                dataGridView1.AutoResizeColumns();
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MailInfos = JsonHelper.GetMailInfos(Config);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("json檔案格式有誤，請確認。\r\nFile path:{0}\r\n{1}", configPath, ex.Message));
                return;
            }
        }

        private void UpdateConfigInfo(Config config)
        {
            rtb_ConfigInfo.Text = config.ToString();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            try
            {
                Config = JsonHelper.GetConfig(configPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("json檔案格式有誤，請確認。\r\nFile path:{0}\r\n{1}", configPath, ex.Message));
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

            dataGridView1.DataSource = MailInfos;
            dataGridView1.AutoResizeColumns();

            rtb_ConfigInfo.ReadOnly = true;
        }
    }
}