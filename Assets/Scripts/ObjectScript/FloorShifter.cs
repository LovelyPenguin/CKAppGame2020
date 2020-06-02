using System.Collections;
using UnityEngine;

public class FloorShifter : MonoBehaviour
{
    private FloorStatMng StatMng;
    public GameObject OtherFloorPos;
    // Start is called before the first frame update
    void Start()
    {
        StatMng = GameObject.Find("GameManager").GetComponent<FloorStatMng>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Floor Shift Trigger Actived");
        if (StatMng.SecondFloorStat)
        {
            other.gameObject.transform.position = OtherFloorPos.transform.position;
        }
    }
}
