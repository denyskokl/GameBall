using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GeneratorItems : MonoBehaviour
{
    [SerializeField]
    private GameObject _circleObject;

    private Vector3 _cameraBorder;

    private float _randowValue;

    [SerializeField]
    private float _minValue;

    [SerializeField]
    private float _maxValue;

    [SerializeField]
    private Circle[] _objects;

    void Awake()
    {
        _objects = FindObjectsOfType<Circle>();
    }

    void Start()
    {
        _cameraBorder = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        StartCoroutine(GenerateCircle());
    }

    IEnumerator GenerateCircle()
    {
        while (true)
        {
            yield return new WaitForSeconds(_randowValue);
            Generate();
        }
    }

    public void Generate()
    {
        if (GamePlayController.Instance.isPause) return;
        _randowValue = Random.Range(0.1f, 3f);
        foreach (var item in _objects)
        {
            if (item.isReady)
            {
                item.isReady = false;
                float posX = Random.Range(_cameraBorder.x, -_cameraBorder.x);
                item.Init(_minValue, _maxValue);
                item.transform.position = new Vector3(posX, _cameraBorder.y + 2f, 0);
                return;
            }
        }
    }

  
}
