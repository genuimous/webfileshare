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
	/// Summary description for Welcome.
	/// </summary>
	public class Welcome : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox KeyTextBox;
		protected System.Web.UI.WebControls.Button ShowContentButton;
		protected System.Web.UI.WebControls.Label ShowContentErrorLabel;
	
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
			this.ShowContentButton.Click += new System.EventHandler(this.ShowContentButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void ShowContentButton_Click(object sender, System.EventArgs e)
		{
			string key = KeyTextBox.Text.Trim();

			if (key.Length > 0)
			{
				bool keyExists = false;

				string visibleName = "";
				string directory = "";
				string path = "";

				SqlDataReader dataReader = Database.OpenQuery("select d.visible_name, d.directory, p.path from dbo.directories d join dbo.parts p on p.id = d.part_id where d.psk = '" + key + "'");

				if (dataReader.Read())
				{
					keyExists = true;

					visibleName = dataReader.GetString(dataReader.GetOrdinal("visible_name"));
					directory = dataReader.GetString(dataReader.GetOrdinal("directory"));
					path = dataReader.GetString(dataReader.GetOrdinal("path"));

					if (!path.EndsWith("\\"))
					{
						path += "\\";
					}
				}

				dataReader.Close();

				if (keyExists)
				{
					Session["dir_visible_name"] = visibleName;
					Session["dir_path"] = path + directory;

					Response.Redirect("DirectoryContent.aspx");
				}
				else
				{
					ShowContentErrorLabel.Text = "Неверный ключ!";
				}
			}
			else
			{
				ShowContentErrorLabel.Text = "Укажите ключ!";
			}
		}	
	}
}
