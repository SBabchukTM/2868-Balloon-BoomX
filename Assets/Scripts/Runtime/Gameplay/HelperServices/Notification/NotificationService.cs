﻿using System;

namespace Runtime.Gameplay.HelperServices.Notification
{
    public abstract class NotificationService
    {
        public abstract void Initialize();

        public abstract void SendNotification(string title, string body, string smallIconId, string largeIconId, DateTime deliveryTime);
        public abstract void CancelAllNotifications();
    }
}