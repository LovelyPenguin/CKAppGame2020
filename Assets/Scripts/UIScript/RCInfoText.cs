using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RCInfoText : MonoBehaviour
{
    public string[] RCInfos = new string[7];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateText(int index)
    {
        if (GameMng.Instance.gameObject.GetComponent<RaccoonMng>().GetRCUnlockData(index) || index == 6)
            GetComponent<Text>().text = RCInfos[index];
        else
            GetComponent<Text>().text = "???";
        Debug.Log("RCInfoUpdate");
    }
}
