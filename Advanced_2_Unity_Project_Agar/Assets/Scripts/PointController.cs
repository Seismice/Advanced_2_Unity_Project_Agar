using UnityEngine;
using Photon.Pun;

public class PointController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Destroy(gameObject); 
        }
    }
}
