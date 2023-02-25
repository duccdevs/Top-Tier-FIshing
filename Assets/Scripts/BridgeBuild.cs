using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeBuild : MonoBehaviour
{
    private GameObject Coots;
    private GameObject GM;
    public GameObject Sploosher;

    void Start()
    {
        Coots = GameObject.Find("Coot");
        GM = GameObject.Find("GameManager");
    }

    void Update()
    {
        if (transform.position.y <= -2.8F)
        {
            //BUILD MORE BRIDGE LOL
            GM.GetComponent<GameManager>().CamShake(2);
            GM.GetComponent<GameManager>().CootsGameObject.GetComponent<Movement>().BridgeCount += 1;
            GM.GetComponent<GameManager>().AddBridge();
            GameObject splobj = Instantiate(Sploosher, new Vector2(transform.position.x, transform.position.y + 0.2F), Quaternion.EulerAngles(-90, 0, 0));
            Destroy(splobj.gameObject, 1);
            Destroy(gameObject);
        }
    }
}
