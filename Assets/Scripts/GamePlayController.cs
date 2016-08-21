using UnityEngine;
using System;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _timerText;

    [SerializeField]

    private GameObject _gameWindow;
   
    public int Score;

    public bool isPause = false;

    public static GamePlayController Instance;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        ResetData();
    }

    void Update()
    {
        if (isPause) return;
        _timerText.text = string.Format("{0}", (int)Time.timeSinceLevelLoad);
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null)
            {
                hit.collider.GetComponent<Circle>().Push();
            }
        }
    }

    public void ResetData()
    {
        _scoreText.text = "0";
        _timerText.text = "0";
        
        Score = 0;
    }

    public void UpdateScore(int points)
    {
        Score += points;
        _scoreText.text = Score.ToString();
    }
    public void PauseGame()
    {
        isPause = true;
        ResetData();
        _gameWindow.SetActive(true);
    }

    public void PLayLevel()
    {
        Application.LoadLevel(Application.loadedLevelName);
    }

}