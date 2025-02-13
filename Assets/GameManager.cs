using UnityEngine;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    private AudioSource m_AudioSource;
    public int leftScore, rightScore;
    public int lastToScoreSign = 1;

    [SerializeField]
    GameObject powerUp;

    [SerializeField]
    Renderer floorRenderer;

    [SerializeField]
    private AudioClip hitClip, scoreClip;

    [SerializeField]
    private Transform paddleL, paddleR;

    [SerializeField]
    private GameObject ballPrefab;

    private Vector3 initPaddleScale, initBallScale;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        DisplayScore();
        initPaddleScale = paddleL.transform.localScale;
        Debug.Log(initPaddleScale);
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

    public void HitSound(float paddlePercent) 
    {
        m_AudioSource.clip = hitClip;
        m_AudioSource.pitch = (paddlePercent + 1) * 5;
        Debug.Log("Paddle Lenght: " + paddlePercent);
        m_AudioSource.Play();
    }

    public void ScoreSound()
    {
        m_AudioSource.clip = scoreClip;
        m_AudioSource.pitch = 1;
        m_AudioSource.Play();
    }

    public void AddScore(int leftScoreToAdd, int rightScoreToAdd) 
    {
        leftScore += leftScoreToAdd;
        rightScore += rightScoreToAdd;
        DisplayScore();
        ScoreSound();
        m_AudioSource.pitch += .1f;
        //IncreaseDifficulty();
        Camera.main.GetComponent<CameraShake>().Shake();
        floorRenderer.material.color = RandomColor();
    }

    public void DisplayScore() 
    {
        scoreText.color = RandomColor();
        scoreText.text = leftScore + " - " + rightScore;
    }

    private Color RandomColor()
    {
        float r, g, b;
        r = Random.Range(0f, 1f);
        g = Random.Range(0f, 1f);
        b = Random.Range(0f, 1f);
        return new Color(r, g, b);
    }

    public void IncreaseDifficulty() 
    {
        paddleL.localScale = paddleL.localScale - Vector3.one * 0.05f;
        paddleR.localScale = paddleR.localScale - Vector3.one * 0.05f;

        DemoBall[] balls = FindObjectsByType<DemoBall>(FindObjectsSortMode.None);
        foreach(DemoBall ball in balls)
            ball.transform.localScale *= 0.995f;

        Time.timeScale += .1f; // makes game faster

    }

    public void ResetDifficulty()
    {
        paddleL.localScale = initPaddleScale;
        paddleR.localScale = initPaddleScale;
        

        DemoBall[] balls = FindObjectsByType<DemoBall>(FindObjectsSortMode.None);
        foreach (DemoBall ball in balls)
            Destroy(ball.gameObject);

        Time.timeScale = 1f; //reset timescale
        Instantiate(ballPrefab, new Vector3(0, 1.11f, 0), Quaternion.identity);
    }
    public void ResetGame()
    {
        ResetDifficulty();
        leftScore = 0;
        rightScore = 0;
        m_AudioSource.pitch = 0;
        DisplayScore();
    }

    
}
