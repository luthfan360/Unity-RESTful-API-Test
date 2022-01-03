using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Fetch : MonoBehaviour
{

    Image buttonImage;
    Text buttonText;
    Texture2D textureCache;
    int randomInt;
    Data data = null;
    string MediaUrl = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/";
    string DataUrl = "https://pokeapi.co/api/v2/pokemon/";

    void Start()
    {
        buttonImage = GameObject.Find("Display").GetComponent<Image>();
        buttonText = GameObject.Find("NameText").GetComponent<Text>();
        randomize();
    }

    public void randomize()
    {
        randomInt = Random.Range(1, 808);
        StartCoroutine(DownloadImage(MediaUrl));
        StartCoroutine(GetName(DataUrl));
    }

    IEnumerator DownloadImage(string MediaUrl)
    {   
        string NewMediaUrl = MediaUrl + randomInt + ".png";
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(NewMediaUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(request.error);
        }   
        else
        {
            textureCache = DownloadHandlerTexture.GetContent(request);
            buttonImage.sprite = Sprite.Create(textureCache, new Rect(0, 0, textureCache.width, textureCache.height), new Vector2(0.5f, 0.5f), 100.0f);
        }       
    }

    IEnumerator GetName(string DataUrl)
    {
        string NewDataUrl = DataUrl + randomInt;
        UnityWebRequest request = UnityWebRequest.Get(NewDataUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError) 
        {
            Debug.Log(request.error);
        }
        else
        {
            string json = request.downloadHandler.text;
            data = JsonUtility.FromJson<Data>(json);
            string pokeName = data.name;
            buttonText.text = char.ToUpper(pokeName[0]) + pokeName.Substring(1);
        }

    }
}
