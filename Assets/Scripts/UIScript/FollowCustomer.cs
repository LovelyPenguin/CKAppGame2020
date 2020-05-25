using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCustomer : MonoBehaviour
{
    public GameObject customer;
    public float setYpos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPos = Camera.main.WorldToScreenPoint(customer.transform.position);
        gameObject.transform.position = new Vector2(newPos.x, newPos.y + setYpos);
    }
}
