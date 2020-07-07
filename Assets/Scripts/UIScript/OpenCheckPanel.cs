using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCheckPanel : MonoBehaviour
{
    private void Awake()
    {
        GameMng.Instance.openEvent.AddListener(Open);
        GameMng.Instance.closeEvent.AddListener(Close);
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LateStart());
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMng.Instance.getOpenData)
        {
            Open();
        }
        else
        {
            Close();
        }
    }

    public void Open()
    {
        gameObject.SetActive(false);
    }

    public void Close()
    {
        gameObject.SetActive(true);
    }

    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.5f);
        
    }
}
