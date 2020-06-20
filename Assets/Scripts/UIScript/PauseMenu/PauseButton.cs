using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    [SerializeField]
    private GameObject pausePopup;
    // Start is called before the first frame update
    void Start()
    {
        //if (pausePopup == null)
        //{
        //    pausePopup = GameObject.Find("PauseMenu");
        //}
        //else
        //{
        //    Debug.LogError("Pause Menu Not found!");
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenPauseMenu()
    {
        pausePopup.SetActive(true);
    }
}
