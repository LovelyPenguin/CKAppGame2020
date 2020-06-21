using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetIconAndName : MonoBehaviour
{
    public string name;

    public Text nameTag;
    public Image icon;
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        nameTag.text = name;
        icon.sprite = GetComponent<Image>().sprite;
    }
}
