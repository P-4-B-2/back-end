resource "azurerm_resource_group" "p4_rg" {
  name = "p4-rg"
  location = var.azure_location

  tags = {
    Name = "Project 4.0 Resource Group"
  }
}

resource "azurerm_mssql_server" "p4_server" {
  name                         = "p4-server"
  resource_group_name          = azurerm_resource_group.p4_rg.name
  location                     = var.azure_location
  version                      = "12.0"
  administrator_login          = var.sql_admin
  administrator_login_password = var.sql_password

  tags = {
    Name = "Project 4.0 SQL Database Server"
  }

  depends_on = [azurerm_resource_group.p4_rg]
}

resource "azurerm_mssql_database" "p4_sql_database" {
  name                = "SQL Database"
  server_id           = azurerm_mssql_server.p4_server.id
  collation           = "SQL_Latin1_General_CP1_CI_AS"
  # license_type        = "LicenseIncluded"
  max_size_gb         = 32
  sku_name            = "GP_S_Gen5_2"
  zone_redundant      = false

  min_capacity                  = 0.5
  auto_pause_delay_in_minutes   = 20

  tags = {
    Name = "Project 4.0 SQL Database"
  }

  depends_on = [azurerm_mssql_server.p4_server]
}

# Maintenance Configuration
resource "azurerm_maintenance_configuration" "p4_sql_maintenance" {
  name                           = "SQL_Default"
  location                       = var.azure_location
  resource_group_name            = azurerm_resource_group.p4_rg.name
  scope                          = "SQLDB"

  depends_on = [azurerm_resource_group.p4_rg]
}
