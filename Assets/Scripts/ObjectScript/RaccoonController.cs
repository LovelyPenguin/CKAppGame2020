using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class RaccoonController : MonoBehaviour
{
    private UnityEngine.Vector3 mOffset;
    private UnityEngine.Vector3 originCoord;

    public float mZCoord;
    private float mDeltaY = 0.0f;
    public GameObject Shadow;
    //GameObject Shadowinst;

    string healMapName;
    State InitState;
    RaycastHit hit;

    private Transform OnMapTransform;
    private Transform DragTransform;

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
                            GameObject.Find("HealMap").GetComponent<HealMapMng>().releaseSeatForName(healMapSeatNum, healMapName);
                            transform.SetParent(GameObject.Find("Raccoons").transform);
                        }
                        RMng.MoveToAnotherFloor(1);

                        GetComponent<RandomMove>().SetTargerFloor(1);
                        GetComponent<RandomMove>().In1StFloor = true;

                        transform.position = hit.point + new UnityEngine.Vector3(0, mDeltaY, 0);

                        animator.SetTrigger("DropTrigger");
                        StartCoroutine(Drop(State.inMap1));

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
                            GameObject.Find("HealMap").GetComponent<HealMapMng>().releaseSeatForName(healMapSeatNum, healMapName);
                            transform.SetParent(GameObject.Find("Raccoons").transform);
                        }
                        RMng.MoveToAnotherFloor(2);
                        
                        GetComponent<RandomMove>().SetTargerFloor(2);
                        GetComponent<RandomMove>().In1StFloor = false;

                        transform.position = hit.point + new UnityEngine.Vector3(0, mDeltaY, 0);

                        animator.SetTrigger("DropTrigger");
                        StartCoroutine(Drop(State.inMap2));

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
                        GameObject.Find("HealMap").GetComponent<HealMapMng>().releaseSeatForName(healMapSeatNum, healMapName);

                    healMapName = hit.transform.gameObject.name;
                    healMapSeatNum = GameObject.Find("HealMap").GetComponent<HealMapMng>().retSeatIndexForName(healMapName);
                    Debug.Log(healMapSeatNum);
                    if (healMapSeatNum != -1)
                    {
                        this.transform.position = GameObject.Find("HealMap").GetComponent<HealMapMng>().retPositionForName(healMapSeatNum, healMapName);
                        //RCState = State.Healing;
                        animator.SetTrigger("DropTrigger");

                        transform.SetParent(GameObject.Find("HealMap").transform);

                        StartCoroutine(Drop(State.Healing));
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

    IEnumerator Drop(State NextState)
    {
        Vector3 initLocation = transform.position;
        float height = 2.0f;
        while (height > 0.0f)
        {
            Sprite.transform.position = initLocation + transform.up * (height -= Time.deltaTime * 8f);
            yield return null;
        }
        Sprite.transform.position = transform.position;
        RCState = NextState;
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

    public enum State { unActive = 0, onDrag, inMap1, inMap2, Healing, dropping };
    private State RCState;

    public State GetRCState
    {
        get
        {
            return RCState;
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

    private int healMapSeatNum;

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
        //SetRCActive(false);

        //navMesh.enabled = false;
        SetMovable(false);

        GetComponent<NavMeshAgent>().speed = vividSpeed;

        //RMng = GameObject.Find("GameManager").GetComponent<RaccoonMng>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (isActive)
        //{
        //    isVisible = !(GameObject.Find("GameManager").GetComponent<FloorStatMng>().CurFloor == FloorStatMng.Floor.Floor1 && transform.position.y > 5f && !isOnDrag);

        //    if (isVisible)
        //    {
        //        GetComponent<SpriteRenderer>().color = OpaqueC;
        //    }
        //    else
        //    {
        //        GetComponent<SpriteRenderer>().color = TransparentC;
        //    }


        //    if (!isHealing)
        //    {
        //        //RCMove();
        //        if ((exhaustTime += Time.deltaTime) > 5.0f && stamina != 0 && isWorking)
        //        {
        //            this.stamina = this.stamina - 1;
        //            // 파티클 시스템
        //            //if (GetComponent<ParticleSystem>())
        //            //{
        //            //    GetComponent<ParticleSystemRenderer>().material = Minus;
        //            //    GetComponent<ParticleSystem>().Play();
        //            //}
        //            exhaustTime = 0.0f;
        //        }
        //    }
        //    StaminaBarUpdate();
        //}

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
                GetComponent<RandomMove>().setTimer = 2.5f;
                exhausted = true;
                if(GetComponent<ParticleSystem>())
                    GetComponent<ParticleSystem>().Play();
            }
            else if(exhausted && stamina > 10)
            {
                GetComponent<NavMeshAgent>().speed = vividSpeed;
                GetComponent<RandomMove>().setTimer = 0.5f;
                exhausted = false;
                if (GetComponent<ParticleSystem>())
                    GetComponent<ParticleSystem>().Stop();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag == "Heal")
        //{
        //    isMoving = false;
        //    this.transform.position = new Vector3(5, 0, 5);
        //    isHealing = true;
        //    healMapSeatNum = GameObject.Find("HealMap").GetComponent<HealMapMng>().retSeatIndexForName(other.gameObject.name);
        //    Debug.Log(healMapSeatNum);
        //    if (healMapSeatNum != -1)
        //    {
        //        this.transform.position = GameObject.Find("HealMap").GetComponent<HealMapMng>().retPositionForName(healMapSeatNum, other.gameObject.name);
        //    }
        //}

    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        //if (other.gameObject.tag == "Heal")
        //{
        //    float efficiency;
        //    int SeatCount = GameObject.Find("HealMap").GetComponent<HealMapMng>().retSeatCountForName(other.gameObject.name);
        //    switch (SeatCount)
        //    {
        //        case 1:
        //            efficiency = 1.0f;
        //            break;
        //        case 2:
        //            efficiency = 2.0f;
        //            break;
        //        default:
        //            efficiency = 3.0f;
        //            break;
        //    }
        //    healTime += Time.deltaTime;
        //    if (healTime > efficiency && stamina != maxStamina)
        //    {
        //        this.stamina = this.stamina + 1;
        //        // 파티클 시스템
        //        //if(GetComponent<ParticleSystem>())
        //        //{
        //        //    GetComponent<ParticleSystemRenderer>().material = Plus;
        //        //    GetComponent<ParticleSystem>().Play();
        //        //}
        //        healTime = 0.0f;
        //    }
        //}
    }
    private void OnTriggerExit(Collider other)
    {
        //if (other.gameObject.tag == "Heal")
        //{
        //    healTime = 0.0f;
        //    isHealing = false;
        //    GameObject.Find("HealMap").GetComponent<HealMapMng>().releaseSeatForName(healMapSeatNum, other.gameObject.name);
        //}
    }
    //private void SetVisible()
    //{
    //    isVisible = !(GameObject.Find("GameManager").GetComponent<FloorStatMng>().CurFloor == FloorStatMng.Floor.Floor1 && RCState == State.inMap2);

    //    if (isVisible)
    //    {
    //        Sprite.GetComponent<SpriteRenderer>().color = OpaqueC;
    //        Shadow.GetComponent<SpriteRenderer>().color = OpaqueC;
    //        StaminaBar.GetComponent<Image>().color = OpaqueC;
    //    }
    //    else
    //    {
    //        Sprite.GetComponent<SpriteRenderer>().color = TransparentC;
    //        Shadow.GetComponent<SpriteRenderer>().color = TransparentC;
    //        StaminaBar.GetComponent<Image>().color = TransparentC;
    //    }
    //}

    private void Healing()
    {
        float efficiency;
        int SeatCount = GameObject.Find("HealMap").GetComponent<HealMapMng>().retSeatCountForName(healMapName);
        switch (SeatCount)
        {
            case 1:
                efficiency = 1.0f;
                break;
            case 2:
                efficiency = 2.0f;
                break;
            default:
                efficiency = 3.0f;
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
        if (isWorking)
            if ((exhaustTime += Time.deltaTime) > 5.0f && isWorking)
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
            Debug.Log("WalkTrigger ACtived");
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
            Debug.Log("idleTrigger ACtived");
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

    //private void OnMouseDown()
    //{
    //    isOnDrag = true;
    //    isMoving = false;
    //    animator.SetTrigger("DragTrigger");
    //}

    //private void OnMouseUp()
    //{
    //    isOnDrag = false;
    //    animator.SetTrigger("idleTrigger");
    //}

    //private void RCMove()
    //{
    //    if(!isOnDrag && isActive)
    //    {
    //        if (!isMoving)
    //        {
    //            if (isMoving = ThinkWay())
    //                animator.SetTrigger("WalkTrigger");
    //        }
    //        else
    //        {
    //            interpolant += (Time.deltaTime / distance);
    //            if(interpolant > 1.0f)
    //            {
    //                interpolant = 1.0f;
    //                isMoving = false;
    //                animator.SetTrigger("idleTrigger");
    //            }
    //            transform.position = UnityEngine.Vector3.Lerp(start, dest, interpolant);
    //        }
    //    }
    //}

    //private bool ThinkWay()
    //{
    //    if ((moveTime += Time.deltaTime) > 1.0f)
    //    {
    //        if (Random.Range(0, 10) > 6)
    //        {
    //            start = this.transform.position;
    //            dest = new UnityEngine.Vector3(Random.Range(0.0f, 10.0f), transform.position.y, Random.Range(0.0f, 10.0f));
    //            distance = (dest - start).magnitude;
    //            direction = (dest - start).normalized;

    //            if (0 < Vector3.Dot(direction, new Vector3(-1, 0, 1)))
    //                transform.localScale = new Vector3(initXScale, initYScale, initZScale);
    //            else
    //                transform.localScale = new Vector3(-initXScale, initYScale, initZScale);

    //            interpolant = 0.0f;
    //            return true;
    //        }
    //        moveTime = 0.0f;
    //    }
    //    return false;
    //}


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
