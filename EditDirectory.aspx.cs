using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace WebFileShare
{
	/// <summary>
	/// Summary description for EditDirectory.
	/// </summary>
	public class EditDirectory : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label NameLabel;
		protected System.Web.UI.WebControls.TextBox VisibleNameTextBox;
		protected System.Web.UI.WebControls.CheckBox ChangeKeyCheckBox;
		protected System.Web.UI.WebControls.Button SaveButton;
		protected System.Web.UI.WebControls.Button CancelButton;
		protected System.Web.UI.WebControls.Label DataErrorLabel;

		private string login = "";
		private string name = "";
		private string visibleName = "";

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!(Session["admin_username"] == null) & !(Session["dir_name"] == null))
			{
				bool dataExists = false;

				login = (string)(Session["admin_username"]);
				name = (string)(Session["dir_name"]);

				SqlDataReader dataReader = Database.OpenQuery("select visible_name from available_directories('" + SqlHelper.NormalizeParameter(login) + "') where directory = '" + SqlHelper.NormalizeParameter(name) + "'");

				if (dataReader.Read())
				{
					dataExists = true;

					visibleName = dataReader.GetString(dataReader.GetOrdinal("visible_name"));
				}

				dataReader.Close();

				if (dataExists)
				{
					NameLabel.Text = "Редактирование каталога \"" + name + "\"";

					if (!IsPostBack)
					{
						VisibleNameTextBox.Text = visibleName;
					}
				}
				else
				{
					Response.Redirect("Admin.aspx");
				}
			}
			else
			{
				Response.Redirect("Admin.aspx");
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
			this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
			this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void SaveButton_Click(object sender, System.EventArgs e)
		{
			string newVisibleName = VisibleNameTextBox.Text.Trim();

			if (newVisibleName.Length > 0)
			{
				if (newVisibleName != visibleName)
				{
					Database.ExecQuery("exec dbo.edit_directory '" + SqlHelper.NormalizeParameter(login) + "', '" + SqlHelper.NormalizeParameter(name) + "', '" + SqlHelper.NormalizeParameter(newVisibleName) + "'");
				}
				
				if (ChangeKeyCheckBox.Checked)
				{
					Database.ExecQuery("declare @psk nvarchar(100) exec dbo.change_key @psk, '" + SqlHelper.NormalizeParameter(login) + "', '" + SqlHelper.NormalizeParameter(name) + "'");
				}

				Response.Redirect("Admin.aspx");
			}
			else
			{
				DataErrorLabel.Text = "Заполните необходимые поля!";
			}
		}

		private void CancelButton_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("Admin.aspx");
		}
	}
}
