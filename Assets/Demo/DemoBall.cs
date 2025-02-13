using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoBall : MonoBehaviour
{
    Rigidbody ballrB;
    public float forceMagnitude = 1f;
    public float speedMultiplier = 1.1f;

    public float minVelocity = 10;

    [SerializeField]
    GameObject ballVFX;

    [SerializeField]GameObject ballGO;
    GameManager gameManager;

    AIPaddle aiPaddle;

    bool hasScored = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        ballrB = GetComponent<Rigidbody>();
        RandomColor();
        StartBall(gameManager.lastToScoreSign);
        aiPaddle = FindAnyObjectByType<AIPaddle>();
        aiPaddle.SetBall(this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartBall(int directionSign)
    {
        ballrB.AddForce(Vector3.forward * forceMagnitude * gameManager.lastToScoreSign, ForceMode.VelocityChange);
    }

    private void FixedUpdate()
    {
        //Clamp velocity
        if (ballrB.linearVelocity.magnitude < minVelocity)
        {
            Debug.Log("Velocity is: " + ballrB.linearVelocity.magnitude);
            ballrB.AddForce(ballrB.linearVelocity.normalized * minVelocity, ForceMode.Impulse);
        }

    }
    public void SpeedBallUp()
    {
       if(ballrB)
        ballrB.linearVelocity *= speedMultiplier;
    }

    public void SpeedBallUp(float _speedMultiplier)
    {
        ballrB.linearVelocity *= _speedMultiplier;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RespawnL") && !hasScored) 
        {
            gameManager.AddScore(0, 1);
            gameManager.lastToScoreSign = 1;
            Instantiate(ballGO, new Vector3(0, 1.11f, 0), Quaternion.identity);
            hasScored = true;
            Debug.Log("Right Player Just Scored!");
            Destroy(this.gameObject);
        }

        if (other.CompareTag("RespawnR") && !hasScored)
        {
            gameManager.AddScore(1, 0);
            gameManager.lastToScoreSign = -1;
            Debug.Log("Respawned");
            Instantiate(ballGO, new Vector3(0, 1.11f, 0), Quaternion.identity);
            hasScored = true;
            Debug.Log("Left Player Just Scored!");
            Destroy(this.gameObject);
        }

        if (other.CompareTag("PowerUp"))
        {
            gameManager.IncreaseDifficulty();
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(Instantiate(ballVFX, transform.position, Quaternion.identity), 2f);  
        SpeedBallUp(1.2f);
    }

    private void RandomColor() 
    {
        float r, g, b;
        r = Random.Range(0f, 1f);
        g = Random.Range(0f, 1f);
        b = Random.Range(0f, 1f);
        GetComponent<Renderer>().material.color = new Color(r, g, b);
    }

}
