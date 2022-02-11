using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class Launcher : MonoBehaviourPunCallbacks, ILobbyCallbacks
{
    public static Launcher launcher;
    public Transform roomsPanel;
    public GameObject roomListingPrefab;

    [HideInInspector] public int roomSize;
    [HideInInspector] public string roomName;
    [SerializeField] private Text showRoomName;
    [SerializeField] private InputField roomNameInputField;

    [SerializeField] GameObject playerListPrefab;
    [SerializeField] Transform playerListContent;

    [SerializeField] GameObject startGameButton;
    [SerializeField] GameObject leaveGameButton;


    private void Awake()
    {
        if (launcher == null)
        {
            launcher = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        Debug.Log("ConnectedToMaster");
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("You are in lobby");
        MenuManager.instance.OpenMenu("title");
        PhotonNetwork.NickName = "Player" + Random.Range(1, 1000).ToString();
        Debug.Log("ConnectedToMaster");
    }

    public void CreateRoom()
    {
        roomSize = 2;
        if (string.IsNullOrEmpty(roomNameInputField.text.ToString()))
            return;
        RoomOptions roomOps;
        roomName = roomNameInputField.text.ToString();
        roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)roomSize };
        PhotonNetwork.CreateRoom(roomNameInputField.text.ToString(), roomOps);
        MenuManager.instance.OpenMenu("loading");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log(message);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform t in roomsPanel)
        {
            Destroy(t.gameObject);
        }
        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                continue;
            Instantiate(roomListingPrefab, roomsPanel).GetComponent<RoomListItem>().Setup(roomList[i]);
        }
    }
    public override void OnJoinedRoom()
    {
        MenuManager.instance.OpenMenu("room");
        showRoomName.text = PhotonNetwork.CurrentRoom.Name;

        Player[] players = PhotonNetwork.PlayerList;
        foreach (Transform chield in playerListContent)
        {
            Destroy(chield.gameObject);
        }

        for (int i = 0; i < players.Length; i++)
        {
            Instantiate(playerListPrefab, playerListContent).GetComponent<PlayerListItem>().Setup(players[i]);
        }
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {

        CheckAllPlayersReady();
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            startGameButton.SetActive(PhotonNetwork.IsMasterClient);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerListPrefab, playerListContent).GetComponent<PlayerListItem>().Setup(newPlayer);
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.instance.OpenMenu("loading");
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.instance.OpenMenu("loading");
    }



    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public void NewProperties(string hashString, bool hashValue)
    {
        var hash = PhotonNetwork.LocalPlayer.CustomProperties;
        hash["Ready"] = hashValue;
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    }

    private void CheckAllPlayersReady()
    {
        var players = PhotonNetwork.PlayerList;
        if (players.All(p => p.CustomProperties.ContainsKey("Ready") && (bool)p.CustomProperties["Ready"]))
        {
            startGameButton.gameObject.GetComponent<Button>().interactable = PhotonNetwork.IsMasterClient;
            Debug.Log("All players are ready!");
        }
    }

}



