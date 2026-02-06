using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Game
{
    public class WebRequest : MonoBehaviour
    {
        private string url = "https://dog.ceo/api/breeds/image/random";
        [SerializeField] private RawImage prefab;
        [SerializeField] private Transform parent;
        private string _imageUrl = "";
        private RawImage currentRawImage;
        void Start()
        {
            for (int i = 0; i < 10; i++)
            {
                
                StartCoroutine(GetData());
            }
        }

        // Reuqest enq anum u stanum enq message um nkari linqy
        IEnumerator GetData()
        {
                using (UnityWebRequest request = UnityWebRequest.Get(url))
                {
                    yield return request.SendWebRequest();  // Send asynchron 

                    if (request.isNetworkError || request.isHttpError)
                    {
                        Debug.Log("Error" + request.error);
                    }
                    else
                    {
                        string jsonResponse = request.downloadHandler.text;
                        
                        Root root = JsonUtility.FromJson<Root>(jsonResponse);
                        _imageUrl = root.message;  // nkari linqna
                        Debug.Log(_imageUrl);
                        // Kanchum enq download y 
                        StartCoroutine(GetTexture(_imageUrl));
                    }
                }
            
        }
        
        
        // Request enq anum vor qashenq linqum gtnvox nkary 
        IEnumerator GetTexture(string imageUrl)
        {
            using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl))
            {
                yield return request.SendWebRequest(); 
            
                if (request.isNetworkError || request.isHttpError)
                {
                    Debug.Log("Error" + request.error);
                }

                else
                {
                    // DownloadHandlerTexture y en texna vortex requesti ardyunqum stacvac kontenty pahum enq
                    Texture2D texture = DownloadHandlerTexture.GetContent(request);
                    Debug.Log("Load texture from :" + imageUrl);
                    var currentRawImage = Instantiate(prefab, parent);  // Instainate y en funkciana vori shnorhiv menq karoxanum enq prefabnery stexcenq ev dnenq scenayum 
                    currentRawImage.texture = texture;
                }
            };
        }
        
        // IEnumerator MakePostRequest()
        // {
        //     WWWForm form = new WWWForm();
        //     form.AddField("title", "aoo");
        //     form.AddField("body", "soo");
        //     form.AddField("userId","1");
        //     
        //     UnityWebRequest request = UnityWebRequest.Post(url, form);
        //     yield return request.SendWebRequest();
        //     
        //     if (request.isNetworkError || request.isHttpError)
        //     {
        //         Debug.Log("Error" + request.error);
        //     }
        //     else
        //     {
        //         Debug.Log("Response" + request.downloadHandler.text);
        //     }
        // }
        
        // IEnumerator MakePostRequest()
        // {
        //     Root postData = new Root()
        //     {
        //         userId = 1,
        //         title = "asw",
        //         body = "awq",
        //     };
        //     
        //     string json = JsonUtility.ToJson(postData);
        //     UnityWebRequest request = UnityWebRequest.Post(url, json);
        //     byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        //     request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        //     request.downloadHandler = new DownloadHandlerBuffer();
        //     request.SetRequestHeader("Content-Type", "application/json");
        //     
        //     yield return request.SendWebRequest();
        //     
        //     if (request.isNetworkError || request.isHttpError)
        //     {
        //         Debug.Log("Error" + request.error);
        //     }
        //     else
        //     {
        //         Debug.Log("Response" + request.downloadHandler.text);
        //     }
        // }
    }

    [System.Serializable]
    public class Root
    {
        public string message;
        public string status;
    }
}


