using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileChange : MonoBehaviour
{
    public GameObject iconList;
    public GameObject cardExit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        iconList.SetActive(true);
        cardExit.SetActive(true);
    }
}
