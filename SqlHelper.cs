using System;

namespace WebFileShare
{
	/// <summary>
	/// Summary description for SqlHelper.
	/// </summary>
	public class SqlHelper
	{
		public static string NormalizeParameter(string parameter)
		{
			return parameter.Trim().Replace("'", "''");
		}
	}
}
