using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorStatMng : MonoBehaviour
{
    public GameObject CollisionGround2;
    public GameObject[] FloorChangeButtons = new GameObject[2];
    public GameObject MenuCollection;

    private bool floor2enable;
    public bool SecondFloorStat
    {
        get { return floor2enable; }
    }   
    public enum Floor { Floor1 = 0, Floor2 };
    public Floor CurFloor
    {
        get { return curFloor; }
    }
    private Floor curFloor;
    // Start is called before the first frame update
    void Start()
    {
        floor2enable = false;
        FloorChangeButtons[1].GetComponent<Button>().enabled = false;
        FloorChangeButtons[1].GetComponent<Image>().color = new Color(0.2f,0.2f,0.2f);
        FloorChangeButtons[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(-80, 0);
        //SetFloor1();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetFloor1()
    {
        curFloor = Floor.Floor1;

        Camera.main.GetComponent<FloorChange>().SetFloorFirst(22.85f);
        MenuCollection.GetComponent<ChangeCameraPosition>().SetInitializeYpos(22.85f);

        FloorChangeButtons[0].GetComponent<Image>().color = Color.white;
        FloorChangeButtons[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -121);
        if (floor2enable)
        {
            FloorChangeButtons[1].GetComponent<Image>().color = Color.gray;
            FloorChangeButtons[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(-20, 0);
        }
        else
        {
            FloorChangeButtons[1].GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f);
            FloorChangeButtons[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(-80, 0);
        }
        CollisionGround2.SetActive(false);
    }

    public void SetFloor2()
    {
        if (floor2enable)
        {
            curFloor = Floor.Floor2;

            Camera.main.GetComponent<FloorChange>().SetFloorSecond(82f);
            MenuCollection.GetComponent<ChangeCameraPosition>().SetInitializeYpos(82f);

            FloorChangeButtons[0].GetComponent<Image>().color = Color.gray;
            FloorChangeButtons[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(-20, -121);
            FloorChangeButtons[1].GetComponent<Image>().color = Color.white;
            FloorChangeButtons[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            CollisionGround2.SetActive(true);
        }
    }

    public void UnlockSecondFloor()
    {
        FloorChangeButtons[1].GetComponent<Button>().enabled = true;
        FloorChangeButtons[1].GetComponent<Image>().color = Color.gray;
        FloorChangeButtons[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(-20, 0);
        floor2enable = true;
    }
}
