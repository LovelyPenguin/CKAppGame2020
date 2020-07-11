using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickToImageSwap : MonoBehaviour
{
    public Button[] buttons;
    public Sprite[] sprites;
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
        previousIndex = -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        Debug.Log("Image Swap");
        int index = Random.Range(0, sprites.Length);

        do
        {
            index = Random.Range(0, sprites.Length);
        } while (previousIndex == index);
        previousIndex = index;
        img.sprite = sprites[index];
    }
}
