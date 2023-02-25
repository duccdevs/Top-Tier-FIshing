using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowFish : MonoBehaviour
{
    float FishAmount = 0;
    public GameObject fishBG;
    public int FishIdAgain = 0;
    bool thingy = false;
    private List<GameObject> AllSpawned = new List<GameObject>();

    public void AddFish(int SHUTP)
    {
        if (FishAmount <= 14 && FishIdAgain == SHUTP)
        {
            if (SHUTP == FishIdAgain)
            {
                if (thingy)
                {
                    GameObject inst = Instantiate(fishBG, new Vector2 (transform.position.x - 0.225F, transform.position.y + (FishAmount / 4)), Quaternion.EulerAngles(0, 0, 45));
                    AllSpawned.Add(inst);
                    inst.transform.parent = transform.parent;
                }
                else
                {
                    GameObject inst = Instantiate(fishBG, new Vector2 (transform.position.x + 0.225F, transform.position.y + (FishAmount / 4)), Quaternion.EulerAngles(0, 0, 45));
                    AllSpawned.Add(inst);
                    inst.transform.parent = transform.parent;
                }
                thingy = !thingy;
                FishAmount += 1;
            }

            if (FishAmount == 14)
            {
                print("FILLED");
                for (int i = 0; i < AllSpawned.Count; i++)
                {
                    GameObject.Destroy(AllSpawned[i]);
                }
                AllSpawned.Clear();
                if (transform.parent.GetComponent<Spawner>().CoinMultiMan < 9)
                {
                    GetComponent<AudioSource>().Play();
                    transform.parent.GetComponent<Spawner>().CoinMultiMan += 1;
                }
                else
                {
                    transform.parent.GetComponent<Spawner>().CoinMultiMan = 9;
                }
                FishAmount = 0;
            }
        }
    }
}
