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

    // Start is called before the first frame update
    void Start()
    {
        targetPostion = target.transform.position;

        targetPostion.x = Random.Range(-15, 15);
        targetPostion.z = Random.Range(-15, 15);
        nav.SetDestination(targetPostion);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            targetPostion = target.transform.position;
        }
        
        if (Vector3.Distance(transform.position, nav.destination) <= 0.3f)
        {
            isArrive = true;
            targetPostion.x = Random.Range(-15, 15);
            targetPostion.z = Random.Range(-15, 15);
            nav.SetDestination(targetPostion);
        }
        else
        {
            isArrive = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(targetPostion, 0.1f);
    }
}
