using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{
    #region Instance
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<GameManager>();

            return instance;
        }
    }
    #endregion

    [SerializeField] private Button startButton;
    public Transform playerOneStartPosition;
    public Transform playerTwoStartPosition;
    public GameObject[] players;
    public bool gameStarted = false;
    PhotonView view;

    void Start()
    {
        startButton.gameObject.SetActive(PhotonNetwork.IsMasterClient);
    }
    public void StartGame()
    {
        FindObjectOfType<GameManager>().gameStarted = true;
        startButton.gameObject.SetActive(false);
        view = GetComponent<PhotonView>();
        view.RPC("RPCgameStarted", RpcTarget.AllBuffered, gameStarted);
    }

    [PunRPC]
    public void RPCgameStarted(bool gamestarted)
    {
        gameStarted = gamestarted;
    }
}