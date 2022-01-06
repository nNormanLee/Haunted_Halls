using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningKey : MonoBehaviour
{
    public GameObject player;
    public float keySpeed;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        if(distance <= 4 && gameObject.transform.position.z <= 3)
        {
            transform.position += new Vector3(0, 0, keySpeed);
        }
        else if(distance <= 4 && gameObject.transform.position.z >= 3 && gameObject.transform.position.x <= -10)
        {
            transform.position += new Vector3(keySpeed, 0, 0);
        }
    }

    public void Test()
    {
        Debug.Log("It works");
    }
}
