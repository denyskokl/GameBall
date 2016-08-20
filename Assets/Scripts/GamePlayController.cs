using UnityEngine;
using System;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _timerText;

    public int Score;


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
        _scoreText.text = "0";
        _timerText.text = "";
        Score = 0;
    }

    void Update()
    {
        _timerText.text = Time.time.ToString();
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

    public void UpdateScore(int points)
    {
        Score += points;
        _scoreText.text = Score.ToString();
    }
}