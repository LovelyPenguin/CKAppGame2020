using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectMoney : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("CLICK");
        gameObject.GetComponentInParent<DrinkTransfer>().Detect(1000);
        gameObject.GetComponentInParent<Customer>().ReturnHome(true);
        gameObject.SetActive(false);
    }
}
