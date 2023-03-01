using System.Collections;
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
    int e;

    void Start()
    {
        GM = GameObject.Find("Coot");
        currentCoins = (4 * Mathf.Pow(1.15F, BoughtAmount)) - 0.5F;
        string thingString = currentCoins.ToString("00000");

        chars = thingString.ToCharArray();
        a = int.Parse(chars[0].ToString());
        b = int.Parse(chars[1].ToString());
        c = int.Parse(chars[2].ToString());
        d = int.Parse(chars[3].ToString());
        e = int.Parse(chars[4].ToString());

        coinImages[0].sprite = coinSprites[a];
        coinImages[1].sprite = coinSprites[b];
        coinImages[2].sprite = coinSprites[c];
        coinImages[3].sprite = coinSprites[d];
        coinImages[4].sprite = coinSprites[e];
    }

    public void SetNewCount()
    {
        if (GM == null)
        {
            GM = GameObject.Find("Coot");
        }
        BoughtAmount++;
        currentCoins = (4 * Mathf.Pow(1.15F, BoughtAmount)) - 0.5F;

        string thingString = currentCoins.ToString("00000");

        chars = thingString.ToCharArray();
        a = int.Parse(chars[0].ToString());
        b = int.Parse(chars[1].ToString());
        c = int.Parse(chars[2].ToString());
        d = int.Parse(chars[3].ToString());
        e = int.Parse(chars[4].ToString());

        coinImages[0].sprite = coinSprites[a];
        coinImages[1].sprite = coinSprites[b];
        coinImages[2].sprite = coinSprites[c];
        coinImages[3].sprite = coinSprites[d];
        coinImages[4].sprite = coinSprites[e];
    }
}
