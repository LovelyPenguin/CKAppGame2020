using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurtainChange : MonoBehaviour
{
    public Texture OpenTex;
    public Texture CloseTex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CurtainOpenChange()
    {
        GetComponent<Renderer>().material.SetTexture("_MainTex", OpenTex);
    }

    public void CurtainCloseChange()
    {
        GetComponent<Renderer>().material.SetTexture("_MainTex", CloseTex);
    }
}
