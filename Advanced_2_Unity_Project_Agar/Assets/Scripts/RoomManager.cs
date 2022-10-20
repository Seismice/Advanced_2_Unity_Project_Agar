using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Collections.Generic;
using System;
using ExitGames.Client.Photon;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField roomName;
    [SerializeField] private ListItem itemPrefab;
    [SerializeField] private Transform content;

    private List<RoomInfo> allRoomsInfo = new List<RoomInfo>();

    private PlayerInstantiater playerInstantiater;

    //[SerializeField] private GameObject point_pref;

    void Start()
    {
        playerInstantiater = GetComponent<PlayerInstantiater>();

        PhotonPeer.RegisterType(typeof(Vector2Int), 242, SerializeVector2Int, DeserializeVector2Int);
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
        //Vector3 pos = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        //for (int i = 0; i < 10; i++)
        //{
        //    PhotonNetwork.InstantiateRoomObject(point_pref.name, pos, Quaternion.identity);
        //}
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
        //Vector3 pos = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        //for (int i = 0; i < 10; i++)
        //{
        //    PhotonNetwork.InstantiateRoomObject(point_pref.name, pos, Quaternion.identity);
        //}
    }

    public void JoinRandomButton()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(roomName.text);
        //Vector3 pos = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        //for (int i = 0; i < 10; i++)
        //{
        //    PhotonNetwork.InstantiateRoomObject(point_pref.name, pos, Quaternion.identity);
        //}
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

    public static object DeserializeVector2Int(byte[] data)
    {
        Vector2Int rezult = new Vector2Int();

        rezult.x = BitConverter.ToInt32(data, 0);
        rezult.y = BitConverter.ToInt32(data, 4);

        return rezult;
    }

    public static byte[] SerializeVector2Int(object obj)
    {
        Vector2Int vector = (Vector2Int)obj;
        byte[] rezult = new byte[8];

        BitConverter.GetBytes(vector.x).CopyTo(rezult, 0);
        BitConverter.GetBytes(vector.y).CopyTo(rezult, 4);

        return rezult;
    }
}
