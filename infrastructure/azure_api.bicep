targetScope = 'resourceGroup'

param sql_server string
param sql_database string
param sql_admin string
@secure()
param sql_password string
param api_name string
param asp_name string
param azure_location string = 'northeurope' // Default location, can be overridden
@secure()
param ai_api_key string

resource app_service_plan 'Microsoft.Web/serverfarms@2022-09-01' = {
  name: asp_name
  location: azure_location
  kind: 'app'
  sku: {
    name: 'F1' // Free tier
    tier: 'Free'
    size: 'F1'
    family: 'F'
    capacity: 1
  }
  tags: {
    Name: 'Project 4.0 API Service'
  }
}

resource p4_api 'Microsoft.Web/sites@2024-04-01' = {
  name: api_name
  location: azure_location
  kind: 'app'
  properties: {
    serverFarmId: app_service_plan.id
    siteConfig: {
      cors: {
        allowedOrigins: [
          '*'
        ]
        supportCredentials: false
      }
      appSettings: [
        {
          name: 'SQL_CONNECTION_STRING'
          value: 'Server=tcp:${sql_server}.${environment().suffixes.sqlServerHostname},1433;Initial Catalog=${sql_database};Persist Security Info=False;User ID=${sql_admin};Password=${sql_password};MultipleActiveResultSets=False;Encrypt=true;TrustServerCertificate=False;Connection Timeout=30;'
        }, {
          name: 'AI__ApiKey'
          value: ai_api_key
        }
      ]
    }
  }
  tags: {
    Name: 'Project 4.0 API'
  }
}
