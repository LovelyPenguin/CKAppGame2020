using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyShow : MonoBehaviour
{
    GameMng Mng;

    // Start is called before the first frame update
    void Start()
    {
        Mng = GameObject.Find("GameManager").GetComponent<GameMng>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = Mng.money.ToString() + "원";
    }
}
