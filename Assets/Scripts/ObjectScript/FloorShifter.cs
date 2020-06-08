using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class FloorShifter : MonoBehaviour
{
    private FloorStatMng StatMng;
    private GameObject[] FloorPos = new GameObject[2];
    private Vector3 diff;
    private RandomMove RDMove;
    // Start is called before the first frame update
    void Start()
    {
        StatMng = GameObject.Find("GameManager").GetComponent<FloorStatMng>();
        FloorPos[0] = GameObject.Find("FloorShiftTrigger1");
        FloorPos[1] = GameObject.Find("FloorShiftTrigger2");
    }

    // Update is called once per frame
    void Update()
    {
        if (StatMng.SecondFloorStat)
        {
            if (Vector3.Distance(transform.position, FloorPos[0].transform.position) < 1.0f)
            {
                GetComponent<RaccoonController>().MoveSecondFloor();
            }
            else if (Vector3.Distance(transform.position, FloorPos[1].transform.position) < 1.0f)
            {
                GetComponent<RaccoonController>().MoveFirstFloor();
            }
        }
    }

}
