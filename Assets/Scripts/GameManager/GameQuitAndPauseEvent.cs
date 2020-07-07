using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameQuitAndPauseEvent : MonoBehaviour
{
    public UnityEvent gameExit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnApplicationQuit()
    {
        Debug.Log("Application Quit");
        gameExit.Invoke();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Debug.Log("Application Pause");
            gameExit.Invoke();
        }
        else
        {
            Debug.Log("Application Rasume");
        }
    }
}
