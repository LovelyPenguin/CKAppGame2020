using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DustGenerator : MonoBehaviour
{
    public GameObject dust1;
    public GameObject dust2;
    public GameObject dust3;

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
            int randNum = Random.Range(0, 3);
            switch (randNum)
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
            float xPos = Random.Range(0.0f, 9.0f);
            float zPos = Random.Range(0.0f, 9.0f);
            item.transform.position = new Vector3(xPos, 1, zPos);
        }
        Debug.Log("Dust generated");
    }
}
