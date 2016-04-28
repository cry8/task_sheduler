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
using System.Windows.Forms;
using System.Windows.Controls.Theming;

namespace View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MView : Window, View.IView
    {
        private System.Windows.Forms.NotifyIcon TrayIcon = null;
        private System.Windows.Controls.ContextMenu TrayMenu = null;
        public WindowState CurrentWindowState { get; set; }
        public bool CanClose { get; set; }
        public MView()
        {
            InitializeComponent();
            CanClose = false;
            CurrentWindowState = WindowState.Normal;
        }
        #region IView Implementation
        
        public event EventHandler AddTaskEvent;
        public event EventHandler EditTaskEvent;
        public event EventHandler DeleteTaskEvent;
        public event EventHandler ExitApplicationEvent;
        public event EventHandler ClearListEvent;
        public void LoadWindow()
        {
            this.Show();
        }
        public System.Windows.Controls.ListBox getCompletedTasks()
        {
            return this.CompletedTasks;
        }
        public void ShowMessage(string message)
        {
            System.Windows.MessageBox.Show(message);
        }

        public void Invoke(Action callback)
        {
            this.Dispatcher.Invoke(callback);
        }
        public void ExitApplication()
        {
            System.Windows.Application.Current.Shutdown();
        }
        public System.Windows.Controls.ListBox getTaskList()
        {
            return this.TaskList;
        }
        #endregion

        private void Add_Task(object sender, RoutedEventArgs e)
        {
            AddTaskEvent(this, e);
        }

        private void Delete_Task(object sender, RoutedEventArgs e)
        {
            if(this.TaskList.SelectedIndex>=0 && this.TaskList.SelectedIndex<this.TaskList.Items.Count)
                DeleteTaskEvent(this, e);
        }

        private void Edit_Task(object sender, RoutedEventArgs e)
        {
            if (this.TaskList.SelectedIndex >= 0 && this.TaskList.SelectedIndex < this.TaskList.Items.Count)
                EditTaskEvent(this, e);
        }

        private void Clear_Tasks(object sender, RoutedEventArgs e)
        {
            ClearListEvent(this, e);
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            ExitApplicationEvent(this, e);
        }

        private void ShowHideMainWindow(object sender, RoutedEventArgs e)
        {
            TrayMenu.IsOpen = false;
            if (IsVisible)
            {
                Hide();
                (TrayMenu.Items[0] as System.Windows.Controls.MenuItem).Header = "Открыть приложение";
            }
            else
            { 
                Show();
                (TrayMenu.Items[0] as System.Windows.Controls.MenuItem).Header = "Свернуть в трей";
                WindowState = CurrentWindowState;
                Activate();
            }
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            createTrayIcon();
        }

        private bool createTrayIcon()
        {
            bool result = false;
            if (TrayIcon == null)
            {
                TrayIcon = new System.Windows.Forms.NotifyIcon(); 
                TrayIcon.Icon = View.Resource1.favicon;
                TrayIcon.Text = "Task Scheduler";
                TrayMenu = Resources["TrayMenu"] as System.Windows.Controls.ContextMenu;
                TrayIcon.Click += delegate(object sender, EventArgs e)
                {
                    if ((e as System.Windows.Forms.MouseEventArgs).Button == System.Windows.Forms.MouseButtons.Left)
                        ShowHideMainWindow(sender, null);
                    else
                    {
                        TrayMenu.IsOpen = true;
                        Activate();
                    }
                };
                result = true;
            }
            else
                result = true;
            TrayIcon.Visible = true;
            return result;
        }
        protected override void OnStateChanged(EventArgs e)
        {
            base.OnStateChanged(e);
            if (this.WindowState == System.Windows.WindowState.Minimized)
            {
                Hide();
                (TrayMenu.Items[0] as System.Windows.Controls.MenuItem).Header = "Открыть приложение";
            }
            else
                CurrentWindowState = WindowState;
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e) 
        {
          base.OnClosing(e);
          if(!CanClose) 
          {
            e.Cancel = true;
            CurrentWindowState = this.WindowState;
            (TrayMenu.Items[0] as System.Windows.Controls.MenuItem).Header = "Открыть приложение";
            Hide();
          }
          else
            TrayIcon.Visible = false;
        }
    }
}
