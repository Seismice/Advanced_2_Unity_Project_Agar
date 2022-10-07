using UnityEngine;
using Photon.Pun;
using Photon.Chat;
using ExitGames.Client.Photon;
using TMPro;

public class ChatManager : MonoBehaviour, IChatClientListener
{
    [Header("Chat")]
    ChatClient chatClient;
    [SerializeField] private string userID; // NickName ������

    [SerializeField] private TMP_Text chatText;
    [SerializeField] private TMP_InputField textMessage;
    [SerializeField] private TMP_InputField textUserName;
    [SerializeField] private TMP_InputField textFriendsName;

    [Header("Login")]
    [SerializeField] private TMP_InputField loginInputField;
    [SerializeField] private GameObject loginPanel;
    [SerializeField] private GameObject chatPanel;

    private string[] friends;

    public void DebugReturn(DebugLevel level, string message)
    {
        Debug.Log($"{level}, {message}");
    }

    public void OnChatStateChange(ChatState state)
    {
        Debug.Log(state);
    }

    public void OnConnected()
    {
        chatText.text += "\n�� ����������� �� ����!";
        chatClient.Subscribe("Chat");
        //chatClient.Subscribe(new string[] { "Chat", "ClanChat"}); // ���� ������� ����������� �� ����� �� ������ ����.
    }

    public void OnDisconnected()
    {
        chatClient.Unsubscribe(new string[] { "Chat" }); // ³������ �� ���� ���������� ����� �����. �����, ���� ��� ����.
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        for (int i = 0; i < senders.Length; i++)
        {
            chatText.text += $"\n[{channelName}] {senders[i]}: {messages[i]}";
        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        chatText.text += $"\n�������� ����������� �� [{sender}]: {message}";
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        chatText.text += $"\n���� {user}: {message} ({status}).";
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        for (int i = 0; i < channels.Length; i++)
        {
            chatText.text += $"\n�� ���������� �� {channels[i]}";
        }
    }

    public void OnUnsubscribed(string[] channels)
    {
        for (int i = 0; i < channels.Length; i++)
        {
            chatText.text += $"\n�� ���������� �� {channels[i]}";
        }
    }

    public void OnUserSubscribed(string channel, string user)
    {
        chatText.text += $"\n ������� {user} ��������� �� {channel}";
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        chatText.text += $"\n ������� {user} ��������� �� {channel}";
    }

    void Start()
    {
        chatClient = new ChatClient(this);
    }

    void Update()
    {
        chatClient.Service();
    }

    public void SendButton()
    {
        if(textUserName.text == "")
        {
            chatClient.SetOnlineStatus(4, "���������");
            if (textMessage.text != "")
            {
                chatClient.PublishMessage("Chat", textMessage.text); 
            }
        }
        else
        {
            chatClient.SetOnlineStatus(4, "���������");
            if (textMessage.text != "")
            {
                chatClient.SendPrivateMessage(textUserName.text, textMessage.text); 
            }
        }
    }

    public void LoginButton()
    {
        chatPanel.SetActive(true);
        loginPanel.SetActive(false);
        userID = loginInputField.text;
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, new AuthenticationValues(userID));
    }

    public void AddFriendsButton()
    {
        friends = textFriendsName.text.Trim().Split(',');
        chatClient.AddFriends(friends);
        chatClient.SetOnlineStatus(2, "���� �����");
    }

    public void RemoveFriendsButon()
    {
        friends = textFriendsName.text.Trim().Split(',');
        chatClient.RemoveFriends(friends);
        chatClient.SetOnlineStatus(3, "������� �����");
    }
}
