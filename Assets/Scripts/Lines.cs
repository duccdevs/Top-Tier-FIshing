using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lines : MonoBehaviour
{
    public Transform obj1;
    public Transform obj2;

    void Update()
    {
        GetComponent<LineRenderer>().SetPosition(0, new Vector2(obj1.position.x, obj1.position.y - 0.2F));
        GetComponent<LineRenderer>().SetPosition(1, obj2.position);
    }
}
