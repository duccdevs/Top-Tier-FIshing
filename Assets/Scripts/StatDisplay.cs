using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatDisplay : MonoBehaviour
{
    public Text statsText;
    private GameObject CharHolder;
    public float GoldAmount;
    public float WeightAmount;
    public float BuffAmount;
    public float ShinyAmount;

    void Update()
    {
        if (CharHolder == null)
        {
            CharHolder = GameObject.Find("GameManager").GetComponent<GameManager>().CootsGameObject;
        }
        else
        {
            GoldAmount = CharHolder.GetComponent<Movement>().GoldMultiply - 1;
            WeightAmount = CharHolder.GetComponent<Movement>().WeightMultiply - 1;
            BuffAmount = CharHolder.GetComponent<Movement>().SpawnerBuffMulti - 1;
            ShinyAmount = CharHolder.GetComponent<Movement>().ShinyDiv - 1;

            statsText.text =
            "+" + GoldAmount.ToString() + " GOLD/FISH" + "\n" +
            "+" + WeightAmount.ToString() + " WEIGHT/FISH" + "\n" +
            "+" + BuffAmount.ToString() + " BUFF TIME" + "\n" +
            "+" + ShinyAmount.ToString() + " SHINY CHANCE";
        }
    }
}
