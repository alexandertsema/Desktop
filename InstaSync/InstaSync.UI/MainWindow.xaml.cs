using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using InstaSync.Models.StaticParameters;
using InstaSync.UOW.Helpers;
using InstaSync.UOW.Managers;
using VisualState = InstaSync.Models.Enums.VisualState;

namespace InstaSync.UI
{
    public partial class MainWindow
    {
        #region fields and properties

        private readonly LogsManager logsManager;
        private readonly String appName = "InstaSyncApp";
        //private VisualState visualState = VisualState.Start;

        #endregion

        #region constructors

        public MainWindow()
        {
            try
            {
                logsManager = new LogsManager();

                InitializeComponent();

                InitializeUI();
            }
            catch (Exception exception)
            {
                logsManager.Log(exception.ToString());
            }
        }

        #endregion constructors
        
        #region public methods

        public void InitializeUI()
        {
            try
            {
                var user = UserManager.GetUser();

                if (user == null) return;

                ProfileImage.Source = ImageHelper.GetImage(user.Image);
                UserNameLabel.Content = user.UserName;
            }
            catch (Exception exception)
            {
                logsManager.Log(exception.ToString());
            }
        }

        #endregion public methods
        
        #region private methods

        private void SwitchVisualSyncronizeState(VisualState state)
        {
            if (state == VisualState.Start)
            {
                LoaderCanvas.Visibility = Visibility.Visible;
                SyncButton.IsEnabled = false;
            }
            else
            {
                LoaderCanvas.Visibility = Visibility.Hidden;
            }
        }

        private void SetStateMessage(string message)
        {
            StatusLabel.Content = message;
        }

        private async Task<bool> Syncronize()
        {
            try
            {
                var settings = SettingsManager.GetSettings();
                
                if (settings != null)
                {
                    SetStateMessage(State.IndexingRemoteRepository);
                    var remoteRepository = await SynchronizationManager.IndexRemoteRepository(settings.User);
                    if (!remoteRepository.Any())
                    {
                        FollowRequest followRequestWindow =
                            new FollowRequest($"{settings.User},\nplease add {appName} and try again", settings.User, this);
                        followRequestWindow.Show();
                    }
                    else
                    {
                        SetStateMessage(State.IndexingLocalRepository);
                        DirectoryHelper.CreateDirectory(settings.Path);
                        var localRepository = SynchronizationManager.IndexLocalRepository(settings.Path);

                        SetStateMessage(State.SyncronizationInProgress);
                        await SynchronizationManager.SaveFiles(remoteRepository, localRepository, settings.Path);
                    
                        SyncButton.IsEnabled = true;
                    }
                }
                else
                {
                    SyncButton.IsEnabled = true;
                }

                SetStateMessage(State.SyncronizationDone);
            }
            catch (Exception exception)
            {
                logsManager.Log(exception.ToString());
            }

            return true;
        }

        #endregion private methods

        #region event handlers

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Settings settingsWindow = new Settings(this);

                settingsWindow.Show();
            }
            catch (Exception exception)
            {
                logsManager.Log(exception.ToString());
            }
        }

        private async void SyncButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //visualState = VisualState.Start;
                SwitchVisualSyncronizeState(VisualState.Start);
                await Syncronize();
                SwitchVisualSyncronizeState(VisualState.End);
                //visualState = VisualState.End;
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
                //if (visualState == VisualState.End)
                //{
                    Application.Current.Shutdown();
                //}
                //else
                //{
                //    MessageBox.Show("Sync");
                //}
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

        #endregion event handlers
    }
}
