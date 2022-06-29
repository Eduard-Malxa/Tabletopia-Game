using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using System.IO;

public class LoadMaterialTexture : MonoBehaviour
{
    [SerializeField]
    private Renderer meshTypeRenderer;

    [SerializeField]
    private int textureIndex;

    private string url;

    public int SetTextureIndex(int value)
    {
        return textureIndex = value;
    }

    public void LoadTexture(int value)
    {
        SetTextureIndex(value);
        StartCoroutine(LoadTextureCor());
    }

    private IEnumerator LoadTextureCor()
    {
        url = Path.Combine(Application.streamingAssetsPath + "/Textures/", "Texture_" + textureIndex + ".png");

        byte[] imageData;
        Texture2D texture = new Texture2D(2, 2);

        if (url.Contains("://") || url.Contains(":///"))
        {
            UnityWebRequest www = UnityWebRequest.Get(url);
            yield return www.SendWebRequest();
            imageData = www.downloadHandler.data;
        }
        else
        {
            imageData = File.ReadAllBytes(url);
        }

        texture.LoadImage(imageData);
        meshTypeRenderer.material.SetTexture("_MainTex", texture);
    }
}
