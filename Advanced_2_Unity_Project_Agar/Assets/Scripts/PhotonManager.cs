using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private string region;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.ConnectToRegion(region);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Ви підключені до регіона: " + PhotonNetwork.CloudRegion);
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby(); 
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Ви відключені від сервера!");
    }
}
