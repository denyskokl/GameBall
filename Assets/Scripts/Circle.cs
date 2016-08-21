using UnityEngine;
using System.Collections;

public class Circle : MonoBehaviour
{
    public bool isReady = false;

    private Rigidbody2D _rb;

    private SpriteRenderer _spriteRenderer;

    private Collider2D _collider;

    private float drag = 5;

    private float _random;

    private int _defaultScore = 10;

    private int _points;

    void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(GamePlayController.Instance.isPause)
        {
            ResetItem();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("BottomLine"))
        {
            isReady = true;
            _collider.enabled = true;
        }
    }

    public void Push()
    {
        GamePlayController.Instance.UpdateScore(_points);
        ResetItem();
    }

    public void ResetItem()
    {
        _spriteRenderer.enabled = false;
        _collider.enabled = false;
    }

    public void Init(float _minValue, float _maxValue)
    {
        _spriteRenderer.enabled = true;
        _points = 0;
        _random = Random.Range(_minValue, _maxValue);
        RandomColor();
        RandomSize(_minValue, _maxValue, _random);
        _rb.velocity = Vector3.zero;
    }


    public void RandomColor()
    {
        _spriteRenderer.enabled = true;
        _spriteRenderer.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
    }

    public void RandomSize(float _minValue, float _maxValue, float random)
    {
        _spriteRenderer.transform.localScale = new Vector3(random, random, 0);
        _rb.drag = _random * drag;
        _points = Mathf.RoundToInt(_defaultScore / random);
    }
}
