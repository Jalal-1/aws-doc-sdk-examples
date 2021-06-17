﻿// Copyright Amazon.com, Inc. or its affiliates. All Rights Reserved.
// SPDX - License - Identifier: Apache - 2.0

namespace TransferAccelerationExample
{
    using Amazon.S3;
    using Amazon.S3.Model;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Amazon Simple Storage Service (Amazon S3) Transfer Acceleration is a
    /// bucket-level feature that enables you to perform faster data transfers
    /// to Amazon S3. This example shows how to configure  transfer
    /// acceleration. The example was written using the AWS SDK for .NET 3.7
    /// and .NET Core 5.0.
    /// </summary>
    class TransferAcceleration
    {
        /// <summary>
        /// The main method initalizes the client object and sets the
        /// Amazon Simple Storage Service (Amazon S3) bucket name before
        /// calling EnableAccelerationAsync.
        /// </summary>
        /// <returns></returns>
        public static async Task Main()
        {
            var s3Client = new AmazonS3Client();
            const string bucketName = "igsmith-temp-bucket"; // "doc-example-bucket";

            await EnableAccelerationAsync(s3Client, bucketName);
        }

        /// <summary>
        /// This method sets the configuration to enable transfer acceleration
        /// for the bucket referred to in the bucketName parameter.
        /// </summary>
        /// <param name="client">An Amazon S3 client used to enable the
        /// acceleration on an S3 bucket.</param>
        /// <param name="bucketName">The name of the S3 bucket for which the
        /// method will be enabling acceleration.</param>
        /// <returns></returns>
        static async Task EnableAccelerationAsync(AmazonS3Client client, string bucketName)
        {
            try
            {
                var putRequest = new PutBucketAccelerateConfigurationRequest
                {
                    BucketName = bucketName,
                    AccelerateConfiguration = new AccelerateConfiguration
                    {
                        Status = BucketAccelerateStatus.Enabled
                    }
                };
                await client.PutBucketAccelerateConfigurationAsync(putRequest);

                var getRequest = new GetBucketAccelerateConfigurationRequest
                {
                    BucketName = bucketName
                };
                var response = await client.GetBucketAccelerateConfigurationAsync(getRequest);

                Console.WriteLine($"Acceleration state = '{response.Status}' ");
            }
            catch (AmazonS3Exception ex)
            {
                Console.WriteLine($"Error occurred. Message:'{ex.Message}' when setting transfer acceleration");
            }
        }
    }
}