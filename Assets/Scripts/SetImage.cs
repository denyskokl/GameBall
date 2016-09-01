using UnityEngine;

public class SetImage : MonoBehaviour
{

    [SerializeField]
    private GameObject _bg;

    private SpriteRenderer _sprite;

    void Awake()
    {
        _sprite = _bg.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        var loadBg = LoadBackgrounds.Instance;
        if (loadBg.ListTextures != null && loadBg.ListTextures.Count > 0)
        {
            SetImages();
            return;
        }
        else
        {
            loadBg.LoadImages(() =>
           {
               SetImages();
           });
        }
    }

    void SetImages()
    {
        var listTextures = LoadBackgrounds.Instance.ListTextures;
        _sprite.sprite = listTextures[Random.Range(0, listTextures.Count)];

        transform.localScale = Vector3.one;

        var width = _sprite.sprite.bounds.size.x;
        var height = _sprite.sprite.bounds.size.y;
        _sprite.sortingOrder = -1;
        var worldScreenHeight = Camera.main.orthographicSize * 2.0;
        var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        var posX = (float)(worldScreenWidth / width);
        var posY = (float)(worldScreenHeight / height);
        transform.localScale = new Vector3(posX, posY, 0f);
    }
}
