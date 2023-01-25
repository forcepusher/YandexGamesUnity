using System;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;

namespace Agava.YandexGames
{
    public static class Billing
    {
        public static void Purchase(string productId) => BillingPurchase(productId);

        [DllImport("__Internal")]
        private static extern void BillingPurchase(string productId);
    }
}
