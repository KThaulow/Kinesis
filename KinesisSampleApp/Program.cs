// Copyright Amazon.com, Inc. or its affiliates. All Rights Reserved. 
// SPDX-License-Identifier: Apache-2.0

using Amazon;
using Amazon.S3;
using Amazon.S3.Model;

class Program
{
    static async Task Main(string[] args)
    {
        string bucketName = "thaulow-test-bucket";

        // Create an S3 client
        var s3Client = new AmazonS3Client(RegionEndpoint.USEast2); // Replace with your region

        try
        {
            // Get bucket metadata
            var response = await s3Client.GetBucketLocationAsync(new GetBucketLocationRequest
            {
                BucketName = bucketName
            });

            Console.WriteLine($"Bucket '{bucketName}' is located in: {response.Location}");

            var objectResult = await s3Client.ListObjectsV2Async(new ListObjectsV2Request
            {
                BucketName = bucketName
            });

            foreach (var s3Object in objectResult.S3Objects)
            {
                Console.WriteLine(s3Object.Key);
            }
        }
        catch (AmazonS3Exception ex)
        {
            Console.WriteLine($"Error encountered: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unknown error: {ex.Message}");
        }
    }
}