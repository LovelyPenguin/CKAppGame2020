using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomMove : MonoBehaviour
{
    public NavMeshAgent nav;
    public GameObject target;
    [SerializeField]
    private Vector3 targetPostion;
    [SerializeField]
    private float speed;
    [SerializeField]
    private bool isArrive;
    [SerializeField]
    public float setTimer = 0.5f;
    private float timer;

    public bool In1StFloor = true;
    public float setDistance = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        //targetPostion = target.transform.position;

        targetPostion.x = Random.Range(0, 10);
        targetPostion.z = Random.Range(0, 10);

        // Start의 함수 실행 순서가 Awake다음으로 빨라서 만약에 오브젝트가 Nav Mesh가 활성화 되어 있다가
        // 이 함수 구문보다 먼저 Nav Mesh의 Enable을 False로 만드는 부분이 있으면
        // 건드는 부분에서 Nav Mesh가 활성화 되있을 경우에만 목적지를 설정할 수 있다는 에러 문구를 띄워서 0.3초 정도 느리게 초기화하는 의도로 제작되었음
        StartCoroutine(InitializeDestination());

        timer = setTimer;
    }

    // Update is called once per frame
    void Update()
    {
        SetRnadomizeDestination();
    }

    private void SetRnadomizeDestination()
    {
        if (Vector3.Distance(transform.position, nav.destination) <= setDistance)
        {
            isArrive = true;
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                if (In1StFloor)
                {
                    targetPostion.x = Random.Range(0, 10);
                    targetPostion.z = Random.Range(0, 10);
                }
                else
                {
                    targetPostion.x = Random.Range(10, 20);
                    targetPostion.z = Random.Range(10, 20);
                }

                if (gameObject.GetComponent<NavMeshAgent>().enabled)
                {
                    nav.SetDestination(targetPostion);
                }

                timer = setTimer + Random.Range(0.0f, 2.0f);
            }
        }
        else
        {
            isArrive = false;
        }
    }

    private void MoveCheck()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(nav.destination, 0.1f);
    }

    public bool Arrived
    {
        get { return isArrive; }
        //set { isArrive = value; }
    }

    public Vector3 GetTargetPos()
    {
        return targetPostion;
    }

    public void SetTargerFloor(int floor)
    {
        if (floor == 2)
        {
            targetPostion.y = 51f;
        }
        else
        {
            targetPostion.y = 0f;
        }
    }

    IEnumerator InitializeDestination()
    {
        yield return new WaitForSeconds(0.3f);

        if (nav.enabled)
        {
            nav.SetDestination(targetPostion);
        }
    }
}
