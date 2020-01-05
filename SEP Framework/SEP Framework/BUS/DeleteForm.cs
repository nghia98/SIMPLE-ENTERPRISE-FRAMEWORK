using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SEP_Framework.BUS
{
    public class DeleteForm:FormRoot
    {
        public DeleteForm(string connectionString, string strNameTable) : base(connectionString, strNameTable)
        {
            form.Text = "Delete";
            this.title = strNameTable;
            this.textBtnAction = "Delete";
        }

        protected override void actionButton()
        {
            try
            {
                DAO.DeleteData(strNameTable, primaryKey, listTextboxs[primaryKey].Text);
                foreach (var i in listTextboxs)
                {
                    i.Value.Text = "";
                }
                MessageBox.Show("Xóa dữ liệu thành công!", "Thành công");
                this.CreateDataGridView();
                grvData.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xóa dữ liệu thất bại!", "Lỗi");
            }

        }

        protected override void SetControlForm()
        {
            foreach (DataColumn col in DAO.LoadData(strNameTable).Columns)
            {
                    TextBox txt = new TextBox();
                    txt.Name = col.ColumnName;
                    listTextboxs.Add(col.ColumnName, txt);                 
            }
            this.CreateDataGridView();
            this.CustomForm(grvData.Location.Y + grvData.Height + 50, 1000);
        }
    }
}
