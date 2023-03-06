using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MealManager : MonoBehaviour
{
    [Header("FinalStats")]
    public float FoodQuality = 0;
    public int WhatFish1 = 0;
    public int WhatFish2 = 0;
    public int WhatFish3 = 0;

    public Sprite[] foshSprites;
    public SpriteRenderer[] allSprites;

    public void SetSprites()
    {
        allSprites[0].sprite = foshSprites[WhatFish1];
        allSprites[1].sprite = foshSprites[WhatFish2];
        allSprites[2].sprite = foshSprites[WhatFish3];
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
}
