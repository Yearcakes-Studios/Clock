using Microsoft.Gaming.XboxGameBar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;

namespace Clock
{
	public sealed partial class Widget1 : Page
	{
		JsonObject.WidgetConfig.Rootobject config;
		string displaytime = "0:0:0";

		public Widget1()
		{
			this.InitializeComponent();

			Load();
		}

		async void Load()
		{
			await ReadConfig();
			FlushTime();
		}

		async Task ReadConfig()
		{
			StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
			try
			{
				StorageFile configfile = await storageFolder.GetFileAsync("config.json");
				string configtext = await FileIO.ReadTextAsync(configfile);
				config = JsonConvert.DeserializeObject<JsonObject.WidgetConfig.Rootobject>(configtext);

				tb_time.FontSize = config.FontSize;
				rectangle.Opacity = config.Opacity / 100.0;
			}
			catch
			{
				StorageFile sampleFile = await storageFolder.CreateFileAsync("config.json", CreationCollisionOption.ReplaceExisting);
				config = new JsonObject.WidgetConfig.Rootobject() { FontSize = 16, Refresh = 100, TimeFormat = "HH:mm:ss", Opacity = 0 };
				await FileIO.WriteTextAsync(sampleFile, JsonConvert.SerializeObject(config));
			}
		}

		async void FlushTime()
		{
			while (true)
			{
				await Task.Delay(config.Refresh);
				string time = DateTime.Now.ToString(config.TimeFormat);
				if (time != displaytime)
				{
					displaytime = time;
					tb_time.Text = time;
				}
			}
		}
	}
}
