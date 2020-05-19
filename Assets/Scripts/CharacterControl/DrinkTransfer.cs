using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkTransfer : MonoBehaviour
{
    public bool isTransfer = false;
    public GameObject particle;
    // Start is called before the first frame update
    void Start()
    {
        particle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Detect()
    {
        particle.transform.position = transform.position;
        particle.SetActive(true);
        isTransfer = true;
        StartCoroutine(Disable());
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(3f);
        particle.SetActive(false);
        isTransfer = false;
    }
}
