# AWS Lambda Project- S3 Operations

The basic agenda of the project is to consume services and develop a basic understanding on serverless services. The function handler is the lambda entry point that focuses on the performing a simple operation. It fetches records from DynamoDB and writes a file on a S3 bucket. On the completion of operations, message is published to SNS Topic suscribers via Simple-Notification-Service.

Consumed AWS services- 
* Lambda
* SNS
* DynamoDB
* S3 

This project consists of:
* Function.cs - class file containing a class with a single function handler method
* aws-lambda-tools-defaults.json - default argument settings for use with Visual Studio and command line deployment tools for AWS
* DynamoDBOperations.cs - Primarily responsible for dynamo database operations.
* SNSOperations.cs - Responsible for SNS topic message publish.
* S3Operations.cs - Responsible for S3 bucket operations.
* Models.cs - Contains model classes.


You may also have a test project depending on the options selected.

The generated function handler is a simple method accepting a string argument that returns the uppercase equivalent of the input string. Replace the body of this method, and parameters, to suit your needs. 

## Here are some steps to follow to get started from the command line:

Once you have edited your template and code you can deploy your application using the [Amazon.Lambda.Tools Global Tool](https://github.com/aws/aws-extensions-for-dotnet-cli#aws-lambda-amazonlambdatools) from the command line.

Install Amazon.Lambda.Tools Global Tools if not already installed.
```
    dotnet tool install -g Amazon.Lambda.Tools
```

If already installed check if new version is available.
```
    dotnet tool update -g Amazon.Lambda.Tools
```

Execute unit tests
```
    cd "S3Operations/test/S3Operations.Tests"
    dotnet test
```

Deploy function to AWS Lambda
```
    cd "S3Operations/src/S3Operations"
    dotnet lambda deploy-function
```
