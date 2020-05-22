using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSpriteFlip : MonoBehaviour
{
    public SpriteRenderer sprite;
    public bool isLeftInitialize;
    public string direction;

    private Vector3 previousPos;
    // Start is called before the first frame update
    void Start()
    {
        // 월드 좌표
        //previousPos = transform.position;

        // 로컬 좌표
        previousPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // 월드 좌표 감식
        //if (previousPos.x <= transform.position.x && previousPos.z <= transform.position.z)
        //{
        //    direction = "Right";
        //    sprite.flipX = isLeftInitialize;
        //}
        //else
        //{
        //    direction = "Left";
        //    sprite.flipX = !isLeftInitialize;
        //}

        //previousPos = transform.position;

        // 로컬 좌표 감식
        if (previousPos.x < transform.localPosition.x)
        {
            direction = "Right";
            sprite.flipX = isLeftInitialize;
        }
        else if (previousPos.x > transform.localPosition.x)
        {
            direction = "Left";
            sprite.flipX = !isLeftInitialize;
        }

        previousPos = transform.localPosition;
    }
}
