using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SEP_Framework.BUS
{
    public class ReadForm:FormRoot
    {
        public ReadForm(string connectionString, string strNameTable) : base(connectionString, strNameTable)
        {
            form.AutoSize = true;
            form.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            form.Text = "Read";
            this.title = strNameTable;
            this.textBtnAction = "Reload";
            this.textBtnCancel = "Close";
        }

        protected override void SetControlForm()
        {
            this.CreateDataGridView();
            this.CustomForm(form.Height, 1000);
        }

        protected override void actionButton()
        {
            this.CreateDataGridView();
        }

    }
}
