using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace WebFileShare
{
	/// <summary>
	/// Summary description for Admin.
	/// </summary>
	public class Admin : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label VisibleNameLabel;
		protected System.Web.UI.WebControls.Table DirTable;
		protected System.Web.UI.WebControls.TextBox DirVisibleNameTextBox;
		protected System.Web.UI.WebControls.TextBox DirNameTextBox;
		protected System.Web.UI.WebControls.Button AddButton;
		protected System.Web.UI.WebControls.Label AddErrorLabel;
	
		private string login = "";
		private string visibleName = "";
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!(Session["admin_username"] == null) & !(Session["admin_visible_name"] == null))
			{
				login = (string)(Session["admin_username"]);
				visibleName = (string)(Session["admin_visible_name"]);

				int dirCount = 0;

				VisibleNameLabel.Text = visibleName;

				// ��������� �������
				TableRow tableHeaderRow = new TableRow();

				// ������������ ��������
				TableCell dirVisibleNameHeaderCell = new TableCell();
				dirVisibleNameHeaderCell.Width = 300;
				dirVisibleNameHeaderCell.Font.Bold = true;
				dirVisibleNameHeaderCell.Text = "��������";
				tableHeaderRow.Cells.Add(dirVisibleNameHeaderCell);
                
				// ����
				TableCell dirKeyHeaderCell = new TableCell();
				dirKeyHeaderCell.Width = 400;
				dirKeyHeaderCell.Font.Bold = true;
				dirKeyHeaderCell.Text = "����";
				tableHeaderRow.Cells.Add(dirKeyHeaderCell);
                
				// ��� ��������
				TableCell dirNameHeaderCell = new TableCell();
				dirNameHeaderCell.Width = 200;
				dirNameHeaderCell.Font.Bold = true;
				dirNameHeaderCell.Text = "�������";
				tableHeaderRow.Cells.Add(dirNameHeaderCell);

				// ����� ���������
				DirTable.Rows.Add(tableHeaderRow);

				// ����� ��������� ���������
				SqlDataReader dataReader = Database.OpenQuery("select visible_name, psk, directory from dbo.available_directories('" + SqlHelper.NormalizeParameter(login) + "')");

				while (dataReader.Read())
				{
					dirCount += 1;

					string dirVisibleName = dataReader.GetString(dataReader.GetOrdinal("visible_name"));
					string dirKey = dataReader.GetString(dataReader.GetOrdinal("psk"));
					string dirName = dataReader.GetString(dataReader.GetOrdinal("directory"));

					// ����� ������
					TableRow tableRow = new TableRow();

					// ��������� ������������ ��������
					TableCell dirVisibleNameCell = new TableCell();
					dirVisibleNameCell.Text = dirVisibleName;
					tableRow.Cells.Add(dirVisibleNameCell);

					// ��������� ����
					TableCell dirKeyCell = new TableCell();
					dirKeyCell.Font.Name = "Monospace";
					dirKeyCell.Text = dirKey;
					tableRow.Cells.Add(dirKeyCell);

					// ��� ��������
					TableCell dirNameCell = new TableCell();
					dirNameCell.Text = dirName;
					tableRow.Cells.Add(dirNameCell);

					// ��������� ������ ��������������
					TableCell editCell = new TableCell();
					Button editButton = new Button();
					editButton.ID = "edit_" + dirName;
					editButton.Width = 100;
					editButton.Text = "��������...";
					editButton.Click += new EventHandler(this.EditButton_Click);
					editCell.Controls.Add(editButton);
					tableRow.Cells.Add(editCell);

					// ��������� ������ ��������
					TableCell deleteCell = new TableCell();
					Button deleteButton = new Button();
					deleteButton.ID = "delete_" + dirName;
					deleteButton.Width = 100;
					deleteButton.Text = "�������";
					deleteButton.Click += new EventHandler(this.DeleteButton_Click);
					deleteButton.Attributes.Add("onclick", "return confirm('������� ������� \"" + dirName + "\"?');");
					deleteCell.Controls.Add(deleteButton);
					tableRow.Cells.Add(deleteCell);

					// ����� ������
					DirTable.Rows.Add(tableRow);
				}

				dataReader.Close();

				// ����� �������
				TableRow tableFooterRow = new TableRow();

				// ����� ���������
				TableCell dirCountCell = new TableCell();
				dirCountCell.ColumnSpan = 3;
				dirCountCell.HorizontalAlign = HorizontalAlign.Left;
				dirCountCell.Font.Bold = true;
				dirCountCell.Text = "����� ���������: " + dirCount.ToString();
				tableFooterRow.Cells.Add(dirCountCell);

				// ����� ������
				DirTable.Rows.Add(tableFooterRow);
			}
			else
			{
				Response.Redirect("AdminLogon.aspx");
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void AddButton_Click(object sender, System.EventArgs e)
		{
			string dirVisibleName = DirVisibleNameTextBox.Text.Trim();
			string dirName = DirNameTextBox.Text.Trim();

			if (dirName.Length > 0)
			{
				// �������� �� ������������
				SqlDataReader dataReader = Database.OpenQuery("select * from dbo.available_directories('" + SqlHelper.NormalizeParameter(login) + "') where directory = '" + SqlHelper.NormalizeParameter(dirName) + "'");
				bool alreadyExists = dataReader.HasRows;
				dataReader.Close();

				if (!alreadyExists)
				{
					if (dirVisibleName.Length == 0)
					{
						dirVisibleName = dirName;
					}
					
					Database.ExecQuery("declare @psk nvarchar(100) exec dbo.create_directory @psk, '" + SqlHelper.NormalizeParameter(login) + "', '" + SqlHelper.NormalizeParameter(dirName) + "', '" + SqlHelper.NormalizeParameter(dirVisibleName) + "'");

					DirVisibleNameTextBox.Text = String.Empty;
					DirNameTextBox.Text = String.Empty;

					Response.Redirect("Admin.aspx");
				}
				else
				{
					AddErrorLabel.Text = "����� ������� ��� ����������!";
				}
			}
			else
			{
				AddErrorLabel.Text = "��������� ����������� ����!";
			}		
		}
		
		private void EditButton_Click(object sender, EventArgs e)
		{
			string dirName = Regex.Replace((sender as Button).ID, "^edit_", "");

			Session["dir_name"] = dirName;

			Response.Redirect("EditDirectory.aspx");
		}
        
		private void DeleteButton_Click(object sender, EventArgs e)
		{
			string dirName = Regex.Replace((sender as Button).ID, "^delete_", "");

			Database.ExecQuery("exec dbo.delete_directory '" + SqlHelper.NormalizeParameter(login) + "', '" + SqlHelper.NormalizeParameter(dirName) + "'");

			Response.Redirect("Admin.aspx");
		}
	}
}
