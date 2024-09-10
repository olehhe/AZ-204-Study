import os
from azure.appconfiguration import AzureAppConfiguration

# Retrieve the connection string from the App Service environment variables
connection_string = os.getenv("APP_CONFIG_CONNECTION_STRING")

# Create an instance of the AzureAppConfiguration class
app_config = AzureAppConfiguration(connection_string=connection_string)

# Get a configuration setting
setting_value = app_config.get_configuration_setting("my_setting_key")
print(f"Value of 'my_setting_key': {setting_value.value}")

# Set a configuration setting
app_config.set_configuration_setting("my_setting_key", "new_value")

# Delete a configuration setting
app_config.delete_configuration_setting("my_setting_key")