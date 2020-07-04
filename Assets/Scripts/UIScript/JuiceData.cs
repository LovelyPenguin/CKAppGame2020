using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JuiceData : MonoBehaviour
{
    public GameObject juice;
    public DrinkMng drinkMng;
    public Image iconImage;

    public bool isUnlock;
    [SerializeField]
    private int tabCountData;
    public int unlockCost;

    [Multiline(3)]
    public string profileText;
    [Multiline(3)]
    public string hashTag;

    public Text headName;
    public Text tagName;

    public GameObject unlockBtn;

    // Start is called before the first frame update
    void Start()
    {
        drinkMng = GameMng.Instance.gameObject.GetComponent<DrinkMng>();
        iconImage = gameObject.transform.GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Click()
    {
        Debug.Log(gameObject.name + " Click");
        drinkMng.SetMiniGameData(tabCountData, juice, ref isUnlock, gameObject);

        if (isUnlock)
        {
            unlockBtn.SetActive(false);
        }
        else
        {
            unlockBtn.SetActive(true);
        }

        headName.text = profileText;
        tagName.text = hashTag;
    }
}
