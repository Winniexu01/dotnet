// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Windows;
using System.Windows.Threading;
using System.Windows.TrustUI;
using System.Windows.Input;
using System.Globalization;
using System.Windows.Interop;

namespace Microsoft.Internal.DeploymentUI
{
    /// <summary>
    /// Interaction logic for TenFeetInstallationProgress.xaml
    /// </summary>
    internal partial class TenFeetInstallationProgress : IProgressPage
    {
        public TenFeetInstallationProgress()
        {
            InitializeComponent();
        }

        static TenFeetInstallationProgress()
        {
          
            CommandManager.RegisterClassCommandBinding(typeof(TenFeetInstallationProgress),
                    new CommandBinding(NavigationCommands.Refresh,
                        new ExecutedRoutedEventHandler(OnCommandRefresh),
                        new CanExecuteRoutedEventHandler(OnCanRefresh)));

            CommandManager.RegisterClassCommandBinding(typeof(TenFeetInstallationProgress),
                    new CommandBinding(NavigationCommands.BrowseStop,
                        new ExecutedRoutedEventHandler(OnCommandStop),
                        new CanExecuteRoutedEventHandler(OnCanStop)));
        }

        /// <summary>
        /// 
        /// </summary>
        public DispatcherOperationCallback RefreshCallback
        {
            set
            {
                _refresh = value;
            }
            get
            {
                return _refresh;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DispatcherOperationCallback StopCallback
        {
            set
            {
                _stop = value;
            }
            get
            {
                return _stop;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Uri DeploymentPath
        {
            set
            {
                _deploymentPath = value;
                DownloadFrom.Text = (value as Uri).ToString();
            }
            get
            {
                return _deploymentPath;
            }
        }



        /// <summary>
        /// 
        /// </summary>
        public string ApplicationName
        {
            set
            {
                _application = value;
                ApplicationNameText.Text = _application;
            }
            get
            {
                return _application;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string PublisherName
        {
            set
            {
                _publisher = value;
                PublisherText.Text = _publisher;
            }
            get
            {
                return _publisher;
            }
        }

        public void UpdateProgress(long bytesDownloaded, long bytesTotal)
        {
            CurrentBytesText.Text = String.Format(CultureInfo.CurrentCulture, SR.ProgressBarKiloBytesStringFormat, (bytesDownloaded / 1024));
            TotalBytesText.Text = String.Format(CultureInfo.CurrentCulture, SR.ProgressBarKiloBytesStringFormat, (bytesTotal / 1024));
            double percentDone = Math.Floor((double)bytesDownloaded / (double)bytesTotal * 100.0);
            if (double.IsNaN(percentDone))
            {
                percentDone = 0.0;
            }
            ProgressBarStatusText.Text = String.Format(CultureInfo.CurrentCulture, SR.ProgressBarPercentageStringFormat, percentDone);
            ProgressBar_1.Value = percentDone;
        }

        private static void OnCommandRefresh(object sender, RoutedEventArgs e)
        {
            TenFeetInstallationProgress page = sender as TenFeetInstallationProgress;
            if (page != null && page.RefreshCallback != null)
            {
                page.RefreshCallback(null);
            }
        }

        private static void OnCanRefresh(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private static void OnCommandStop(object sender, RoutedEventArgs e)
        {
            TenFeetInstallationProgress page = sender as TenFeetInstallationProgress;
            if (page != null && page.StopCallback != null)
            {
                page.StopCallback(null);
            }
        }

        private static void OnCanStop(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            StopCallback(null);
        }

        private string _publisher;
        private string _application;
        private DispatcherOperationCallback _stop;
        private DispatcherOperationCallback _refresh;
        private Uri _deploymentPath;

    }
}
