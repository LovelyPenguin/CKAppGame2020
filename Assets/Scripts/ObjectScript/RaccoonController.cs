using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Events;

public class RaccoonController : MonoBehaviour
{
    private UnityEngine.Vector3 mOffset;
    private UnityEngine.Vector3 originCoord;

    public float mZCoord;
    private float mDeltaY = 0.0f;
    public GameObject Shadow;
    //GameObject Shadowinst;

    State InitState;
    RaycastHit hit;

    private Transform OnMapTransform;
    private Transform DragTransform;

    public HealMapInfo HealInfo;

    void OnMouseDown()
    {
        InitState = RCState;
        RCState = State.onDrag;
        Debug.Log("RCDrag_OnMouseDown");
        originCoord = transform.position;
        transform.Translate(0, mDeltaY, 0);
        mZCoord = 1;
        //Shadowinst = Instantiate(Shadow) as GameObject;
        Physics.Raycast(transform.position - new UnityEngine.Vector3(0, mDeltaY, 0), transform.forward, out hit, Mathf.Infinity);
        //Shadowinst.transform.position = transform.position + new UnityEngine.Vector3(0, -0.1f, 0);


        //isOnDrag = true;
        //isMoving = false;

        //애니메이터
        //isMoving = false;
        animator.ResetTrigger("idleTrigger");
        animator.ResetTrigger("WalkTrigger");
        animator.ResetTrigger("DropTrigger");
        animator.SetTrigger("DragTrigger");

        Camera.main.GetComponent<CameraController>().RememberPos();

        //애니메이터
        //animator.SetTrigger("DragTrigger");

        Sprite.transform.localEulerAngles = new Vector3(30f, 0f, 0f);
        Sprite.transform.localScale = new Vector3(1f, 1f, 1f);

        if (InitState == State.inMap1)
            RMng.ReleaseMapCount(1);
        else if (InitState == State.inMap2)
            RMng.ReleaseMapCount(2);
    }

