Generate AWS Signature V4 by using IAM credential (Access key ID and Secret access key).

How to use:

Install-Package SW.AWS.SignatureV4.Generator -Version 1.0.1

using AWS.SignatureV4.Generator;

var generator = new AWSSignatureV4Generator();
string ret = generator.GenerateSignature("xxxxxxxxxx.iot.ap-northeast-1.amazonaws.com", "ap-northeast-1", "your access key id", "your secret access key");
