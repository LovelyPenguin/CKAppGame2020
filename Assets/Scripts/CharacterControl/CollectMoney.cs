using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectMoney : MonoBehaviour
{
    public AudioClip clip;
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = transform.GetComponentInParent<AudioSource>();
        source.clip = clip;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("CLICK");
        gameObject.GetComponentInParent<DrinkTransfer>().Detect(5000);
        gameObject.GetComponentInParent<Customer>().ReturnHome(true);
        gameObject.GetComponentInParent<Customer>().ItemDrop();
        gameObject.GetComponentInParent<Customer>().AddStamp();
        gameObject.SetActive(false);
        source.Play();
    }
}
