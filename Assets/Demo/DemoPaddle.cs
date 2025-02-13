using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DemoPaddle : MonoBehaviour
{
    public float unitsPerSecond = 3f;
    public string axisName;

    private GameManager gameManager;

    [SerializeField]
    private DemoBall ball;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
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

        // get the point on the paddle where the ball hits
        float dist = (collision.gameObject.transform.position.x - transform.localPosition.x) % transform.lossyScale.x;
        gameManager.HitSound(dist);
        Debug.Log($"we hit {collision.gameObject.name}");
        
        // get reference to paddle collider
        BoxCollider bc = GetComponent<BoxCollider>();
        Bounds bounds = bc.bounds;
        float maxX = bounds.max.x;
        float minX = bounds.min.x;

        Debug.Log($"maxX = {maxX}, minY = {minX}");
        Debug.Log($"x pos of ball is {collision.transform.position.x}");
     
        ball.SpeedBallUp();
    }
}

