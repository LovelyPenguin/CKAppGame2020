using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneControl : MonoBehaviour
{
    public GameObject BackGround;
    public GameObject[] CutScenes = new GameObject[7];
    public GameObject SkipBtn;
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
        BackGround.SetActive(true);
        CutScenes[ID].SetActive(true);
        SkipBtn.SetActive(true);
        CurCutScene = CutScenes[ID];
        return ref CutScenes[ID];
    }

    public void CutSceneEnd()
    {
        BackGround.SetActive(false);
        SkipBtn.SetActive(false);
        CurCutScene = null;
    }

    public void StopCutScene()
    {
        if(CurCutScene)
        {
            if (CurCutScene.name == "Intro")
                CurCutScene.GetComponent<IntroCutScene>().endCalls.Invoke();
            CurCutScene.SetActive(false);
            CutSceneEnd();
        }
    }
}
