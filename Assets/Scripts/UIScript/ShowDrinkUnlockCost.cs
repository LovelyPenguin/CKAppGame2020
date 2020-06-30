using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDrinkUnlockCost : MonoBehaviour
{
    public Text uiText;
    // Start is called before the first frame update
    void Start()
    {
        uiText.text = GameMng.Instance.GetComponent<DrinkMng>().mainObjTemp.GetComponent<JuiceData>().unlockCost.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        uiText.text = GameMng.Instance.GetComponent<DrinkMng>().mainObjTemp.GetComponent<JuiceData>().unlockCost.ToString();
    }
}
