using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LoadBackgrounds : MonoBehaviour
{
    public static LoadBackgrounds Instance;
    public List<Sprite> ListTextures;
    string urlPrefix = "http://denis@montana-games.org/AssetBundles/";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadImages(Action onSuccess)
    {
        StartCoroutine(LoadBundles(onSuccess));
    }

    IEnumerator LoadBundles(Action onSuccess)
    {
        while (!Caching.ready)
            yield return null;
        WWW www = new WWW("http://denis@montana-games.org/AssetBundles/AssetBundles");
        yield return www;

        AssetBundle bundle = www.assetBundle;

        AssetBundleManifest globalManifest = bundle.LoadAsset("AssetBundleManifest") as AssetBundleManifest;
        yield return globalManifest;
        string[] bundles = globalManifest.GetAllAssetBundles();

        foreach (string bundleName in bundles)
        {
            WWW wwwBundle = new WWW(urlPrefix + bundleName);
            yield return wwwBundle;

            AssetBundle bundleImage = wwwBundle.assetBundle;

            AssetBundleRequest request = bundleImage.LoadAssetAsync(bundleName, typeof(Texture2D));
            yield return request;

            Texture2D texture = request.asset as Texture2D;
            Sprite newSprite = Sprite.Create(texture as Texture2D, new Rect(0f, 0f, texture.width, texture.height), Vector2.zero);

            ListTextures.Add(newSprite);
        }

        if (ListTextures != null && ListTextures.Count > 0)
        {
            if (onSuccess != null) onSuccess();
        }
    }
}