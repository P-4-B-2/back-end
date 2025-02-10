terraform {
  required_version = ">= 1.5"
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 3.0"
    }
    # docker = {
    #   source  = "kreuzwerker/docker"
    #   version = "~> 3.0"
    # }
  }
}

# Configure the AzureRM Provider
provider "azurerm" {
  features {}
  subscription_id = var.azure_subscription_id
  client_id       = var.azure_client_id
  client_secret   = var.azure_client_secret
  tenant_id       = var.azure_tenant_id

  # skip_provider_registration = true
}

# # Configure Docker provider
# provider "docker" {
#   host = "unix://${var.docker_sock}"
# }
