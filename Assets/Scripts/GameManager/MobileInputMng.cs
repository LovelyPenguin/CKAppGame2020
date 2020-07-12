using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInputMng : MonoBehaviour
{
    public float timer = 0.1f;
    private bool key;
    private float saveTimerValue;
    // Start is called before the first frame update
    void Start()
    {
        key = false;
        saveTimerValue = timer;
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
                    Debug.Log("Two Application Quit");
                    GameMng.Instance.GetComponent<GameQuitAndPauseEvent>().ForceSave();
                    Application.Quit();
                }
            }
            else
            {
                key = false;
                timer = saveTimerValue;
            }
        }
        if (Input.GetKeyUp(KeyCode.Escape) && !key)
        {
            key = true;
            Debug.Log("One");
        }
    }
}
