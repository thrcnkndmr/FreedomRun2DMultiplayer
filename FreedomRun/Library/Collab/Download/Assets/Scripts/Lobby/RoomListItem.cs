using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class RoomListItem : MonoBehaviour
{
    public Text nameText;
    public string roomName;
    public int roomSize;
    public RoomInfo info;
    public void Setup(RoomInfo _info)
    {
        info = _info;
        nameText.text = _info.Name;
    }
    public void JoinRoomOnClick()
    {
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinRoom(info.Name);
        }

    }
}
