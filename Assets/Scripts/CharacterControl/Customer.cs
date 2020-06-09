using System.Collections;
using System.Collections.Generic;
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
    private bool isReturning = false;
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

        if (isReturning == true)
        {
            ReturnHome();
        }

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
                int number = Random.Range(0, 2);

                // 디버깅용
                number = 1;

                if (number == 1)
                {
                    Debug.Log("Active");
                    GameMng.Instance.customerCount++;
                    isActive = true;
                    isReturning = false;
                    InitializeSpawnData();
                    gameObject.transform.position = entrancePos;
                    gameObject.GetComponent<NavMeshAgent>().enabled = true;
                    StartCoroutine(ExcuteCollectMoney());
                }
                else
                {
                    Debug.Log("FAILED!!");
                    isReturning = false;
                    InitializeSpawnData();
                }
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
                isReturning = true;
            }
        }

        else
        {
            isReturning = true;
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
            isMoneyCollect = false;
            isActive = false;
            isReturning = false;
        }
    }

    public void ReturnHome(bool isActive)
    {
        ReturnHome();
        isReturning = true;
    }

    IEnumerator ExcuteCollectMoney()
    {
        float timer = 0;
        if (GameMng.Instance.openTime > duration)
        {
            timer = duration - setMoneyCollectTime;
        }
        else
        {
            timer = GameMng.Instance.openTime - setMoneyCollectTime;
        }

        Debug.Log("Set Timer: " + timer);
        yield return new WaitForSeconds(timer);
        isMoneyCollect = true;
        moneyIcon.SetActive(true);
    }
}
