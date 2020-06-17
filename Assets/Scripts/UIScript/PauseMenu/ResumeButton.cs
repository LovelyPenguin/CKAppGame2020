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
        pausePopup = GetComponentInParent<GameObject>();
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
