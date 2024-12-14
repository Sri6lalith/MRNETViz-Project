using UnityEngine;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System;
using Newtonsoft.Json.Linq;

public class MQTTReceiver : MonoBehaviour
{
    public PacketVisualizer packetVisualizer;
    private MqttClient client;

    void Start()
    {
        string brokerAddress = "localhost"; // Replace with the laptop's IP running the MQTT broker
        client = new MqttClient(brokerAddress);

        try
        {
            client.Connect(Guid.NewGuid().ToString());
            Debug.Log("Connected to MQTT broker at " + brokerAddress);
            client.Subscribe(new string[] { "network/packets" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
            client.MqttMsgPublishReceived += OnMessageReceived;
        }
        catch (Exception e)
        {
            Debug.LogError("Connection to MQTT broker failed: " + e.Message);
        }
    }

    void OnMessageReceived(object sender, MqttMsgPublishEventArgs e)
    {
        string message = System.Text.Encoding.UTF8.GetString(e.Message);
        Debug.Log("Received message: " + message);

        try
        {
            JObject packet = JObject.Parse(message);

            string type = packet["type"].ToString();
            int size = int.Parse(packet["size"].ToString());
            string destination = packet["destination"].ToString();
            string destinationIP = packet["destination_ip"].ToString();
            string sourceIP = packet["source_ip"].ToString();

            Debug.Log($"Type: {type}, Size: {size}, Source IP: {sourceIP}, Destination: {destination}, Destination IP: {destinationIP}");

            if (packetVisualizer != null)
            {
                UnityMainThreadDispatcher.Instance().Enqueue(() =>
                {
                    Debug.Log("Enqueuing packet visualization...");
                    packetVisualizer.VisualizePacket(type, size, destination, sourceIP, destinationIP);
                });
            }
            else
            {
                Debug.LogError("packetVisualizer is not assigned.");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error parsing message: " + ex.Message);
        }
    }
}
