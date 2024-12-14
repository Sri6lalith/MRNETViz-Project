import paho.mqtt.client as mqtt
import time
import random
import json

# MQTT Broker IP
BROKER = "localhost"  # Replace with the actual IP of the laptop running Mosquitto
TOPIC = "network/packets"

# Source Laptop IP Address
SOURCE_LAPTOP_IP = "10.110.76.7"

client = mqtt.Client()
client.connect(BROKER, 1883, 60)

# Dictionary of access points with their IP addresses
access_points = {
    "AP1": "10.110.0.10",
    "AP2": "10.110.0.11",
    "AP3": "10.110.0.12"
}

def send_packet():
    packet_types = ["TCP", "UDP", "HTTP", "FTP"]

    while True:
        packet_type = random.choice(packet_types)
        ap_name = random.choice(list(access_points.keys()))
        ap_ip = access_points[ap_name]
        message = f"Data sent from {SOURCE_LAPTOP_IP} to {ap_name} ({ap_ip})"
        packet_size = len(message.encode('utf-8')) + random.randint(100, 1000)  # Packet size in bytes

        packet = {
            "type": packet_type,
            "size": packet_size,
            "message": message,
            "destination": ap_name,
            "destination_ip": ap_ip,
            "source_ip": SOURCE_LAPTOP_IP,
            "timestamp": time.time()
        }
        
        # Publish the packet to the MQTT broker
        client.publish(TOPIC, json.dumps(packet))
        print(f"Sent: {packet}")
        
        time.sleep(30)  # Send a packet every 30 seconds

send_packet()
