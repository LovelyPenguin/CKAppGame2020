using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerItemInfo : MonoBehaviour
{
    public GameObject host;
    public int itemIndex;
    public GameObject standardPos;

    private bool click = false;
    private bool goToTarget = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ActivateClick());
        standardPos = GameObject.Find("StandardPos");
    }

    // Update is called once per frame
    void Update()
    {
        if (goToTarget)
        {
            transform.position = Vector3.Lerp(transform.position, Camera.main.ScreenToWorldPoint(standardPos.transform.position), Time.deltaTime * 5);
            //transform.Rotate(0, 0, 500 * Time.deltaTime);
            Debug.Log(Vector3.Distance(transform.position, Camera.main.ScreenToWorldPoint(standardPos.transform.position)));
            if (Vector3.Distance(transform.position, Camera.main.ScreenToWorldPoint(standardPos.transform.position)) < 1f)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("Click!");
        if (click)
        {
            host.GetComponent<Customer>().itemActive[itemIndex] = true;
            host.GetComponent<Customer>().itemGen[itemIndex] = false;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            goToTarget = true;
            //Destroy(gameObject);
        }
    }

    IEnumerator ActivateClick()
    {
        yield return new WaitForSeconds(0.5f);
        click = true;
    }
}
