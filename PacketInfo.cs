using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class PacketInfo : MonoBehaviour
{
    public string packetType;
    public int packetSize;
    public string sourceIP;
    public string destination;
    public string destinationIP;

    // Method to display packet information
    public void DisplayInfo()
    {
        string info = $"Packet Info:\n" +
                      $"Type: {packetType}\n" +
                      $"Size: {packetSize} bytes\n" +
                      $"Source IP: {sourceIP}\n" +
                      $"Destination: {destination}\n" +
                      $"Destination IP: {destinationIP}";

        Debug.Log(info);

        // Optionally display this info in the UI using UIManager
        UIManager uiManager = FindFirstObjectByType<UIManager>();
        if (uiManager != null)
        {
            uiManager.DisplayPacketInfo(info);
        }
        else
        {
            Debug.LogError("UIManager not found in the scene.");
        }
    }
}
