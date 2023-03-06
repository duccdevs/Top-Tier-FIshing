using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour
{
    public Sprite[] Skeletons;
    public SpriteRenderer SkeletonHolder;
    public int rrNum;
    public Sprite openedSprite;
    public ParticleSystem goldpart;

    private GameObject CoinHolder;
    public SpriteRenderer[] coinImagesADD;
    public Sprite[] coinSprites;

    char[] chars;
    int a;
    int b;
    int c;
    int d;

    void Start()
    {
        CoinHolder = GameObject.Find("GameManager").GetComponent<GameManager>().CootsGameObject.GetComponent<Movement>().CoinManagerHolder;
    }

    public void SetFirst()
    {
        rrNum = Random.Range(0, Skeletons.Length);
        SetNewFish();
    }

    public void SetNewFish()
    {
        SkeletonHolder.sprite = Skeletons[rrNum];
    }

    void Update()
    {
        if (transform.position.y <= -5.8F)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            transform.rotation = Quaternion.identity;
            transform.position = new Vector2(-4, 5);
        }
    }

    public void OpenChest()
    {
        //Opened
        SkeletonHolder.enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<BoxCollider2D>().enabled = false;
        Invoke("Opened", 2);
    }
    void Opened()
    {
        GetComponent<SpriteRenderer>().sprite = openedSprite;
        goldpart.Play();
        GetComponent<AudioSource>().Play();
        GetMoney();
        Destroy(this.gameObject, 4);
    }

    void GetMoney()
    {
        //AddMoney
        GameObject.Find("GameManager").GetComponent<GameManager>().CamShake(0.5F);
        GetComponent<AudioSource>().Play();
        float CashToAdd = Random.Range(1, 2345);
        CoinHolder.GetComponent<CoinManager>().AddCash((int)CashToAdd, 1);

        string thingString = CashToAdd.ToString("0000");

        chars = thingString.ToCharArray();
        a = int.Parse(chars[0].ToString());
        b = int.Parse(chars[1].ToString());
        c = int.Parse(chars[2].ToString());
        d = int.Parse(chars[3].ToString());

        coinImagesADD[0].sprite = coinSprites[a];
        coinImagesADD[1].sprite = coinSprites[b];
        coinImagesADD[2].sprite = coinSprites[c];
        coinImagesADD[3].sprite = coinSprites[d];

        print(CashToAdd + " CHEST MONEY");
        GetComponent<Animator>().SetTrigger("GetMoney");
    }
}
