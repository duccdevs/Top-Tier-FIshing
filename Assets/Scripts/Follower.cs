using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public Transform CootsObj;
    Vector2 velocity;

    void Update()
    {
        transform.position = Vector2.Lerp(transform.position, CootsObj.position, Time.deltaTime * 10);
    }
}
