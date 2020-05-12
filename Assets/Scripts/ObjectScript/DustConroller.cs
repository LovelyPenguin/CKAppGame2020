using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class DustConroller : MonoBehaviour
{
    UnityEngine.Vector3 originScale;
    float currentScale;

    // Start is called before the first frame update
    void Start()
    {
        originScale = transform.localScale;
        transform.rotation = Camera.main.transform.rotation;
        transform.localScale = new UnityEngine.Vector3(0, 0, 1);
        currentScale = 1.0f;
        StartCoroutine("Generate");
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnMouseDown()
    {
        if (currentScale > 0.6f)
            currentScale -= 0.2f;
        else
            currentScale = 0.0f;
        transform.localScale = originScale * currentScale;
        if (currentScale == 0.0f)
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        GameObject.Find("GameManager").GetComponent<GameMng>().money += 10000;
    }

    IEnumerator Generate()
    {
        for (float f = 0.0f; f <= 1.0f; f += Time.deltaTime)
        {
            transform.localScale = UnityEngine.Vector3.Lerp(new UnityEngine.Vector3(0,0,0), originScale, f);
            yield return null;
        }
    }
}
