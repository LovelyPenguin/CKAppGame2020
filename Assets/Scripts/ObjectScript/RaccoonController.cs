using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;

public class RaccoonController : MonoBehaviour
{
    public float stamina = 100;
    private float time;
    bool isOnDrag = false;
    bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.rotation = Camera.main.transform.rotation;
        time = 0.0f;
        SetRCActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        RCMove();
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        if (other.gameObject.name == "HealMap")
        {
            Debug.Log("Triggered!");
            this.stamina += 10;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);
    }

    private void OnMouseDown()
    {
        isOnDrag = true;
        Debug.Log("raccon clicked");
    }

    private void OnMouseUp()
    {
        isOnDrag = false;
        Debug.Log("raccon unclicked");
    }

    private void RCMove()
    {
        if(!isOnDrag && isActive)
        {
            transform.Translate(0, 0, 0);
        }
    }

    public bool GetIsDrag()
    {
        return isOnDrag;
    }

    public void SetRCActive(bool activity)
    {
        isActive = activity;
    }
}
