using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketMan : MonoBehaviour
{
    private GameObject Coots;
    public GameObject Sploosh;
    LineRenderer ropeLine;

    void Start()
    {
        ropeLine = GetComponent<LineRenderer>();
    }
    void Update()
    {
        if (Coots == null)
        {
            Coots = GameObject.Find("GameManager").GetComponent<GameManager>().CootsGameObject;
        }
        if (transform.position.y <= -5.8F)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            transform.rotation = Quaternion.identity;
            transform.position = new Vector2(-4, 5);
        }

        ropeLine.SetPosition(0, transform.position);
        ropeLine.SetPosition(1, new Vector2(transform.position.x, -1.5F));

        if (transform.position.x > -3.275F)
        {
            ropeLine.enabled = true;

        }
        else
        {
            ropeLine.enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Fish" && !Coots.GetComponent<Movement>().isSpecialFish)
        {
            int rr = Random.Range(0, 2);
            if (collider.GetComponent<IAmFish>().angle < 0 && collider.transform.position.y > transform.position.y + 0.125F && rr == 0)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().CootsGameObject.GetComponent<Movement>().BucketCaughtFish(transform.position, collider.gameObject);
                collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                GameObject splooshinst = Instantiate(Sploosh, new Vector2(transform.position.x, transform.position.y + 0.25F), Quaternion.identity);
                Destroy(splooshinst, 1);
                GetComponent<Animator>().SetTrigger("Rotate");
            }
        }
    }
}