using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseArm : Photon.MonoBehaviour
{
    public float rotationSpeed = 5f;
    public float maxRotationAngle;
    public float minRotationAngle;

    bool flipsdafuhsedugsyhg;
    float angle;

    private GameObject GM;
    int wiggleamount = 0;

    void Start()
    {
        GM = GameObject.Find("GameManager");

        if (photonView.isMine)
        {
            NormalMode();
        }

        flipsdafuhsedugsyhg = transform.parent.GetComponent<Movement>().flipped;
    }
    void NormalMode()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (transform.parent.GetComponent<Movement>().foshHolder == null && !transform.parent.GetComponent<Movement>().holdingFosh && !transform.parent.GetComponent<Movement>().holdingItem && transform.parent.GetComponent<Movement>().CanFish)
        {
            if (collider.tag == "Bridge")
            {
                transform.parent.GetComponent<Movement>().foshHolder = collider.gameObject;
                transform.parent.GetComponent<Movement>().realBridge = true;
            }
            if (collider.tag == "Bucket")
            {
                transform.parent.GetComponent<Movement>().foshHolder = collider.gameObject;
                transform.parent.GetComponent<Movement>().realBucket = true;
            }
            if (collider.tag == "Fish")
            {
                transform.parent.GetComponent<Movement>().fishiddd = collider.GetComponent<IAmFish>().fishID;
                transform.parent.GetComponent<Movement>().foshHolder = collider.gameObject;
                transform.parent.GetComponent<Movement>().InsideFish = true;
            }
            if (collider.tag == "Skin")
            {
                transform.parent.GetComponent<Movement>().skinsId = collider.GetComponent<ItemManager>().SkinID;
                transform.parent.GetComponent<Movement>().foshHolder = collider.gameObject;
                transform.parent.GetComponent<Movement>().realSkin = true;
            }
            if (collider.tag == "Flower")
            {
                transform.parent.GetComponent<Movement>().foshHolder = collider.gameObject;
                transform.parent.GetComponent<Movement>().realFlower = true;
            }
            if (collider.tag == "Meal")
            {
                transform.parent.GetComponent<Movement>().foshHolder = collider.gameObject;
                if (!transform.parent.GetComponent<Movement>().HoldtoEat)
                {
                    transform.parent.GetComponent<Movement>().SetMealStats();
                }
                transform.parent.GetComponent<Movement>().realMeal = true;
            }
            if (collider.tag == "Beef")
            {
                transform.parent.GetComponent<Movement>().foshHolder = collider.gameObject;
                transform.parent.GetComponent<Movement>().realBeef = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (transform.parent.GetComponent<Movement>().foshHolder != null)
        {
            if (collider.tag == "Bridge" && transform.parent.GetComponent<Movement>().foshHolder.gameObject.tag == "Bridge")
            {
                transform.parent.GetComponent<Movement>().realBridge = false;
                transform.parent.GetComponent<Movement>().foshHolder = null;
                transform.parent.GetComponent<Movement>().InsideFish = false;
            }
            else
            if (collider.tag == "Bucket" && transform.parent.GetComponent<Movement>().foshHolder.gameObject.tag == "Bucket")
            {
                transform.parent.GetComponent<Movement>().realBucket = false;
                transform.parent.GetComponent<Movement>().foshHolder = null;
                transform.parent.GetComponent<Movement>().InsideFish = false;
            }
            else
            if (collider.tag == "Fish" && transform.parent.GetComponent<Movement>().foshHolder.gameObject.tag == "Fish")
            {
                transform.parent.GetComponent<Movement>().foshHolder = null;
                transform.parent.GetComponent<Movement>().InsideFish = false;
            }
            else
            if (collider.tag == "Skin" && transform.parent.GetComponent<Movement>().foshHolder.gameObject.tag == "Skin")
            {
                transform.parent.GetComponent<Movement>().realSkin = false;
                transform.parent.GetComponent<Movement>().foshHolder = null;
                transform.parent.GetComponent<Movement>().InsideFish = false;
            }
            else
            if (collider.tag == "Flower" && transform.parent.GetComponent<Movement>().foshHolder.gameObject.tag == "Flower")
            {
                transform.parent.GetComponent<Movement>().realFlower = false;
                transform.parent.GetComponent<Movement>().foshHolder = null;
                transform.parent.GetComponent<Movement>().InsideFish = false;
            }
            else
            if (collider.tag == "Meal" && transform.parent.GetComponent<Movement>().foshHolder.gameObject.tag == "Meal")
            {
                transform.parent.GetComponent<Movement>().realMeal = false;
                transform.parent.GetComponent<Movement>().HOLDINGMEAL = false;
                transform.parent.GetComponent<Movement>().foshHolder = null;
                transform.parent.GetComponent<Movement>().InsideFish = false;
            }
            else
            if (collider.tag == "Beef" && transform.parent.GetComponent<Movement>().foshHolder.gameObject.tag == "Beef")
            {
                transform.parent.GetComponent<Movement>().realBeef = false;
                transform.parent.GetComponent<Movement>().foshHolder = null;
                transform.parent.GetComponent<Movement>().InsideFish = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (GM.GetComponent<GameManager>().CanMove)
        {
            if (transform.parent.GetComponent<Movement>().flipped)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos = Input.mousePosition;
                maxRotationAngle = 90;
                minRotationAngle = -50;
                GetComponent<SpriteRenderer>().flipX = true;
                GetComponent<BoxCollider2D>().offset = new Vector2(-0.26F, 0.04F);
                Vector3 targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, transform.position.z));
                Vector3 direction = targetPosition - transform.position;
                angle = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;

                transform.GetChild(0).transform.localPosition = new Vector2(-0.275F, 0);
            }
            else
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos = Input.mousePosition;
                maxRotationAngle = 50;
                minRotationAngle = -90;
                GetComponent<SpriteRenderer>().flipX = false;
                GetComponent<BoxCollider2D>().offset = new Vector2(0.26F, 0.04F);
                Vector3 targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, transform.position.z));
                Vector3 direction = targetPosition - transform.position;
                angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                transform.GetChild(0).transform.localPosition = new Vector2(0.275F, 0);
            }

            angle = Mathf.Clamp(angle, minRotationAngle, maxRotationAngle);
        }

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    public void Wiggle()
    {
        if (transform.parent.GetComponent<Movement>().flipped)
        {
            wiggleamount++;
            if (wiggleamount % 2 == 0)
            {
                angle = -50;
            }
            if (wiggleamount % 2 == 1)
            {
                angle = -90;
            }
            if (wiggleamount <= 4)
            {
                GetComponent<AudioSource>().Play();
                Invoke("Wiggle", 0.3F);
            }
            else
            {
                wiggleamount = 0;
            }
        }
        else
        {
            wiggleamount++;
            if (wiggleamount % 2 == 0)
            {
                angle = 50;
            }
            if (wiggleamount % 2 == 1)
            {
                angle = 90;
            }
            if (wiggleamount <= 4)
            {
                GetComponent<AudioSource>().Play();
                Invoke("Wiggle", 0.3F);
            }
            else
            {
                wiggleamount = 0;
            }
        }
    }
}
