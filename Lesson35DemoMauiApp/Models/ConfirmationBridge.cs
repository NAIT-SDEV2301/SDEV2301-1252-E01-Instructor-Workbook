using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson35DemoMauiApp.Models
{
    public class ConfirmationBridge
    {
        // Global singleton; easy for demo and teaching.
        public static ConfirmationBridge Instance { get; } = new ConfirmationBridge();

        // Event raised whenever MAUI has a new confirmation result.
        public event Action<bool>? ConfirmationChanged;

        public void PublishResult(bool result)
        {
            ConfirmationChanged?.Invoke(result);
        }
    }

}
