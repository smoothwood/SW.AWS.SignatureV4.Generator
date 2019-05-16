Generate AWS Signature V4 by using IAM credential (Access key ID and Secret access key).
# Install
```Install-Package SW.AWS.SignatureV4.Generator -Version 1.0.2```

# How to use
`using AWS.SignatureV4.Generator;`

```var generator = new AWSSignatureV4Generator();```

```string ret = generator.GenerateSignature("xxxxxxxxxx.iot.ap-northeast-1.amazonaws.com", "ap-northeast-1", "your access key id", "your secret access key");```

**ret** contains url like: 
```wss://<your_iot>.iot.ap-northeast-1.amazonaws.com/mqtt?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAZQKM55U7B3CIWRLZ%2F20190516%2Fap-northeast-1%2Fiotdevicegateway%2Faws4_request&X-Amz-Date=20190516T042048Z&X-Amz-SignedHeaders=host&X-Amz-Signature=0a209d0870635d549a9d4f63c0f92eb1595e82515b5159e7b4b759a6c292082e```

# Testing your url

You can test it with any Node.js mqtt library.
Below example copied from [https://github.com/pedrohbtp/aws_signv4_mqtt](https://github.com/pedrohbtp/aws_signv4_mqtt)

```javascript
var mqtt = require('mqtt')
url = '<your_signed_url>'
port = 443
topic = '<your_topic>'
i = 0

var client  = mqtt.connect(url,
    {
        connectTimeout:5*1000,
        port: port,
    })
 
client.on('connect', function () {
  client.subscribe(topic, function (err) {
    if (!err) {
      client.publish(topic, 'Hello mqtt')
    }
  })
})
 
client.on('message', function (topic, message) {
  console.log(message.toString())
  i = i+1
  client.publish(topic, 'Hello mqtt '+String(i))
})
```
