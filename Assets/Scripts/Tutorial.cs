using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public bool KeepGoing;
    float AnimationSpeed;
    private GameObject GM;

    void Start()
    {
        AnimationSpeed = GetComponent<Animator>().speed;
        GM = GameObject.Find("GameManager");
    }

    public void PauseAnimation()
    {
        GetComponent<Animator>().speed = 0.0F;
        KeepGoing = true;
    }

    public void StartGame()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Spawner");
        foreach (GameObject go in gos)
        {
            go.GetComponent<Spawner>().StartSpawning();
        }
        GM.GetComponent<GameManager>().CanMove = true;
    }

    void Update()
    {
        if (Input.anyKeyDown && KeepGoing)
        {
            KeepGoingNow();
            KeepGoing = false;
        }
    }

    void KeepGoingNow()
    {
        GetComponent<Animator>().speed = AnimationSpeed;
    }
}
