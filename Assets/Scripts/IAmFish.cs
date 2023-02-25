using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAmFish : MonoBehaviour
{
    private GameObject Cootss;
    bool notDead = true;
    public GameObject Splooshed;
    private GameObject Shinyd;
    public int fishID;
    public float angle;
    public bool SpecialFish = false;
    bool rel = false;
    Vector2 origPos;
    public AudioClip gone;
    public AudioClip shinyrare;

    [Header("Stats")]
    public float Weight = 1.0F;
    public Vector2 WeightModifier;
    public int CoinWorth;
    public int ShinyCoinWorth = 1;
    public int CoinMultiMan = 1;
    public bool isShiny = false;

    public void Released()
    {
        rel = true;
        GetComponent<Animator>().enabled = false;
        float rr = Random.Range(0.15F, 0.45F);
        GetComponent<Rigidbody2D>().AddForce(Vector2.right * rr, ForceMode2D.Impulse);
    }

    void Start()
    {
        origPos = transform.position;
        Shinyd = transform.GetChild(1).gameObject;
        Cootss = GameObject.Find("GameManager").GetComponent<GameManager>().CootsGameObject;

        if (!rel)
        {
            GameObject splobj = Instantiate(Splooshed, new Vector2(transform.position.x, transform.position.y + 0.75F), Quaternion.EulerAngles(-90, 0, 0));
            Destroy(splobj.gameObject, 1);
            if (GetComponent<BoxCollider2D>() != null)
            {
                GetComponent<BoxCollider2D>().enabled = true;
            }
            if (!SpecialFish)
            {
                SetRarity();
            }
        }
    }

    void SetRarity()
    {
        Weight = Weight + Random.Range(WeightModifier.x, WeightModifier.y);
        CoinWorth = (CoinWorth * CoinMultiMan) + (int)Weight / 5;

        //Shiny
        float Shiny = Random.Range(0, 25 / Cootss.GetComponent<Movement>().ShinyDiv);
        if (Shiny <= 1.0F)
        {
            isShiny = true;
            print("Shiny");
            transform.GetChild(2).gameObject.SetActive(true);
            GetComponent<Animator>().SetBool("Shiny", true);
            GetComponent<AudioSource>().PlayOneShot(shinyrare);
            Shinyd.SetActive(true);
            Weight = Weight + Random.Range(WeightModifier.x * 1.25F, WeightModifier.y * 1.25F);
            ShinyCoinWorth = (ShinyCoinWorth * CoinMultiMan) + (int)Weight / 5;
        }
    }

    void Update()
    {
        if (notDead)
        {
            Vector2 moveDirecxtion = gameObject.GetComponent<Rigidbody2D>().velocity;
            angle = Mathf.Atan2(moveDirecxtion.y, moveDirecxtion.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            if (transform.position.y <= -2.8F && angle < 0)
            {
                //IamDead
                notDead = false;
                GetComponent<AudioSource>().PlayOneShot(gone);
                Killme();
            }

            if (GetComponent<Rigidbody2D>().velocity.x < 0)
            {
                GetComponent<SpriteRenderer>().flipY = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipY = false;
            }
        }

        if (transform.position.x > origPos.x + 0.375F)
        {
            //wall
            FlipFish(0);
            return;
        }
        if (transform.position.x < origPos.x - 0.375F)
        {
            //wall
            FlipFish(1);
            return;
        }
    }

    void FlipFish(int side)
    {
        if (side == 0)
        {
            transform.position = new Vector2(origPos.x + 0.35F, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(origPos.x - 0.35F, transform.position.y);
        }
        GetComponent<Rigidbody2D>().velocity = new Vector2((-GetComponent<Rigidbody2D>().velocity.x / 2), GetComponent<Rigidbody2D>().velocity.y);
    }

    void Killme()
    {
        GameObject splobj = Instantiate(Splooshed, new Vector2(transform.position.x, transform.position.y + 0.2F), Quaternion.EulerAngles(-90, 0, 0));
        Destroy(splobj.gameObject, 1);
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(gameObject, 0.8F);
    }
}
