using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject FoshObj;
    private GameObject GM;
    public float FoshSpeed = 10.0F;
    public Vector2 NewFish;
    public Vector2 randoHeight;
    //
    bool isBuff = false;
    bool CookBuffBool = false;
    public bool isShiny = false;
    public int CoinMultiMan = 1;
    private GameObject myThing;
    public float timerthing = 0;
    //
    float spawnRate = 1.0F;
    float spawnTimer = 0.0F;
    //
    float fullsize = 1.125F;
    float MaxShit;

    void Start()
    {
        GM = GameObject.Find("GameManager");
        spawnRate = Random.Range(NewFish.x, NewFish.y);
        myThing = transform.GetChild(1).gameObject;
        timerthing = 0;
    }

    void Update()
    {
        if (GM.GetComponent<GameManager>().CootsGameObject != null)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnRate)
            {
                spawnTimer = 0.0F;
                //SpawnFish
                SpawnFishNow();
            }

            //BuffSize
            if (isBuff || CookBuffBool)
            {
                if (timerthing > 0.0F)
                {
                    //SCALE AND SHTI
                    timerthing -= Time.deltaTime;
                    float newSize = Mathf.Clamp(timerthing / MaxShit, 0.0F, 1.0F) * fullsize;
                    Vector3 newscale = new Vector3(0.3F, newSize, 1);
                    myThing.transform.localScale = newscale;
                }
                else
                {
                    isBuff = false;
                    CookBuffBool = false;
                    isShiny = false;
                    timerthing = 0.0F;
                    Vector3 newscale = new Vector3(0.3F, 0, 1);
                    myThing.transform.localScale = newscale;
                }
            }
        }
    }

    void SpawnFishNow()
    {
        //SpawnFish
        GameObject foshint = Instantiate(FoshObj, transform.position, Quaternion.identity);

        //Send it
        float rr = Random.Range(-2.65F, 2.65F);
        foshint.GetComponent<Rigidbody2D>().AddForce(Vector2.up * (FoshSpeed + Random.Range(randoHeight.x, randoHeight.y)), ForceMode2D.Impulse);
        foshint.GetComponent<Rigidbody2D>().AddForce(Vector2.right * rr, ForceMode2D.Impulse);
        foshint.GetComponent<IAmFish>().CoinMultiMan = CoinMultiMan;

        if (!isBuff && !CookBuffBool)
        {
            spawnRate = Random.Range(NewFish.x, NewFish.y);
        }
    }

    public void Buff(bool shiner)
    {
        if (!CookBuffBool)
        {
            isBuff = true;
            if (shiner)
            {
                //ShinyBuff
                isShiny = true;
                timerthing = 8.0F;
                spawnRate = Random.Range(NewFish.x / 10, NewFish.y / 10);
                MaxShit = 8.0F;
                return;
            }
            
            if (!shiner)
            {
                if (isShiny)
                {
                    //ShinyBuff
                    isShiny = true;
                    timerthing += 0.75F;
                    spawnRate = Random.Range(NewFish.x / 10, NewFish.y / 10);
                    MaxShit = timerthing;
                    return;
                }
                else
                {
                    //NormalBuff
                    isShiny = false;
                    timerthing = 5.0F;
                    spawnRate = Random.Range(NewFish.x / 5, NewFish.y / 5);
                    MaxShit = 5.0F;
                    return;
                }
            }
        }
    }
    public void CookBuff(float cookTime)
    {
        timerthing = cookTime;
        spawnRate = Random.Range(NewFish.x / 15, NewFish.y / 15);
        MaxShit = cookTime;
        print(cookTime + " " + spawnRate);
        CookBuffBool = true;
        return;
    }
}
