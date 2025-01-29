output "sql_connection_string" {
  description = "The connection string for the Azure SQL Database."
  value       = format(
    "Server=tcp:%s,1433;Initial Catalog=%s;Persist Security Info=False;User ID=%s;Password=******;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;",
    azurerm_mssql_server.p4_server.fully_qualified_domain_name,
    azurerm_mssql_database.p4_sql_database.name,
    azurerm_mssql_server.p4_server.administrator_login
  )
}
