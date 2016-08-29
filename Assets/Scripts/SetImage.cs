using UnityEngine;
using System.Collections;

public class SetImage : MonoBehaviour {

	[SerializeField]
	private GameObject _bg;
	private SpriteRenderer _sprite;

	void Awake()
	{
		_sprite = _bg.GetComponent<SpriteRenderer> ();
	}

	void Start () {

		var loadBg = LoadBackgrounds.ins;
		if (loadBg.listTextures != null && loadBg.listTextures.Count > 0) {
			SetImages ();
			return;
		}

		else {
			loadBg.LoadImages (() =>
			{
				SetImages();	
			});
		}
	}

	void SetImages()
	{
		var listTextures = LoadBackgrounds.ins.listTextures;
		_sprite.sprite = listTextures [UnityEngine.Random.Range (0, listTextures.Count + 1)];
	
		transform.localScale = Vector3.one;

		var width = _sprite.sprite.bounds.size.x;
		var height = _sprite.sprite.bounds.size.y;

		var worldScreenHeight = Camera.main.orthographicSize * 2.0;
		var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

		var posX = (float)(worldScreenWidth / width);
		var posY = (float)(worldScreenHeight / height);

		transform.localScale = new Vector3 (posX, posY, 0f);

	}
}