    void OnMouseUp()
    {
        Debug.DrawRay(transform.position + transform.forward - new UnityEngine.Vector3(0, mDeltaY, 0), Sprite.transform.forward, Color.red, 5f);

        //Destroy(Shadowinst);

        animator.ResetTrigger("DragTrigger");
        animator.ResetTrigger("WalkTrigger");

        Debug.Log("RCDrag_OnMouseUp");
        if (Physics.Raycast(transform.position + transform.forward - new UnityEngine.Vector3(0, mDeltaY, 0) - transform.up * 0.7f, Sprite.transform.forward, out hit, Mathf.Infinity))
        //if (Physics.Raycast(transform.position + transform.forward - new UnityEngine.Vector3(0, mDeltaY, 0), transform.forward, out hit, Mathf.Infinity))
        {
            // 1층에 놓았을떄
            if (hit.transform.gameObject.tag == "Ground")
            {
                // 가게가 운영중이며 다른 공간에서 데려온 경우
                // 이동은 취소되야 된다.
                if (isWorking && InitState != State.inMap1 && InitState != State.inMap2)
                {
                    transform.position = originCoord;
                    Debug.Log("Drag While Working is not allow");

                    animator.SetTrigger("DropTrigger");
                    StartCoroutine(Drop(InitState));

                    Camera.main.GetComponent<CameraController>().RollbackPos();
                }
                // 아닌 경우 즉 가게가 운영중이 아니거나 맵에서 끌고오는 경우
                else
                {
                    // 1층으로 이동이 가능한 경우
                    if (RMng.CanMoveToAnotherFloor(1))
                    {
                        if (InitState == State.Healing)
                        {
                            HealMapMng.Instance.releaseSeatForName(HealInfo.SeatNum, HealInfo.HealMapName);
                            transform.SetParent(GameObject.Find("Raccoons").transform);
                            HealInfo.HealMapName = null;
                            HealInfo.SeatCount = -1;
                            HealInfo.SeatNum = -1;
                        }
                        RMng.MoveToAnotherFloor(1);

                        GetComponent<RandomMove>().SetTargerFloor(1);
                        GetComponent<RandomMove>().In1StFloor = true;

                        transform.position = hit.point + new UnityEngine.Vector3(0, mDeltaY, 0);

                        animator.SetTrigger("DropTrigger");
                        StartCoroutine(Drop(State.inMap1));

                        if (hit.transform.gameObject.GetComponent<SetCameraPos>())
                            hit.transform.gameObject.GetComponent<SetCameraPos>().Set();

                        Debug.Log("GroundHit");
                    }
                    // 1층이 가득차 이동이 불가능 한 경우
                    else
                    {
                        if (InitState == State.inMap2)
                            RMng.MoveToAnotherFloor(2);

                        transform.position = originCoord;
                        Debug.Log("there is no space in 1st floor");

                        animator.SetTrigger("DropTrigger");
                        StartCoroutine(Drop(InitState));

                        Camera.main.GetComponent<CameraController>().RollbackPos();
                    }
                }
            }
            // 2층애 놓았을떄
            else if (hit.transform.gameObject.tag == "2ndGround")
            {
                // 가게가 운영중이며 다른 공간에서 데려온 경우
                // 이동은 취소되야 된다.
                if (isWorking && InitState != State.inMap1 && InitState != State.inMap2)
                {
                    transform.position = originCoord;
                    Debug.Log("Drag While Working is not allow");

                    animator.SetTrigger("DropTrigger");
                    StartCoroutine(Drop(InitState));

                    Camera.main.GetComponent<CameraController>().RollbackPos();
                }
                else
                {
                    // 2층에 이동 가능한 경우
                    if (RMng.CanMoveToAnotherFloor(2))
                    {
                        if (InitState == State.Healing)
                        {
                            HealMapMng.Instance.releaseSeatForName(HealInfo.SeatNum, HealInfo.HealMapName);
                            transform.SetParent(GameObject.Find("Raccoons").transform);
                            HealInfo.HealMapName = null;
                            HealInfo.SeatCount = -1;
                            HealInfo.SeatNum = -1;
                        }
                        RMng.MoveToAnotherFloor(2);
                        
                        GetComponent<RandomMove>().SetTargerFloor(2);
                        GetComponent<RandomMove>().In1StFloor = false;

                        transform.position = hit.point + new UnityEngine.Vector3(0, mDeltaY, 0);

                        animator.SetTrigger("DropTrigger");
                        StartCoroutine(Drop(State.inMap2));

                        if (hit.transform.gameObject.GetComponent<SetCameraPos>())
                            hit.transform.gameObject.GetComponent<SetCameraPos>().Set();

                        Debug.Log("2ndGroundHit");
                    }
                    // 2층이 가득차 이동이 불가능 한 경우
                    else
                    {
                        if (InitState == State.inMap1)
                            RMng.MoveToAnotherFloor(1);

                        transform.position = originCoord;
                        Debug.Log("There is no space in 2nd floor");

                        animator.SetTrigger("DropTrigger");
                        StartCoroutine(Drop(InitState));

                        Camera.main.GetComponent<CameraController>().RollbackPos();
                    }
                }
            }
            // 휴식 공간으로 이동시킨 경우
            else if (hit.transform.gameObject.tag == "Heal")
            {
                // 가게가 운영중이며 휴식공간 간에 이동이 아닌경우
                if (isWorking && InitState != State.Healing)
                {
                    transform.position = originCoord;
                    Debug.Log("Drag While Working is not allow");

                    RMng.MoveToAnotherFloor(InitState == State.inMap1 ? 1 : 2);

                    animator.SetTrigger("DropTrigger");
                    StartCoroutine(Drop(InitState));

                    Camera.main.GetComponent<CameraController>().RollbackPos();
                }
                // 아닌 경우 즉 가게가 운영중이 아닌 경우
                else
                {
                    if (InitState == State.Healing)
                        HealMapMng.Instance.releaseSeatForName(HealInfo.SeatNum, HealInfo.HealMapName);

                    HealInfo.HealMapName = hit.transform.gameObject.name;
                    HealInfo.SeatNum = HealMapMng.Instance.retSeatIndexForName(HealInfo.HealMapName);
                    Debug.Log(HealInfo.SeatNum);
                    if (HealInfo.SeatNum != -1)
                    {
                        this.transform.position = HealMapMng.Instance.retPositionForName(HealInfo.SeatNum, HealInfo.HealMapName);
                        //RCState = State.Healing;
                        animator.SetTrigger("DropTrigger");

                        transform.SetParent(HealMapMng.Instance.gameObject.transform);

                        StartCoroutine(Drop(State.Healing));

                        if (hit.transform.gameObject.GetComponent<SetCameraPos>())
                            hit.transform.gameObject.GetComponent<SetCameraPos>().Set();
                    }
                    else
                    {
                        RMng.MoveToAnotherFloor(InitState == State.inMap1 ? 1 : 2);

                        animator.SetTrigger("DropTrigger");

                        this.transform.position = originCoord;
                        StartCoroutine(Drop(InitState));
                    }
                }

                //   transform.position = hit.point + new UnityEngine.Vector3(0, mDeltaY, 0);
                Debug.Log("HealHit");
            }
            // 휴식 공간도 맵도 아닌 경우
            else
            {
                transform.position = originCoord;
                Debug.Log("ElseHit");
                Debug.Log("hit = " + hit.collider.gameObject.name);

                if (InitState == State.inMap1)
                    RMng.MoveToAnotherFloor(1);
                if (InitState == State.inMap2)
                    RMng.MoveToAnotherFloor(2);

                animator.SetTrigger("DropTrigger");
                StartCoroutine(Drop(InitState));

                Camera.main.GetComponent<CameraController>().RollbackPos();
            }
        }
        // 충돌 판정에 실패한 경우
        else
        {
            transform.position = originCoord;
            Debug.Log("NothingHit");

            if (InitState == State.inMap1)
                RMng.MoveToAnotherFloor(1);
            if (InitState == State.inMap2)
                RMng.MoveToAnotherFloor(2);

            animator.SetTrigger("DropTrigger");
            StartCoroutine(Drop(InitState));

            Camera.main.GetComponent<CameraController>().RollbackPos();
        }

        Sprite.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        Sprite.transform.localScale = new Vector3(1f, 1.1547f, 1f);
    }

