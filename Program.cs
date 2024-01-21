using System;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;

class Program
{
    static async Task Main(string[] args)
    {
        var factory = new MqttFactory();
        var mqttClient = factory.CreateMqttClient();

        var options = new MqttClientOptionsBuilder()
            .WithTcpServer("test.mosquitto.org", 1883)
            .WithClientId("ClientId1")
            .Build();

        mqttClient.UseConnectedHandler(e =>
        {
            Console.WriteLine("Connected to the broker!");
        });

        await mqttClient.ConnectAsync(options);

        var message = new MqttApplicationMessageBuilder()
            .WithTopic("test/topic")
            .WithPayload("Hello MQTT from C#")
            .Build();

        await mqttClient.PublishAsync(message);

        Console.WriteLine("Message published!");

        await Task.Delay(2000);

        await mqttClient.DisconnectAsync();
    }
}
