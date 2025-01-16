resource "google_cloud_run_v2_service" "p4_cloudrun_service" {
  name     = "p4-cloudrun-service"
  location = var.google_location
  deletion_protection = false
  ingress = "INGRESS_TRAFFIC_ALL" # Further research required

  template {
    containers {
      name = "API-service"
      image = "{{DOCKERHUB URL HERE}}"

      env {
        name = "p4-server"
        value = azurerm_mssql_server.p4_server.fully_qualified_domain_name
      }
      env {
        name = "p4-database"
        value = azurerm_mssql_database.p4_sql_database.name
      }
      env {
        name = "sql-admin"
        value = azurerm_mssql_server.p4_server.administrator_login
      }
      env {
        name = "sql-password"
        value = azurerm_mssql_server.p4_server.administrator_login_password
      }
    }
    containers {
      name = "AI-manager-service"
      image = "{{DOCKERHUB URL HERE}}"
    }
  }

  depends_on = [azurerm_mssql_database.p4_sql_database]
}