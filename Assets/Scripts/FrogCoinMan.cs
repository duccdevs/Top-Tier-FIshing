﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrogCoinMan : MonoBehaviour
{
    public float currentCoins;
    public GameObject FrogHolder;
    public Image[] coinImages;
    public Sprite[] coinSprites;
    private GameObject GM;
    public int BoughtAmount = 1;

    char[] chars;
    int a;
    int b;
    int c;
    int d;

    void Start()
    {
        GM = GameObject.Find("Coot");
        currentCoins = (3 * Mathf.Pow(1.3F, BoughtAmount)) - 1.25F;
        string thingString = currentCoins.ToString("0000");

        chars = thingString.ToCharArray();
        a = int.Parse(chars[0].ToString());
        b = int.Parse(chars[1].ToString());
        c = int.Parse(chars[2].ToString());
        d = int.Parse(chars[3].ToString());

        coinImages[0].sprite = coinSprites[a];
        coinImages[1].sprite = coinSprites[b];
        coinImages[2].sprite = coinSprites[c];
        coinImages[3].sprite = coinSprites[d];
    }

    public void SetNewCount()
    {
        if (GM == null)
        {
            GM = GameObject.Find("Coot");
        }
        BoughtAmount++;
        currentCoins = (3 * Mathf.Pow(1.3F, BoughtAmount)) - 1.25F;

        string thingString = currentCoins.ToString("0000");

        chars = thingString.ToCharArray();
        a = int.Parse(chars[0].ToString());
        b = int.Parse(chars[1].ToString());
        c = int.Parse(chars[2].ToString());
        d = int.Parse(chars[3].ToString());

        coinImages[0].sprite = coinSprites[a];
        coinImages[1].sprite = coinSprites[b];
        coinImages[2].sprite = coinSprites[c];
        coinImages[3].sprite = coinSprites[d];

        GetComponent<AudioSource>().Play();
    }
}