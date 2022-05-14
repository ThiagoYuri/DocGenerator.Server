# API for document generation
##### *Context: DocumentGenerator is a API Services, It main working is creating and organizing of document the consumer.*

## List Projects
 + Web API 
   - DocGenerator.Producer - Web API with methods HTTPS.
   - DocGenerator.ConsumerGeneratorPDF - Consumer using RabbitMQ to get queue and create documents.
   - DocGenerator.Shared - Contains shared habbitMQ logic to use in WebAPI and Consumer.
   - DocGenerator.TestingAPI - Tests Web API.
 + Web Client
   - DocGenerator.Certificate - Web system to using generator of document.
   - DocGenerator.ProofOfPayment - Web system to using generator of document.
   - DocGenerator.ClientDLL - DLL with request support.
   - DocGenerator.TestClient - Tests ClientDLL.
<br></br>
 ## Install project
