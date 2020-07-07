using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagement : MonoBehaviour
{
    public AudioClip[] clips;

    private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        int random = Random.Range(0, clips.Length - 1);
        audio.clip = clips[random];
        if (!audio.isPlaying)
        {
            audio.Play();
        }
    }
}
