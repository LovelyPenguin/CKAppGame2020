using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCheckPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameMng.Instance.openEvent.AddListener(Open);
        GameMng.Instance.closeEvent.AddListener(Close);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        gameObject.SetActive(false);
    }

    public void Close()
    {
        gameObject.SetActive(true);
    }
}
