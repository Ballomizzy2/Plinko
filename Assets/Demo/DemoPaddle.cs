using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DemoPaddle : MonoBehaviour
{
    public float unitsPerSecond = 3f;
    public string axisName;

    [SerializeField]
    private DemoBall ball;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        float horizontalValue = Input.GetAxis(axisName);
        Vector3 force = Vector3.right * horizontalValue * unitsPerSecond * Time.deltaTime;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(force, ForceMode.Force);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ball"))
            return;

        Debug.Log($"we hit {collision.gameObject.name}");
        
        // get reference to paddle collider
        BoxCollider bc = GetComponent<BoxCollider>();
        Bounds bounds = bc.bounds;
        float maxX = bounds.max.x;
        float minX = bounds.min.x;

        Debug.Log($"maxX = {maxX}, minY = {minX}");
        Debug.Log($"x pos of ball is {collision.transform.position.x}");


        Rigidbody ballRb = collision.rigidbody;
        Vector3 ballPosition = collision.transform.position;
        Vector3 paddlePosition = transform.position;

        float hitFactorX = (ballPosition.x - paddlePosition.x) / GetComponent<BoxCollider>().bounds.extents.x;
        float hitFactorZ = (ballPosition.z - paddlePosition.z) / GetComponent<BoxCollider>().bounds.extents.z;
        

        Quaternion rotation = Quaternion.Euler(hitFactorX, 0f, hitFactorZ * 2);
        Vector3 bounceDirection = rotation * Vector3.up;

        Rigidbody rb = collision.rigidbody;
        rb.AddForce(bounceDirection * 10f, ForceMode.Impulse);

        ball.SpeedBallUp();
    }
}

