using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JuiceData : MonoBehaviour
{
    public GameObject juice;
    public DrinkMng drinkMng;

    [SerializeField]
    private bool isUnlock = false;
    [SerializeField]
    private int tabCountData;
    // Start is called before the first frame update
    void Start()
    {
        drinkMng = GameMng.Instance.gameObject.GetComponent<DrinkMng>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Click()
    {
        Debug.Log(gameObject.name + " Click");
        drinkMng.SetMiniGameData(tabCountData, juice);
    }
}
