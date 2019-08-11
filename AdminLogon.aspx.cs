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
	/// Summary description for AdminLogon.
	/// </summary>
	public class AdminLogon : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox LoginTextBox;
		protected System.Web.UI.WebControls.TextBox PasswordTextBox;
		protected System.Web.UI.WebControls.Button LogonButton;
		protected System.Web.UI.WebControls.Label LogonErrorLabel;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			Session.Clear();
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
			this.LogonButton.Click += new System.EventHandler(this.LogonButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void LogonButton_Click(object sender, System.EventArgs e)
		{
			string login = LoginTextBox.Text.Trim().Replace("'", "''");
			string password = PasswordTextBox.Text.Trim().Replace("'", "''");

			if ((login.Length > 0) & (password.Length > 0))
			{
				bool partExists = false;

				string visibleName = "";

				SqlDataReader dataReader = Database.OpenQuery("select visible_name from dbo.parts where owner = '" + login + "' and passphrase = '" + password + "'");

				if (dataReader.Read())
				{
					partExists = true;

					visibleName = dataReader.GetString(dataReader.GetOrdinal("visible_name"));
				}

				dataReader.Close();

				if (partExists)
				{
					Session["admin_username"] = login;
					Session["admin_visible_name"] = visibleName;

					Response.Redirect("Admin.aspx");
				}
				else
				{
					LogonErrorLabel.Text = "Неверное сочетание имени и пароля!";
				}
			}
			else
			{
				LogonErrorLabel.Text = "Укажите имя и пароль!";
			}
		}
	}
}
