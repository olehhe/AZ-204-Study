using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.CosmosDB;
using Microsoft.Azure.WebJobs.Extensions.Storage;
using Microsoft.Azure.WebJobs.Extensions.Timers;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http;

public static class Triggers
{
    [FunctionName("HttpTrigger")]
    public static HttpResponseMessage Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequestMessage req,
        ILogger log)
    {
        // Your code logic here
        return req.CreateResponse(HttpStatusCode.OK, "Hello from HTTP trigger!");
    }

    [FunctionName("CosmosDBTrigger")]
    public static void Run(
        [CosmosDBTrigger(databaseName: "yourDatabaseName", collectionName: "yourCollectionName", ConnectionStringSetting = "yourConnectionStringSetting", LeaseCollectionName = "leases", CreateLeaseCollectionIfNotExists = true)] IReadOnlyList<Document> documents,
        ILogger log)
    {
        // Your code logic here
    }

    [FunctionName("BlobTrigger")]
    public static void Run(
        [BlobTrigger("yourContainerName/{name}", Connection = "yourConnectionStringSetting")] Stream myBlob,
        string name,
        ILogger log)
    {
        // Your code logic here
    }

    // The timer notation "0 */5 * * * *" used in the [TimerTrigger] attribute is a CRON expression that specifies the schedule on which the Azure Function should run. Here's a breakdown of how this CRON expression works:
    // Seconds (0): The function will trigger at the 0th second of the minute.
    // Minutes (*/5): The function will trigger every 5 minutes.
    // Hours (*): The function will trigger every hour.
    // Day of month (*): The function will trigger every day of the month.
    // Month (*): The function will trigger every month.
    // Day of week (*): The function will trigger every day of the week. 
    [FunctionName("TimerTrigger")]
    public static void Run(
        [TimerTrigger("0 */5 * * * *")] TimerInfo myTimer,
        ILogger log)
    {
        // Your code logic here
    }

    [FunctionName("QueueTrigger")]
    public static void Run(
        [QueueTrigger("yourQueueName", Connection = "yourConnectionStringSetting")] string myQueueItem,
        ILogger log)
    {
        // Your code logic here
    }
}