using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerItemInfo : MonoBehaviour
{
    public GameObject host;
    public int itemIndex;

    private bool click = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ActivateClick());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("Click!");
        if (click)
        {
            host.GetComponent<Customer>().itemActive[itemIndex] = true;
            host.GetComponent<Customer>().itemGen[itemIndex] = false;
            Destroy(gameObject);
        }
    }

    IEnumerator ActivateClick()
    {
        yield return new WaitForSeconds(0.5f);
        click = true;
    }
}
