cd "D:\GitHub\DotNet4Sql\DotNet4Sql.Commands\bin\debug"
set-alias installutil $env:windir\Microsoft.NET\Framework64\v4.0.30319\installutil
installutil /u "DotNet4Sql.Commands.dll"
installutil "DotNet4Sql.Commands.dll"
add-pssnapin "DotNet4SqlCommandsSnapIn"
#get-json -FromUrl http://localhost:49222/Activities/Json -OfType "DotNet4Sql.DataViewModels.ActivityViewModel, DotNet4Sql.DataViewModels" | Format-Pdf -PdfName .\test.pdf
get-sql -DataSource .\SQL2012R2Express -InitialCatalog DotNet4SqlCRMDB -Query "select * from crm.ActivityViewModels" | Format-Table
