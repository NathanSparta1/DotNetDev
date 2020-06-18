<%@ Page Language="VB" %>
<%
Response.Write(System.Security.Principal.WindowsIdentity.GetCurrent().Name)
%>