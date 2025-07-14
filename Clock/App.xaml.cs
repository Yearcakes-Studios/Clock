using Microsoft.Gaming.XboxGameBar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Clock
{
	sealed partial class App : Application
	{
		private XboxGameBarWidget widget1 = null;

		public App()
		{
			this.InitializeComponent();
			this.Suspending += OnSuspending;
		}

		protected override void OnActivated(IActivatedEventArgs args)
		{
			XboxGameBarWidgetActivatedEventArgs widgetArgs = null;
			if (args.Kind == ActivationKind.Protocol)
			{
				var protocolArgs = args as IProtocolActivatedEventArgs;
				string scheme = protocolArgs.Uri.Scheme;
				if (scheme.Equals("ms-gamebarwidget"))
				{
					widgetArgs = args as XboxGameBarWidgetActivatedEventArgs;
				}
			}
			if (widgetArgs != null)
			{
				if (widgetArgs.IsLaunchActivation)
				{
					var rootFrame = new Frame();
					rootFrame.NavigationFailed += OnNavigationFailed;
					Window.Current.Content = rootFrame;

					widget1 = new XboxGameBarWidget(
						widgetArgs,
						Window.Current.CoreWindow,
						rootFrame);
					rootFrame.Navigate(typeof(Widget1), widget1);

					Window.Current.Closed += Widget1Window_Closed;

					Window.Current.Activate();
				}
				else
				{
					// You can perform whatever behavior you need based on the URI payload.
				}
			}
		}

		private void Widget1Window_Closed(object sender, Windows.UI.Core.CoreWindowEventArgs e)
		{
			widget1 = null;
			Window.Current.Closed -= Widget1Window_Closed;
		}

		protected override void OnLaunched(LaunchActivatedEventArgs e)
		{
			Frame rootFrame = Window.Current.Content as Frame;

			if (rootFrame == null)
			{
				rootFrame = new Frame();

				rootFrame.NavigationFailed += OnNavigationFailed;

				if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
				{

				}

				Window.Current.Content = rootFrame;
			}

			if (e.PrelaunchActivated == false)
			{
				if (rootFrame.Content == null)
				{
					rootFrame.Navigate(typeof(MainPage), e.Arguments);
				}
				Window.Current.Activate();
			}
		}

		void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
		{
			throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
		}

		private void OnSuspending(object sender, SuspendingEventArgs e)
		{
			var deferral = e.SuspendingOperation.GetDeferral();
			deferral.Complete();
		}
    }
}
