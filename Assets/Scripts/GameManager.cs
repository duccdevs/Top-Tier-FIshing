using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool CanMove = true;
    public bool CanMoveMouse = true;
    public int BridgeCount = 0;
    public GameObject BridgeObj;
    public GameObject CootsGameObject;
    public GameObject[] SPawnymans;

    void Start()
    {
        //Application.targetFrameRate = 60;
        //Screen.SetResolution(1280, 720, false);
    }

    public void CamShake(float shakeTime)
    {
        Camera.main.GetComponent<Animator>().SetBool("Shake", true);
        Invoke("EndShake", shakeTime);
    }
    void EndShake()
    {
        Camera.main.GetComponent<Animator>().SetBool("Shake", false);
    }

    public void AddBridge()
    {
        BridgeCount++;
        Instantiate(BridgeObj, new Vector2(-3.25F + BridgeCount, -2), Quaternion.identity);
    }

    void OnDisable()
    {
        PlayerPrefs.DeleteAll();
    }
}
