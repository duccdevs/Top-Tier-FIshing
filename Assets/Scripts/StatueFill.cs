using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StatueFill : MonoBehaviour
{
    public bool InStatue = false;
    //100k
    public float NeededGold = 5;
    public float NeededGoldFinal;
    public float CurrentGold = 0;
    private GameObject Coots;
    public GameObject BuildStatue;

    public ParticleSystem[] Effects;
    public ParticleSystem[] Confetti;
    public AudioClip Victory;
    public Animator WinScreen;

    float fullsize = 1.0F;
    float FullStatue;

    public Image[] coinImages;
    public Sprite[] coinSprites;

    char[] chars;
    int a;
    int b;
    int c;
    int d;
    int e;

    void Start()
    {
        FullStatue = NeededGold;
        NeededGoldFinal = NeededGold;

        SetNewCount();
    }

    void Update()
    {
        if (Coots == null)
        {
            //set coots
            Coots = GameObject.Find("GameManager").GetComponent<GameManager>().CootsGameObject;
        }

        if (InStatue && Input.GetMouseButtonDown(0) && Coots.GetComponent<Movement>().CoinManagerHolder.GetComponent<CoinManager>().currentCoins > 0)
        {
            print("WEEEEE");
            //AddGold
            if (Coots.GetComponent<Movement>().CoinManagerHolder.GetComponent<CoinManager>().currentCoins < NeededGold)
            {
                AddedCoins();
                CurrentGold += Coots.GetComponent<Movement>().CoinManagerHolder.GetComponent<CoinManager>().currentCoins;
                NeededGold -= Coots.GetComponent<Movement>().CoinManagerHolder.GetComponent<CoinManager>().currentCoins;
                Coots.GetComponent<Movement>().CoinManagerHolder.GetComponent<CoinManager>().currentCoins -= Coots.GetComponent<Movement>().CoinManagerHolder.GetComponent<CoinManager>().currentCoins;
                Coots.GetComponent<Movement>().CoinManagerHolder.GetComponent<CoinManager>().AddCash(0, 0);
                SetNewCount();
                print("cuhching");
                return;
            }
            else
            {
                WinGame();
                CurrentGold = NeededGoldFinal;
                Coots.GetComponent<Movement>().CoinManagerHolder.GetComponent<CoinManager>().currentCoins -= NeededGold;
                Coots.GetComponent<Movement>().CoinManagerHolder.GetComponent<CoinManager>().AddCash(0, 0);
                NeededGold = 0;
                SetNewCount();
                //WON GAME WOO
                print("YOU WIN MATE NICE JOB ALRIGHTY");
                return;
            }
        }

        //StatueSize
        float newSize = Mathf.Clamp(CurrentGold / FullStatue, 0.0F, 1.0F) * fullsize;
        SpriteRenderer spr = BuildStatue.GetComponent<SpriteRenderer>();
        spr.drawMode = SpriteDrawMode.Tiled;
        spr.size = new Vector2(0.71875F, newSize);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Hand")
        {
            InStatue = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Hand")
        {
            InStatue = false;
        }
    }

    void AddedCoins()
    {
        for (int i = 0; i < Effects.Length; i++)
        {
            Effects[i].Play();
        }

        GetComponent<AudioSource>().Play();
        GameObject.Find("GameManager").GetComponent<GameManager>().CamShake(1);
    }

    void WinGame()
    {
        for (int i = 0; i < Confetti.Length; i++)
        {
            Confetti[i].Play();
        }

        GetComponent<AudioSource>().PlayOneShot(Victory);
        GameObject.Find("GameManager").GetComponent<GameManager>().CamShake(0.5F);
        Coots.GetComponent<Movement>().enabled = false;

        Invoke("GoBackHome", 4);
    }

    void GoBackHome()
    {
        WinScreen.SetBool("Win", true);
    }

    public void SetNewCount()
    {
        string thingString = NeededGold.ToString("00000");

        print(thingString);

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

        GetComponent<AudioSource>().Play();
    }
}
