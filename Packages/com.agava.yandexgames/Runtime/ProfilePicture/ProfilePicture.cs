using System;
using System.Threading;
using UnityEngine;

namespace Agava.YandexGames
{
    public static class ProfilePicture
    {
        public static async void Fetch(string url, Action<Texture2D> successCallback = null, Action<string> errorCallback = null, ProfilePictureSize size = ProfilePictureSize.medium, CancellationToken cancellationToken = default)
        {
            var profilePicture = new RemoteImage(url);

            await profilePicture.Download();

            if (profilePicture.IsDownloadSuccessful)
                successCallback?.Invoke(profilePicture.Texture);
            else
                errorCallback?.Invoke(profilePicture.DownloadErrorMessage);
        }
    }
}
