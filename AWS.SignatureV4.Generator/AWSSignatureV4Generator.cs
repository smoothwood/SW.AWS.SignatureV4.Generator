using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace AWS.SignatureV4.Generator
{
    public class AWSSignatureV4Generator
    {
        public string GenerateSignature(string iot_host, string iot_region, string access_key, string secret_key, string session_token = null)
        {
            string method = "GET";
            string service = "iotdevicegateway";
            string host = iot_host;
            string region = iot_region;
            string canonical_uri = "/mqtt";
            string algorithm = "AWS4-HMAC-SHA256";
            DateTime t = DateTime.UtcNow;
            string amzdate = t.ToString("yyyyMMddTHHmmssZ");
            string datestamp = t.ToString("yyyyMMdd");
            string credential_scope = datestamp + "/" + region + "/" + service + "/" + "aws4_request";
            string canonical_querystring = "X-Amz-Algorithm=" + algorithm + "&" +
                                           "X-Amz-Credential=" + WebUtility.UrlEncode(access_key + "/" + credential_scope) + "&" +
                                           "X-Amz-Date=" + amzdate + "&" +
                                           "X-Amz-SignedHeaders=" + "host";

            string canonical_headers = "host:" + host + "\n";
            string payload_hash = "".SHA256();
            string canonical_request = method + "\n" + canonical_uri + "\n" + canonical_querystring + "\n" + canonical_headers + "\nhost\n" + payload_hash;
            string string_to_sign = algorithm + "\n" + amzdate + "\n" + credential_scope + "\n" + canonical_request.SHA256();
            byte[] signing_key = GetSignatureKey(secret_key, datestamp, region, service);
            string signature = BitConverter.ToString(Sign(signing_key, string_to_sign)).Replace("-", "").ToLower();
            string signed_url = "wss://" + host + canonical_uri + "?" + canonical_querystring + "&X-Amz-Signature=" + signature;
            return signed_url;
        }

        private byte[] GetSignatureKey(string key, string dateStamp, string regionName, string serviceName)
        {
            byte[] kDate = Sign(Encoding.UTF8.GetBytes("AWS4" + key), dateStamp);
            byte[] kRegion = Sign(kDate, regionName);
            byte[] kService = Sign(kRegion, serviceName);
            byte[] kSigning = Sign(kService, "aws4_request");
            return kSigning;
        }
        private byte[] Sign(byte[] key, string msg)
        {
            var kha = KeyedHashAlgorithm.Create("HMACSHA256");
            kha.Key = key;
            return kha.ComputeHash(Encoding.UTF8.GetBytes(msg));
        }

    }
}
