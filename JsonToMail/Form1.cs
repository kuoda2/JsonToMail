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
        public Form1()
        {
            InitializeComponent();
            var array = JsonHelper.ConvertTo(JsonHelper.ReadJson("D:\\modified\\export.json"),
                 string.Format(@"{0}\ims_contract_for_instruction.sql", Environment.SpecialFolder.Desktop));

            JsonHelper.GetMailInfos(array);
            MailInfo mail = new MailInfo() { };
        }
    }
}