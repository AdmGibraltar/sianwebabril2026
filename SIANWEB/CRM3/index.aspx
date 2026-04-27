<%@ Page Language="C#" AutoEventWireup="true" %>

<%
	string siteName = "";
	string appVirtualPath = System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath;
	if (appVirtualPath == "/") siteName = System.Web.Hosting.HostingEnvironment.ApplicationHost.GetSiteName();
	else siteName = appVirtualPath.TrimStart('/');
%>
<!doctype html>
<html lang="en">

<head>
	<meta charset="UTF-8" />
	<link rel="icon" type="image/svg+xml" href="/src/assets/crm.svg" />
	<meta name="viewport" content="width=device-width, initial-scale=1.0" />
	<title>CRM</title>
	<script>
		window.iisAppName = '<%=siteName%>'
		window.crmBasePath  = '/' + window.iisAppName + '/CRM3/'
	</script>
	<link rel="stylesheet" crossorigin href="/<%=siteName%>/CRM3/assets/index-Dqilw8w-.css">
	<script type="module" crossorigin src="/<%=siteName%>/CRM3/assets/index-otsPqfAO.js"></script>
</head>

<body>
	<div id="app"></div>
</body>

</html>