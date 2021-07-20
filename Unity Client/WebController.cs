using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class WebController : MonoBehaviour
{
    #region SINGLETON
    private static WebController instance;

    public static WebController Instance
    {
        get => instance;
    }
    #endregion

    [SerializeField] private int id;
    [SerializeField] private string ownerName;
    [SerializeField] private string townName;

    private void Awake()
    {
        instance = this;
    }

    public IEnumerator HttpRequestJson(string url, object obj)
    {
        string jsonString = JsonUtility.ToJson(obj);
        Debug.Log(jsonString);
        byte[] jsonData = Encoding.UTF8.GetBytes(jsonString);

        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(jsonData);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Accept", "application/json");
            request.uploadHandler.contentType = "application/json";

            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log("Post Request Complete!");
            }
        }
    }
}




