using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BridgeManager : MonoBehaviour
{
    public int BridgeCost = 10;
    public GameObject[] BridgeObj;
    public GameObject[] ItemObj;
    public GameObject[] SkinObj;
    public GameObject MoneyThing;
    private AudioSource audi;
    private GameObject GM;
    private Animator anim;
    public Animator animCoins;
    public AudioClip[] sounds;
    public BoxCollider2D thisCol;

    public bool CanBuy = true;
    float nobuytime = 2.0F;
    int Times = 2;
    int rr = 0;

    public int ItemINT;
    public int SkinINT;

    public GameObject MouthOpen;
    public GameObject Tongue;
    public GameObject Inhale;
    public LineRenderer tongueLine;

    public GameObject BridgeBubble;
    public GameObject ItemBubble;
    public GameObject CosmeticBubble;
    public GameObject DisplayTextHolder;

    void Start()
    {
        GM = GameObject.Find("GameManager");
        audi = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();

        MoneyThing.GetComponent<FrogCoinMan>().SetNewCount();
    }

    void Update()
    {
        if (nobuytime > 0.0F)
        {
            nobuytime -= Time.deltaTime;
            CanBuy = false;
        }
        else
        {
            nobuytime = 0.0F;
            CanBuy = true;
        }
    }

    public void Bought()
    {
        //WOW NICE BRIDGE BUDDY
        ItemINT = ItemBubble.GetComponent<BubbleManager>().RandomItem;
        SkinINT = CosmeticBubble.GetComponent<BubbleManager>().RandomItem;
        GM.GetComponent<GameManager>().CamShake(0.8F);
        audi.PlayOneShot(sounds[0]);
        MouthOpen.SetActive(true);
        nobuytime = 10;
        thisCol.enabled = false;
        SetTongue();
    }
    void SetTongue()
    {
        if (rr == 0)
        {
            if (GM.GetComponent<GameManager>().CootsGameObject.GetComponent<Movement>().BridgeCount <= 7)
            {
                //Bridge
                Tongue.transform.position = BridgeBubble.transform.position;
                BridgeBubble.GetComponent<BubbleManager>().PopBubble();
                if (PlayerPrefs.GetInt("Bridge", 0) == 0)
                {
                    DisplayTextHolder.GetComponent<DisplayText>().SetText("BRIDGE", "drop into water to catch better fish");
                    PlayerPrefs.SetInt("Bridge", 1);
                }
            }
            else
            {
                rr += 1;
            }
        }
        if (rr == 1)
        {
            //Item
            Tongue.transform.position = ItemBubble.transform.position;
            ItemBubble.GetComponent<BubbleManager>().PopBubble();
            if (PlayerPrefs.GetInt("Item" + ItemINT.ToString(), 0) == 0)
            {
                if (ItemINT == 0)
                {
                    DisplayTextHolder.GetComponent<DisplayText>().SetText("BUCKET", "drop into water for a chance to catch fish");
                }
                if (ItemINT == 1)
                {
                    DisplayTextHolder.GetComponent<DisplayText>().SetText("MOGI", "this mogul friend will make you rich");
                }
                PlayerPrefs.SetInt("Item" + ItemINT.ToString(), 1);
            }
        }
        if (rr == 2)
        {
            //Cosmetic
            Tongue.transform.position = CosmeticBubble.transform.position;
            CosmeticBubble.GetComponent<BubbleManager>().PopBubble();
            if (PlayerPrefs.GetInt("Cosmetic" + SkinINT.ToString(), 0) == 0)
            {
                if (SkinINT == 0)
                {
                    DisplayTextHolder.GetComponent<DisplayText>().SetText("FROG HAT", "gain more gold per fish (stackable)");
                }
                if (SkinINT == 1)
                {
                    DisplayTextHolder.GetComponent<DisplayText>().SetText("LIGHTNING CROCS", "fish weight more than usual (stackable)");
                }
                if (SkinINT == 2)
                {
                    DisplayTextHolder.GetComponent<DisplayText>().SetText("HALO", "longer buff times when eating fish (stackable)");
                }
                if (SkinINT == 3)
                {
                    DisplayTextHolder.GetComponent<DisplayText>().SetText("RAINBOW", "higher chance of finding shiny fish (stackable)");
                }
                PlayerPrefs.SetInt("Cosmetic" + SkinINT, 1);
            }
        }

        rr++;

        Tongue.SetActive(true);

        tongueLine.SetPosition (0, MouthOpen.transform.position);
        tongueLine.SetPosition (1, Tongue.transform.position);
        Invoke("DoneBuying", 0.8F);
    }
    void DoneBuying()
    {
        MouthOpen.SetActive(false);
        Tongue.SetActive(false);
        Invoke("AboutToPuke", 0.6F);
    }
    void AboutToPuke()
    {
        GM.GetComponent<GameManager>().CamShake(0.2F);
        audi.PlayOneShot(sounds[0]);
        if (Times != 0)
        {
            Invoke("AboutToPuke", 0.9F);
            Inhale.SetActive(true);
            Invoke("CloseInhale", 0.5F);
        }
        else
        {
            SpitOutItem();
            Inhale.SetActive(false);
        }
        Times--;
    }
    void CloseInhale()
    {
        Inhale.SetActive(false);
    }
    void SpitOutItem()
    {
        Times = 3;
        MouthOpen.SetActive(true);

        Invoke("CloseMouth", 0.5F);
        if (rr == 1)
        {
            GameObject objInst = Instantiate(BridgeObj[0], transform.position, Quaternion.identity);
            objInst.GetComponent<Rigidbody2D>().AddForce(Vector2.up * Random.Range(1.75F, 2.5F), ForceMode2D.Impulse);
            objInst.GetComponent<Rigidbody2D>().AddForce(Vector2.right * Random.Range(2.75F, 3.5F), ForceMode2D.Impulse);
        }
        if (rr == 2)
        {
            GameObject objInst = Instantiate(ItemObj[ItemINT], transform.position, Quaternion.identity);
            objInst.GetComponent<Rigidbody2D>().AddForce(Vector2.up * Random.Range(1.75F, 2.5F), ForceMode2D.Impulse);
            objInst.GetComponent<Rigidbody2D>().AddForce(Vector2.right * Random.Range(2.75F, 3.5F), ForceMode2D.Impulse);
        }
        if (rr == 3)
        {
            GameObject objInst = Instantiate(SkinObj[SkinINT], transform.position, Quaternion.identity);
            objInst.GetComponent<Rigidbody2D>().AddForce(Vector2.up * Random.Range(1.75F, 2.5F), ForceMode2D.Impulse);
            objInst.GetComponent<Rigidbody2D>().AddForce(Vector2.right * Random.Range(2.75F, 3.5F), ForceMode2D.Impulse);
        }
        MoneyThing.GetComponent<FrogCoinMan>().SetNewCount();
        EnableBox();
        if (rr == 3)
        {
            rr = 0;
        }
    }
    void CloseMouth()
    {
        nobuytime = 0.4F;
        MouthOpen.SetActive(false);
    }
    public void EnableBox()
    {
        thisCol.enabled = true;
    }

    void SetsText(int thingy)
    {
        
    }

    public void NOMEGGIES()
    {
        //LMAO BROKE + L + RATIO
        print("nomoney");
        nobuytime = 0.4F;
        GM.GetComponent<GameManager>().CamShake(0.4F);
        animCoins.SetTrigger("Shake");
        audi.PlayOneShot(sounds[1]);
    }
}
