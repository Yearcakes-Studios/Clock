using Microsoft.Gaming.XboxGameBar;
using System;
using System.Diagnostics;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Clock
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        private XboxGameBarWidget clockWidget = null;

        /// <summary>
        /// Initializes the singleton application object.
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            this.UnhandledException += OnUnhandledException;
        }

        /// <summary>
        /// Invoked when the application is activated through a protocol.
        /// This is specifically used for Xbox Game Bar widget activation.
        /// </summary>
        /// <param name="args">Details about the activation request.</param>
        protected override void OnActivated(IActivatedEventArgs args)
        {
            try
            {
                XboxGameBarWidgetActivatedEventArgs widgetArgs = null;
                
                if (args.Kind == ActivationKind.Protocol)
                {
                    var protocolArgs = args as IProtocolActivatedEventArgs;
                    if (protocolArgs?.Uri.Scheme.Equals("ms-gamebarwidget") == true)
                    {
                        widgetArgs = args as XboxGameBarWidgetActivatedEventArgs;
                    }
                }

                if (widgetArgs != null)
                {
                    HandleWidgetActivation(widgetArgs);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error during activation: {ex.Message}");
            }
        }

        /// <summary>
        /// Handles the widget activation process
        /// </summary>
        /// <param name="widgetArgs">Widget activation arguments</param>
        private void HandleWidgetActivation(XboxGameBarWidgetActivatedEventArgs widgetArgs)
        {
            if (widgetArgs.IsLaunchActivation)
            {
                var rootFrame = new Frame();
                rootFrame.NavigationFailed += OnNavigationFailed;
                Window.Current.Content = rootFrame;

                clockWidget = new XboxGameBarWidget(
                    widgetArgs,
                    Window.Current.CoreWindow,
                    rootFrame);
                
                rootFrame.Navigate(typeof(Widget1), clockWidget);
                Window.Current.Closed += OnWidgetWindowClosed;
                Window.Current.Activate();
            }
            else
            {
                Debug.WriteLine("Widget activation was not a launch activation");
            }
        }

        /// <summary>
        /// Handles the widget window closed event
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">Event arguments</param>
        private void OnWidgetWindowClosed(object sender, Windows.UI.Core.CoreWindowEventArgs e)
        {
            clockWidget = null;
            Window.Current.Closed -= OnWidgetWindowClosed;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            try
            {
                Frame rootFrame = Window.Current.Content as Frame;

                if (rootFrame == null)
                {
                    rootFrame = new Frame();
                    rootFrame.NavigationFailed += OnNavigationFailed;

                    if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                    {
                        // TODO: Load state from previously suspended application
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
            catch (Exception ex)
            {
                Debug.WriteLine($"Error during launch: {ex.Message}");
            }
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception($"Failed to load Page {e.SourcePageType.FullName}");
        }

        /// <summary>
        /// Invoked when application execution is being suspended.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            try
            {
                // TODO: Save application state and stop any background activity
            }
            finally
            {
                deferral.Complete();
            }
        }

        /// <summary>
        /// Invoked when an unhandled exception occurs
        /// </summary>
        /// <param name="sender">The source of the unhandled exception</param>
        /// <param name="e">Details about the unhandled exception</param>
        private void OnUnhandledException(object sender, Windows.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            Debug.WriteLine($"Unhandled exception: {e.Exception.Message}");
            e.Handled = true; // Prevent app crash in release mode
        }
    }
}
