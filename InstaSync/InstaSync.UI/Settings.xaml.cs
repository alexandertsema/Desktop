using System;
using System.Windows;
using System.Windows.Input;
using InstaSync.UOW.Managers;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace InstaSync.UI
{
    public partial class Settings
    {
        #region fields and properties

        private readonly LogsManager logsManager;
        private readonly MainWindow mainWindow;

        #endregion fields and properties
        
        #region constructors

        public Settings()
        {
            try
            {
                logsManager = new LogsManager();

                InitializeComponent();
            }
            catch (Exception exception)
            {
                logsManager.Log(exception.ToString());
            }
        }

        public Settings(MainWindow _window)
        {
            try
            {
                logsManager = new LogsManager();

                InitializeComponent();

                var settings = SettingsManager.GetSettings();
                mainWindow = _window;
                mainWindow.SettingsButton.IsEnabled = false;

                if (settings == null) return;

                UserNameTextBox.Text = settings.User;
                PathTextBox.Text = settings.Path;
            }
            catch (Exception exception)
            {
                logsManager.Log(exception.ToString());
            }
        }

        #endregion constructors
        
        #region event handlers

        private async void OkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(UserNameTextBox.Text) || String.IsNullOrEmpty(PathTextBox.Text)) return;

                var settingsNew = new Models.Models.Settings()
                {
                    User = UserNameTextBox.Text,
                    Path = PathTextBox.Text
                };

                SettingsManager.SaveSettings(settingsNew);


                UserManager.SaveUser(await UserManager.GetNewUser(UserNameTextBox.Text));

                mainWindow.InitializeUI();
                mainWindow.SettingsButton.IsEnabled = true;

                Close();
            }
            catch (Exception exception)
            {
                logsManager.Log(exception.ToString());
            }
        }

        private void OpenDialofButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenDialofButton.IsEnabled = false;
                var dialog = new CommonOpenFileDialog {IsFolderPicker = true};
                CommonFileDialogResult result = dialog.ShowDialog();

                switch (result)
                {
                    case CommonFileDialogResult.Ok:
                    {
                        PathTextBox.Text = dialog.FileName;
                        OpenDialofButton.IsEnabled = true;
                        break;
                    }
                    case CommonFileDialogResult.None:
                    {
                        OpenDialofButton.IsEnabled = true;
                        break;
                    }
                    case CommonFileDialogResult.Cancel:
                    {
                        OpenDialofButton.IsEnabled = true;
                        break;
                    }
                }
            }
            catch (Exception exception)
            {
                logsManager.Log(exception.ToString());
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (Exception exception)
            {
                logsManager.Log(exception.ToString());
            }
        }

        private void CloseButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Close();
                mainWindow.SettingsButton.IsEnabled = true;
            }
            catch (Exception exception)
            {
                logsManager.Log(exception.ToString());
            }
        }

        #endregion event handlers
    }
}
