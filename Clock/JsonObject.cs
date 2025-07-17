using System;
using System.ComponentModel;

namespace Clock
{
    /// <summary>
    /// Contains data models for the Clock Widget configuration
    /// </summary>
    public static class JsonObject
    {
        /// <summary>
        /// Configuration settings for the Clock Widget
        /// </summary>
        public class WidgetConfig
        {
            /// <summary>
            /// Time format string (e.g., "HH:mm:ss", "h:mm:ss tt")
            /// </summary>
            [DefaultValue("HH:mm:ss")]
            public string TimeFormat { get; set; } = "HH:mm:ss";

            /// <summary>
            /// Refresh interval in milliseconds
            /// </summary>
            [DefaultValue(100)]
            public int Refresh { get; set; } = 100;

            /// <summary>
            /// Font size for the time display
            /// </summary>
            [DefaultValue(16)]
            public int FontSize { get; set; } = 16;

            /// <summary>
            /// Background opacity (0-100)
            /// </summary>
            [DefaultValue(0)]
            public int Opacity { get; set; } = 0;

            /// <summary>
            /// Validates the configuration values
            /// </summary>
            /// <returns>True if configuration is valid</returns>
            public bool IsValid()
            {
                return !string.IsNullOrEmpty(TimeFormat) &&
                       Refresh > 0 &&
                       FontSize > 0 &&
                       Opacity >= 0 && Opacity <= 100;
            }
        }
    }
}
