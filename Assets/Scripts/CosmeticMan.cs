using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmeticMan : Photon.MonoBehaviour
{
    public GameObject[] Skins;

    void Start()
    {
        if (photonView.isMine)
        {
            Skins[0].GetComponent<SpriteRenderer>().color = Color.white;
            Skins[1].GetComponent<SpriteRenderer>().color = Color.white;
            Skins[2].GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public void FlipSkins(bool isFlipped)
    {
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("FlipSkins2", PhotonTargets.All, isFlipped);
    }
    [PunRPC]
    public void FlipSkins2(bool isFlipped)
    {
        foreach (GameObject sprite in Skins)
        {
            sprite.GetComponent<SpriteRenderer>().flipX = isFlipped;
        }
    }
}
