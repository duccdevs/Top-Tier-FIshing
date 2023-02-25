using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerManager : MonoBehaviour
{
    private GameObject CoinHolder;
    public SpriteRenderer[] coinImagesADD;
    public Sprite[] coinSprites;

    char[] chars;
    int a;
    int b;

    void Start()
    {
        CoinHolder = GameObject.Find("GameManager").GetComponent<GameManager>().CootsGameObject.GetComponent<Movement>().CoinManagerHolder;
        Invoke("GetMoney", Random.Range(5, 10));
    }

    void GetMoney()
    {
        //AddMoney
        GameObject[] MogiAmount = GameObject.FindGameObjectsWithTag("Flower");
        float CashToAdd = Mathf.Clamp(5 * MogiAmount.Length, 1, 99);
        CoinHolder.GetComponent<CoinManager>().AddCash((int)CashToAdd, 1);

        string thingString = CashToAdd.ToString("00");

        chars = thingString.ToCharArray();
        a = int.Parse(chars[0].ToString());
        b = int.Parse(chars[1].ToString());

        coinImagesADD[0].sprite = coinSprites[a];
        coinImagesADD[1].sprite = coinSprites[b];

        print(CashToAdd + " MOGI MONEY");
        GetComponent<Animator>().SetTrigger("GetMoney");
        Invoke("GetMoney", Random.Range(5, 10));
    }

    void Update()
    {
        if (transform.position.y <= -2.8F)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            transform.rotation = Quaternion.identity;
            transform.position = new Vector2(-4, 5);
        }
    }
}
