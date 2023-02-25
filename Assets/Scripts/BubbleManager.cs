using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    public int BubbleID = 0;
    public GameObject FrogHolder;
    public ParticleSystem bursts;
    public GameObject ItemHolder;
    public SpriteRenderer BubbleSprite;
    public Sprite[] Sprites;

    private Vector2 origPos;
    public int RandomItem;

    void Start()
    {
        origPos = transform.position;
        SetStartings();
    }

    void SetStartings()
    {
        if (BubbleID == 1 || BubbleID == 2)
        {
            RandomItem = Random.Range(0, Sprites.Length);
            ItemHolder.GetComponent<SpriteRenderer>().sprite = Sprites[RandomItem];
        }
        else
        {
            //ItemHolder.GetComponent<SpriteRenderer>().sprite = Sprites[0];
        }
        BubbleSprite.enabled = true;
        ItemHolder.SetActive(true);
        GetComponent<Animator>().SetTrigger("New");
        transform.position = new Vector2(origPos.x + Random.Range(-0.1F, 0.1F), origPos.y + Random.Range(-0.1F, 0.1F));
        GetComponent<Animator>().speed = Random.Range(0.2F, 1.2F);
    }

    public void PopBubble()
    {
        BubbleSprite.enabled = false;
        bursts.Play();
        GetComponent<AudioSource>().Play();
        Invoke("SetStartings", 2);
        Invoke("ItemToo", 0.8F);
    }
    void ItemToo()
    {
        ItemHolder.SetActive(false);
    //    ItemHolder.GetComponent<SpriteRenderer>().sprite = Sprites[RandomItem];
        //if (BubbleID == 2)
        //{
        //    FrogHolder.GetComponent<BridgeManager>().ItemINT = RandomItem;
        //}
        //if (BubbleID == 1)
        //{
        //    FrogHolder.GetComponent<BridgeManager>().SkinINT = RandomItem;
        //}
    }
}
