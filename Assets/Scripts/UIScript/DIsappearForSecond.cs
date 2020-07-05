using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DIsappearForSecond : MonoBehaviour
{
    public float clock;

    private void OnEnable()
    {
        StartCoroutine(Deactive());
        Debug.Log("Start");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Deactive()
    {
        yield return new WaitForSeconds(clock);
        gameObject.SetActive(false);
    }
}
