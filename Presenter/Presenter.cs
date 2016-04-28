using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using View;
using System.Windows.Controls;

namespace Presenter
{
    public class Presenter
    {
        IView _view;
        IModel _model;
        TaskController t_controller;
        public Presenter(IView view, IModel model)
        {
            this._view = view;
            this._model = model;
            this._model.LoadData();
            InsertTasks();
            this._view.AddTaskEvent += _view_AddTaskEvent;
            this._view.EditTaskEvent += _view_EditTaskEvent;
            this._view.DeleteTaskEvent += _view_DeleteTaskEvent;
            this._view.ClearListEvent += _view_ClearListEvent;
            this._view.ExitApplicationEvent += _view_ExitApplicationEvent;
            this._view.LoadWindow();
            t_controller = new TaskController() { tasks = _model.Tasks };
            t_controller.NeedToRun += t_controller_NeedToRun;
        }

        void t_controller_NeedToRun(object sender, EventArgs e)
        {
            Model.Task task = sender as Model.Task;
            task.Status = Model.TaskStatus.PROCESSING;
            _view.Invoke(new Action(delegate
            {
                _view.getTaskList().Items.Refresh();
            }));
            try
            {
                System.Diagnostics.Process.Start(task.ExecFile);
                task.Status = Model.TaskStatus.COMPLETED;
                _view.Invoke(new Action(delegate
                {
                    _view.getCompletedTasks().Items.Add(task.ExecFile + "\t|\t" + task.Status);
                    _view.getTaskList().Items.Refresh();
                }));
            }
            catch(Exception ex)
            {
                task.Status = Model.TaskStatus.FAILED;
                _view.Invoke(new Action(delegate
                {
                    _view.ShowMessage(ex.Message);
                    _view.getCompletedTasks().Items.Add(task.ExecFile + "\t|\t" + task.Status);
                    _view.getTaskList().Items.Refresh();
                }));
            }
            finally
            {
                task.Status = Model.TaskStatus.WAITING;
                if (task.Period == Model.TaskPeriod.EVERYDAY)
                    task.Time = task.Time.AddDays(1);
                else if (task.Period == Model.TaskPeriod.EVERYMONTH)
                    task.Time = task.Time.AddMonths(1);
                else if (task.Period == Model.TaskPeriod.EVERYWEEK)
                    task.Time = task.Time.AddDays(7);
                else if(task.Period == Model.TaskPeriod.ONCE)
                {
                    _view.Invoke(new Action(delegate
                    {
                        _view.getTaskList().Items.Remove(task);
                    }));
                    _model.Tasks.Remove(task);
                }
                _view.Invoke(new Action(delegate
                {
                    _view.getTaskList().Items.Refresh();
                }));
            }
        }

        void _view_ExitApplicationEvent(object sender, EventArgs e)
        {
            _model.SaveData();
            _view.ExitApplication();
        }

        void InsertTasks()
        {
            foreach (Model.Task task in _model.Tasks)
                _view.getTaskList().Items.Add(task);
        }
        void _view_ClearListEvent(object sender, EventArgs e)
        {
            _view.getCompletedTasks().Items.Clear();
        }

        void _view_DeleteTaskEvent(object sender, EventArgs e)
        {
            if (_view.getTaskList().SelectedItem == null)
                throw new Exception("Задание не выбрано");
            _model.Tasks.Remove(_view.getTaskList().SelectedItem as Model.Task);
            _view.getTaskList().Items.Remove(_view.getTaskList().SelectedItem);
        }

        void _view_EditTaskEvent(object sender, EventArgs e)
        {
            if (_view.getTaskList().SelectedItem == null)
                throw new Exception("Задание не выбрано");
            Model.Task task = _view.getTaskList().SelectedItem as Model.Task;
            IBrowser browser = new TaskBrowser();
            BrowserPresenter bPresenter = new BrowserPresenter(browser);
            bPresenter.Finished += bPresenter_Finished;
            bPresenter.ShowDialog(task, true);
        }

        void _view_AddTaskEvent(object sender, EventArgs e)
        {
            Model.Task task = new Model.Task();
            IBrowser browser = new TaskBrowser();
            BrowserPresenter bPresenter = new BrowserPresenter(browser);
            bPresenter.Finished += bPresenter_Finished;
            bPresenter.ShowDialog(task);
        }

        void bPresenter_Finished(object sender, EventArgs e)
        {
            (sender as Model.Task).Status = Model.TaskStatus.WAITING;
            if (_view.getTaskList().Items.Contains(sender))
                _view.getTaskList().Items.Refresh();
            else
            {
                _view.getTaskList().Items.Add(sender);
                _model.Tasks.Add(sender as Model.Task);
            }
        }
    }
}
