<%@ Page language="c#" Codebehind="DirectoryContent.aspx.cs" AutoEventWireup="false" Inherits="WebFileShare.DirectoryContent" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>WebFileShare</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</head>
	<body MS_POSITIONING="GridLayout">
		<form id="DirectoryContentForm" method="post" runat="server" style="MARGIN: 50px; FONT-FAMILY: Sans-Serif; TEXT-ALIGN: center">
			<div style="MARGIN: 0px auto; WIDTH: 600px">
				<div>
					<asp:label id="VisibleNameLabel" runat="server" Font-Size="Large" Font-Bold="True"></asp:label>
				</div>
				<div style="MARGIN-TOP: 25px">
					<asp:table id="FileTable" runat="server" HorizontalAlign="Center"></asp:table>
				</div>
			</div>
		</form>
	</body>
</html>
