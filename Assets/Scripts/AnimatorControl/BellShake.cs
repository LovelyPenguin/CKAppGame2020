using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellShake : MonoBehaviour
{
    private Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        ani.SetTrigger("Bell");
    }
}
