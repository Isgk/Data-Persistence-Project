using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text bestScoreText;
    public Text playerNameText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    private int hPoints;
    
    private bool m_GameOver = false;
    
    public string nameToDIsplay;


    // Start is called before the first frame update
    void Start()
    {
        
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
                
            }
        }
        PlayerName();
        GameDataManager.dataManager.LoadBestScore();



    }

    private void Update()
    {
        if (!m_Started)
        {
           
            

            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
                

            }
        }
        else if (m_GameOver)
        {
            GameDataManager.dataManager.SaveBestScore();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        UpdateBestScore(GameDataManager.dataManager.bestScore, GameDataManager.dataManager.bestName);
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    public void PlayerName()
    {
         nameToDIsplay = GameDataManager.dataManager.playerNameIn;
        playerNameText.text = "Name :" + nameToDIsplay;

    }
    public void UpdateBestScore(int highScore , string currentPlayer)
    {
        currentPlayer = GameDataManager.dataManager.bestName;
        highScore = GameDataManager.dataManager.bestScore;
        if (GameDataManager.dataManager.bestScore < m_Points)
        {
            GameDataManager.dataManager.bestScore = m_Points;
            GameDataManager.dataManager.bestName = nameToDIsplay;
            
            

            Debug.Log(GameDataManager.dataManager.bestScore);
        }
        

        bestScoreText.text = "Best Score: "+ GameDataManager.dataManager.bestName + " : " +  GameDataManager.dataManager.bestScore;
    }



}
