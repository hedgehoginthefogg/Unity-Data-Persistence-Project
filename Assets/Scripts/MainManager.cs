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
    // create variable to start best score text
    public Text BestScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
 
    
    private bool m_GameOver = false;


    // Start is called before the first frame update
    void Start()
    {
        LoadSavedScore();
        // 
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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(1);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {

        updateBestScore();
        // save the data
        DataHandler.Instance.SaveBestScore();
        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    // Method to update best score if the current score is better
    public void updateBestScore()
    {
        Debug.Log(BestScoreText.text);
         if (BestScoreText == null)
        {
            Debug.LogError("BestScoreText is not assigned!");
            return;
        }

        if (m_Points > DataHandler.Instance.bestScorePoints)
        { 
            // if current score is highest,store in DataHandler so it persists
            DataHandler.Instance.bestScorePoints = m_Points;
            // if current user name has best score, store it in best score username in DataHandler so persists
            DataHandler.Instance.bestScoreUserName = DataHandler.Instance.userName;
            // update on screen name
            BestScoreText.text = "Best Score: " + DataHandler.Instance.bestScoreUserName + " : " + DataHandler.Instance.bestScorePoints;
        }
    }

    public void LoadSavedScore()
    {
    // load best score
    DataHandler.Instance.LoadBestScore();
    // debug
    Debug.Log(DataHandler.Instance.bestScorePoints);
    Debug.Log(DataHandler.Instance.bestScoreUserName);

    // update text
    if (DataHandler.Instance.bestScoreUserName != null && DataHandler.Instance.bestScorePoints != null)
    {
        BestScoreText.text = "Best Score: " + DataHandler.Instance.bestScoreUserName + " : " + DataHandler.Instance.bestScorePoints;
    }

    }
}
