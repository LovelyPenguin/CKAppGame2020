using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class renderingLayertest : MonoBehaviour
{
    public int SortingOrder;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Material>().renderQueue = SortingOrder;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