    private UnityEngine.Vector3 GetMouseWorldPos()
    {
        UnityEngine.Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZCoord;
        mousePoint.y += mDeltaY;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        //애니메이터
        //animator.SetTrigger("DragTrigger");
        //Debug.Log("RCDrag_OnMouseDrag");
        transform.position = GetMouseWorldPos();
        //Physics.Raycast(transform.position - new UnityEngine.Vector3(0, mDeltaY, 0), transform.forward, out hit, Mathf.Infinity);
        //Shadowinst.transform.position = transform.position + new UnityEngine.Vector3(0, -0.1f, 0);
        //Shadow.transform.position = transform.position + new UnityEngine.Vector3(0, -0.1f, 0);
    }

    public IEnumerator Drop(State NextState)
    {
        GetComponent<BoxCollider>().enabled = false;
        Vector3 initLocation = transform.position;
        float height = 2.0f;
        while (height > 0.0f)
        {
            Sprite.transform.position = initLocation + transform.up * (height -= Time.deltaTime * 8f);
            yield return null;
        }
        Sprite.transform.position = transform.position;
        RCState = NextState;
        yield return new WaitForSeconds(0.5f);
        GetComponent<BoxCollider>().enabled = true;
    }


    // -------------------------------------------------------------------------------------------


    Animator animator;
    private float initXScale;
    private float initYScale;
    private float initZScale;

    //public GameObject Tail;

    public int Stamina;
    private static int maxStamina = 100;
    public int stamina
    {
        get
        {
            return Stamina;
        }
        set
        {
            if (value > 100)
                Stamina = 100;
            else if (value < 0)
                Stamina = 0;
            else
                Stamina = value;
        }
    }
    //public Material Plus;
    //public Material Minus;
    public int[] Cost = new int[5];

    private float moveTime;
    private float exhaustTime;
    private float healTime;
    private bool isWorking;
    //bool isOnDrag = false;
    //bool isActive = false;
    //bool isHealing = false;
    private bool Movable;

