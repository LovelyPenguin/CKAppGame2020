using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorChange : MonoBehaviour
{
    public GameObject followObject;

    private float dis;

    // Start is called before the first frame update
    void Start()
    {
        dis = followObject.transform.position.y - transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetFloorFirst(float firstfloorYPos)
    {
        StartCoroutine(ChangeYPos(firstfloorYPos));
    }

    public void SetFloorSecond(float secondFloorYPos)
    {
        StartCoroutine(ChangeYPos(secondFloorYPos));
    }

    IEnumerator ChangeYPos(float YPos)
    {
        float p = 0.0f;
        Vector3 initPos = transform.position;
        Vector3 newPos = new Vector3(transform.position.x, YPos, transform.position.z);
        Vector3 newFollowPos = followObject.transform.position;
        while (p <= 1.0f)
        {
            transform.position = Vector3.Lerp(initPos, newPos, p);
            newFollowPos.y = transform.position.y + dis;
            followObject.transform.position = newFollowPos;
            p += Time.deltaTime * 4;
            gameObject.GetComponent<CameraController>().isMoved = true;
            yield return null;
        }
        transform.position = newPos;
        newFollowPos.y = transform.position.y + dis;
        followObject.transform.position = newFollowPos;
        gameObject.GetComponent<CameraController>().isMoved = true;
    }
}
