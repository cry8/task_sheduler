using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View;

namespace Presenter
{
    public class BrowserPresenter
    {
        public Model.Task Task { get; set; }
        IBrowser _browser;
        public event EventHandler Finished;
        public BrowserPresenter(IBrowser browser)
        {
            _browser = browser;
            _browser.PeriodChanged += _browser_PeriodChanged;
        }

        void _browser_PeriodChanged(object sender, EventArgs e)
        {
            int value = Convert.ToInt32(sender);
            if (value == 1)
                _browser.activateWeekBox();
            else
                _browser.deactivateWeekBox();
        }

        public void ShowDialog(Model.Task task)
        {
            if (task.Period == Model.TaskPeriod.EVERYWEEK)
                _browser.activateWeekBox();
            bool result = _browser.LoadWindow();
            if (result == true)
            {
                this.Task = task;
                ReadData();
                Finished(this.Task, EventArgs.Empty);
            }
        }

        public void ShowDialog(Model.Task task, bool Edit)
        {
            if (Edit)
            {
                this.Task = task;
                _browser.setBegin(Task.Time.Date);
                _browser.setCheckboxStatus(Task.DayOfWeeks);
                _browser.setExecFile(Task.ExecFile);
                _browser.setPeriod((int)Task.Period);
                _browser.setTime(task.Time);
                bool result = _browser.LoadWindow();
                if(result == true)
                {
                    ReadData();
                    Finished(this.Task, EventArgs.Empty);
                }
            }
            else
                ShowDialog(task);
        }

        private void ReadData()
        {
            this.Task.ExecFile = _browser.getExecFile();
            this.Task.Time = _browser.getBegin();
            this.Task.Time = this.Task.Time.AddHours(_browser.getTime().Hour);
            this.Task.Time = this.Task.Time.AddMinutes(_browser.getTime().Minute);
            this.Task.DayOfWeeks = _browser.getCheckboxStatus();
            this.Task.Status = Model.TaskStatus.UNKNOWN;
            this.Task.Period = (Model.TaskPeriod)_browser.getPeriod();
        }
    }
}
