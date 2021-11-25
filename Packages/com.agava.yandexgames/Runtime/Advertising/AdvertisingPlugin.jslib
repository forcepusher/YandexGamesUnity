const advertisingLibrary = {
  // External C# call.
  ShowInterestialAd: function() {
    window['YandexGamesSdk'].showFullscreenAdv();
  },

  // External C# call.
  ShowVideoAd: function() {
    window['YandexGamesSdk'].showRewardedVideo();
  },
}

mergeInto(LibraryManager.library, advertisingLibrary);
