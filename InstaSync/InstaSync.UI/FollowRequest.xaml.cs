using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using InstaSync.API.Managers;
using InstaSync.UOW.Managers;
using User = InstaSync.Models.Models.User;

namespace InstaSync.UI
{
    public partial class FollowRequest
    {
        #region fields and porperies

        public String User { get; set; }
        private readonly LogsManager logsManager;
        private readonly MainWindow mainWindow;

        #endregion fields and porperies

        #region constructors

        public FollowRequest()
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

        public FollowRequest(string message, string userName, MainWindow _mainWindow)
        {
            try
            {
                logsManager = new LogsManager();

                mainWindow = _mainWindow;
                User = userName;

                InitializeComponent();

                mainWindow.SyncButton.IsEnabled = false;
                MessageLabel.Content = message;
            }
            catch (Exception exception)
            {
                logsManager.Log(exception.ToString());
            }
        }

        #endregion constructors

        #region event handlers

        private void SendFollowRequestButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new Follow().RequestFollow(Convert.ToInt64(User));
                mainWindow.SyncButton.IsEnabled = true;
                Close();
            }
            catch (Exception exception)
            {
                logsManager.Log(exception.ToString());
            }
        }

        private void DeclineFollowRequestButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Close();
                mainWindow.SyncButton.IsEnabled = true;
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
