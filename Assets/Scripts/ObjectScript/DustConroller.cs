using System.Collections;
using System.Drawing;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class DustConroller : MonoBehaviour
{
    UnityEngine.Vector3 originScale;
    float size;
    float Size
    {
        get { return size; }
        set { if (value > 1.0f) size = 1f;
            else if (value < 0.0f) size = 0f;
            else size = value; }
    }
    public float SizeIncrease = 0.1f;
    public float SizeDecrease = 0.2f;

    public int maxReward = 50;
    public int minReward = 30;

    float initXscale;
    float initYscale;

    IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        originScale = transform.localScale;
        transform.rotation = Camera.main.transform.rotation;
        transform.localScale = new UnityEngine.Vector3(0, 0, 1);

        Size = 1.0f;

        coroutine = Generate();
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = originScale * Size;
    }

    private void OnMouseDown()
    {
        Size -= SizeDecrease;
        if (Size <= 0.6f)
        {
            Destroy(gameObject);
            GameObject.Find("GameManager").GetComponent<GameMng>().money += (Random.Range(minReward, maxReward) * 10);
        }
    }

    IEnumerator Generate()
    {
        float time = 0f;

        while (time <= 1)
        {
            transform.localScale = UnityEngine.Vector3.Lerp(Vector3.zero, originScale, time += Time.deltaTime);

            yield return null;
        }

        coroutine = IncreaseSize();
        StartCoroutine(coroutine);
    }

    IEnumerator IncreaseSize()
    {
        while (true)
        {
            Size += SizeIncrease * Time.deltaTime;

            yield return null;
        }
    }
}