    public bool isExhaustable = true;

    public enum State { unActive = 0, onDrag, inMap1, inMap2, Healing, dropping };
    private State RCState;

    public State GetRCState
    {
        get
        {
            return RCState;
        }
        set
        {
            RCState = value;
        }
    }

    public bool isMoving = false;
    public float interpolant;
    public UnityEngine.Vector3 start;
    public UnityEngine.Vector3 dest;
    public UnityEngine.Vector3 direction;
    public float distance;

    public GameObject StaminaBar;
    //public GameObject StaminaBarBack;
    public GameObject Sprite;

    private Color OpaqueC = new Color(1f, 1f, 1f, 1f);
    private Color TransparentC = new Color(1f, 1f, 1f, 0f);
    private bool isVisible = true;

    public NavMeshAgent navMesh;
    private RaccoonMng RMng;

    public Vector3 FirstFloorInitPos = new Vector3(5, 0, 5);
    public Vector3 SecondFloorInitPos = new Vector3(15, 51, 15);

    public float exhaustedSpeed;
    public float vividSpeed;
    private bool exhausted = false;

    public UnityEvent ExhaustStart;
    public UnityEvent ExhaustStop;

    // Start is called before the first frame update
    private void Awake()
    {
        RCState = State.unActive;
        animator = GetComponentInChildren<Animator>();
        RMng = GameObject.Find("GameManager").GetComponent<RaccoonMng>();
    }
    void Start()
    {
        //애니메이터
        initXScale = transform.localScale.x;
        initYScale = transform.localScale.y;
        initZScale = transform.localScale.z;

        //this.transform.rotation = Camera.main.transform.rotation;
        moveTime = 0.0f;
        exhaustTime = 0.0f;
        healTime = 0.0f;

        //navMesh.enabled = false;
        SetMovable(false);

        GetComponent<NavMeshAgent>().speed = vividSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        if (RCState != State.unActive)
        {
            //SetVisible();
            switch (RCState)
            {
                case State.Healing:
                    SetMovable(false);
                    Healing();
                    break;
                case State.inMap1:
                    SetMovable(true);
                    Exhausting();
                    break;
                case State.inMap2:
                    SetMovable(true);
                    Exhausting();
                    break;
                case State.onDrag:
                    SetMovable(false);
                    break;
                case State.dropping:
                    SetMovable(false);
                    break;
            }
            Move();
            StaminaBarUpdate();

            if(!exhausted && stamina <= 10)
            {
                GetComponent<NavMeshAgent>().speed = exhaustedSpeed;
                GetComponent<RandomMove>().setTimer = 4.0f;
                exhausted = true;
                if(GetComponent<ParticleSystem>())
                    GetComponent<ParticleSystem>().Play();
                ExhaustStart.Invoke();
            }
            else if(exhausted && stamina > 10)
            {
                GetComponent<NavMeshAgent>().speed = vividSpeed;
                GetComponent<RandomMove>().setTimer = 1.0f;
                exhausted = false;
                if (GetComponent<ParticleSystem>())
                    GetComponent<ParticleSystem>().Stop();
                ExhaustStop.Invoke();
            }
        }
    }

    private void Healing()
    {
        float efficiency;
        HealInfo.SeatCount = GameObject.Find("HealMap").GetComponent<HealMapMng>().retSeatCountForName(HealInfo.HealMapName);
        switch (HealInfo.SeatCount)
        {
            case 1:
                efficiency = 60.0f;
                break;
            case 2:
                efficiency = 120.0f;
                break;
            default:
                efficiency = 180.0f;
                break;
        }
        healTime += Time.deltaTime;
        if (healTime > efficiency)
        {
            this.stamina += 1;
            healTime = 0.0f;
        }
    }
    private void Exhausting()
    {
        if (isWorking && isExhaustable)
            if ((exhaustTime += Time.deltaTime) > 300.0f && isWorking)
            {
                this.stamina -= 1;
                exhaustTime = 0.0f;
            }
    }

    void SetMovable(bool boolean)
    {
        Movable = boolean;
        if (Movable)
        {
            navMesh.enabled = true;
        }
        else
        {
            navMesh.enabled = false;
            isMoving = false;
        }

    }

