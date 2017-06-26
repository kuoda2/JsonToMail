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
            Config = JsonHelper.GetConfig("D:\\modified\\config.json");
            MailInfos = JsonHelper.GetMailInfos(config);

            gridControl1.DataSource = MailInfos;

            me_ConfigInfo.ReadOnly = true;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
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
            MailInfos = JsonHelper.GetMailInfos(JsonHelper.GetConfig("D:\\modified\\config.json"));
        }

        private void UpdateConfigInfo(Config config)
        {
            me_ConfigInfo.Text = config.ToString();
        }
    }
}