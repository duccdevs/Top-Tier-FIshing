using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Animator anim;
    public AudioClip[] audios;
    public GameObject[] Screens;

    void Start()
    {
        Application.targetFrameRate = 60;
        Screen.SetResolution(1280, 720, false);
    }

    public void StartGame()
    {
        anim.SetTrigger("Start");
        GetComponent<AudioSource>().PlayOneShot(audios[0]);
        Invoke("StartedGame", 1.0F);
    }

    public void QuitsGame()
    {
        anim.SetTrigger("Start");
        GetComponent<AudioSource>().PlayOneShot(audios[1]);
        Invoke("QuitGame", 1.0F);
    }

    public void Settings()
    {
        GetComponent<AudioSource>().PlayOneShot(audios[2]);
        Screens[0].SetActive(false);
        Screens[1].SetActive(true);
    }
    public void Back()
    {
        GetComponent<AudioSource>().PlayOneShot(audios[2]);
        Screens[0].SetActive(true);
        Screens[1].SetActive(false);
    }
    public void SetReso1()
    {
        Screen.SetResolution(1920, 1080, Screen.fullScreen);
    }
    public void SetReso2()
    {
        Screen.SetResolution(1280, 720, Screen.fullScreen);
    }

    void StartedGame()
    {
        SceneManager.LoadScene(1);
    }
    void QuitGame()
    {
        Application.Quit();
    }
}
