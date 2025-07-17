using Microsoft.Gaming.XboxGameBar;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;

namespace Clock
{
    /// <summary>
    /// Main widget page that displays the clock
    /// </summary>
    public sealed partial class Widget1 : Page
    {
        private JsonObject.WidgetConfig config;
        private string displayTime = "0:0:0";
        private CancellationTokenSource cancellationTokenSource;
        private bool isDisposed = false;

        /// <summary>
        /// Initializes a new instance of the Widget1 class
        /// </summary>
        public Widget1()
        {
            this.InitializeComponent();
            cancellationTokenSource = new CancellationTokenSource();
        }

        /// <summary>
        /// Called when the page is navigated to
        /// </summary>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await LoadAsync();
        }

        /// <summary>
        /// Called when the page is navigated away from
        /// </summary>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            Dispose();
        }

        /// <summary>
        /// Loads the widget configuration and starts the clock
        /// </summary>
        private async Task LoadAsync()
        {
            try
            {
                await ReadConfigAsync();
                await StartClockAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading widget: {ex.Message}");
            }
        }

        /// <summary>
        /// Reads the configuration from the local storage
        /// </summary>
        private async Task ReadConfigAsync()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            try
            {
                StorageFile configFile = await storageFolder.GetFileAsync("config.json");
                string configText = await FileIO.ReadTextAsync(configFile);
                config = JsonConvert.DeserializeObject<JsonObject.WidgetConfig>(configText);
                
                if (config == null || !config.IsValid())
                {
                    config = CreateDefaultConfig();
                    await SaveConfigAsync();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error reading config: {ex.Message}");
                config = CreateDefaultConfig();
                await SaveConfigAsync();
            }

            ApplyConfiguration();
        }

        /// <summary>
        /// Creates a default configuration
        /// </summary>
        private JsonObject.WidgetConfig CreateDefaultConfig()
        {
            return new JsonObject.WidgetConfig
            {
                FontSize = 16,
                Refresh = 100,
                TimeFormat = "HH:mm:ss",
                Opacity = 0
            };
        }

        /// <summary>
        /// Saves the current configuration to local storage
        /// </summary>
        private async Task SaveConfigAsync()
        {
            try
            {
                StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                StorageFile configFile = await storageFolder.CreateFileAsync("config.json", CreationCollisionOption.ReplaceExisting);
                string configJson = JsonConvert.SerializeObject(config, Formatting.Indented);
                await FileIO.WriteTextAsync(configFile, configJson);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving config: {ex.Message}");
            }
        }

        /// <summary>
        /// Applies the configuration to the UI elements
        /// </summary>
        private void ApplyConfiguration()
        {
            if (config != null)
            {
                tb_time.FontSize = config.FontSize;
                rectangle.Opacity = config.Opacity / 100.0;
            }
        }

        /// <summary>
        /// Starts the clock update loop
        /// </summary>
        private async Task StartClockAsync()
        {
            try
            {
                while (!cancellationTokenSource.Token.IsCancellationRequested && !isDisposed)
                {
                    await Task.Delay(config.Refresh, cancellationTokenSource.Token);
                    
                    if (cancellationTokenSource.Token.IsCancellationRequested || isDisposed)
                        break;

                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, UpdateTimeDisplay);
                }
            }
            catch (OperationCanceledException)
            {
                // Expected when cancellation is requested
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in clock update loop: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates the time display on the UI thread
        /// </summary>
        private void UpdateTimeDisplay()
        {
            if (isDisposed) return;

            try
            {
                string currentTime = DateTime.Now.ToString(config.TimeFormat);
                if (currentTime != displayTime)
                {
                    displayTime = currentTime;
                    tb_time.Text = currentTime;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error updating time display: {ex.Message}");
            }
        }

        /// <summary>
        /// Disposes resources used by the widget
        /// </summary>
        private void Dispose()
        {
            if (isDisposed) return;
            
            isDisposed = true;
            cancellationTokenSource?.Cancel();
            cancellationTokenSource?.Dispose();
        }
    }
}
