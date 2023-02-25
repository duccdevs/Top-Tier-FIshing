using UnityEngine;
using System.Collections;

public class Network : Photon.MonoBehaviour
{
    static public bool connecting = false;
    static public bool connected = false;

    public GameObject AllHolder;

    void Start()
    {
        connecting = false;
        connected = false;
        ConnectToGame();
    }

    void ConnectToGame()
    {
        if (!connecting)
        {
            connecting = true;
            PhotonNetwork.ConnectUsingSettings("alpha 0.3");
        }
        else if (!connected)
        {
            print(PhotonNetwork.connectionStateDetailed.ToString());
        }
    }

    void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed()
    {
        PhotonNetwork.CreateRoom(null);
    }

    void OnJoinedRoom()
    {
        connected = true;
        SpawnPlayer();
        print("Connected");
    }

    void SpawnPlayer()
    {
        //SetALL
        GameObject Player = PhotonNetwork.Instantiate("Coot", new Vector2(-4, -1.175F), Quaternion.Euler(0,0,-0.03F), 0);
        Player.transform.rotation = Quaternion.Euler(0,0,-0.03F);
        GetComponent<GameManager>().CootsGameObject = Player;
        Player.GetComponent<Movement>().enabled = true;
        Player.GetComponent<Movement>().Canvass = GameObject.Find("Canvas");
        Player.GetComponent<Movement>().CoinManagerHolder = GameObject.Find("CoinManager");
        Player.GetComponent<Movement>().BdigeMan = GameObject.Find("BridgeSeller");
        Player.GetComponent<Movement>().FrogCoin = GameObject.Find("FroggeCoin");
        Player.GetComponent<Movement>().Spawners = GetComponent<GameManager>().SPawnymans;
        Player.transform.GetChild(0).GetComponent<MouseArm>().enabled = true;
        Player.transform.GetChild(0).transform.GetChild(0).GetComponent<WhatFosh>().enabled = true;
        AllHolder.GetComponent<CamFollowe>().Coots = Player.transform;
        Player.transform.GetChild(2).transform.parent = transform;
        Player.transform.GetChild(2).transform.parent = transform;
        Player.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
    }
}
