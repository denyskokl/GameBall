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

    private float factore = 1f;

    void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        factore = factore / GamePlayController.Level;
        Debug.Log(factore + " factore " + GamePlayController.Level + " level");
    }

    void Update()
    {
        if (GamePlayController.Instance.IsPause)
        {
            DisableItem();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("BottomLine"))
        {
            isReady = true;
            _rb.isKinematic = true;
        }
    }

    public void Push()
    {
        GamePlayController.Instance.UpdateScore(_points);
        DisableItem();
    }

    public void DisableItem()
    {
        isReady = true;
        _spriteRenderer.enabled = false;
        _collider.enabled = false;
    }

    public void Init(float _minValue, float _maxValue)
    {
        _rb.isKinematic = false;
        _collider.enabled = true;
        _spriteRenderer.enabled = true;
        _points = 0;
        _random = Random.Range(_minValue, _maxValue);
        InitRandomItem(_minValue, _maxValue, _random);
        _rb.velocity = Vector3.zero;
    }

    public void InitRandomItem(float _minValue, float _maxValue, float random)
    {
        _spriteRenderer.enabled = true;
        _spriteRenderer.transform.localScale = new Vector3(random, random, 0);
        _spriteRenderer.color = new Color(Random.Range(0f, random), Random.Range(0f, random), Random.Range(0f, random), 1f);
        _rb.drag = _random / drag* factore;
        _points = Mathf.RoundToInt(_defaultScore / random);
        Debug.Log(_rb.drag + " drag");
    }
}
