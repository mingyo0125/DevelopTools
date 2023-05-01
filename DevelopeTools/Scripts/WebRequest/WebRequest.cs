using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequest : MonoBehaviour
{
    [SerializeField]
    private string url; //사용할 이미지 파일 주소
    
    [SerializeField]
    private int pixelPerUnit = 16; //pixelPerUnit

    [SerializeField]
    private Vector2 pivot = Vector2.one * 0.5f; //pivot

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(DownloadTexture());
            //StartCoroutine(GetJsonData());
        }
    }

    private IEnumerator GetJsonData()
    {
        UnityWebRequest req = UnityWebRequest.Get(url);

        yield return req.SendWebRequest();

        Debug.Log(req.downloadHandler.text);

        string jsonText = req.downloadHandler.text;

        LOLItemJson json = JsonUtility.FromJson<LOLItemJson>(jsonText);

        Debug.Log($"{json.version}, {json.type}");
    }

    private IEnumerator DownloadTexture()
    {
        UnityWebRequest req = UnityWebRequestTexture.GetTexture(url); //텍스쳐를 받아온다.

        yield return req.SendWebRequest(); //웹에다가 전송 요청을 보내고 기다린다.

        if(req.result == UnityWebRequest.Result.Success)
        {
            var texture = DownloadHandlerTexture.GetContent(req);

            Debug.Log(texture);
            float w = texture.width;
            float h = texture.height;
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, w, h), pivot, pixelPerUnit); //texture, rect, pivot, pixelperUnit

            gameObject.AddComponent<SpriteRenderer>().sprite = sprite;
        }
        else
        {
            Debug.LogError("전송 실패");
            Debug.LogError(req.responseCode);
            Debug.Log(req.error);
        }
    }
}
