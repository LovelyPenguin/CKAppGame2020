using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScreenTouchDetect : MonoBehaviour, IPointerClickHandler
{
    private TutorialManager TutoMng;

    void Awake()
    {
        TutoMng = GameObject.Find("Tutorial").GetComponent<TutorialManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        TutoMng.NextEvent();
    }
}
