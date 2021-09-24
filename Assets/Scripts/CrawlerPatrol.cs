using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerPatrol : MonoBehaviour
{
    

    protected Vector3 velocity;
    public Transform _transform;
    public float distanceLeft = 1f;
    public float distanceAway = 10f;
    public float speed = 1f;
    Vector3 _originalPosition;
    public bool isGoingLeft = false;
    public bool isGoingAway = false;
    public float xDistFromStart;
    public float zDistFromStart;

    // Start is called before the first frame update
    void Start()
    {
        

        _originalPosition = gameObject.transform.position;
        


        _transform = GetComponent<Transform>();
        velocity = new Vector3(speed, 0, speed);
        _transform.Translate(velocity.x * Time.deltaTime, 0, velocity.z * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        

        xDistFromStart = transform.position.x - _originalPosition.x;
        zDistFromStart = transform.position.z - _originalPosition.z;

        if (isGoingLeft)
        {
            // If gone too far, switch direction
            if (xDistFromStart < -distanceLeft)
                SwitchDirection();

            _transform.Translate(-velocity.x * Time.deltaTime, 0, 0);
            
        }
        else
        {
            // If gone too far, switch direction
            if (xDistFromStart > distanceLeft)
                SwitchDirection();

            _transform.Translate(velocity.x * Time.deltaTime, 0, 0);
        }

        if (isGoingAway)
        {
            // If gone too far, turnaround
            if (zDistFromStart < distanceAway)
                TurnAround();

            _transform.Translate(0, 0, velocity.z * Time.deltaTime);
            _transform.Rotate(0, -180, 0);
        }
        else
        {
            // If gone too far, turn around
            if (zDistFromStart > distanceAway)
                TurnAround();
            

            _transform.Translate(0, 0, velocity.z * Time.deltaTime);
            //_transform.Rotate(0, 180, 0);
        }
    }
    void SwitchDirection()
    {
        isGoingLeft = !isGoingLeft;
        
    }
    void TurnAround()
    {
        isGoingAway = !isGoingAway;
        
        //TODO: Change facing direction, animation, etc
    }
}
