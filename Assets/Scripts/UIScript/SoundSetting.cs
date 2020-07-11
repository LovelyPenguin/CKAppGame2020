using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSetting : MonoBehaviour
{
    private bool sound = true;
    private AudioListener al;
    private Text childText;
    // Start is called before the first frame update
    void Start()
    {
        al = Camera.main.GetComponent<AudioListener>();
        childText = transform.GetChild(0).GetComponent<Text>();

        if (sound)
        {
            childText.text = "사운드 : 끄기";
        }
        else
        {
            childText.text = "사운드 : 켜기";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        sound = !sound;
        al.enabled = sound;

        if (sound)
        {
            childText.text = "사운드 : 끄기";
        }
        else
        {
            childText.text = "사운드 : 켜기";
        }
    }
}
