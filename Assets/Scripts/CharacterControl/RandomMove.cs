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
    private float setTimer = 0.5f;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        targetPostion = target.transform.position;

        targetPostion.x = Random.Range(-15, 15);
        targetPostion.z = Random.Range(-15, 15);
        nav.SetDestination(targetPostion);
        timer = setTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            targetPostion = target.transform.position;
            nav.SetDestination(targetPostion);
        }
        SetRnadomizeDestination();
    }

    private void SetRnadomizeDestination()
    {
        if (Vector3.Distance(transform.position, nav.destination) <= 0.5f)
        {
            isArrive = true;
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                targetPostion.x = Random.Range(-15, 15);
                targetPostion.z = Random.Range(-15, 15);
                nav.SetDestination(targetPostion);
                timer = setTimer;
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
}
