using UnityEngine;

public class Raycasting : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                PacketInfo packetInfo = hit.collider.GetComponent<PacketInfo>();

                if (packetInfo != null)
                {
                    packetInfo.DisplayInfo();
                }
                else
                {
                    Debug.Log("No PacketInfo component found on the clicked object.");
                }
            }
        }
    }
}
