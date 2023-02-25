using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour
{
    public int currentCombo = 1;
    int oldComb;
    public GameObject ValueHolder;
    public Image[] coinImages;
    public Sprite[] coinSprites;
    float CountDownTimer = 60;

    char[] chars;
    int a;

    void Start()
    {
        oldComb = currentCombo;
        string thingString = currentCombo.ToString("0");

        chars = thingString.ToCharArray();
        a = int.Parse(chars[0].ToString());

        coinImages[0].sprite = coinSprites[a];
    }

    void Update()
    {
        currentCombo = ValueHolder.GetComponent<Spawner>().CoinMultiMan;
        if (currentCombo > 1)
        {
            CountDownTimer -= Time.deltaTime;
            if (CountDownTimer <= 0.0F)
            {
                CountDownTimer = 60;
                ValueHolder.GetComponent<Spawner>().CoinMultiMan -= 1;
            }
        }
        if (oldComb != currentCombo)
        {
            SetNew();
        }
    }

    void SetNew()
    {
        string thingString = currentCombo.ToString("0");
        CountDownTimer = 60;

        chars = thingString.ToCharArray();
        a = int.Parse(chars[0].ToString());

        coinImages[0].sprite = coinSprites[a];

        oldComb = currentCombo;
    }
}
