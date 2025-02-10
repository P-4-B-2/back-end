param azure_location string = 'northeurope' // Default location, can be overridden
param sql_server string
param sql_admin string
@secure()
param sql_password string
param sql_database string

resource p4_server 'Microsoft.Sql/servers@2021-02-01-preview' = {
  name: sql_server
  location: azure_location
  properties: {
    administratorLogin: sql_admin
    administratorLoginPassword: sql_password
    version: '12.0'
    // publicNetworkAccess: 'Enabled'
  }
  tags: {
    Name: 'Project 4.0 SQL Database Server'
  }
}

resource p4_sql_database 'Microsoft.Sql/servers/databases@2023-08-01-preview' = {
  name: sql_database
  location: azure_location
  parent: p4_server
  properties: {
    collation: 'SQL_Latin1_General_CP1_CI_AS'
    maxSizeBytes: 32 * 1024 * 1024 * 1024 // 32 GB
    zoneRedundant: false
    // minCapacity: 1
    autoPauseDelay: 60
    useFreeLimit: true
    freeLimitExhaustionBehavior: 'AutoPause'
  }
  sku: {
    name: 'GP_S_Gen5_2'
    tier: 'GeneralPurpose'
    family: 'Gen5'
    capacity: 2
  }
  tags: {
    Name: 'Project 4.0 SQL Database'
  }
}

resource p4_sql_firewall_rule 'Microsoft.Sql/servers/firewallRules@2021-02-01-preview' = {
  name: 'Allow Thomas More Network'
  parent: p4_server
  properties: {
    startIpAddress: '193.190.124.1'
    endIpAddress: '193.190.124.253'
  }
}

resource allow_azure_resources 'Microsoft.Sql/servers/firewallRules@2021-02-01-preview' = {
  name: 'Allow Azure Resources'
  parent: p4_server
  properties: {
    startIpAddress: '0.0.0.0'
    endIpAddress: '0.0.0.0'
  }
}
