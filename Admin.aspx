<%@ Page language="c#" Codebehind="Admin.aspx.cs" AutoEventWireup="false" Inherits="WebFileShare.Admin" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WebFileShare</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="AdminForm" method="post" runat="server" style="MARGIN: 50px; FONT-FAMILY: Sans-Serif; TEXT-ALIGN: center">
			<div style="MARGIN: 0px auto; WIDTH: 1100px">
				<div>
					<asp:label id="VisibleNameLabel" runat="server" Font-Size="Large" Font-Bold="True"></asp:label>
				</div>
				<div>
					<span style="FONT-SIZE: small">[<A href="AdminLogon.aspx">выход</A>]</span>
				</div>
				<div style="MARGIN-TOP: 25px">
					<asp:table id="DirTable" style="MARGIN-TOP: 50px" runat="server" HorizontalAlign="Center"></asp:table>
				</div>
				<div style="MARGIN-TOP: 25px">
					<span style="FONT-WEIGHT: bold">Добавление каталога</span>
				</div>
				<div style="MARGIN-TOP: 25px">
					<table align="center">
						<tr>
							<td align="left">
								<span style="FONT-SIZE: small">Название</span>
							</td>
							<td align="left">
								<span style="FONT-SIZE: small">Каталог</span>
								<span style="FONT-SIZE: small; COLOR: red">*</span>
							</td>
						</tr>
						<tr>
							<td align="left">
								<asp:textbox id="DirVisibleNameTextBox" runat="server" Width="250px"></asp:textbox>
							</td>
							<td align="left">
								<asp:textbox id="DirNameTextBox" runat="server" Width="150px"></asp:textbox>
							</td>
							<td align="left">
								<asp:button id="AddButton" runat="server" Width="100px" Text="Добавить"></asp:button>
							</td>
						</tr>
					</table>
					<div style="MARGIN-TOP: 25px">
						<asp:Label id="AddErrorLabel" runat="server" ForeColor="Red"></asp:Label>
					</div>
				</div>
			</div>
		</form>
	</body>
</HTML>
