targetScope = 'subscription'

param azure_location string = 'northeurope' // Default location, can be overridden

resource p4_rg 'Microsoft.Resources/resourceGroups@2021-04-01' = {
  name: 'p4-rg'
  location: azure_location
  tags: {
    Name: 'Project 4.0 Resource Group'
  }
}