    private void StartMove()
    {
        if (isMoving == false)
        {
            isMoving = true;
            animator.ResetTrigger("idleTrigger");
            animator.ResetTrigger("DragTrigger");
            animator.SetTrigger("WalkTrigger");
            //Debug.Log("WalkTrigger ACtived");
        }
    }

    private void EndMove()
    {
        if (isMoving == true)
        {
            isMoving = false;
            animator.ResetTrigger("WalkTrigger");
            animator.ResetTrigger("DragTrigger");
            animator.SetTrigger("idleTrigger");
            //Debug.Log("idleTrigger ACtived");
        }
    }

    private void Move()
    {
        if (Movable)
        {
            // 애니메이션
            if (this.GetComponent<RandomMove>().Arrived)
                EndMove();
            else
                StartMove();

            Vector3 dir = navMesh.desiredVelocity;

            if (dir.z - dir.x > 0)
                Sprite.GetComponent<SpriteRenderer>().flipX = false;
            else
                Sprite.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void CallUpgradeTrigger(int rank)
    {
        switch (rank)
        {
            case 2:
                animator.SetTrigger("UpgradeTrigger");
                SetMovable(false);
                break;
            case 3:
                animator.SetTrigger("UpgradeTrigger");
                animator.SetTrigger("UpgradeTrigger2");
                SetMovable(false);
                break;
        }
    }


    public bool GetIsDrag()
    {
        return RCState == State.onDrag;
        //   return isOnDrag;
    }

    public void SetRCActive(bool activity)
    {
        //isActive = activity;
        if (RMng.CanMoveToAnotherFloor(1))
        {
            MoveFirstFloor(true);
        }
        else if (RMng.CanMoveToAnotherFloor(2))
        {
            MoveSecondFloor(true);
        }
    }
    public void SetRCActiveInFloor(bool isFist)
    {
        //isActive = activity;
        if (isFist)
        {
            MoveFirstFloor(true);
        }
        else
        {
            MoveSecondFloor(true);
        }
    }

    public void StartWork()
    {
        isWorking = true;
    }

    public void StopWork()
    {
        isWorking = false;
    }

    private void StaminaBarUpdate()
    {
        if (isVisible)
        {
            StaminaBar.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + this.transform.up * 2.8f);
            //StaminaBarBack.transform.position = Camera.main.WorldToScreenPoint(this.transform.position);
            if (stamina == maxStamina)
                StaminaBar.GetComponentsInChildren<Image>()[1].color = Color.green;
            else if (stamina == 0)
                StaminaBar.GetComponentsInChildren<Image>()[1].color = Color.gray;
            else
                StaminaBar.GetComponentsInChildren<Image>()[1].color = new Color32(255, 127, 0, 255);


            if (stamina == 0)
                StaminaBar.GetComponentsInChildren<Image>()[1].fillAmount = 1.0f;
            else
                StaminaBar.GetComponentsInChildren<Image>()[1].fillAmount = ((float)stamina / maxStamina);
        }
        else
        {
            StaminaBar.GetComponentsInChildren<Image>()[1].fillAmount = 0.0f;
        }
        this.animator.SetFloat("Stamina", stamina / 100f + 0.4f);
    }


    public void MoveFirstFloor(bool init = false)
    {
        if (RMng.MoveToAnotherFloor(1))
        {
            if (!init)
                RMng.ReleaseMapCount(2);
            SetMovable(false);
            transform.position = FirstFloorInitPos;
            RCState = State.inMap1;
            GetComponent<RandomMove>().SetTargerFloor(1);
            GetComponent<RandomMove>().In1StFloor = true;
        }
    }

    public void MoveSecondFloor(bool init = false)
    {
        if (RMng.MoveToAnotherFloor(2))
        {
            if (!init)
                RMng.ReleaseMapCount(1);
            SetMovable(false);
            transform.position = SecondFloorInitPos;
            RCState = State.inMap2;
            GetComponent<RandomMove>().SetTargerFloor(2);
            GetComponent<RandomMove>().In1StFloor = false;
        }
    }
}
