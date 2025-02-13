using UnityEngine;

public class AIPaddle : MonoBehaviour
{
    private Transform ball;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(ball.position.x, transform.position.y, transform.position.z);    
    }

    public void SetBall(Transform _ball) 
    {
        ball = _ball;
    }
}
