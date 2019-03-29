using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace WebFileShare
{
	/// <summary>
	/// Summary description for DirectoryContent.
	/// </summary>
	public class DirectoryContent : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label VisibleNameLabel;
		protected System.Web.UI.WebControls.Table FileTable;
	
		private const long KB = 1024;
		private const long MB = 1048576;
		private const long GB = 1073741824;
		private const long TB = 1099511627776;
		private const long PB = 1125899906842624;
		private const long EnormousSize = 1152921504606846976;

		private const int roundDigits = 2;

		private string visibleName = "";
		private string path = "";

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!(Session["dir_visible_name"] == null) & !(Session["dir_path"] == null))
			{
				visibleName = (string)(Session["dir_visible_name"]);
				path = (string)(Session["dir_path"]);

				if (!path.EndsWith("\\"))
				{
					path += "\\";
				}
				
				int fileCount = 0;
				double totalSize = 0;
				
				VisibleNameLabel.Text = visibleName;

				// заголовок таблицы
				TableRow tableHeaderRow = new TableRow();

				// имя файла
				TableCell fileNameHeaderCell = new TableCell();
				fileNameHeaderCell.Width = 300;
				fileNameHeaderCell.Font.Bold = true;
				fileNameHeaderCell.Text = "Файл";
				tableHeaderRow.Cells.Add(fileNameHeaderCell);

				// размер
				TableCell fileSizeHeaderCell = new TableCell();
				fileSizeHeaderCell.Width = 100;
				fileSizeHeaderCell.Font.Bold = true;
				fileSizeHeaderCell.Text = "Размер";
				tableHeaderRow.Cells.Add(fileSizeHeaderCell);

				// конец заголовка
				FileTable.Rows.Add(tableHeaderRow);

				if (Directory.Exists(path))
				{
					string[] files = Directory.GetFiles(path);

					for(int fileCounter = 0; fileCounter < files.Length; fileCounter++)
					{
						FileInfo file = new FileInfo(files[fileCounter]);

						string fileName = file.Name;
						double fileSize = file.Length;

						if (fileSize > 0)
						{
							fileCount += 1;
							totalSize += fileSize;

							// новая строка
							TableRow tableRow = new TableRow();

							// добавляем имя файла
							TableCell fileNameCell = new TableCell();
							fileNameCell.Text = fileName;
							tableRow.Cells.Add(fileNameCell);

							// добавляем размер
							TableCell fileSizeCell = new TableCell();
							fileSizeCell.Text = CalcStrFileSize(fileSize);
							tableRow.Cells.Add(fileSizeCell);

							// добавляем кнопку для скачивания
							TableCell downloadCell = new TableCell();
							Button downloadButton = new Button();
							downloadButton.ID = "download_" + fileName;
							downloadButton.Width = 100;
							downloadButton.Text = "Скачать";
							downloadButton.Click += new EventHandler(this.DownloadButton_Click);
							downloadCell.Controls.Add(downloadButton);
							tableRow.Cells.Add(downloadCell);

							// конец строки
							FileTable.Rows.Add(tableRow);
						}
					}
				}

				// итоги таблицы
				TableRow tableFooterRow = new TableRow();

				// итого файлов
				TableCell fileCountCell = new TableCell();
				fileCountCell.Font.Bold = true;
				fileCountCell.Text = "Всего файлов: " + fileCount.ToString();
				tableFooterRow.Cells.Add(fileCountCell);

				// итого размер
				TableCell totalSizeCell = new TableCell();
				totalSizeCell.Font.Bold = true;
				totalSizeCell.Text = CalcStrFileSize(totalSize);
				tableFooterRow.Cells.Add(totalSizeCell);

				// конец итогов
				FileTable.Rows.Add(tableFooterRow);
			}
			else
			{
				Response.Redirect("Welcome.aspx");
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private string CalcStrFileSize(double fileSize)
		{
			string result = "";

			if (fileSize < KB)
			{
				result = fileSize.ToString() + "B";
			}
			else if (fileSize < MB)
			{
				result = Math.Round(fileSize / KB, roundDigits).ToString() + " KB";
			}
			else if (fileSize < GB)
			{
				result = Math.Round(fileSize / MB, roundDigits).ToString() + " MB";
			}
			else if (fileSize < TB)
			{
				result = Math.Round(fileSize / GB, roundDigits).ToString() + " GB";
			}
			else if (fileSize < PB)
			{
				result = Math.Round(fileSize / TB, roundDigits).ToString() + " TB";
			}
			else if (fileSize < EnormousSize)
			{
				result = Math.Round(fileSize / PB, roundDigits).ToString() + " PB";
			}
			else
			{
				result = "N/A";
			}

			return result;
		}

		private void DownloadButton_Click(object sender, EventArgs e)
		{
			FileInfo file = new FileInfo(path + Regex.Replace((sender as Button).ID, "^download_", ""));

			Response.Clear();
			Response.ContentType = "application/octet-stream";
			Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
			Response.AddHeader("Content-Length", file.Length.ToString());
			Response.Flush();
			Response.WriteFile(file.FullName);
			Response.End();
		}	
	}
}
