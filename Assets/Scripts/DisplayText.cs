using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayText : MonoBehaviour
{
    public Text TitleText;
    public Text DescText;

    public void SetText(string TText, string DText)
    {
        TitleText.text = TText;
        DescText.text = DText;

        GetComponent<Animator>().SetTrigger("Textt");
    }
}
