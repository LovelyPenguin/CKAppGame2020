using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class ProfileSetting : MonoBehaviour
{
    public Text nameTag;
    public Image icon;

    public string[] name;
    public Sprite[] sprite;

    private int index = -1;

    // Start is called before the first frame update
    void Start()
    {
        index = PlayerPrefs.GetInt("PROFILEINDEX");
        if (index != -1)
        {
            SetProfile(index);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick(int index)
    {
        PlayerPrefs.SetInt("PROFILEINDEX", index);
        SetProfile(index);
    }

    private void SetProfile(int index)
    {
        nameTag.text = name[index];
        icon.sprite = sprite[index];
    }
}
