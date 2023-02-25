using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowe : MonoBehaviour
{
    public Transform Coots;
    public Vector2 CamLookHere;
    public Vector2 CamLookHereLeft;
    public Vector2 CamLookHereRight;
    public Vector2 CamLookHereRightLast;
    public float SmoothTime = 1.0F;
    Vector2 velocity;

    void Update()
    {
        if (Coots != null)
        {
            if (Coots.position.x < -4.75F)
            {
                //LeftSide
                CamLookHere = CamLookHereLeft;
                transform.position = Vector2.SmoothDamp(transform.position, CamLookHere, ref velocity, SmoothTime);
            }
            
            if (Coots.position.x > -4.75F && Coots.position.x < 4.88F)
            {
                //Middle
                CamLookHere = CamLookHereRight;
                transform.position = Vector2.SmoothDamp(transform.position, new Vector2(CamLookHere.x + (Coots.position.x + 8) / 10, 0), ref velocity, SmoothTime);
            }

            if (Coots.position.x > 4.88F)
            {
                //Right
                CamLookHere = CamLookHereRightLast;
                transform.position = Vector2.SmoothDamp(transform.position, CamLookHere, ref velocity, SmoothTime);
            }
        }
    }
}
