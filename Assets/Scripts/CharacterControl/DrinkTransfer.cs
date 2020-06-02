using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkTransfer : MonoBehaviour
{
    public bool isTransfer = false;
    public bool isDemandJuice;
    public GameObject particle;
    public string selectJuice;
    public GameObject juiceIcon;
    public float setDrinkWaitingTime;

    private float setTimer;
    private string[] juiceList;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        if (particle != null)
        {
            particle.SetActive(false);
        }
        juiceIcon.SetActive(false);
        juiceList = GameMng.Instance.GetComponent<JuiceList>().juiceList;

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
        if (selectJuice == juiceList[0])
        {
            juiceIcon.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        if (selectJuice == juiceList[1])
        {
            juiceIcon.GetComponent<SpriteRenderer>().color = Color.red;
        }
        if (selectJuice == juiceList[2])
        {
            juiceIcon.GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }

    private void InitalizeParameter()
    {
        isTransfer = true;
        isDemandJuice = false;
        selectJuice = juiceList[Random.Range(0, juiceList.Length - 1)];
        juiceIcon.SetActive(false);
        timer = Random.Range(10, 30);
    }

    IEnumerator DrinkWaiting()
    {
        yield return new WaitForSeconds(setDrinkWaitingTime);
        InitalizeParameter();
    }
}
