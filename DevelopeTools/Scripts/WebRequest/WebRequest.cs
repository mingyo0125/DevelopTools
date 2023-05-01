using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequest : MonoBehaviour
{
    [SerializeField]
    private int pixelPerUnit = 16; //pixelPerUnit

    [SerializeField]
    private Vector2 pivot = Vector2.one * 0.5f; //pivot

    [SerializeField]
    private string url; //����� �̹��� ���� �ּ�


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
        UnityWebRequest req = UnityWebRequestTexture.GetTexture(url); //�ؽ��ĸ� �޾ƿ´�.

        yield return req.SendWebRequest(); //�����ٰ� ���� ��û�� ������ ��ٸ���.

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
            Debug.LogError("���� ����");
            Debug.LogError(req.responseCode);
            Debug.Log(req.error);
        }
    }
}
