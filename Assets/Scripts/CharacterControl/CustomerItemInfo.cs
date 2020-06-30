using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerItemInfo : MonoBehaviour
{
    public GameObject host;
    public int itemIndex;

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
        Debug.Log("Click!");
        host.GetComponent<Customer>().itemActive[itemIndex] = true;
        Destroy(gameObject);
    }
}
