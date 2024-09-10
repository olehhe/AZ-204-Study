import redis

# Connect to the Redis Cache
redis_cache = redis.Redis(host='<your_redis_host>', port=<your_redis_port>, password='<your_redis_password>')

# Set a key-value pair in the cache
redis_cache.set('my_key', 'my_value')

# Get the value for a given key from the cache
value = redis_cache.get('my_key')
print(value)

# Delete a key from the cache
redis_cache.delete('my_key')