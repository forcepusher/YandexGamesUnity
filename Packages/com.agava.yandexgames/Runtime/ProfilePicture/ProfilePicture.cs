using System;
using System.Threading;
using UnityEngine;

namespace Agava.YandexGames
{
    public static class ProfilePicture
    {
        public static void Fetch(string url, Action<Texture2D> successCallback = null, Action<string> errorCallback = null, ProfilePictureSize size = ProfilePictureSize.medium, CancellationToken cancellationToken = default)
        {
            //RemoteImage
        }
    }
}
