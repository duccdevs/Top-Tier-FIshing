using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Photon.MonoBehaviour
{
    public bool CanMove = true;
    public bool CanMoveMouse = true;
    public int BridgeCount = 0;
    public GameObject BridgeObj;
    public GameObject CootsGameObject;
    public GameObject[] SPawnymans;
    public GameObject PauseScreen;
    public Text pauseTimerText;
    float quitToMenu = 1.0F;
    bool paused = false;

    void Start()
    {
        Application.targetFrameRate = 60;
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            if (!paused)
            {
                quitToMenu = 1.0F;
                //CanMove = true;
                //CanMoveMouse = true;
            }

            if (paused)
            {
                PauseScreen.SetActive(true);
            }
            else
            {
                PauseScreen.SetActive(false);
            }
        }

        if (paused)
        {
            //CanMove = false;
            //CanMoveMouse = false;
        }

        //holdtoquit
        if (Input.GetKey(KeyCode.Escape) && quitToMenu != 0.0F)
        {
            quitToMenu -= Time.deltaTime;
            pauseTimerText.text = quitToMenu.ToString("F1");
        }

        if (quitToMenu <= 0.0F)
        {
            quitToMenu = 0.0F;
            pauseTimerText.text = "disconnected - returning to menu";
            PhotonNetwork.Disconnect();
        }
    }

    void OnDisconnectedFromPhoton()
    {
        SceneManager.LoadScene(0);
        print("we go now");
    }
}
