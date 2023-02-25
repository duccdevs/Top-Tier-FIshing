using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class Movement : Photon.MonoBehaviour
{
    private GameObject GM;
    public float speed = 1.0F;
    float input;
    public bool flipped = false;

    [Header("NormalStuff")]
    public GameObject GhostBridge;
    public GameObject FishyHoldy;
    public GameObject GhostPart;
    public GameObject SpriteHolder;

    [Header("PowerUps")]
    public float GoldMultiply = 1;
    public float WeightMultiply = 1;
    public float SpawnerBuffMulti = 1;
    public float ShinyDiv = 1;

    [Header("Misc")]
    public SpriteRenderer Hand;
    public Sprite FishSprite;
    public int fishiddd = 0;
    public int HoldingId = 0;
    public int skinsId = 0;
    public GameObject foshHolder;
    public bool issShiny;
    public GameObject FishText;
    public GameObject CosmeticHolder;
    public GameObject Canvass;
    public GameObject CoinManagerHolder;
    public GameObject BdigeMan;
    public GameObject FrogCoin;
    public GameObject GrabTextObj;
    float fishDelay = 0.0F;
    GameObject CookStation;
    public bool holdingFosh = false;
    public bool holdingItem = false;
    public GameObject[] AllFish;
    public GameObject[] AllFishSkeleton;
    public float BridgeCount = 0.0F;

    [Header("Checker")]
    public bool InsideFish = false;
    public bool canCook = false;
    public bool frogged = false;
    public bool isSpecialFish = false;

    [Header("FinalStats")]
    public float FoodQuality = 0;
    public int WhatFish1 = 0;
    public int WhatFish2 = 0;
    public int WhatFish3 = 0;

    [Header("Other")]
    private Image[] WeightImages;
    private Sprite[] WeightSprites;
    private Image[] CoinImages;
    private Sprite[] CoinSprites;
    public ParticleSystem chomppies;
    GameObject foshinst;
    public GameObject[] Spawners;
    public GameObject MealItem;
    public Animator EmojiStuff;
    public bool HoldtoEat = false;
    public bool realBridge = false;
    public bool realBucket = false;
    public bool realSkin = false;
    public bool realFlower = false;
    public bool realMeal = false;
    public bool realBeef = false;
    public bool HOLDINGMEAL = false;
    public float EatTime = 1.2F;
    public bool eaten = false;
    public bool CanFish = true;
    public bool CanFish2 = true;

    [Header("Audio")]
    private AudioSource audioSource;
    public AudioClip GrabEmpty;
    public AudioClip GrabFish;
    public AudioClip GrabShiny;
    public AudioClip ItemOn;

    void Start()
    {
        GM = GameObject.Find("GameManager");
        audioSource = GetComponent<AudioSource>();

        if (photonView.isMine)
        {
            NormalMode();
        }
    }
    void NormalMode()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        FishyHoldy.GetComponent<SpriteRenderer>().color = Color.white;
        GhostBridge.SetActive(false);
        GhostPart.SetActive(false);
        GetComponent<SpriteMask>().enabled = false;
        SpriteHolder.SetActive(false);
        SpriteRenderer sprrrrrr = Hand.GetComponent<SpriteRenderer>();
        sprrrrrr.maskInteraction = SpriteMaskInteraction.None;
        FishyHoldy.GetComponent<Animator>().SetBool("Ghost", false);
    }

    void CaughtFish()
    {
        //Logic
        if (CanFish && foshHolder != null && !realSkin && !realBridge && !realBucket && !realFlower && !realMeal && !realBeef)
        {
            CanFish = false;
            isSpecialFish = foshHolder.GetComponent<IAmFish>().SpecialFish;
            FishyHoldy.GetComponent<SpriteRenderer>().color = Color.white;
            Hand.transform.GetChild(0).GetComponent<WhatFosh>().SetFish(fishiddd);
            GameObject textInstance = Instantiate(FishText, new Vector2(Mathf.Clamp(transform.position.x + 0.75F, -4, 3), transform.position.y + 0.25F), Quaternion.identity);
            textInstance.transform.parent = Canvass.transform;
            textInstance.transform.localScale = Vector2.one;
            if (foshHolder.GetComponent<IAmFish>().isShiny)
            {
                Hand.transform.GetChild(0).GetComponent<Animator>().SetBool("Shiny", true);
                audioSource.PlayOneShot(GrabShiny);
            }
            else
            {
                Hand.transform.GetChild(0).GetComponent<Animator>().SetBool("Shiny", false);
                audioSource.PlayOneShot(GrabFish);
            }

            //GetNumbers
            issShiny = foshHolder.GetComponent<IAmFish>().isShiny;
        
            float FoshWeight = foshHolder.GetComponent<IAmFish>().Weight * WeightMultiply;
            float CoinAmount = Mathf.Clamp(foshHolder.GetComponent<IAmFish>().CoinWorth, 1, 99);
            WeightImages = textInstance.GetComponent<TextManager>().WeightImagess;
            WeightSprites = textInstance.GetComponent<TextManager>().WeightSpritess;
            CoinImages = textInstance.GetComponent<TextManager>().CoinImagess;
            CoinSprites = textInstance.GetComponent<TextManager>().CoinSpritess;

            //SetText
            float fvalue = Mathf.Clamp((int)FoshWeight / 10, 1, Mathf.Infinity);
            WeightImages[0].sprite = WeightSprites[(int) fvalue];
            float fvalue2 = Mathf.Clamp((int)FoshWeight % 10, 1, Mathf.Infinity);
            WeightImages[1].sprite = WeightSprites[(int)fvalue2];
            float LastNumber = Mathf.Clamp((FoshWeight % 1) * 10, 1, Mathf.Infinity);
            WeightImages[2].sprite = WeightSprites[(int)LastNumber];

            CoinImages[0].sprite = CoinSprites[(int)CoinAmount / 10];
            CoinImages[1].sprite = CoinSprites[(int)CoinAmount % 10];

            if (FoshWeight < 10)
            {
                WeightImages[0].enabled = false;
            }
            if (CoinAmount < 10)
            {
                CoinImages[0].enabled = false;
            }

            CoinManagerHolder.GetComponent<CoinManager>().AddCash((int)CoinAmount, GoldMultiply);

            //Kill
            Destroy(textInstance, 4.5F);
            holdingFosh = true;
            HoldingId = fishiddd;
            GameObject[] thing = GameObject.FindGameObjectsWithTag("tierhold");
            foreach (GameObject things in thing)
            {
                things.GetComponent<ShowFish>().AddFish(HoldingId);
            }
            Destroy(foshHolder);
        }
    }

    public void BucketCaughtFish(Vector2 pos, GameObject foshholster)
    {
        if (CanFish2)
        {
            //Logic
            CanFish2 = false;
            foshHolder = foshholster;
            GameObject textInstance = Instantiate(FishText, new Vector2(pos.x -0.5F, pos.y + 1.25F), Quaternion.identity);
            textInstance.transform.parent = Canvass.transform;
            textInstance.transform.localScale = Vector2.one;
            if (foshHolder.GetComponent<IAmFish>().isShiny)
            {
                audioSource.PlayOneShot(GrabShiny);
            }
            else
            {
                audioSource.PlayOneShot(GrabFish);
            }

            //GetNumbers
            issShiny = foshHolder.GetComponent<IAmFish>().isShiny;
            float FoshWeight = foshHolder.GetComponent<IAmFish>().Weight;
            float CoinAmount = Mathf.Clamp(foshHolder.GetComponent<IAmFish>().CoinWorth, 1, 99);
            WeightImages = textInstance.GetComponent<TextManager>().WeightImagess;
            WeightSprites = textInstance.GetComponent<TextManager>().WeightSpritess;
            CoinImages = textInstance.GetComponent<TextManager>().CoinImagess;
            CoinSprites = textInstance.GetComponent<TextManager>().CoinSpritess;

            //SetText
            float fvalue = Mathf.Clamp((int)FoshWeight / 10, 1, Mathf.Infinity);
            if (WeightImages[0] != null)
            {
                WeightImages[0].sprite = WeightSprites[(int) fvalue];
            }
            float fvalue2 = Mathf.Clamp((int)FoshWeight % 10, 1, Mathf.Infinity);
            WeightImages[1].sprite = WeightSprites[(int)fvalue2];
            float LastNumber = Mathf.Clamp((FoshWeight % 1) * 10, 1, Mathf.Infinity);
            WeightImages[2].sprite = WeightSprites[(int)LastNumber];

            CoinImages[0].sprite = CoinSprites[(int)CoinAmount / 10];
            CoinImages[1].sprite = CoinSprites[(int)CoinAmount % 10];

            if (FoshWeight < 10)
            {
                WeightImages[0].enabled = false;
            }
            if (CoinAmount < 10)
            {
                CoinImages[0].enabled = false;
            }

            CoinManagerHolder.GetComponent<CoinManager>().AddCash((int)CoinAmount, GoldMultiply);

            //Kill
            Destroy(textInstance, 4.5F);
            Destroy(foshHolder);
            Invoke("Returner", 0.05F);
        }
    }
    void Returner()
    {
        CanFish2 = true;
    }

    void SelectsSkin(int skinIDDD)
    {
        if (skinIDDD == 0)
        {
            //froghat
            GoldMultiply += 0.25F;
        }
        if (skinIDDD == 1)
        {
            //Crocs
            WeightMultiply += 0.25F;
        }
        if (skinIDDD == 2)
        {
            //Halo
            SpawnerBuffMulti += 1.25F;
        }
        if (skinIDDD == 3)
        {
            //Rainbow
            ShinyDiv += 0.25F;
        }
        //
        skinsId = skinIDDD;
        CosmeticHolder.GetComponent<CosmeticMan>().Skins[skinsId].GetComponent<SpriteRenderer>().enabled = true;
        if (skinIDDD == 3)
        {
            CosmeticHolder.GetComponent<CosmeticMan>().Skins[skinsId].transform.GetChild(0).GetComponent<ParticleSystem>().loop = true;
            CosmeticHolder.GetComponent<CosmeticMan>().Skins[skinsId].transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        }
        if (skinIDDD == 2)
        {
            CosmeticHolder.GetComponent<CosmeticMan>().Skins[skinsId].GetComponent<Light2D>().enabled = true;
        }
        //ITEM TEXT
        audioSource.PlayOneShot(ItemOn);
        GM.GetComponent<GameManager>().CamShake(0.2F);
        Destroy(foshHolder);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Cooking" && !holdingItem && !isSpecialFish)
        {
            canCook = true;
            CookStation = collider.gameObject;
        }
        else
        if(collider.tag == "Frog" && !holdingItem)
        {
            frogged = true;
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.tag == "Cooking")
        {
            canCook = false;
            CookStation = null;
        }
        if(collider.tag == "Frog")
        {
            frogged = false;
        }
    }

    void SerialKillerOnTheLoosePleaseHelpMeThisIsNotAJoke()
    {
        EmojiStuff.SetInteger("Emoji", -1);
    }

    void Update()
    {
        if (GM.GetComponent<GameManager>().CanMove)
        {
            input = Input.GetAxisRaw("Horizontal");
        }
        else
        {
            input = 0.0F;
        }
        if (input != 0)
        {
            GetComponent<Animator>().SetBool("Walk", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("Walk", false);
        }
        Hand.GetComponent<SpriteRenderer>().enabled = !GetComponent<Animator>().GetBool("Walk");

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EmojiStuff.SetInteger("Emoji", 0);
            Invoke("SerialKillerOnTheLoosePleaseHelpMeThisIsNotAJoke", 0.9F);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EmojiStuff.SetInteger("Emoji", 1);
            Invoke("SerialKillerOnTheLoosePleaseHelpMeThisIsNotAJoke", 0.9F);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EmojiStuff.SetInteger("Emoji", 2);
            Invoke("SerialKillerOnTheLoosePleaseHelpMeThisIsNotAJoke", 0.9F);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            EmojiStuff.SetInteger("Emoji", 3);
            Invoke("SerialKillerOnTheLoosePleaseHelpMeThisIsNotAJoke", 0.9F);
        }

        if (Input.GetMouseButtonDown(0) && frogged && !holdingFosh && BdigeMan.GetComponent<BridgeManager>().CanBuy)
        {
            //Buy bridge
            if (CoinManagerHolder.GetComponent<CoinManager>().currentCoins >= FrogCoin.GetComponent<FrogCoinMan>().currentCoins)
            {
                //WE CAN BUY
                CoinManagerHolder.GetComponent<CoinManager>().currentCoins -= (int)FrogCoin.GetComponent<FrogCoinMan>().currentCoins;
                BdigeMan.GetComponent<BridgeManager>().Bought();
                CoinManagerHolder.GetComponent<CoinManager>().SetNewCount();
                frogged = false;
            }
            else
            {
                //WE CANNOT BUY SADGIES
                BdigeMan.GetComponent<BridgeManager>().NOMEGGIES();
            }
            return;
        }

        if (Input.GetMouseButtonDown(0) && fishDelay == 0.0F && !holdingFosh)
        {
            //Catch
            if (realBridge)
            {
                //Grab Bridge
                HoldItem(8);
                BdigeMan.GetComponent<BridgeManager>().thisCol.enabled = true;
                return;
            }
            else
            if (realBucket)
            {
                //Grab Bucket
                HoldItem(9);
                BdigeMan.GetComponent<BridgeManager>().thisCol.enabled = true;
                return;
            }
            else
            if (realSkin)
            {
                //SelectSkin
                SelectsSkin(skinsId);
                BdigeMan.GetComponent<BridgeManager>().thisCol.enabled = true;
                return;
            }
            else
            if (realFlower)
            {
                //GrabFlower
                HoldItem(10);
                BdigeMan.GetComponent<BridgeManager>().thisCol.enabled = true;
                return;
            }
            else
            if (realMeal)
            {
                //GrabMeal
                HoldItem(11);
                HOLDINGMEAL = true;
                BdigeMan.GetComponent<BridgeManager>().thisCol.enabled = true;
                return;
            }
            else
            if (realBeef)
            {
                //GrabBeef
                HoldItem(12);
                BdigeMan.GetComponent<BridgeManager>().thisCol.enabled = true;
                return;
            }

            GM.GetComponent<GameManager>().CamShake(0.1F);
            GameObject grabInst = Instantiate(GrabTextObj, new Vector2(transform.position.x, Random.Range(-0.7F, -0.6F)), Quaternion.identity);
            grabInst.transform.SetParent(Canvass.transform);
            grabInst.transform.localScale = Vector2.one;
            Destroy(grabInst, 0.85F);
            fishDelay += 0.2F;

            if (InsideFish && !realBridge && !realBucket && !realSkin && !realFlower && !realMeal && !realBeef)
            {
                //Caught Fish
                CaughtFish();
                return;
            }
            else
            {
                audioSource.PlayOneShot(GrabEmpty);
            }

            return;
        }

        if (Input.GetMouseButtonDown(0) && fishDelay == 0.0F && holdingFosh && input == 0 && !eaten)
        {
            if (!holdingItem)
            {
                //Eat Held Fish
                HoldtoEat = true;
                EatTime = 1.2F;
                fishDelay = 1.2F;
                chomppies.loop = true;
                chomppies.Play();
                Hand.GetComponent<MouseArm>().Wiggle();
                GM.GetComponent<GameManager>().CanMove = false;
                GM.GetComponent<GameManager>().CanMoveMouse = false;
            }
            else if (HOLDINGMEAL)
            {
                //Eat Held Fish
                HoldtoEat = true;
                EatTime = 1.2F;
                fishDelay = 1.2F;
                chomppies.loop = true;
                chomppies.Play();
                Hand.GetComponent<MouseArm>().Wiggle();
                GM.GetComponent<GameManager>().CanMove = false;
                GM.GetComponent<GameManager>().CanMoveMouse = false;
            }
        }

        if (HoldtoEat)
        {
            //EATING
            EatTime -= Time.deltaTime;
            if (EatTime <= 0.0F)
            {
                if (!HOLDINGMEAL)
                {
                    //DONE EATING
                    eaten = true;
                    Hand.transform.GetChild(0).GetComponent<WhatFosh>().SetFishSkel(fishiddd);
                    if (!isSpecialFish)
                    {
                        Spawners[fishiddd].GetComponent<Spawner>().Buff(issShiny);
                    }
                    else
                    {
                        print("LionFish Buff");
                        foreach (GameObject go in Spawners)
                        {
                            go.GetComponent<Spawner>().Buff(true);
                        }

                        isSpecialFish = false;
                    }
                    HoldtoEat = false;
                    GM.GetComponent<GameManager>().CanMove = true;
                    GM.GetComponent<GameManager>().CanMoveMouse = true;
                    chomppies.loop = false;
                    EatTime = 1.2F;
                    return;
                }
                else
                {
                    //WE ATE SUSHI YAY
                    eaten = true;
                    APPLYSTATS();
                    print("ATE ONLY SUSHI");
                    HoldtoEat = false;
                    GM.GetComponent<GameManager>().CanMove = true;
                    GM.GetComponent<GameManager>().CanMoveMouse = true;
                    chomppies.loop = false;
                    holdingFosh = false;
                    holdingItem = false;
                    HOLDINGMEAL = false;
                    eaten = false;
                    Hand.transform.GetChild(0).GetComponent<WhatFosh>().ReleaseFish();
                    EatTime = 1.2F;
                    Destroy(foshHolder);
                    return;
                }
            }
        }
        
        if (Input.GetMouseButtonUp(0) && HoldtoEat)
        {
            //let go of eat
            fishDelay = 0.1F;
            HoldtoEat = false;
            chomppies.loop = false;
            GM.GetComponent<GameManager>().CanMove = true;
            GM.GetComponent<GameManager>().CanMoveMouse = true;
        }

        if (Input.GetMouseButtonDown(1) && holdingFosh && !HoldtoEat)
        {
            //RELEASE
            if (!canCook)
            {
                if (!eaten)
                {
                    foshinst = Instantiate(AllFish[HoldingId], Hand.transform.GetChild(0).transform.position, Quaternion.identity);
                    if (HoldingId == 11)
                    {
                        //Meal
                        foshinst.GetComponent<MealManager>().FoodQuality = FoodQuality;
                        foshinst.GetComponent<MealManager>().WhatFish1 = WhatFish1;
                        foshinst.GetComponent<MealManager>().WhatFish2 = WhatFish2;
                        foshinst.GetComponent<MealManager>().WhatFish3 = WhatFish3;
                        HOLDINGMEAL = false;
                    }
                }
                else
                {
                    if (HoldingId != 11)
                    {
                        foshinst = Instantiate(AllFishSkeleton[HoldingId], Hand.transform.GetChild(0).transform.position, Quaternion.identity);
                    }
                }
                if (!holdingItem)
                {
                    foshinst.GetComponent<IAmFish>().Released();
                }
                holdingItem = false;
                eaten = false;
            }
            else
            {
                if (!eaten && !isSpecialFish)
                {
                    //what fish + if shiny
                    CookStation.GetComponent<CookManager>().AddFish(HoldingId, issShiny);
                }
            }

            realBucket = false;
            realBridge = false;
            realFlower = false;
            realMeal = false;
            realSkin = false;
            realBeef = false;

            CanFish = true;

            if (!eaten)
            {
                fishDelay += 0.2F;
                holdingFosh = false;
                Hand.transform.GetChild(0).GetComponent<WhatFosh>().ReleaseFish();
            }
        }

        if (fishDelay > 0.0F)
        {
            fishDelay -= Time.deltaTime;
        }
        else
        {
            fishDelay = 0.0F;
        }

        if (input < -0.1F)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            Hand.transform.position = new Vector2(transform.position.x -0.296F, Hand.transform.position.y);
            flipped = true;
            CosmeticHolder.GetComponent<CosmeticMan>().FlipSkins(flipped);
        }

        if (input > 0.1F)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            Hand.transform.position = new Vector2(transform.position.x + 0.296F, Hand.transform.position.y);
            flipped = false;
            CosmeticHolder.GetComponent<CosmeticMan>().FlipSkins(flipped);
        }

        Vector3 tmpPos = transform.position;
        if (BridgeCount <= 7)
        {
            tmpPos.x = Mathf.Clamp(tmpPos.x, -12F, -3.25F + BridgeCount);
        }
        else
        {
            tmpPos.x = Mathf.Clamp(tmpPos.x, -12F, 9.75F);
        }
        transform.position = tmpPos;

        GetComponent<Rigidbody2D>().AddForce(Vector2.right * input * speed * Time.deltaTime, ForceMode2D.Impulse);
    }

    void APPLYSTATS()
    {
        Spawners[WhatFish1].GetComponent<Spawner>().CookBuff(FoodQuality);
        Spawners[WhatFish2].GetComponent<Spawner>().CookBuff(FoodQuality);
        Spawners[WhatFish3].GetComponent<Spawner>().CookBuff(FoodQuality);
    }

    public void SetMealStats()
    {
        if (foshHolder != null)
        {
            FoodQuality = 0;
            //
            FoodQuality = foshHolder.GetComponent<MealManager>().FoodQuality;
            WhatFish1 = foshHolder.GetComponent<MealManager>().WhatFish1;
            WhatFish2 = foshHolder.GetComponent<MealManager>().WhatFish2;
            WhatFish3 = foshHolder.GetComponent<MealManager>().WhatFish3;
        }
    }

    void HoldItem(int itemID)
    {
        //Logic
        fishDelay = 0.2F;
        Hand.transform.GetChild(0).GetComponent<WhatFosh>().SetFish(itemID);
        Hand.transform.GetChild(0).GetComponent<Animator>().SetBool("Shiny", false);
        audioSource.PlayOneShot(GrabFish);

        //Kill
        holdingFosh = true;
        holdingItem = true;
        canCook = false;
        HoldingId = itemID;
        Destroy(foshHolder);
    }
}
