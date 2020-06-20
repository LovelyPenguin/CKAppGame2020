using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeButton : MonoBehaviour
{
    [SerializeField]
    private GameObject pausePopup;
    // Start is called before the first frame update
    void Start()
    {
        //if (pausePopup == null)
        //{
        //    Debug.Log("Initialize");
        //    pausePopup = GetComponentInParent<GameObject>();
        //}
        //else
        //{
        //    Debug.Log("File Ready");
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResumeGame()
    {
        pausePopup.SetActive(false);
    }
}
