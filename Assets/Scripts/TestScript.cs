using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestScript : MonoBehaviour
{
    public RectTransform myRect;
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.SendMessage("Func");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TestFunction()
    {
        Debug.Log(gameObject.transform.parent.name);
    }

    void Func()
    {
        Debug.Log("Hello!");
    }
}
