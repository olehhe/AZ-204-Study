import logging

import azure.functions as func

def http_trigger(req: func.HttpRequest) -> func.HttpResponse:
    logging.info('HTTP trigger function processed a request.')
    # Your code here
    return func.HttpResponse("This is an HTTP trigger.")

def cosmosdb_trigger(documents: func.DocumentList) -> None:
    if documents:
        logging.info('Cosmos DB trigger function processed a batch of documents.')
        # Your code here

def blob_trigger(blob: func.InputStream) -> None:
    logging.info('Blob trigger function processed a blob.')
    # Your code here

def timer_trigger(timer: func.TimerRequest) -> None:
    logging.info('Timer trigger function ran.')
    # Your code here

def queue_trigger(msg: func.QueueMessage) -> None:
    logging.info('Queue trigger function processed a message.')
    # Your code here