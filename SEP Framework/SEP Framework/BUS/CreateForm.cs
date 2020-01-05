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
    public class CreateForm: FormRoot
    {
        protected override void actionButton()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            foreach (var txt in listTextboxs)
            {
                if (txt.Value.Text == "")
                {
                    MessageBox.Show("Vui lòng hoàn tất trường " + txt.Key + " trước khi thêm mới!", "Lỗi");
                    return;
                }
                data.Add(txt.Key, txt.Value.Text);
            }
            try
            {
                DAO.InsertData(data, strNameTable);
                
                //Reset dữ liệu khi đã insert thành công
                foreach (var i in listTextboxs)
                {
                    i.Value.Text = "";
                }
                data.Clear();
                MessageBox.Show("Thêm dữ liệu thành công!", "Thông báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thêm dữ liệu thất bại!", "Thông báo");
            }

        }
        public CreateForm(string connetionString, string strNameTable) : base(connetionString, strNameTable)
        {
            form.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.title = strNameTable;
            form.Text = "Insert";
            this.textBtnAction = "Insert";
        }

        protected override void SetControlForm()
        {
            int y = 0;
            foreach (DataColumn col in DAO.LoadData(strNameTable).Columns)
            {
                if (col.ColumnName != primaryKey)
                {
                    Label lb = new Label();
                    TextBox txt = new TextBox();
                    txt.Name = col.ColumnName;
                    txt.Width = 150;
                    lb.Text = col.ColumnName + " :";

                    listLabels.Add(col.ColumnName, lb);
                    listTextboxs.Add(col.ColumnName, txt);

                    lb.Location = new Point(100, 60 + y * 30);
                    txt.Location = new Point(200, 60 + y * 30);
                    y++;
                    form.Controls.Add(lb);
                    form.Controls.Add(txt);
                }
            }

            this.CustomForm(listLabels.ElementAt(listLabels.Count - 1).Value.Location.Y + listLabels.ElementAt(listLabels.Count - 1).Value.Height + 50, 500);
        }
    }
}
