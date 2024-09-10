# Provision authentication and authorization resource
az ad app create --display-name "MyApp" --password "MyPassword" --reply-urls "https://myapp.com/callback"

# Update authentication and authorization resource
az ad app update --id "<app-id>" --reply-urls "https://myapp.com/callback" "https://myapp.com/redirect"

# Delete authentication and authorization resource
az ad app delete --id "<app-id>"

# Enable MFA for a user
az ad user update --id "<user-id>" --force-change-password-next-login true --password "MyPassword" --password-policies "DisablePasswordExpiration,DisableStrongPassword"

# Disable MFA for a user
az ad user update --id "<user-id>" --force-change-password-next-login true --password "MyPassword" --password-policies "DisablePasswordExpiration,DisableStrongPassword"

# Get MFA settings for a user
az ad user show --id "<user-id>" --query "mfaMethods"

# Enable MFA for an application
az ad app update --id "<app-id>" --required-resource-accesses "resourceAppId=<resource-app-id>;resourceAccess=[{id=<mfa-access-id>,type=Scope}]"

# Disable MFA for an application
az ad app update --id "<app-id>" --required-resource-accesses "resourceAppId=<resource-app-id>;resourceAccess=[]"

# Get MFA settings for an application
az ad app show --id "<app-id>" --query "requiredResourceAccesses"