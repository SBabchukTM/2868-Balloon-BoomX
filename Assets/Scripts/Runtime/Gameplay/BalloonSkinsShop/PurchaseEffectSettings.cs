﻿using System;

namespace Runtime.Gameplay.BalloonSkinsShop
{
    [Serializable]
    public class PurchaseEffectSettings
    {
        public bool PlaySoundOnPurchase = true;
        public bool PlaySoundOnSelectPurchased = true;
        public bool PlaySoundIfNotEnoughCurrency = true;
        public bool ShakeIfNotEnoughCurrency = true;
        public PurchaseFailedShakeParameters PurchaseFailedShakeParameters;
    }
}