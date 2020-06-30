using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrinkTransfer : MonoBehaviour
{
    public bool isTransfer = false;
    public bool isDemandJuice;
    public GameObject particle;
    public string selectJuice;
    public GameObject juiceIcon;
    public float setDrinkWaitingTime;

    private float setTimer;
    private string[] juiceList = new string[5];
    private float timer;

    public float[] itemDropPercent = new float[3];

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        if (particle != null)
        {
            particle.SetActive(false);
        }
        juiceIcon.SetActive(false);
        for (int i = 0; i < 5; i++)
        {
            juiceList[i] = GameMng.Instance.GetComponent<DrinkMng>().juiceList[i].juice.GetComponent<JuiceObject>().juiceName;
        }

        setTimer = Random.Range(3, 5);
        timer = setTimer;
        selectJuice = juiceList[Random.Range(0, juiceList.Length - 1)];
        isDemandJuice = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Customer>().isActive)
        {
            DemandJuice();
        }
    }

    public void Detect(int money = 100)
    {
        GameMng.Instance.money += money;
        GetComponent<Customer>().money += money;
        ParticleManager(true);
        InitalizeParameter();
        StartCoroutine(Disable());
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(3f);
        ParticleManager(false);
        isTransfer = false;
    }

    private void ParticleManager(bool isActive)
    {
        if (particle != null)
        {
            particle.SetActive(isActive);
        }
    }

    private void DemandJuice()
    {
        if (!isDemandJuice)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0 && !isDemandJuice && !gameObject.GetComponent<Customer>().isMoneyCollect)
        {
            isDemandJuice = true;
            PickJuiceSprite();
            juiceIcon.SetActive(true);
            isTransfer = false;
            StartCoroutine(DrinkWaiting());
        }
        else if (gameObject.GetComponent<Customer>().isMoneyCollect)
        {
            InitalizeParameter();
        }
    }


    private void PickJuiceSprite()
    {
        Sprite[] juice = GameMng.Instance.GetComponent<DrinkMng>().juiceSprites;
        for (int i = 0; i < juiceList.Length; i++)
        {
            if (selectJuice == juiceList[i])
            {
                juiceIcon.GetComponent<SpriteRenderer>().sprite = juice[i];
            }
        }
    }

    private void InitalizeParameter()
    {
        isTransfer = true;
        isDemandJuice = false;
        selectJuice = juiceList[Random.Range(0, juiceList.Length - 1)];
        juiceIcon.SetActive(false);
        timer = Random.Range(5, 8);
    }

    IEnumerator DrinkWaiting()
    {
        yield return new WaitForSeconds(setDrinkWaitingTime);
        InitalizeParameter();
    }
}
