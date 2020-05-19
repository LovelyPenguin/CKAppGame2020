using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSpriteFlip : MonoBehaviour
{
    public SpriteRenderer sprite;
    public bool isLeftInitialize;
    public string direction;

    private float previousXpos;
    // Start is called before the first frame update
    void Start()
    {
        previousXpos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (previousXpos <= transform.position.x)
        {
            direction = "Right";
            sprite.flipX = isLeftInitialize;
        }
        else
        {
            direction = "Left";
            sprite.flipX = !isLeftInitialize;
        }

        previousXpos = transform.position.x;
    }
}
