using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RCStaminaShowTEST : MonoBehaviour
{
    RaccoonController RC;

    // Start is called before the first frame update
    void Start()
    {
        RC = GameObject.Find("rc1").GetComponent<RaccoonController>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = "라쿤1 체력 : " + RC.stamina.ToString();
    }
}
