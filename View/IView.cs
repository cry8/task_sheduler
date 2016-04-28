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


namespace View
{
    public interface IView
    {
        void LoadWindow();
        void ShowMessage(string message);
        void ExitApplication();
        ListBox getTaskList();
        ListBox getCompletedTasks();

        void Invoke(Action callback);
        event EventHandler AddTaskEvent;
        event EventHandler EditTaskEvent;
        event EventHandler DeleteTaskEvent;
        event EventHandler ExitApplicationEvent;
        event EventHandler ClearListEvent;
    }
}
