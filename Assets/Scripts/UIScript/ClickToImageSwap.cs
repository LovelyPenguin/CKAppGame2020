using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickToImageSwap : MonoBehaviour
{
    public Sprite sprite;
    public Button[] buttons;
    private int previousIndex;

    private Image img;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].onClick.AddListener(OnClick);
        }
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        img.sprite = sprite;
    }
}
