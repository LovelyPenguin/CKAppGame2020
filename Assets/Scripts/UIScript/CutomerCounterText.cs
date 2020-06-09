using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutomerCounterText : MonoBehaviour
{
    private Text txt;

    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponent<Text>();
        txt.text = GameMng.Instance.customerCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = GameMng.Instance.customerCount.ToString();
    }
}
