using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private AudioSource m_AudioSource;
    public int leftScore, rightScore;
    public int lastToScoreSign = 1;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(leftScore >= 11)
        {
            Debug.Log("GameOver!");
            Debug.Log("Left Player Wins");
            ResetGame();
        }
        else if(rightScore >= 11)
        {
            Debug.Log("GameOver!");
            Debug.Log("Right Player Wins");
            ResetGame();
        }
    }

    public void AddScore(int leftScoreToAdd, int rightScoreToAdd) 
    {
        leftScore += leftScoreToAdd;
        rightScore += rightScoreToAdd;
        Debug.Log("Left Player | " + leftScore + " - " + rightScore + " | Right Player" );
        m_AudioSource.Play();
        m_AudioSource.pitch += .1f;
        Camera.main.GetComponent<CameraShake>().Shake();
    }

    public void ResetGame()
    {
        leftScore = 0;
        rightScore = 0;
        m_AudioSource.pitch = 0;
    }

    
}
