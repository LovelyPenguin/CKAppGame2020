using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    //[HideInInspector]
    public bool isMoneyCollect;
    //[HideInInspector]
    public bool isActive = false;

    [Header("시간 설정")]
    public float duration;
    public float activeTime;
    public float setMoneyCollectTime;

    [Header("포지션")]
    public Vector3 poolingPos;
    public Vector3 entrancePos;

    [Header("기타 설정")]
    [SerializeField]
    private GameObject moneyIcon;

    private float durationSecond;
    private float currentCafeTime;

    private bool isOpen;
    // Start is called before the first frame update
    void Awake()
    {

    }

    private void Start()
    {
        //durationSecond = duration * 60;
        durationSecond = duration;
        GameMng.Instance.openEvent.AddListener(InitializeSpawnData);
        gameObject.transform.position = poolingPos;
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        moneyIcon.SetActive(false);
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
                Debug.Log("Active");
                isActive = true;
                InitializeSpawnData();
                gameObject.transform.position = entrancePos;
                gameObject.GetComponent<NavMeshAgent>().enabled = true;
                StartCoroutine(ExcuteCollectMoney());
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
        activeTime = Random.Range(5, 10);
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

    public void ReturnHome()
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
            isMoneyCollect = false;
            isActive = false;
        }
    }

    IEnumerator ExcuteCollectMoney()
    {
        Debug.Log(duration - setMoneyCollectTime + " Second");
        yield return new WaitForSeconds(duration - setMoneyCollectTime);
        isMoneyCollect = true;
        moneyIcon.SetActive(true);
    }
}
