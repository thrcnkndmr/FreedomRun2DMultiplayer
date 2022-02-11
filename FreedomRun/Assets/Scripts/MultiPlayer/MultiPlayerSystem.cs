using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;


public class MultiPlayerSystem : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Server'a girildi.");

        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinOrCreateRoom("oda", new RoomOptions { MaxPlayers = 2, IsOpen = true, IsVisible = true }, TypedLobby.Default);
        Debug.Log("Lobi'ye girildi.");
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Oda'ya girildi.");
        
        GameObject yeniOyuncu = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity, 0, null);
        yeniOyuncu.GetComponent<PhotonView>().Owner.NickName = Random.Range(1, 100) + "(Misafir)";
        print(yeniOyuncu.GetPhotonView().Owner.NickName);
    }
    public override void OnLeftRoom()
    {
        Debug.Log("Oda'dan ayrýldý.");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
