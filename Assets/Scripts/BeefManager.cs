using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeefManager : MonoBehaviour
{
    private GameObject Coots;
    private GameObject GM;
    public GameObject Sploosher;

    public GameObject FoshObj;
    public float FoshSpeed = 10.0F;

    void Start()
    {
        Coots = GameObject.Find("Coot");
        GM = GameObject.Find("GameManager");
    }

    void Update()
    {
        if (transform.position.y <= -2.8F)
        {
            //ACTIVATE BIG FISH TIME
            GM.GetComponent<GameManager>().CamShake(1);
            GameObject splobj = Instantiate(Sploosher, new Vector2(transform.position.x, transform.position.y + 0.2F), Quaternion.EulerAngles(-90, 0, 0));
            
            //SpawnFish
            GameObject foshint = Instantiate(FoshObj, transform.position, Quaternion.identity);
            foshint.GetComponent<Rigidbody2D>().AddForce(Vector2.up * FoshSpeed, ForceMode2D.Impulse);
            foshint.GetComponent<Rigidbody2D>().AddForce(Vector2.right * Random.Range(-0.02F, 0.02F) * FoshSpeed, ForceMode2D.Impulse);

            print("Spawned LION FISH");
            
            Destroy(splobj.gameObject, 1);
            Destroy(gameObject);
        }
    }
}
