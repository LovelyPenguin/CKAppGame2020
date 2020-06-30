using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Raccoon4EffectController : MonoBehaviour
{
    public GameObject[] EffectSprites = new GameObject[2];
    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject go in EffectSprites)
            go.SetActive(false);
        coroutine = Animate();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Play()
    {
        StartCoroutine(coroutine);
    }

    public void Stop()
    {
        StopCoroutine(coroutine);
        foreach (GameObject go in EffectSprites)
            go.SetActive(false);
    }

    private WaitForSeconds wait = new WaitForSeconds(1.0f);
    private int currentOnSprite = 0;
    private int currentOffSprite = 1;

    IEnumerator Animate()
    {
        while (true)
        {
            EffectSprites[currentOnSprite].SetActive(true);
            EffectSprites[currentOffSprite].SetActive(false);
            int temp = currentOffSprite;
            currentOffSprite = currentOnSprite;
            currentOnSprite = temp;
            yield return wait;
        }
    }
}
