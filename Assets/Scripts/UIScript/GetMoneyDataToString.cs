using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetMoneyDataToString : MonoBehaviour
{
    private Text myText;

    // Start is called before the first frame update
    void Start()
    {
        myText = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        myText.text = string.Format("{0:#,###}", GameMng.Instance.money);
    }
}
