from azure.identity import DefaultAzureCredential
from azure.mgmt.redis import RedisManagementClient
from azure.mgmt.redis.models import RedisCreateParameters, RedisUpdateParameters

# Set your Azure subscription ID and resource group name
subscription_id = 'your-subscription-id'
resource_group_name = 'your-resource-group-name'

# Set your Azure credentials
credential = DefaultAzureCredential()

# Set the Redis Cache name and location
redis_cache_name = 'your-redis-cache-name'
location = 'your-location'

# Create a Redis Cache
redis_client = RedisManagementClient(credential, subscription_id)
redis_params = RedisCreateParameters(location=location, sku={'name': 'Standard', 'family': 'C', 'capacity': 1})
redis_client.redis.create(resource_group_name, redis_cache_name, redis_params)

# Update the Redis Cache
redis_update_params = RedisUpdateParameters(sku={'name': 'Standard', 'family': 'C', 'capacity': 2})
redis_client.redis.update(resource_group_name, redis_cache_name, redis_update_params)

# Delete the Redis Cache
redis_client.redis.delete(resource_group_name, redis_cache_name)