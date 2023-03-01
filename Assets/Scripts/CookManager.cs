using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookManager : MonoBehaviour
{
    public GameObject MealObj;

    public int FishAmount = 0;
    public Sprite[] AllFish;
    public GameObject Fish1;
    public GameObject Fish2;
    public GameObject Fish3;

    public Image numText;
    public Sprite[] nums;

    public bool CanDoCook = true;

    public Animator fireAnim;

    public ParticleSystem[] things;
    public ParticleSystem CookSmoke;

    [Header("FinalStats")]
    public float FoodQuality = 0;
    public int WhatFish1 = 0;
    public int WhatFish2 = 0;
    public int WhatFish3 = 0;

    public void AddFish(int WhatFish, bool Shiny)
    {
        if (CanDoCook)
        {
            FishAmount++;
            fireAnim.SetInteger("int", FishAmount);
            numText.sprite = nums[FishAmount];
            if (FishAmount == 0)
            {
                FoodQuality = 0;
                WhatFish1 = 0;
                WhatFish2 = 0;
                WhatFish3 = 0;
                //no fish
                things[0].loop = true;
                things[0].Play();

                things[1].loop = false;
                things[2].loop = false;
                things[3].loop = false;

                Fish1.GetComponent<SpriteRenderer>().enabled = false;
                Fish2.GetComponent<SpriteRenderer>().enabled = false;
                Fish3.GetComponent<SpriteRenderer>().enabled = false;
                return;
            }

            if (FishAmount == 1)
            {
                FoodQuality += (1 + WhatFish) * 2;
                WhatFish1 = WhatFish;
                if (Shiny)
                {
                    FoodQuality += (1 + WhatFish) * 3;
                }
                //uno fish
                things[0].loop = false;
                things[1].loop = true;
                things[1].Play();
                things[2].loop = true;
                things[2].Play();
                things[3].loop = false;

                fireAnim.GetComponent<Animator>().speed = 0.5F;

                if (Shiny)
                {
                    Fish1.GetComponent<Animator>().SetBool("Shiny", true);
                }
                else
                {
                    Fish1.GetComponent<Animator>().SetBool("Shiny", false);
                }

                Fish1.GetComponent<SpriteRenderer>().enabled = true;
                Fish1.GetComponent<SpriteRenderer>().sprite = AllFish[WhatFish];
                return;
            }
            if (FishAmount == 2)
            {
                FoodQuality += (1 + WhatFish) * 2;
                WhatFish2 = WhatFish;
                if (Shiny)
                {
                    FoodQuality += (1 + WhatFish) * 3;
                }
                //uno mas
                things[3].loop = true;
                things[3].Play();

                fireAnim.GetComponent<Animator>().speed = 1F;

                if (Shiny)
                {
                    Fish2.GetComponent<Animator>().SetBool("Shiny", true);
                }
                else
                {
                    Fish2.GetComponent<Animator>().SetBool("Shiny", false);
                }

                Fish2.GetComponent<SpriteRenderer>().enabled = true;
                Fish2.GetComponent<SpriteRenderer>().sprite = AllFish[WhatFish];
                return;
            }
            if (FishAmount == 3)
            {
                FoodQuality += (1 + WhatFish) * 2;
                WhatFish3 = WhatFish;
                if (Shiny)
                {
                    FoodQuality += (1 + WhatFish) * 3;
                }
                //all 3
                print("COOKING FISH NOW " + FoodQuality);
                CookSmoke.loop = true;
                CookSmoke.Play();
                //play jingle here
                GetComponent<Animator>().SetBool("Cook", true);
                Invoke("ReadyFish", 3);
                GetComponent<AudioSource>().Play();
                CanDoCook = false;
                things[0].loop = true;
                things[0].Play();

                fireAnim.GetComponent<Animator>().speed = 1.5F;

                if (Shiny)
                {
                    Fish3.GetComponent<Animator>().SetBool("Shiny", true);
                }
                else
                {
                    Fish3.GetComponent<Animator>().SetBool("Shiny", false);
                }

                Fish3.GetComponent<SpriteRenderer>().enabled = true;
                Fish3.GetComponent<SpriteRenderer>().sprite = AllFish[WhatFish];
                return;
            }
        }
    }

    void ReadyFish()
    {
        CanDoCook = true;
        GetComponent<Animator>().SetBool("Cook", false);
        CookSmoke.loop = false;
        //no fish
        things[0].loop = true;
        things[0].Play();

        things[1].loop = false;
        things[2].loop = false;
        things[3].loop = false;

        Fish1.GetComponent<SpriteRenderer>().enabled = false;
        Fish2.GetComponent<SpriteRenderer>().enabled = false;
        Fish3.GetComponent<SpriteRenderer>().enabled = false;

        things[4].Play();
        GameObject mealinst = Instantiate(MealObj, transform.position, Quaternion.identity);
        mealinst.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 4, ForceMode2D.Impulse);
        mealinst.GetComponent<MealManager>().FoodQuality = FoodQuality;
        mealinst.GetComponent<MealManager>().WhatFish1 = WhatFish1;
        mealinst.GetComponent<MealManager>().WhatFish2 = WhatFish2;
        mealinst.GetComponent<MealManager>().WhatFish3 = WhatFish3;

        FoodQuality = 0;
        WhatFish1 = 0;
        WhatFish2 = 0;
        WhatFish3 = 0;

        FishAmount = 0;
        numText.sprite = nums[FishAmount];
    }
}
