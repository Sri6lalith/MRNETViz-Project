import paho.mqtt.client as mqtt
from scapy.all import IP, TCP, UDP, send, Raw
import time
import random
import json
import string

# MQTT Broker IP
BROKER = "localhost"  # Replace with the actual IP of the laptop running Mosquitto
TOPIC = "network/packets"

# Source Laptop IP Address
SOURCE_LAPTOP_IP = "10.110.76.7"

# Dictionary of access points with their IP addresses
access_points = {
    "AP1": "10.110.0.10",
    "AP2": "10.110.0.11",
    "AP3": "10.110.0.12"
}

client = mqtt.Client()
client.connect(BROKER, 1883, 60)

def generate_random_message():
    length = random.randint(20, 100)  # Random message length between 20 and 100 characters
    message = ''.join(random.choices(string.ascii_letters + string.digits + ' ', k=length))
    return message

def send_mqtt_packet(packet_type, packet_size, ap_name, ap_ip, message):
    packet = {
        "type": packet_type,
        "size": packet_size,
        "message": message,
        "destination": ap_name,
        "destination_ip": ap_ip,
        "source_ip": SOURCE_LAPTOP_IP,
        "timestamp": time.time()
    }
    client.publish(TOPIC, json.dumps(packet))
    print(f"Sent to MQTT: {packet}")

def send_tcp_packet(destination_ip, message):
    packet = IP(src=SOURCE_LAPTOP_IP, dst=destination_ip) / TCP(dport=80) / Raw(load=message)
    send(packet)
    print(f"Sent TCP packet to {destination_ip}")

def send_udp_packet(destination_ip, message):
    packet = IP(src=SOURCE_LAPTOP_IP, dst=destination_ip) / UDP(dport=8081) / Raw(load=message)
    send(packet)
    print(f"Sent UDP packet to {destination_ip}")

def send_http_packet(destination_ip, message):
    http_request = f"GET / HTTP/1.1\r\nHost: {destination_ip}\r\n\r\n{message}"
    packet = IP(src=SOURCE_LAPTOP_IP, dst=destination_ip) / TCP(dport=80) / Raw(load=http_request)
    send(packet)
    print(f"Sent HTTP packet to {destination_ip}")

def send_ftp_packet(destination_ip, message):
    ftp_command = f"USER anonymous\r\nPASS {message}\r\n"
    packet = IP(src=SOURCE_LAPTOP_IP, dst=destination_ip) / TCP(dport=21) / Raw(load=ftp_command)
    send(packet)
    print(f"Sent FTP packet to {destination_ip}")

def send_packet():
    packet_types = ["TCP", "UDP", "HTTP", "FTP"]

    while True:
        packet_type = random.choice(packet_types)
        ap_name = random.choice(list(access_points.keys()))
        ap_ip = access_points[ap_name]
        
        # Generate a random message
        random_message = generate_random_message()
        
        # Calculate the packet size based on the message
        packet_size = len(random_message.encode('utf-8')) + random.randint(50, 1000)  # Base size + random overhead

        # Send the MQTT packet for visualization
        send_mqtt_packet(packet_type, packet_size, ap_name, ap_ip, random_message)

        # Send the actual network packet based on type
        if packet_type == "TCP":
            send_tcp_packet(ap_ip, random_message)
        elif packet_type == "UDP":
            send_udp_packet(ap_ip, random_message)
        elif packet_type == "HTTP":
            send_http_packet(ap_ip, random_message)
        elif packet_type == "FTP":
            send_ftp_packet(ap_ip, random_message)

        time.sleep(30)  # Send a packet every 30 seconds

send_packet()