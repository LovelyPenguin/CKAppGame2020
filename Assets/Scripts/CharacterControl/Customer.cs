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
    [HideInInspector]
    public bool[] itemActive;
    [HideInInspector]
    public bool[] itemGen;
    public int[] itemPercentage;

    public DustGenerator dustGen;
    public int stamp;
    public float activePercent;

    public Animator anim;

    public Vector3 FirstFloorInitPos = new Vector3(5, 0, 5);
    public Vector3 SecondFloorInitPos = new Vector3(15, 51, 15);

    private RandomMove moveState;
    private NavMeshAgent nav;
    // Start is called before the first frame update
    void Awake()
    {
        itemGen = new bool[3];

        gameObject.transform.position = poolingPos;
    }

    private void Start()
    {
        activeTime = Random.Range(10, 50);
        string objectTime = gameObject.name + "Time";
        moveState = GetComponent<RandomMove>();
        nav = GetComponent<NavMeshAgent>();
        if (isActive)
        {
            if (durationSecond > 0)
            {
                durationSecond = PlayerPrefs.GetFloat(objectTime) - GameMng.Instance.GetComponent<TimeMng>().getCompareTime;
                gameObject.transform.position = entrancePos;
                gameObject.GetComponent<NavMeshAgent>().enabled = true;
                StartCoroutine(ExcuteCollectMoney());
            }
            else
            {
                isActive = false;
                durationSecond = duration * 60;
                gameObject.GetComponent<NavMeshAgent>().enabled = false;
            }
        }
        else
        {
            isActive = false;
            durationSecond = duration * 60;
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
        }
        GameMng.Instance.openEvent.AddListener(InitializeSpawnData);
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
            AnimationCheck();

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
                int number = Random.Range(0, 100);

                // 디버깅용
                //number = 100;

                if (activePercent <= (number + GameMng.Instance.GetComponent<CustomerMng>().buff) - (dustGen.CurDustCount * 2))
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
                PlayerPrefs.SetFloat(objectTime, duration * 60);
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
            PlayerPrefs.SetFloat(objectTime, duration * 60);
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

        activeTime = Random.Range(10, 50);
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
        if (gameObject.GetComponent<NavMeshAgent>().enabled && moveState.In1StFloor)
        {
            gameObject.GetComponent<NavMeshAgent>().SetDestination(entrancePos);
        }

        else if (gameObject.GetComponent<NavMeshAgent>().enabled && !moveState.In1StFloor)
        {
            gameObject.GetComponent<NavMeshAgent>().SetDestination(new Vector3(17.71f, 52, 19.52f));
        }

        if (Vector3.Distance(gameObject.transform.position, entrancePos) <= 1f)
        {
            gameObject.transform.position = poolingPos;
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            durationSecond = duration * 60;
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
        if (GameMng.Instance.openTime > durationSecond)
        {
            timer = durationSecond - setMoneyCollectTime;
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

    public void ItemDrop()
    {
        Debug.Log("Item Drop!");

        int randomIndex = Random.Range(0, 3);
        float percentCalc = Random.Range(0, 100);

        Debug.Log("Item : " + items[randomIndex]);
        Debug.Log("Drop Percentage : " + percentCalc);

        if (percentCalc <= itemPercentage[randomIndex] && itemActive[randomIndex] == false && itemGen[randomIndex] == false)
        {
            GameObject obj = Instantiate(items[randomIndex], transform.position, transform.rotation);
            Rigidbody objrig = obj.GetComponent<Rigidbody>();
            obj.GetComponent<CustomerItemInfo>().host = gameObject;
            itemGen[randomIndex] = true;
            obj.GetComponent<CustomerItemInfo>().itemIndex = randomIndex;
            objrig.AddForce(0, 10, 0);
        }
    }

    public void ItemDrop(Vector3 pos)
    {
        Debug.Log("Item Drop!");

        int randomIndex = Random.Range(0, 3);
        float percentCalc = Random.Range(0, 100);

        Debug.Log("Item : " + items[randomIndex]);
        Debug.Log("Drop Percentage : " + percentCalc);

        if (percentCalc <= itemPercentage[randomIndex] && itemActive[randomIndex] == false && itemGen[randomIndex] == false)
        {
            GameObject obj = Instantiate(items[randomIndex], pos, transform.rotation);
            Rigidbody objrig = obj.GetComponent<Rigidbody>();
            obj.GetComponent<CustomerItemInfo>().host = gameObject;
            itemGen[randomIndex] = true;
            obj.GetComponent<CustomerItemInfo>().itemIndex = randomIndex;
            objrig.AddForce(0, 10, 0);
        }
    }

    public void AddStamp()
    {
        float randomNumber = Random.Range(0, 100);

        if (randomNumber >= 10)
        {
            stamp++;
        }
        if (stamp == 200)
        {
            Debug.Log("BUFFFFFFFFF");
        }
    }

    public void AnimationCheck()
    {
        anim.SetBool("isWalk", !GetComponent<RandomMove>().Arrived);
    }

    public void FloorChange1st()
    {
        nav.enabled = false;
        transform.position = FirstFloorInitPos;
        moveState.SetTargerFloor(1);
        moveState.In1StFloor = true;
        nav.enabled = true;
    }
    public void FloorChange2nd()
    {
        nav.enabled = false;
        transform.position = SecondFloorInitPos;
        moveState.SetTargerFloor(2);
        moveState.In1StFloor = false;
        nav.enabled = true;
    }
}
