using UnityEngine;
using System.Collections;
using Oculus.Platform.Models;
using static Unity.VisualScripting.Member;

public class PacketVisualizer : MonoBehaviour
{
    public GameObject packetPrefab;  // Assign the PacketPrefab in the Inspector
    public Transform laptop;          // Assign the Laptop (Source) GameObject
    public Transform[] accessPoints;  // Assign AP1, AP2, and AP3

    public void VisualizePacket(string type, int size, string destination, string sourceIP, string destinationIP)
    {
        Debug.Log($"Visualizing packet: Type: {type}, Size: {size}, Source IP: {sourceIP}, Destination: {destination}, Destination IP: {destinationIP}");

        Transform targetAP = GetAccessPoint(destination);

        if (targetAP == null)
        {
            Debug.LogError("Target access point not found.");
            return;
        }

        // Instantiate the packet prefab
        GameObject packet = Instantiate(packetPrefab, laptop.position, Quaternion.identity);

        // Set the scale based on the packet size
        float scale = Mathf.Clamp(size / 500f, 0.1f, 2f);
        packet.transform.localScale = new Vector3(scale, scale, scale);

        // Assign packet info to the PacketInfo script
        PacketInfo packetInfo = packet.GetComponent<PacketInfo>();
        if (packetInfo != null)
        {
            packetInfo.packetType = type;
            packetInfo.packetSize = size;
            packetInfo.sourceIP = sourceIP;
            packetInfo.destination = destination;
            packetInfo.destinationIP = destinationIP;
        }
        else
        {
            Debug.LogError("PacketInfo script not found on the packet prefab.");
        }

        // Set color based on the packet type
        Renderer renderer = packet.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = GetColorByPacketType(type);
        }

        // Move the packet from the laptop to the access point
        StartCoroutine(MovePacket(packet, targetAP.position));
    }


    // Method to get color based on packet type
    private Color GetColorByPacketType(string type)
    {
        switch (type)
        {
            case "TCP": return Color.red;
            case "UDP": return Color.blue;
            case "HTTP": return Color.green;
            case "FTP": return Color.yellow;
            default: return Color.gray;
        }
    }

    // Method to configure the LineRenderer
    private void ConfigureLineRenderer(LineRenderer lr, Vector3 start, Vector3 end, string type)
    {
        if (lr == null)
        {
            Debug.LogError("LineRenderer is null and cannot be configured.");
            return;
        }

        lr.positionCount = 2;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);

        lr.startWidth = 0.05f;
        lr.endWidth = 0.05f;

        if (lr.material == null)
        {
            lr.material = new Material(Shader.Find("Sprites/Default"));
        }

        lr.startColor = lr.endColor = GetColorByPacketType(type);
        lr.useWorldSpace = true;
        lr.alignment = LineAlignment.View;
    }

    IEnumerator MovePacket(GameObject packet, Vector3 targetPosition)
    {
        float duration = 2f;  // Time in seconds for the packet to travel
        float elapsed = 0f;
        Vector3 startPosition = packet.transform.position;

        while (elapsed < duration)
        {
            packet.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        packet.transform.position = targetPosition;

        // Destroy the packet after it reaches the destination
        Destroy(packet, 2f);
    }

    Transform GetAccessPoint(string destination)
    {
        foreach (Transform ap in accessPoints)
        {
            if (ap.name == destination)
                return ap;
        }
        Debug.LogWarning("Access Point not found for destination: " + destination);
        return null;
    }
}
