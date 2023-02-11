using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Agava.YandexGames
{
    public class RemoteImage
    {
        private readonly string _url;

        private Texture2D _texture;
        public Texture2D Texture
        {
            get
            {
                if (!IsDownloadFinished)
                    throw new InvalidOperationException($"Attempt to get {nameof(Texture)} while {nameof(IsDownloadFinished)} = {IsDownloadFinished}");

                if (!IsDownloadSuccessful)
                    throw new InvalidOperationException($"Attempt to get {nameof(Texture)} while {nameof(IsDownloadSuccessful)} = {IsDownloadSuccessful}");

                return _texture;
            }
            private set
            {
                _texture = value;
            }
        }

        public bool IsDownloadFinished { get; private set; }

        public bool IsDownloadSuccessful { get; private set; }

        public string DownloadErrorMessage { get; private set; }

        public RemoteImage(string url)
        {
            _url= url;
        }

        public async Task Download(Action<Texture2D> successCallback = null, Action<string> errorCallback = null, CancellationToken cancellationToken = default)
        {
            using (UnityWebRequest downloadTextureWebRequest = UnityWebRequestTexture.GetTexture(_url))
            {
                UnityWebRequestAsyncOperation downloadOperation = downloadTextureWebRequest.SendWebRequest();

                while (!downloadOperation.isDone)
                    await Task.Yield();

                if (downloadOperation.webRequest.result != UnityWebRequest.Result.Success)
                    DownloadErrorMessage = downloadOperation.webRequest.error;
                
                Texture = DownloadHandlerTexture.GetContent(downloadTextureWebRequest);
            }

            if (IsDownloadSuccessful)
                successCallback?.Invoke(Texture);
            else
                errorCallback?.Invoke(DownloadErrorMessage);
        }

        //private IEnumerator DownloadAndSetProductImage(string imageUri)
        //{
        //    using (UnityWebRequest textureRequest = UnityWebRequestTexture.GetTexture(imageUri))
        //    {
        //        yield return textureRequest.SendWebRequest();
        //        _productImage.texture = DownloadHandlerTexture.GetContent(textureRequest);
        //    }
        //}
    }
}
