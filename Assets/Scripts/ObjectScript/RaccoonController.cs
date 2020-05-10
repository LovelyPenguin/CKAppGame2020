using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class RaccoonController : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
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
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "HealMap")
        {
            isHealing = true;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        if (other.gameObject.name == "HealMap")
        {
            healTime += Time.deltaTime;
            if (healTime > 2.0f && stamina != maxStamina)
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
        if(other.gameObject.name == "HealMap")
        {
            healTime = 0.0f;
            isHealing = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);
    }

    private void OnMouseDown()
    {
        isOnDrag = true;
        isMoving = false;
    }

    private void OnMouseUp()
    {
        isOnDrag = false;
    }

    private void RCMove()
    {
        if(!isOnDrag && isActive)
        {
            if (!isMoving)
            {
                isMoving = ThinkWay();
            }
            else
            {
                transform.position = UnityEngine.Vector3.LerpUnclamped(start, dest, (interpolant += (Time.deltaTime / distance)));
                if (interpolant >= 1.0f)
                    isMoving = false;
            }
        }
    }

    private bool ThinkWay()
    {
        if ((moveTime += Time.deltaTime) > 1.0f)
        {
            if (Random.Range(0, 10) > 6)
            {
                start = transform.position;
                dest = new UnityEngine.Vector3(Random.Range(0.0f, 8.5f), transform.position.y + 0.1f, Random.Range(0.0f, 8.5f));
                distance = (dest - start).magnitude;
                direction = (dest - start).normalized;
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
}
