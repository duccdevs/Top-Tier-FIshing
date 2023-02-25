using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhatFosh : Photon.MonoBehaviour
{
    public Sprite[] AllFish;
    public Sprite[] AllFishSkel;

    public void SetFish(int fishdd)
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<SpriteRenderer>().sprite = AllFish[fishdd];

        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("SetFish2", PhotonTargets.All, fishdd);
    }

    public void SetFishSkel(int fishdd)
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<SpriteRenderer>().sprite = AllFishSkel[fishdd];

        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("SetFishSkel2", PhotonTargets.All, fishdd);
    }

    public void ReleaseFish()
    {
        GetComponent<SpriteRenderer>().enabled = false;

        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("ReleaseFish2", PhotonTargets.All);
    }

    [PunRPC]
    public void SetFish2(int fishdd)
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<SpriteRenderer>().sprite = AllFish[fishdd];
    }
    [PunRPC]
    public void SetFishSkel2(int fishdd)
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<SpriteRenderer>().sprite = AllFishSkel[fishdd];
    }
    [PunRPC]
    public void ReleaseFish2()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
