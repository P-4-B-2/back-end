targetScope = 'subscription'

param azure_location string
param sql_server string
param sql_admin string
@secure()
param sql_password string
param sql_database string
param rg_name string
param api_name string
param asp_name string
@secure()
param ai_api_key string

// Resource Group Module
module rgModule './azure_rg.bicep' = {
  scope: subscription()
  name: 'rgDeployment'
  params: {
    azure_location: azure_location
  }
}

// SQL Module
module sqlModule './azure_sql.bicep' = {
  scope: resourceGroup(rg_name)
  name: 'sqlDeployment'
  params: {
    azure_location: azure_location
    sql_server: sql_server
    sql_admin: sql_admin
    sql_password: sql_password
    sql_database: sql_database
  }
  dependsOn: [
    rgModule
  ]
}

// API Module
module apiModule './azure_api.bicep' = {
  scope: resourceGroup(rg_name)
  name: 'apiDeployment'
  params: {
    azure_location: azure_location
    sql_server: sql_server
    sql_database: sql_database
    sql_admin: sql_admin
    sql_password: sql_password
    api_name: api_name
    asp_name: asp_name
    ai_api_key : ai_api_key
  }
  dependsOn: [
    rgModule, sqlModule
  ]
}
