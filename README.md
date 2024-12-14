
# MRVizNet: Immersive Network Visualization with Mixed Reality

### Authors: Sri Nithya Anne, Sneha Sugilal, Govind Iyer

## Project Overview

MRVizNet is a Unity-based immersive network visualization tool that leverages Mixed Reality (MR) to display real-time network traffic. The project uses MQTT for communication and Scapy for packet capture. The tool helps visualize packet exchanges with spatial context in a 3D environment.

## Requirements

### Libraries & Packages
- **Unity Packages:**
  - M2Mqtt
  - Newtonsoft.Json
  - TextMeshPro
  - XR Interaction Toolkit (if XR features are required)
  - Meta XR Plugin (for Meta Quest headset support)

- **Python Libraries:**
  - paho-mqtt

  Install with the following command:
  ```bash
  pip install paho-mqtt
  ```

### Software Tools
- **Unity 6** (for creating the visualization environment)
- **MQTT Broker (Mosquitto)** (for real-time data routing)
- **Packet Sniffer:** Scapy (to capture Wi-Fi packets)

### Hardware Tools
- **Laptop** (source device for packet transmission)
- **Wireless Access Points (AP1, AP2, AP3)**
- **VR Headset (optional):** Meta Quest 3

---

## Step-by-Step Procedure

### 1. Import Packages
- Open Unity and import the following via the **Unity Package Manager**:
  - M2Mqtt
  - Newtonsoft.Json
  - TextMeshPro
  - XR Interaction Toolkit (for XR features)
  - Meta XR Plugin (for VR headset integration)

- Install `paho-mqtt` for Python:
  ```bash
  pip install paho-mqtt
  ```

### 2. Create Prefabs

#### Packet Prefab
- Create a **Packet Prefab** with the following components:
  - `PacketInfo` script
  - `BoxCollider`
  - `LineRenderer`
- Assign default materials for packet types (e.g., red for TCP, blue for UDP).

#### Access Point Prefabs
- Create **Access Point Prefabs** (AP1, AP2, AP3) using cylinder models.

### 3. Design the Scene
- Add the following objects to the scene:
  - **Laptop** (source device)
  - **Access Points** (AP1, AP2, AP3)
  - **Canvas** (for displaying packet information)
- Create a **NetworkManager** GameObject and attach the `MQTTReceiver` script.
- Attach the `UIManager` script to the Canvas to manage the UI display.

### 4. Python Sender Script (`mqttsender.py`)
- Create `mqttsender.py` to simulate sending packets to the MQTT broker.

### 5. Assign References in the Inspector
- **In `MQTTReceiver.cs`**: Assign the `PacketVisualizer` script for visualization.
- **In `PacketVisualizer.cs`**: Assign the packet prefab, access points, and materials.
- **In `UIManager.cs`**: Assign the TextMeshPro UI element for packet display.

### 6. Testing the Project
1. Run the Python sender script:
   ```bash
   python mqttsender.py
   ```
   or
    ```bash
   python wifi_packet_capture.py
   ```
3. Start the Unity scene.
4. Verify that packets are visualized with the correct type, size, and IP addresses.
5. Click on a packet to display its details. The UI should update accordingly.
6. Ensure packets are destroyed after reaching their destination or upon interaction.

---

## Project Hierarchy Diagram

```
MRVizNet Project
|
├── Scripts
|   ├── MQTTReceiver.cs
|   ├── PacketVisualizer.cs
|   ├── PacketInfo.cs
|   ├── Raycasting.cs
|   └── UIManager.cs
|
├── Prefabs
|   ├── PacketPrefab
|   └── AccessPointPrefabs
|
├── Python Scripts
|   └── mqttsender.py or wifi_packet_capture.py
|
├── Scene
|   ├── Laptop
|   ├── Access Points (AP1, AP2, AP3)
|   └── Canvas
|
└── Materials
    ├── TCP Material
    ├── UDP Material
    ├── HTTP Material
    └── FTP Material
```

---

## Results & Key Findings
- **Real-time Packet Capture:** Packets are captured and visualized in Unity with minimal latency.
- **Color-Coded Packets:** Different colors represent various packet types (e.g., TCP, UDP).
- **Interactive Visualization:** Clicking on packets displays detailed information.

---

## Future Scope
- Integration with Meta Quest 3 for immersive VR visualization.
- Enhanced packet analysis (latency, throughput, error rates).
- Support for additional network protocols.
- Performance optimization for high traffic volumes.

---

## References
1. [MQTT Documentation](http://mqtt.org)
2. [Scapy Documentation](https://scapy.readthedocs.io)
3. [Unity 6](https://unity.com)
4. [Meta Quest 3 Development](https://developer.oculus.com)
