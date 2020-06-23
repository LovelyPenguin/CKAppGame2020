using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeGauge : MonoBehaviour
{
    [Range(0, 15), SerializeField]
    private float timer;
    private Image img;
    private float setOpenTime;

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        img.fillAmount = 1 / (GameMng.Instance.setOpenTime / GameMng.Instance.openTime);
    }
}
