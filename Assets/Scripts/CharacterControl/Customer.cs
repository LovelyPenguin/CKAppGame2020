using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    public bool isOpen;
    public bool isActive = false;
    public float currentCafeTime;
    public int duration;
    public float activeTime;
    public Vector3 poolingPos;
    public Vector3 entrancePos;

    private float durationSecond;
    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("START");
    }

    private void Start()
    {
        //durationSecond = duration * 60;
        durationSecond = duration;
        GameMng.Instance.openEvent.AddListener(InitializeSpawnData);
        gameObject.transform.position = poolingPos;
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        OpenCheck();
        DurationCheck();

        if (isActive == true)
        {
        }
    }

    void OpenCheck()
    {
        isOpen = GameMng.Instance.getOpenData;
        currentCafeTime = GameMng.Instance.openTime;

        if (isOpen == false)
        {
            isActive = false;
        }
        else
        {
            if (isActive == false)
            {
                activeTime -= Time.deltaTime;
            }
            if (activeTime <= 0)
            {
                //Debug.Log("Active");
                isActive = true;
                InitializeSpawnData();
                gameObject.transform.position = entrancePos;
                gameObject.GetComponent<NavMeshAgent>().enabled = true;
            }
        }
    }

    void DurationCheck()
    {
        if (isOpen && isActive)
        {
            durationSecond -= Time.deltaTime;

            if (durationSecond <= 0)
            {
                ReturnHome();
            }
        }
        else
        {
            ReturnHome();
        }
    }

    void Spawn()
    {
        isActive = true;
    }

    void InitializeSpawnData()
    {
        activeTime = Random.Range(5, 25);
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Debug.Log("Game Stop");
        }
        else
        {
            Debug.Log("Game Restart");
            durationSecond -= GameMng.Instance.GetComponent<TimeMng>().flowTime;
            Debug.Log(GameMng.Instance.GetComponent<TimeMng>().flowTime);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(poolingPos, 0.5f);
        Gizmos.color = Color.blue;

        Gizmos.DrawSphere(entrancePos, 0.5f);
        Gizmos.color = Color.yellow;
    }

    private void ReturnHome()
    {
        if (gameObject.GetComponent<NavMeshAgent>().enabled)
        {
            gameObject.GetComponent<NavMeshAgent>().SetDestination(entrancePos);
        }

        if (Vector3.Distance(gameObject.transform.position, gameObject.GetComponent<NavMeshAgent>().destination) <= 1f)
        {
            gameObject.transform.position = poolingPos;
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            durationSecond = duration;
            isActive = false;
        }
    }
}
