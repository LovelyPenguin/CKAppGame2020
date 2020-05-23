using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkTransfer : MonoBehaviour
{
    public bool isTransfer = false;
    public GameObject particle;
    // Start is called before the first frame update
    void Start()
    {
        if (particle != null)
        {
            particle.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Detect(int money = 100)
    {
        //particle.transform.position = transform.position;
        GameMng.Instance.money += money;
        ParticleManager(true);
        isTransfer = true;
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
}
