using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMenuActive : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MenuActivate()
    {
        gameObject.SetActive(true);
    }

    public void MenuDeactivate()
    {
        gameObject.SetActive(false);
    }
}
