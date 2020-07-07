using System.Collections;
using UnityEngine;

public class SetCameraPos : MonoBehaviour
{
    public static bool CameraMoveEnable = true;

    public Vector3 newPos;

    Vector3 MainPos1 = new Vector3(-20.23f, 22.85f, -20.23f);
    Vector3 MainPos2 = new Vector3(-20.23f, 82f, -20.23f);

    ChangeCameraPosition CCP;
    GameObject GMng;

    // Start is called before the first frame update
    void Start()
    {
        GMng = GameObject.Find("GameManager");
        CCP = GameObject.Find("MenuCollection").GetComponent<ChangeCameraPosition>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    float InitYPos;
    public void Set()
    {
        if (GMng.GetComponent<FloorStatMng>().CurFloor == FloorStatMng.Floor.Floor1)
            InitYPos = 22.85f;
        else
            InitYPos = 82f;

        if (CameraMoveEnable)
        {
            CameraMoveEnable = false;
            StartCoroutine(ChangePos(new Vector3(newPos.x, newPos.y + InitYPos, newPos.z)));
        }
    }

    IEnumerator ChangePos(Vector3 Pos)
    {
        while (Vector3.Distance(Camera.main.transform.position, Pos) > 0.01f)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, Pos, Time.deltaTime * 10);
            CCP.SetInitializeYpos(Camera.main.transform.position.y);
            yield return null;
        }
        Camera.main.transform.position = Pos;
        CameraMoveEnable = true;
    }
}
