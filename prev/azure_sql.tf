resource "azurerm_resource_group" "p4_rg" {
  name = "p4-rg"
  location = var.azure_location

  tags = {
    Name = "Project 4.0 Resource Group"
  }
}

resource "azurerm_mssql_server" "p4_server" {
  name                         = var.sql_server
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
  name                = var.sql_database
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

resource "azurerm_mssql_firewall_rule" "p4_sql_firewall_rule" {
  name                           = "Allow Thomas More Network"
  server_id                      = azurerm_mssql_server.p4_server.id
  start_ip_address               = "193.190.124.1"  
  end_ip_address                 = "193.190.124.253"

  depends_on = [azurerm_mssql_server.p4_server]
}

resource "azurerm_mssql_firewall_rule" "allow_azure_resources" {
  name                           = "Allow Azure Resources"
  server_id                      = azurerm_mssql_server.p4_server.id
  start_ip_address               = "0.0.0.0"
  end_ip_address                 = "0.0.0.0"

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
