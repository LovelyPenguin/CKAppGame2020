using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaccoonController : MonoBehaviour
{
    Animator animator;
    private float initXScale;
    private float initYScale;
    private float initZScale;

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
    public Material Plus;
    public Material Minus;
    public int[] Cost = new int[5];

    private float moveTime;
    private float exhaustTime;
    private float healTime;
    private bool isWorking;
    bool isOnDrag = false;
    bool isActive = false;
    bool isHealing = false;

    public bool isMoving = false;
    public float interpolant;
    public UnityEngine.Vector3 start;
    public UnityEngine.Vector3 dest;
    public UnityEngine.Vector3 direction;
    public float distance;

    public GameObject StaminaBar;
    
    private int healMapSeatNum;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
        initXScale = transform.localScale.x;
        initYScale = transform.localScale.y;
        initZScale = transform.localScale.z;

        this.transform.rotation = Camera.main.transform.rotation;
        moveTime = 0.0f;
        exhaustTime = 0.0f;
        healTime = 0.0f;
        stamina = 50;
        SetRCActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (!isHealing)
            {
                RCMove();
                if ((exhaustTime += Time.deltaTime) > 5.0f && stamina != 0 && isWorking)
                {
                    this.stamina = this.stamina - 1;
                    if (GetComponent<ParticleSystem>())
                    {
                        GetComponent<ParticleSystemRenderer>().material = Minus;
                        GetComponent<ParticleSystem>().Play();
                    }
                    exhaustTime = 0.0f;
                }
            }
            StaminaBarUpdate();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Heal")
        {
            isMoving = false;
            this.transform.position = new Vector3(5, 0, 5);
            isHealing = true;
            healMapSeatNum = GameObject.Find("HealMap").GetComponent<HealMapMng>().retSeatIndexForName(other.gameObject.name);
            Debug.Log(healMapSeatNum);
            if (healMapSeatNum != -1)
            {
                this.transform.position = GameObject.Find("HealMap").GetComponent<HealMapMng>().retPositionForName(healMapSeatNum, other.gameObject.name);
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "Heal")
        {
            float efficiency;
            int SeatCount = GameObject.Find("HealMap").GetComponent<HealMapMng>().retSeatCountForName(other.gameObject.name);
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
                if(GetComponent<ParticleSystem>())
                {
                    GetComponent<ParticleSystemRenderer>().material = Plus;
                    GetComponent<ParticleSystem>().Play();
                }
                healTime = 0.0f;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Heal")
        {
            healTime = 0.0f;
            isHealing = false;
            GameObject.Find("HealMap").GetComponent<HealMapMng>().releaseSeatForName(healMapSeatNum, other.gameObject.name);
        }
    }

    private void OnMouseDown()
    {
        isOnDrag = true;
        isMoving = false;
        animator.SetTrigger("DragTrigger");
    }

    private void OnMouseUp()
    {
        isOnDrag = false;
        animator.SetTrigger("idleTrigger");
    }

    private void RCMove()
    {
        if(!isOnDrag && isActive)
        {
            if (!isMoving)
            {
                if (isMoving = ThinkWay())
                    animator.SetTrigger("WalkTrigger");
            }
            else
            {
                interpolant += (Time.deltaTime / distance);
                if(interpolant > 1.0f)
                {
                    interpolant = 1.0f;
                    isMoving = false;
                    animator.SetTrigger("idleTrigger");
                }
                transform.position = UnityEngine.Vector3.Lerp(start, dest, interpolant);
            }
        }
    }

    private bool ThinkWay()
    {
        if ((moveTime += Time.deltaTime) > 1.0f)
        {
            if (Random.Range(0, 10) > 6)
            {
                start = this.transform.position;
                dest = new UnityEngine.Vector3(Random.Range(0.0f, 10.0f), 0.0f, Random.Range(0.0f, 10.0f));
                distance = (dest - start).magnitude;
                direction = (dest - start).normalized;
              
                if (0 < Vector3.Dot(direction, new Vector3(-1, 0, 1)))
                    transform.localScale = new Vector3(initXScale, initYScale, initZScale);
                else
                    transform.localScale = new Vector3(-initXScale, initYScale, initZScale);

                interpolant = 0.0f;
                return true;
            }
            moveTime = 0.0f;
        }
        return false;
    }
    

    public bool GetIsDrag()
    {
        return isOnDrag;
    }

    public void SetRCActive(bool activity)
    {
        isActive = activity;
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
}
