using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public float currentCoins = 0;
    public Image[] coinImages;
    public Image[] coinImagesADD;
    public Sprite[] coinSprites;

    char[] chars;
    int a;
    int b;
    int c;
    int d;
    int e;
    int f;
    int g;
    int h;

    void Start()
    {
        string thingString = Mathf.Clamp(currentCoins, 0, 99999999).ToString("00000000");

        chars = thingString.ToCharArray();
        a = int.Parse(chars[0].ToString());
        b = int.Parse(chars[1].ToString());
        c = int.Parse(chars[2].ToString());
        d = int.Parse(chars[3].ToString());
        e = int.Parse(chars[4].ToString());
        f = int.Parse(chars[5].ToString());
        g = int.Parse(chars[6].ToString());
        h = int.Parse(chars[7].ToString());

        coinImages[0].sprite = coinSprites[a];
        coinImages[1].sprite = coinSprites[b];
        coinImages[2].sprite = coinSprites[c];
        coinImages[3].sprite = coinSprites[d];
        coinImages[4].sprite = coinSprites[e];
        coinImages[5].sprite = coinSprites[f];
        coinImages[6].sprite = coinSprites[g];
        coinImages[7].sprite = coinSprites[h];
    }

    public void AddCash(int CashAmount, float mult)
    {
        currentCoins += CashAmount * mult;
        float addAmount = CashAmount * mult;

        coinImagesADD[0].sprite = coinSprites[Mathf.Clamp((int)addAmount / 10, 0, 9)];
        coinImagesADD[1].sprite = coinSprites[Mathf.Clamp((int)addAmount % 10, 0, 9)];

        print(CashAmount * mult);

        GetComponent<Animator>().SetTrigger("Cash");
    }

    public void SetNewCount()
    {
        string thingString = Mathf.Clamp(currentCoins, 0, 99999999).ToString("00000000");

        chars = thingString.ToCharArray();
        a = int.Parse(chars[0].ToString());
        b = int.Parse(chars[1].ToString());
        c = int.Parse(chars[2].ToString());
        d = int.Parse(chars[3].ToString());
        e = int.Parse(chars[4].ToString());
        f = int.Parse(chars[5].ToString());
        g = int.Parse(chars[6].ToString());
        h = int.Parse(chars[7].ToString());

        coinImages[0].sprite = coinSprites[a];
        coinImages[1].sprite = coinSprites[b];
        coinImages[2].sprite = coinSprites[c];
        coinImages[3].sprite = coinSprites[d];
        coinImages[4].sprite = coinSprites[e];
        coinImages[5].sprite = coinSprites[f];
        coinImages[6].sprite = coinSprites[g];
        coinImages[7].sprite = coinSprites[h];

        GetComponent<AudioSource>().Play();
    }
}
