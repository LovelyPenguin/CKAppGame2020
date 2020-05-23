using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class RaccoonController : MonoBehaviour
{
    private UnityEngine.Vector3 mOffset;
    private UnityEngine.Vector3 originCoord;

    public float mZCoord;
    private float mDeltaY = 0.3f;
    public GameObject Shadow;
    GameObject Shadowinst;

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
        Shadowinst = Instantiate(Shadow) as GameObject;
        Physics.Raycast(transform.position - new UnityEngine.Vector3(0, mDeltaY, 0), transform.forward, out hit, Mathf.Infinity);
        Shadowinst.transform.position = transform.position + new UnityEngine.Vector3(0, -0.1f, 0);


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
    }

    void OnMouseUp()
    {
        Debug.DrawRay(transform.position + transform.forward - new UnityEngine.Vector3(0, mDeltaY, 0), Sprite.transform.forward, Color.red, 5f);

        Destroy(Shadowinst);

        Debug.Log("RCDrag_OnMouseUp");
        if (Physics.Raycast(transform.position + transform.forward - new UnityEngine.Vector3(0, mDeltaY, 0) - transform.up * 1f, Sprite.transform.forward, out hit, Mathf.Infinity))
            //if (Physics.Raycast(transform.position + transform.forward - new UnityEngine.Vector3(0, mDeltaY, 0), transform.forward, out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.tag == "Ground")
            {
                if (InitState == State.Healing)
                    GameObject.Find("HealMap").GetComponent<HealMapMng>().releaseSeatForName(healMapSeatNum, healMapName);
                RCState = State.inMap1;
                GetComponent<RandomMove>().SetTargerFloor(1);
                transform.position = hit.point + new UnityEngine.Vector3(0, mDeltaY, 0);
                Debug.Log("GroundHit");
            }
            else if (hit.transform.gameObject.tag == "2ndGround")
            {
                if (InitState == State.Healing)
                    GameObject.Find("HealMap").GetComponent<HealMapMng>().releaseSeatForName(healMapSeatNum, healMapName);
                RCState = State.inMap2;
                GetComponent<RandomMove>().SetTargerFloor(2);
                transform.position = hit.point + new UnityEngine.Vector3(0, mDeltaY, 0);
                Debug.Log("2ndGroundHit");
            }
            //else if (hit.transform.gameObject.tag == "WallL")
            //{
            //    transform.position = hit.point + new UnityEngine.Vector3(0, 0, -1.5f);
            //    Debug.Log("WallLHit");
            //}
            //else if (hit.transform.gameObject.tag == "WallR")
            //{
            //    transform.position = hit.point + new UnityEngine.Vector3(-1.5f, 0, 0);
            //    Debug.Log("WallRHit");
            //}
            else if (hit.transform.gameObject.tag == "Heal")
            {
                if (InitState == State.Healing)
                    GameObject.Find("HealMap").GetComponent<HealMapMng>().releaseSeatForName(healMapSeatNum, healMapName);

                healMapName = hit.transform.gameObject.name;
                healMapSeatNum = GameObject.Find("HealMap").GetComponent<HealMapMng>().retSeatIndexForName(healMapName);
                Debug.Log(healMapSeatNum);
                if (healMapSeatNum != -1)
                {
                    this.transform.position = GameObject.Find("HealMap").GetComponent<HealMapMng>().retPositionForName(healMapSeatNum, healMapName);
                    RCState = State.Healing;
                }
                else
                {
                    this.transform.position = originCoord;
                    RCState = InitState;
                }   

             //   transform.position = hit.point + new UnityEngine.Vector3(0, mDeltaY, 0);
                Debug.Log("HealHit");
            }
            else
            {
                RCState = InitState;
                transform.position = originCoord;
                Debug.Log("ElseHit");
                Debug.Log(hit.collider.gameObject.name);

                Camera.main.GetComponent<CameraController>().RollbackPos();
            }
        }
        else
        {
            RCState = InitState;
            transform.position = originCoord;
            Debug.Log("NothingHit");

            Camera.main.GetComponent<CameraController>().RollbackPos();
        }

        //애니메이터
        //animator.SetTrigger("idleTrigger");
        //isOnDrag = false;

        // 애니메이터
        animator.SetTrigger("idleTrigger");

        //Destroy(Shadowinst);

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
        Physics.Raycast(transform.position - new UnityEngine.Vector3(0, mDeltaY, 0), transform.forward, out hit, Mathf.Infinity);
        Shadowinst.transform.position = transform.position + new UnityEngine.Vector3(0, -0.1f, 0);
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

    private enum State { unActive = 0, onDrag, inMap1, inMap2, Healing};
    private State RCState;

    public bool isMoving = false;
    public float interpolant;
    public UnityEngine.Vector3 start;
    public UnityEngine.Vector3 dest;
    public UnityEngine.Vector3 direction;
    public float distance;

    public GameObject StaminaBar;
    public GameObject Sprite;
    
    private int healMapSeatNum;

    private Color OpaqueC = new Color(1f, 1f, 1f, 1f);
    private Color TransparentC = new Color(1f, 1f, 1f, 0f);
    private bool isVisible = true;

    public NavMeshAgent navMesh;

    // Start is called before the first frame update
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
        stamina = 50;
        //SetRCActive(false);
        RCState = State.unActive;

        //navMesh.enabled = false;
        SetMovable(false);

        animator = GetComponentInChildren<Animator>();
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
            SetVisible();
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
            }
            Move();
            StaminaBarUpdate();
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
    private void SetVisible()
    {
        isVisible = !(GameObject.Find("GameManager").GetComponent<FloorStatMng>().CurFloor == FloorStatMng.Floor.Floor1 && RCState == State.inMap2);
        
        if (isVisible)
        {
            Sprite.GetComponent<SpriteRenderer>().color = OpaqueC;
        }
        else
        {
            Sprite.GetComponent<SpriteRenderer>().color = TransparentC;
        }
    }

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
        if (healTime > efficiency && stamina != maxStamina)
        {
            this.stamina = this.stamina + 1;
            healTime = 0.0f;
        }
    }
    private void Exhausting()
    {
        if (isWorking)
            if ((exhaustTime += Time.deltaTime) > 5.0f && stamina != 0 && isWorking)
            {
                this.stamina = this.stamina - 1;
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
        if(isMoving == false)
        {
            isMoving = true;
            animator.SetTrigger("WalkTrigger");
            Debug.Log("WalkTrigger ACtived");
        }
    }

    private void EndMove()
    {
        if(isMoving == true)
        {
            isMoving = false;
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
        RCState = State.inMap1;
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
            StaminaBar.transform.position = Camera.main.WorldToScreenPoint(this.transform.position);

            if (stamina == maxStamina)
                StaminaBar.GetComponent<Image>().color = Color.green;
            else if (stamina == 0)
                StaminaBar.GetComponent<Image>().color = Color.gray;
            else
                StaminaBar.GetComponent<Image>().color = new Color32(255, 127, 0, 255);


            if (stamina == 0)
                StaminaBar.GetComponent<Image>().fillAmount = 1.0f;
            else
                StaminaBar.GetComponent<Image>().fillAmount = ((float)stamina / maxStamina);
        }
        else
        {
            StaminaBar.GetComponent<Image>().fillAmount = 0.0f;
        }
    }
}
