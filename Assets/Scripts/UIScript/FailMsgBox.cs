using System.Collections;
using System.Collections.Generic;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.UI;

public class FailMsgBox : MonoBehaviour
{
    public Sprite PopupBackground;
    public Sprite popupBtnImage;
    public Font PopupFont;

    private GameObject FailPopup;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Create()
    {
        Create("돈이 부족해요!");
    }
    public void Create(string context)
    {
        if(FailPopup)
        {
            DestroyImmediate(FailPopup);
        }
        FailPopup = new GameObject("Fail");
        FailPopup.transform.SetParent(GameObject.Find("Canvas").transform);
        FailPopup.layer = 5;
        FailPopup.transform.localScale = Vector3.one;
        Image PopupBG = FailPopup.AddComponent<Image>();
        PopupBG.sprite = PopupBackground;
        FailPopup.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        FailPopup.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 300);

        GameObject txt = new GameObject("Text");
        txt.transform.SetParent(FailPopup.transform);
        txt.transform.localScale = Vector3.one;
        Text ctext = txt.AddComponent<Text>();
        ctext.text = context;
        ctext.font = PopupFont;
        ctext.fontSize = 30;
        ctext.color = Color.black;
        ctext.alignment = TextAnchor.MiddleCenter;
        txt.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        txt.GetComponent<RectTransform>().sizeDelta = new Vector2(350, 250);

        GameObject btn = new GameObject("Btn");
        btn.transform.SetParent(FailPopup.transform);
        btn.transform.localScale = Vector3.one;
        Button button = btn.AddComponent<Button>();
        UnityEventTools.AddObjectPersistentListener(button.onClick, DestroySelf, FailPopup);
        Image btnImg = btn.AddComponent<Image>();
        btnImg.sprite = popupBtnImage;
        btn.GetComponent<RectTransform>().anchoredPosition = new Vector2(80, -110);
        btn.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 50);
    }

    private void DestroySelf(GameObject go)
    {
        DestroyImmediate(go);
    }
}
