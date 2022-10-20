using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class PointInstantiate : MonoBehaviour
{
    private PhotonView View;

    [SerializeField] private PointController point_pref;
    void Start()
    {
        View = GetComponent<PhotonView>();
        if (SceneManager.GetActiveScene().name == "game")
        {
            if (View.IsMine)
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        SendPointInstantiate();
                    }
                }
            }
        }
    }

    public void SendPointInstantiate()
    {
        View.RPC("Point_Instantiate", RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void Point_Instantiate()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Vector3 pos = new Vector3(Random.Range(-19f, 19f), Random.Range(-19f, 19f));
            PhotonNetwork.InstantiateRoomObject(point_pref.name, pos, Quaternion.identity);
        }
    }
}
