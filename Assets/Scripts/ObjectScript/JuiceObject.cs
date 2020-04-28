using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JuiceObject : MonoBehaviour
{
    [SerializeField]
    private GameObject mainSprite;
    [SerializeField]
    private GameObject decoSprite;

    public bool isComplete = false;
    public float percentValue;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetDecoSprite(bool isVisible)
    {
        decoSprite.SetActive(isVisible);
    }

    public void ResetAllValue()
    {
        isComplete = true;
        percentValue = 0f;
        mainSprite.SetActive(true);
        decoSprite.SetActive(true);
    }

    public void FillMainSprite()
    {
        if (mainSprite.GetComponent<Image>().fillAmount >= 1)
        {
            mainSprite.GetComponent<Image>().fillAmount = 0;
        }

        mainSprite.GetComponent<Image>().fillAmount += 0.35f;
    }

    public void FillMainSprite(int value)
    {
        mainSprite.GetComponent<Image>().fillAmount = value;
    }
}
