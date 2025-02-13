targetScope = 'resourceGroup'

param azure_location string = 'northeurope'
param environment_name string
param job_name string
param docker_image string

resource managedEnvironment 'Microsoft.App/managedEnvironments@2023-11-02-preview' = {
  name: environment_name
  location: azure_location
  properties: {
    workloadProfiles: [
      {
        workloadProfileType: 'Consumption'
        name: 'Consumption'
      }
    ]
  }
  tags: {
    Name: 'Project 4.0 AI'
  }
}

resource job 'Microsoft.App/jobs@2023-11-02-preview' = {
  name: job_name
  location: azure_location
  properties: {
    environmentId: managedEnvironment.id
    workloadProfileName: 'Consumption'
    configuration: {
      triggerType: 'Manual'
      replicaTimeout: 1800
      replicaRetryLimit: 1
      manualTriggerConfig: {
        replicaCompletionCount: 1
        parallelism: 1
      }
    }
    template: {
      containers: [
        {
          image: docker_image
          name: job_name
          resources: {
            cpu: 4
            memory: '8Gi'
          }
        }
      ]
    }
  }
  tags: {
    Name: 'Project 4.0 AI'
  }
}
