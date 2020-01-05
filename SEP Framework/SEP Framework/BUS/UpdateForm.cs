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
    public class UpdateForm:FormRoot
    {
        public UpdateForm(string connectionString, string strNameTable): base(connectionString, strNameTable)
        {
            form.Text = "Update";
            this.title = strNameTable;
            this.textBtnAction = "Update";
        }

        protected override void actionButton()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            foreach (var txt in listTextboxs)
            {
                if (txt.Value.Text == "")
                {
                    MessageBox.Show("Trường dữ liệu " + txt.Key + " còn trống !", "Lỗi");
                    return;
                }
                data.Add(txt.Key, txt.Value.Text);
            }
            try
            {
                DAO.UpdateData(data, strNameTable, primaryKey);

                //Reset texbox và dữ liệu khi insert thành công
                foreach (var i in listTextboxs)
                {
                    i.Value.Text = "";
                }
                data.Clear();
                MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo");
                this.CreateDataGridView();
                grvData.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cập nhật dữ liệu thất bại!", "Lỗi");
            }

        }

        protected override void SetControlForm()
        {

            int y = 0;
            foreach (DataColumn col in DAO.LoadData(strNameTable).Columns)
            {
                    Label label = new Label();
                    TextBox txt = new TextBox();

                    txt.Name = col.ColumnName;
                    txt.Width = 200;
                    label.Text = col.ColumnName + " :";

                    listLabels.Add(col.ColumnName, label);
                    listTextboxs.Add(col.ColumnName, txt);

                    if (col.ColumnName == primaryKey)
                        txt.Enabled = false;

                    label.Location = new Point(370, 325 + y * 30);
                    txt.Location = new Point(470, 325 + y * 30);
                    y++;

                    form.Controls.Add(label);
                    form.Controls.Add(txt);
            }

            this.CreateDataGridView();

            this.CustomForm(325+y * 30 + 30, 1000);
        }

    }
}
