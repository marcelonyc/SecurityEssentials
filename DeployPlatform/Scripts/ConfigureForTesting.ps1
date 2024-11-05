<#
.SYNOPSIS
    Enables Azure Devops agent to connect to the sql database
.DESCRIPTION
	Opens a port in the firewall so that the unit tests can change the database state before running integration tests
	Changes the connection string and app settings for the acceptance tests
.NOTES
    Author: John Staveley
    Date:   17/03/2020    
#>
param (
	[Parameter(Mandatory=$true)]
	[string] $TestConfigPath,
	[Parameter(Mandatory=$true)]
	[string] $WebServerUrl,
	[Parameter(Mandatory=$true)]
	[string] $ResourceGroup, 
	[Parameter(Mandatory=$true)]
	[string] $SqlServerName,
	[Parameter(Mandatory=$true)]
	[string] $WebDatabaseName,
	[Parameter(Mandatory=$true)]
	[string] $RuleName,
	[Parameter(Mandatory=$true)]
	[string] $SqlAdminUserName,
	[Parameter(Mandatory=$true)]
	[string] $SqlAdminPassword,
	[Parameter(Mandatory=$true)]
	[string] $StorageAccountNonVNetName
	)

Write-Host ("Configure for Testing Started")

[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12
#NB: This puts a dependency on the website http://ipinfo.io/json, but it seems to work fine
$agentIPAddresss = Invoke-RestMethod http://ipinfo.io/json | Select -exp ip
Write-Host ("Adding firewall rule $RuleName for Server $SqlServerName allowing IP Address $agentIPAddresss")
New-AzureRmSqlServerFirewallRule -ResourceGroupName $ResourceGroup -ServerName $SqlServerName -FirewallRuleName $RuleName -StartIpAddress "$($agentIPAddresss)" -EndIpAddress "$($agentIPAddresss)"
Write-Host ("Enable Database access for Testing Complete")

Write-Host ("Getting test config file from $TestConfigPath")
$appConfig = (Get-Content $TestConfigPath) -as [Xml]
$appConfigRoot = $appConfig.get_DocumentElement()
$defaultConnection = $appConfigRoot.connectionStrings.SelectNodes("add")
[string] $defaultConnectionString = "Data Source=tcp:$SqlServerName.database.windows.net,1433;Initial Catalog=$WebDatabaseName;User Id=$SqlAdminUserName;Password=$SqlAdminPassword"
Write-Host ("Changing connection string to Data Source=tcp:$SqlServerName.database.windows.net,1433;Initial Catalog=$WebDatabaseName;User Id=$SqlAdminUserName;Password=********")
$defaultConnection.SetAttribute("connectionString", $defaultConnectionString)

Write-Host ("Changing Web Server Url to $WebServerUrl")
$appSettingWebServerUrl = $appConfigRoot.appSettings.SelectSingleNode("//add[@key='WebServerUrl']")
$appSettingWebServerUrl.SetAttribute("value", $webServerUrl)
$appSettingTakeScreenShotOnFailure = $appConfigRoot.appSettings.SelectSingleNode("//add[@key='TakeScreenShotOnFailure']")
$appSettingTakeScreenShotOnFailure.SetAttribute("value", "true")

$storageApiKey = (Get-AzureRmStorageAccountKey -Name $StorageAccountNonVNetName -ResourceGroupName $ResourceGroup -ErrorAction Stop).Value[1]
$TestScreenCaptureStorage = "DefaultEndpointsProtocol=https;AccountName=$StorageAccountNonVNetName;AccountKey=$storageApiKey;EndpointSuffix=core.windows.net"
Write-Host ("Changing TestScreenCaptureStorage to $TestScreenCaptureStorage")
$appSettingTestScreenCaptureStorage = $appConfigRoot.appSettings.SelectSingleNode("//add[@key='TestScreenCaptureStorage']")
$appSettingTestScreenCaptureStorage.SetAttribute("value", $TestScreenCaptureStorage)
Write-Host("Writing test config changes to $TestConfigPath")
$appConfig.Save($TestConfigPath)

Write-Host ("Configure for Testing Complete")