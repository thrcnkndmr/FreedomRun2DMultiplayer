using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    PhotonView _pv;
    private void Awake()
    {
        _pv = GetComponent<PhotonView>();
        if (_pv.IsMine)
        {
            CreateController();
        }

    }
    void Start()
    {
    }

    void CreateController()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), Vector3.zero, Quaternion.identity);
        Debug.Log("Player Spawned");
    }
}
