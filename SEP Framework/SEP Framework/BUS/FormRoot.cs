using SEP_Framework.DAO;
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
    public class FormRoot
    {
        protected Form form;

        protected string title;

        // Lưới hiển thị dữ liệu
        protected DataGridView grvData = new DataGridView(); 
        
        // Button
        protected Button btnAction;
        protected Button btnCancel;
        protected string textBtnAction;
        protected string textBtnCancel = "Cancel";

        // Lưu danh sách các Label và Textbox tương ứng column trong table
        protected Dictionary<string, Label> listLabels = new Dictionary<string, Label>();
        protected Dictionary<string, TextBox> listTextboxs = new Dictionary<string, TextBox>();

        protected AbstractDAO DAO;

        protected string strNameTable; //Tên bảng
        protected string primaryKey; //Khóa chính
        protected Dictionary<string, string> listColumns = new Dictionary<string, string>();
        protected DataTable dataTable;           

        public FormRoot(string connectionString, string strNameTable)
        {
            this.form = new Form();
            this.strNameTable = strNameTable;

            this.DAO = new SQLServerDAO(connectionString);

            this.btnAction = new Button();
            this.btnCancel = new Button();
            this.btnAction.Click += Action_Click;
            this.btnCancel.Click += Cancel_Click;

            this.primaryKey = DAO.GetPrimaryKey(strNameTable);
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.form.Close();
        }

        private void Action_Click(object sender, EventArgs e)
        {
            this.actionButton();
        }

        protected virtual void actionButton() { }

        public void ShowForm()
        {
            this.SetControlForm();
            this.SetTitle();
            this.form.Show();
        }

        protected virtual void SetControlForm() { }

        private void SetTitle()
        {
            Label title = new Label();
            title.Name = "Label Title";
            title.Text = this.title;
            title.AutoSize = true;
            title.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            title.Location = new Point(form.Width / 2 - title.Width / 2 - 20, 10);
            form.Controls.Add(title);
        }

        protected void CreateDataGridView()
        {
            grvData.Columns.Clear();
            grvData.Refresh();

            dataTable = DAO.LoadData(strNameTable);

            DataGridViewColumn[] columns = { };

            foreach (DataColumn col in dataTable.Columns)
            {
                // Tạo cột mới
                if (!String.IsNullOrEmpty(col.ColumnName))
                {
                    DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    column.DataPropertyName = col.ColumnName;
                    column.HeaderText = col.ColumnName;
                    column.Name = col.ColumnName;
                    column.ReadOnly = true;
                    columns = columns.Concat(new DataGridViewColumn[] { column }).ToArray();
                }
            }

            // Add tất cả column vào GridView
            grvData.Columns.AddRange(columns);

            //Thêm properties vào Data GridView
            grvData.DataSource = dataTable;
            grvData.Location = new Point(0, 60);
            grvData.Size = new Size(1000, 245);
            grvData.Name = "Data Table";
            grvData.ReadOnly = true;
            grvData.CellClick += DataBinding;
            form.Controls.Add(grvData);

            //Binding data vào các texbox
            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0, j = 0; i < listTextboxs.Count && j < grvData.ColumnCount; i++, j++)
                {
                    listTextboxs.ElementAt(i).Value.Text = dataTable.Rows[0].ItemArray[j].ToString();
                }
            }
        }
        
        private void DataBinding(object sender, DataGridViewCellEventArgs e)
        {
            if (grvData.Rows.Count > -1)
            {
                for (int i = 0, j = 0; i < listTextboxs.Count && j < grvData.ColumnCount; i++, j++)
                {
                    listTextboxs.ElementAt(i).Value.Text = grvData.Rows[e.RowIndex].Cells[j].Value.ToString();
                }
            }
        }

        public virtual void CustomForm(int height, int width)
        {
            form.Width = width;
            form.Height = height;

            this.btnAction.Text = this.textBtnAction;
            this.btnAction.Location = new Point(form.Width / 3 - 20 , form.Height - 15);

            this.btnCancel.Text = this.textBtnCancel;
            this.btnCancel.Location = new Point(form.Width * 2 / 3 - 20, form.Height - 15);

            form.Controls.Add(this.btnAction);
            form.Controls.Add(this.btnCancel);
            form.Height = btnAction.Location.Y + btnAction.Height + 80;
        }
    }
}
