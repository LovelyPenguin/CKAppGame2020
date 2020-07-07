using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetMenuActive : MonoBehaviour
{
    public Image[] img = new Image[2];
    [SerializeField]
    private bool initColor = false;
    // Start is called before the first frame update
    void Start()
    {
        if (initColor)
        {
            for (int i = 0; i < 2; i++)
            {
                img[i].color = new Color(0.5f, 0.5f, 0.5f);
            }
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MenuActivate()
    {
        for (int i = 0; i < 2; i++)
        {
            img[i].color = new Color(1f, 1f, 1f);
        }
        gameObject.SetActive(true);
    }

    public void MenuDeactivate()
    {
        for (int i = 0; i < 2; i++)
        {
            img[i].color = new Color(0.5f, 0.5f, 0.5f);
        }
        gameObject.SetActive(false);
    }
}
