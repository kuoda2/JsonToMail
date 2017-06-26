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
        private List<MailInfo> MailInfos;

        public Form1()
        {
            InitializeComponent();
            Config = JsonHelper.GetConfig("D:\\modified\\config.json");
            MailInfos = JsonHelper.GetMailInfos(config);

            gridControl1.DataSource = MailInfos;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            foreach (var mailInfo in MailInfos)
            {
                OutLookTool.ComposeMail(mailInfo, true);
            }
        }
    }
}