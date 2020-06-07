using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerCounter : MonoBehaviour
{
    private Text counterText;
    // Start is called before the first frame update
    void Start()
    {
        counterText = gameObject.GetComponent<Text>();   
    }

    // Update is called once per frame
    void Update()
    {
        counterText.text = "들린 손님 : " + GameMng.Instance.customerCount.ToString();
    }
}
