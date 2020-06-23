using System.Collections;
using UnityEngine;

public class DustGenerator : MonoBehaviour
{
    public GameObject dust1;
    public GameObject dust2;
    public GameObject dust3;

    public Sprite Sweep1;
    public Sprite Sweep2;

    public float SweepTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Generate()
    {
        GameObject item;
        for (int i = 0; i < 3; i++)
        {
            switch (i)
            {
                case 0:
                    item = Instantiate(dust1) as GameObject;
                    break;

                case 1:
                    item = Instantiate(dust2) as GameObject;
                    break;

                default:
                    item = Instantiate(dust3) as GameObject;
                    break;
            }
            float xPos = Random.Range(0.0f, 8.0f);
            float zPos = Random.Range(0.0f, 8.0f);
            item.transform.position = new Vector3(xPos, 1, zPos);
            item.transform.SetParent(transform);
        }
        Debug.Log("Dust generated");
    }

    public void StartSweepAnim(Vector3 Pos)
    {
        StartCoroutine(SweepAnim(Pos));
    }
    IEnumerator SweepAnim(Vector3 Pos)
    {
        GameObject Sweep = new GameObject();
        Sweep.transform.position = Pos + Vector3.up * 1.5f;
        Sweep.transform.localScale = Vector3.one;
        Sweep.transform.eulerAngles = new Vector3(30.0f, 45.0f, 0.0f);
        SpriteRenderer SweepSprite = Sweep.AddComponent<SpriteRenderer>();

        SweepSprite.sprite = Sweep1;
        yield return new WaitForSeconds(SweepTime);
        SweepSprite.sprite = Sweep2;
        yield return new WaitForSeconds(SweepTime);

        DestroyImmediate(Sweep);
    }
}
