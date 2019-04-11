<%@ Page language="c#" Codebehind="AdminLogon.aspx.cs" AutoEventWireup="false" Inherits="WebFileShare.AdminLogon" %>
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
		<form id="AdminLogonForm" method="post" runat="server" style="MARGIN: 50px; FONT-FAMILY: Sans-Serif; TEXT-ALIGN: center">
			<div style="MARGIN: 0px auto; WIDTH: 600px">
				<div>
					<span style="FONT-WEIGHT: bold">Для управления содержимым введите учетные данные</span>
				</div>
				<div>
					<span style="FONT-SIZE: small">[<a href="Welcome.aspx">войти как пользователь</a>]</span>
				</div>
				<div style="MARGIN-TOP: 25px">
					<table align="center">
						<tr>
							<td>
								<table align="center">
									<tr>
										<td align="right">
											<span style="FONT-SIZE: small">Имя:</span>
										</td>
										<td>
											<asp:textbox id="LoginTextBox" runat="server" Width="150px"></asp:textbox>
										</td>
									</tr>
									<tr>
										<td align="right">
											<span style="FONT-SIZE: small">Пароль:</span>
										</td>
										<td>
											<asp:textbox id="PasswordTextBox" runat="server" Width="150px" TextMode="Password"></asp:textbox>
										</td>
									</tr>
								</table>
							</td>
							<td>
								<asp:button id="LogonButton" runat="server" Width="100px" Text="Продолжить"></asp:button>
							</td>
						</tr>
					</table>
				</div>
				<div style="MARGIN-TOP: 25px">
					<asp:label id="LogonErrorLabel" runat="server" ForeColor="Red"></asp:label>
				</div>
			</div>
		</form>
	</body>
</html>
