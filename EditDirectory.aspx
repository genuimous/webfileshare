<%@ Page language="c#" Codebehind="EditDirectory.aspx.cs" AutoEventWireup="false" Inherits="WebFileShare.EditDirectory" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>WebFileShare</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</head>
	<body MS_POSITIONING="GridLayout">
		<form id="EditDirectoryForm" method="post" runat="server" style="MARGIN: 50px; FONT-FAMILY: Sans-Serif; TEXT-ALIGN: center">
			<div style="MARGIN: 0px auto; WIDTH: 600px">
				<div>
					<asp:label id="NameLabel" runat="server" Font-Size="Large" Font-Bold="True"></asp:label>
				</div>
				<div style="MARGIN-TOP: 25px">
					<table align="center">
						<tr>
							<td align="right">
								<span style="FONT-SIZE: small">Название:</span>
							</td>
							<td>
								<asp:textbox id="VisibleNameTextBox" runat="server" Width="250px"></asp:textbox>
								<span style="FONT-SIZE: small; COLOR: red">*</span>
							</td>
						</tr>
					</table>
				</div>
				<div style="MARGIN-TOP: 25px">
					<asp:checkbox id="ChangeKeyCheckBox" runat="server" Text="Сменить ключ" Font-Size="Small"></asp:checkbox>
				</div>
				<div style="MARGIN-TOP: 25px">
					<asp:button id="SaveButton" runat="server" Width="100px" Text="Сохранить"></asp:button>
					<asp:button id="CancelButton" runat="server" Width="100px" Text="Отмена"></asp:button>
				</div>
				<div style="MARGIN-TOP: 25px">
					<asp:label id="DataErrorLabel" runat="server" ForeColor="Red"></asp:label>
				</div>
			</div>
		</form>
	</body>
</html>
