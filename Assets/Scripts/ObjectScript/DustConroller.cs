using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class DustConroller : MonoBehaviour
{
    UnityEngine.Vector3 originScale;

    // Start is called before the first frame update
    void Start()
    {
        originScale = transform.localScale;
        transform.rotation = Camera.main.transform.rotation;
        transform.localScale = new UnityEngine.Vector3(0, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = UnityEngine.Vector3.Lerp(transform.localScale, originScale, Time.deltaTime * 1);
    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
    }
}
