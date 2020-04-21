using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSizeLimit : MonoBehaviour
{
    private Camera myCam;
    // Start is called before the first frame update
    void Start()
    {
        myCam = gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myCam.orthographicSize > 5f)
        {
            myCam.orthographicSize = Mathf.Lerp(myCam.orthographicSize, 5, Time.deltaTime * 3);
        }
    }
}
