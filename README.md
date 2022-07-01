# LambaWebApi

## Prerequisites

To run the project you will need to install dotnet version 6 [https://dotnet.microsoft.com/en-us/download/dotnet/6.0] and also have docker desktop installed [https://www.docker.com/get-started/].

## Intro

The repo includes an AWS lambda dotnet web api. To run the dotnet web api locally, first of all you need to have a DynamoDb instance running locally. This can be done by running it within a docker container. A docker compose file has been included to help with this. To run it just use the command 'docker-compose up'.

The web api can then be run using the command 'dotnet run --project LambdaWebApi\LambdaWebApi.csproj', you can then test the api by navigating to http://localhost:63551/swagger/index.html

There are 2 endpoints on the web api. A get method /api/Teams/{sportType} that returns a list of sport teams by their sportType e.g. Football, Rugby, Cricket etc. The other endpoint is a Post method /api/Teams used to add teams to DynamoDb in the form {"sportType": "Football", "teamName", "Leeds United"}.

The project also contains unit and integration tests that can be run with the command 'dotnet test'. The integration test must have the DynamoDb docker container running for the test to pass.

## Tech Test

We would like you to create a build and deploy pipeline for this web api. The pipeline should build the dotnet lambda project, run the unit tests, package it up for deployment and then deploy it to an AWS environment. Ideally we would also like the integration test to run as part of the pipeline. We would like the pipeline to be triggered every time code is pushed to the master/main branch of code.

We would like you to use a cloudformation template to deploy this to an AWS environment. We expect a minimal of 3 AWS components to be used for the test. The code should be deployed to an AWS Lambda, with an AWS API Gateway to provide access to the web api endpoints (within the lambda) and AWS DynamoDb as a storage solution for the sport teams.

To do the test you will need to use a CI tool account for the pipeline of which there are numerous free tools out there. You can use any tool of your choice for this but if you need recommendations you can sign up for free accounts with Azure DevOps or Github. You will also need to sign up for an AWS account to test it. All the components needed for this test are included within the AWS 'Always free' tier.

You can clone or fork the project to your version control software of choice. At the end we expect to get a link to your repo and it should also include within it a cloudformation file and a file to define the pipeline. We would also ask for a link to your pipeline which shows the build and deployment of the Lambda.

Free feel to add any best software practices that you see fit within the test. Good Luck.