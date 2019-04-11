<%@ Page language="c#" Codebehind="Welcome.aspx.cs" AutoEventWireup="false" Inherits="WebFileShare.Welcome" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>WebFileShare</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</head>
	<body MS_POSITIONING="GridLayout">
		<form id="WelcomeForm" method="post" runat="server" style="MARGIN: 50px; FONT-FAMILY: Sans-Serif; TEXT-ALIGN: center">
			<div style="MARGIN: 0px auto; WIDTH: 600px">
				<div>
					<span style="FONT-WEIGHT: bold">��� ������� � ������ ������� ����</span>
				</div>
				<div>
					<span style="FONT-SIZE: small">[<a href="Admin.aspx">����� ��� �������������</a>]</span>
				</div>
				<div style="MARGIN-TOP: 25px">
					<asp:textbox id="KeyTextBox" runat="server" Width="300px"></asp:textbox>
					<asp:button id="ShowContentButton" runat="server" Width="100px" Text="����������"></asp:button>
				</div>
				<div style="MARGIN-TOP: 25px">
					<asp:label id="ShowContentErrorLabel" runat="server" ForeColor="Red"></asp:label>
				</div>
			</div>
		</form>
	</body>
</html>
