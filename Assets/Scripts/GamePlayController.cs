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

    [SerializeField]
    private GameObject _lvlUpWindow;

    [SerializeField]
    private Text _currentLevel;

    private int _score;

    public bool IsPause = false;

    public static GamePlayController Instance;

    public static int Level = 1;

    public static int Score;

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
        _currentLevel.text = string.Format("Level {0}", Level + 1);
        ResetData();
        Score += 100;
    }

    void Update()
    {
        if (IsPause) return;
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
        if(Score <= _score)
        {
            LevelUpOpenWindow();
        }
    }

    public void ResetData()
    {
        _scoreText.text = "0";
        _timerText.text = "0";

        _score = 0;
       
    }

    public void UpdateScore(int points)
    {
        _score += points;
        _scoreText.text = _score.ToString();
    }
    public void PauseGame(GameObject window)
    {
        IsPause = true;
        ResetData();
        window.SetActive(true);
    }

    public void PlayLevel()
    {
        Debug.Log(Level + " level");
        Application.LoadLevel(Application.loadedLevelName);
    }

    public void LevelUpOpenWindow()
    {
        Level++;
        
        PauseGame(_lvlUpWindow);
    }

    public void LvlUp()
    {
        PlayLevel();
    }


}