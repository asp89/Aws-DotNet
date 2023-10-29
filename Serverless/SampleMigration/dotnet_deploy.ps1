$functionName="SampleMigration"
$template="serverless.template"
$s3Bucket="learners-lambda-bucket"
$awsregion="us-east-2"

Push-Location $PSScriptRoot
dotnet lambda deploy-serverless $functionName --s3-bucket $s3Bucket --template $template --region $awsregion
Pop-Location