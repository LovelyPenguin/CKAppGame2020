using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneControl : MonoBehaviour
{
    public GameObject BackGround;
    public GameObject[] CutScenes = new GameObject[7];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CutSceneStart(int ID)
    {
        BackGround.SetActive(true);
        CutScenes[ID].SetActive(true);
    }

    public void CutSceneEnd()
    {
        BackGround.SetActive(false);
    }
}
