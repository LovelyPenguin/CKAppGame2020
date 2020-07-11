using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneControl : MonoBehaviour
{
    public GameObject BackGround;
    public GameObject[] CutScenes = new GameObject[7];
    private GameObject CurCutScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public ref GameObject CutSceneStart(int ID)
    {
        if (BackGround)
            BackGround.SetActive(true);
        CutScenes[ID].SetActive(true);
        CurCutScene = CutScenes[ID];
        return ref CutScenes[ID];
    }

    public void CutSceneEnd()
    {
        if (BackGround)
            BackGround.SetActive(false);
        CurCutScene = null;
    }
}
