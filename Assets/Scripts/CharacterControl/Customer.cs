using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    //[HideInInspector]
    public bool isMoneyCollect;
    //[HideInInspector]
    public bool isActive;
    public bool unlock;
    public int money;

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

    public GameObject[] items;
    public bool[] itemActive;
    public int[] itemPercentage;

    public DustGenerator dustGen;
    public int stamp;
    // Start is called before the first frame update
    void Awake()
    {

    }

    private void Start()
    {
        string objectTime = gameObject.name + "Time";
        if (isActive)
        {
            if (durationSecond > 0)
            {
                durationSecond = PlayerPrefs.GetFloat(objectTime) - GameMng.Instance.GetComponent<TimeMng>().getCompareTime;
                gameObject.transform.position = entrancePos;
                gameObject.GetComponent<NavMeshAgent>().enabled = true;
            }
            else
            {
                isActive = false;
                durationSecond = duration;
                gameObject.GetComponent<NavMeshAgent>().enabled = false;
            }
        }
        else
        {
            isActive = false;
            durationSecond = duration;
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
        }
        GameMng.Instance.openEvent.AddListener(InitializeSpawnData);
        gameObject.transform.position = poolingPos;
        moneyIcon.SetActive(false);
        dustGen = GameObject.Find("DustGenerator").GetComponent<DustGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (unlock)
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
            if (isActive == false && GetComponent<CheckCustomerAppear>().CheckAppear())
            {
                activeTime -= Time.deltaTime;
            }
            if (activeTime <= 0)
            {
                int number = Random.Range(0, 2 + dustGen.CurDustCount);
                Debug.Log("Customer Random Number : " + number);

                // 디버깅용
                number = 1;

                if (number == 1)
                {
                    Debug.Log("Active");
                    GameMng.Instance.customerCount++;
                    isActive = true;
                    string objectActive = gameObject.name + "IsActive";
                    PlayerPrefs.SetInt(objectActive, 1);
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

    private void OnApplicationPause(bool pause)
    {
        string objectTime = gameObject.name + "Time";
        string objectActiveTime = gameObject.name + "ActiveTime";
        string objectActive = gameObject.name + "IsActive";

        if (pause)
        {
            //isActive = (PlayerPrefs.GetInt(objectActive) == 1) ? true : false;
            PlayerPrefs.SetInt(objectActive, isActive ? 1 : 0);
            if (isActive)
            {
                PlayerPrefs.SetFloat(objectTime, durationSecond);
            }
            else
            {
                PlayerPrefs.SetFloat(objectTime, duration);
                PlayerPrefs.SetFloat(objectActiveTime, activeTime);
            }
        }
        else
        {
            isActive = (PlayerPrefs.GetInt(objectActive) == 1) ? true : false;

            if (isActive)
            {
                durationSecond = PlayerPrefs.GetFloat(objectTime) - GameMng.Instance.GetComponent<TimeMng>().getCompareTime;
            }
            else
            {
                if (GameMng.Instance.getOpenData)
                {
                    activeTime = PlayerPrefs.GetFloat(objectActiveTime) - GameMng.Instance.GetComponent<TimeMng>().getCompareTime;
                }
            }
        }
    }

    IEnumerator DelayFunction(float time)
    {
        yield return new WaitForSeconds(time);

        
    }

    private void OnApplicationQuit()
    {
        string objectTime = gameObject.name + "Time";
        string objectActiveTime = gameObject.name + "ActiveTime";
        string objectActive = gameObject.name + "IsActive";

        PlayerPrefs.SetInt(objectActive, isActive ? 1 : 0);

        if (isActive)
        {
            PlayerPrefs.SetFloat(objectTime, durationSecond);
        }
        else
        {
            PlayerPrefs.SetFloat(objectTime, duration);
            PlayerPrefs.SetFloat(objectActiveTime, activeTime);
        }
    }

    void Spawn()
    {
        isActive = true;
    }

    void InitializeSpawnData()
    {
        string objectTime = gameObject.name + "ActiveTime";

        activeTime = Random.Range(5, 10);
        PlayerPrefs.SetFloat(objectTime, activeTime);
        moneyIcon.SetActive(false);
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
            string objectTime = gameObject.name + "Time";
            PlayerPrefs.SetFloat(objectTime, durationSecond);
            isMoneyCollect = false;
            isActive = false;
            string objectActive = gameObject.name + "IsActive";
            PlayerPrefs.SetInt(objectActive, 0);
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

    IEnumerator SpawnItem(float waitForSec)
    {
        Debug.Log(waitForSec);
        yield return new WaitForSeconds(waitForSec);
        if (isActive)
        {
            Debug.Log("Item Drop!");
            int itemIndex = Random.Range(0, 3);
            GameObject obj = Instantiate(items[0], transform.position, transform.rotation);
            Rigidbody objrig = obj.GetComponent<Rigidbody>();
            obj.GetComponent<CustomerItemInfo>().host = gameObject;
            objrig.AddForce(0, 10, 0);
        }
    }
}
