using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectContor : MonoBehaviour
{
    public float speed = 3;
    private Vector3 playerPosition;
    // Start is called before the first frame update
    void Start()
    {
        playerPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            playerPosition.x -= speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            playerPosition.x += speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            playerPosition.z += speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            playerPosition.z -= speed * Time.deltaTime;
        }

        transform.position = playerPosition;
    }
}
