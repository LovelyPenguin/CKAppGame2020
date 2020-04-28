using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustConroller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Camera.main.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
    }
}
