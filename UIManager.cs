using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI packetInfoText;

    public void DisplayPacketInfo(string info)
    {
        if (packetInfoText != null)
        {
            packetInfoText.text = info;
            Invoke(nameof(ClearPacketInfo), 5f); // Clear the text after 5 seconds
        }
        else
        {
            Debug.LogError("PacketInfoText is not assigned in the UIManager.");
        }
    }

    private void ClearPacketInfo()
    {
        if (packetInfoText != null)
        {
            packetInfoText.text = "";
        }
    }
}
