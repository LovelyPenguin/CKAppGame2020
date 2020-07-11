using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerFloorChange : MonoBehaviour
{
    private GameObject floor1;
    private GameObject floor2;

    private Customer cus;
    private RandomMove cusMove;
    private FloorStatMng flos;
    // Start is called before the first frame update
    void Start()
    {
        floor1 = GameObject.Find("FloorShiftTrigger1");
        floor2 = GameObject.Find("FloorShiftTrigger2");

        cus = GetComponent<Customer>();
        cusMove = GetComponent<RandomMove>();

        flos = GameMng.Instance.GetComponent<FloorStatMng>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flos.SecondFloorStat && Vector3.Distance(transform.position, floor1.transform.position) < 1f && cusMove.In1StFloor)
        {
            cus.FloorChange2nd();
        }

        if (flos.SecondFloorStat && Vector3.Distance(transform.position, floor2.transform.position) < 1f && !cusMove.In1StFloor)
        {
            cus.FloorChange1st();
        }
    }
}
