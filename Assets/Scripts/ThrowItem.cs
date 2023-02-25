using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowItem : MonoBehaviour
{
    public Transform Coots;
    public float ThrowPower = 1.0F;
    public float MaxThrowPower = 10.0F;
    bool throwBool = false;
    bool thrown = false;

    void Start()
    {
        GetComponent<Rigidbody2D>().isKinematic = true;
    }

    void Update()
    {
        if (GetComponent<Rigidbody2D>().isKinematic)
        {
            transform.position = Coots.position;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!thrown)
            {
                throwBool = true;
            }
        }
        
        if (Input.GetKeyUp(KeyCode.Space))
        {
            throwBool = false;
            ThrowItemNow();
        }

        if (throwBool)
        {
            if (ThrowPower <= MaxThrowPower)
            {
                ThrowPower += Time.deltaTime * 10;
            }
            else
            {
                ThrowPower = MaxThrowPower;
            }
        }
    }

    void ThrowItemNow()
    {
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<Rigidbody2D>().AddForce(Vector2.right * (ThrowPower * 25) * Time.deltaTime, ForceMode2D.Impulse);
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * (ThrowPower * 50) * Time.deltaTime, ForceMode2D.Impulse);
        ThrowPower = 1.0F;
        thrown = true;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Water")
        {
            print("SPLOOSH");
        }
    }
}
