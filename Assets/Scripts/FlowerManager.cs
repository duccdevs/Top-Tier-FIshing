using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerManager : MonoBehaviour
{
    private GameObject CoinHolder;

    void Start()
    {
        CoinHolder = GameObject.Find("GameManager").GetComponent<GameManager>().CootsGameObject.GetComponent<Movement>().CoinManagerHolder;
        Invoke("GetMoney", Random.Range(3, 10));
    }

    void GetMoney()
    {
        //AddMoney
        CoinHolder.GetComponent<CoinManager>().AddCash(10, 1);
        GetComponent<Animator>().SetTrigger("GetMoney");
        Invoke("GetMoney", Random.Range(5, 15));
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
