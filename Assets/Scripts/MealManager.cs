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
