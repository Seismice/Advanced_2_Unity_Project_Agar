using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField roomName;
    [SerializeField] private ListItem itemPrefab;
    [SerializeField] private Transform content;

    private List<RoomInfo> allRoomsInfo = new List<RoomInfo>();

    //private GameObject player;
    //[SerializeField] private GameObject player_pref;

    private PlayerInstantiater playerInstantiater;

    void Start()
    {
        //if (SceneManager.GetActiveScene().name == "game")
        //{
        //    player = PhotonNetwork.Instantiate(player_pref.name, Vector3.zero, Quaternion.identity);
        //    //player.GetComponent<SpriteRenderer>().color = Color.green; // Змінити колір гравця
        //}

        playerInstantiater = GetComponent<PlayerInstantiater>();
    }

    public void CreateButton()
    {
        if(!PhotonNetwork.IsConnected)
        {
            return;
        }

        RoomOptions roomOption = new RoomOptions();
        roomOption.MaxPlayers = 20;
        PhotonNetwork.CreateRoom(roomName.text, roomOption, TypedLobby.Default);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Створена кімната: " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Кімната не створилася!");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(RoomInfo info in roomList)
        {
            for (int i = 0; i < allRoomsInfo.Count; i++)
            {
                if(allRoomsInfo[i].masterClientId == info.masterClientId)
                {
                    return;
                }
            }

            ListItem listItem = Instantiate(itemPrefab, content);

            if(listItem != null)
            {
                listItem.SetInfo(info);
                allRoomsInfo.Add(info);
            }
        }
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("game");
    }

    public void JoinRandomButton()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(roomName.text);
    }

    public void LeaveButton()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.Destroy(playerInstantiater.gameObject);
        PhotonNetwork.LoadLevel("main");
    }
}
