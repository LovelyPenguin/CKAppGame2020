using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenCloseSpriteSwap : MonoBehaviour
{
    private Image img;
    public Sprite night;
    public Sprite daylight;

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();

        StartCoroutine(DelayStart());
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnClick()
    {
        if (GameMng.Instance.getOpenData)
        {
            img.sprite = daylight;
        }
        else
        {
            img.sprite = night;
        }
    }

    IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(0.5f);
        if (GameMng.Instance.getOpenData)
        {
            img.sprite = daylight;
        }
        else
        {
            img.sprite = night;
        }
    }
}
