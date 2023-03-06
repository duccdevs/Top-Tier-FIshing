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
        //0 = bridge || 1 = cosmetic || 2 = item
        if (BubbleID == 0)
        {
            //ItemHolder.GetComponent<SpriteRenderer>().sprite = Sprites[0];
        }
        if (BubbleID == 1)
        {
            RandomItem = Random.Range(0, Sprites.Length);
            ItemHolder.GetComponent<SpriteRenderer>().sprite = Sprites[RandomItem];
        }
        if (BubbleID == 2)
        {
            RandomItem++;
            if (RandomItem == 4)
            {
                RandomItem = 0;
            }
            ItemHolder.GetComponent<SpriteRenderer>().sprite = Sprites[RandomItem];
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
    }
}
