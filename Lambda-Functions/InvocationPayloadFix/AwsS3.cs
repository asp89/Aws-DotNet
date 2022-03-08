using System;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Newtonsoft.Json;
using System.Net.Http;

namespace InvocationPayloadFix
{
    public class AwsS3
    {
        private readonly AmazonS3Client client;
        private const string bucket = "my-learners-bucket";
        private const string fileName = "data.json";
        private const string objectKey = "resultSet.json";
        private const double duration = 12;

        public AwsS3()
        {
            client = new AmazonS3Client(Amazon.RegionEndpoint.USEast2);
        }

        public async Task<T> ReadJson<T>()
        {
            var (bytes, contentType) = await ReadFile();
            return Deserialize<T>(bytes);
        }

        public Task<(byte[] bytes, string contentType)> ReadFile() =>
        GetObject(
            bucket: bucket,
            key: $"{fileName}"
        );

        private async Task<(byte[] value, string contentType)> GetObject(string bucket, string key)
        {
            var request = new GetObjectRequest
            {
                BucketName = bucket,
                Key = key,
            };

            using (var response = await client.GetObjectAsync(request))
            using (var memStream = new MemoryStream())
            {
                await response.ResponseStream.CopyToAsync(memStream);
                return (
                    value: memStream.ToArray(),
                    contentType: response.Headers["Content-Type"]
                );
            }
        }

        public string GeneratePreSignedURL(DummyData data)
        {
            string urlString = string.Empty;
            try
            {
                GetPreSignedUrlRequest request = new GetPreSignedUrlRequest
                {
                    BucketName = bucket,
                    Key = objectKey,
                    Expires = DateTime.UtcNow.AddHours(duration)
                };
                urlString = client.GetPreSignedURL(request);
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error encountered on server. Message:'{0}' when writing an object", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
            }
            return urlString;
        }

        private T Deserialize<T>(byte[] bytes) =>
            bytes == null
                ? default(T)
                : JsonConvert.DeserializeObject<T>(
                    Encoding.UTF8.GetString(bytes))
            ;
    }
}