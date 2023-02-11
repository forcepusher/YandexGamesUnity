using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Agava.YandexGames
{
    public class RemoteImage
    {
        private string _url;

        public RemoteImage(string url)
        {

        }

        //private IEnumerator DownloadAndSetProductImage(string imageUri)
        //{
        //    using (UnityWebRequest textureRequest = UnityWebRequestTexture.GetTexture(imageUri))
        //    {
        //        yield return textureRequest.SendWebRequest();
        //        _productImage.texture = DownloadHandlerTexture.GetContent(textureRequest);
        //    }
        //}

        public async void Download(string imageUrl, Action<Texture2D> successCallback = null, Action<string> errorCallback = null, CancellationToken cancellationToken = default)
        {
            using (UnityWebRequest downloadTextureWebRequest = UnityWebRequestTexture.GetTexture(imageUrl))
            {
                UnityWebRequestAsyncOperation downloadOperation = downloadTextureWebRequest.SendWebRequest();

                while (!downloadOperation.isDone)
                    await Task.Yield();

                Texture2D texture = DownloadHandlerTexture.GetContent(downloadTextureWebRequest);

                successCallback?.Invoke(texture);
            }
        }
    }
}
