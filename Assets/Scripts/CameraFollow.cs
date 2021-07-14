using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Vector3 offset;
    Transform ball;
    
    // Start is called before the first frame update
    void Start()
    {
        ball = FindObjectOfType<BallController>().transform;
        offset = ball.position-transform.position;
    }

    void LateUpdate()
    {
        transform.position = ball.position - offset;
    }
}
