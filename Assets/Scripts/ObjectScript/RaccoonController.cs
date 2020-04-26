using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RaccoonController : MonoBehaviour
{
    public float stamina = 100;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.rotation = Camera.main.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > 0)
        {
            transform.position = transform.position - new Vector3(0, Time.deltaTime * 2, 0);
            if (transform.position.y < 0) transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        this.transform.rotation = Camera.main.transform.rotation;
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.name == "HealMap")
        {
            Debug.Log("Triggered!");
            this.stamina += 10;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}
