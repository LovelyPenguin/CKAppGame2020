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
    public GameObject juiceSpeechBubble;
    public float setDrinkWaitingTime;

    private float setTimer;
    private string[] juiceList = new string[6];
    private bool[] juiceUnlockList = new bool[6];
    private float timer;

    public GameObject loveIcon;
    public GameObject hateIcon;

    public AudioClip clip;
    private AudioSource audio;

    //public float[] itemDropPercent = new float[3];

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
        juiceSpeechBubble.SetActive(false);
        for (int i = 0; i < 6; i++)
        {
            juiceList[i] = GameMng.Instance.GetComponent<DrinkMng>().juiceList[i].juice.GetComponent<JuiceObject>().juiceName;
            juiceUnlockList[i] = GameMng.Instance.GetComponent<DrinkMng>().juiceList[i].isUnlock;
        }

        setTimer = Random.Range(5, 20);
        timer = setTimer;

        int randomIndex = Random.Range(0, juiceList.Length);
        do
        {
            randomIndex = Random.Range(0, juiceList.Length);
        } while (juiceUnlockList[randomIndex] == false);

        selectJuice = juiceList[randomIndex];

        isDemandJuice = false;

        audio = GetComponent<AudioSource>();
        audio.clip = clip;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Customer>().isActive)
        {
            DemandJuice();
        }
    }

    public void Detect(float percent, int money = 100)
    {
        if (isDemandJuice)
        {
            loveIcon.SetActive(true);
            audio.Play();
        }
        else if (isDemandJuice && percent < 50)
        {
            WrongDrink();
            audio.Play();
        }
        GameMng.Instance.money += money;
        GetComponent<Customer>().money += money;
        ParticleManager(true);
        InitalizeParameter();
        StartCoroutine(Disable());
    }

    public void WrongDrink()
    {
        if (isDemandJuice)
        {
            InitalizeParameter();
            Debug.Log("Wrong");
            hateIcon.SetActive(true);
            audio.Play();
        }
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
            juiceSpeechBubble.SetActive(true);
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

        for (int i = 0; i < 6; i++)
        {
            juiceUnlockList[i] = GameMng.Instance.GetComponent<DrinkMng>().juiceList[i].isUnlock;
        }
        int randomIndex = Random.Range(0, juiceList.Length);
        do
        {
            randomIndex = Random.Range(0, juiceList.Length);
        } while (juiceUnlockList[randomIndex] == false);

        selectJuice = juiceList[randomIndex];

        juiceIcon.SetActive(false);
        juiceSpeechBubble.SetActive(false);
        timer = Random.Range(5, 20);
    }

    IEnumerator DrinkWaiting()
    {
        yield return new WaitForSeconds(setDrinkWaitingTime);
        InitalizeParameter();
    }
}
