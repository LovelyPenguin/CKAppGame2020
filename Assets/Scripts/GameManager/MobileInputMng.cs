using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInputMng : MonoBehaviour
{
    public float timer = 0.1f;
    private bool key;
    // Start is called before the first frame update
    void Start()
    {
        key = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (key)
        {
            timer -= Time.deltaTime;
            if (timer >= 0)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Debug.Log("Click2");
                    Application.Quit();
                }
            }
            else
            {
                key = false;
                timer = 0.2f;
            }
        }
        if (Input.GetKeyUp(KeyCode.Escape) && !key)
        {
            key = true;
            Debug.Log("Click1");
        }
    }
}
