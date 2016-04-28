using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Threading;

namespace Presenter
{
    public class TaskController
    {
        public event EventHandler NeedToRun;
        private Timer timer;
        public List<Model.Task> tasks { get; set; }
        public TaskController()
        {
            timer = new Timer(1000);
            timer.Elapsed += timer_Elapsed;
            timer.Start();
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            new Task(new Action(delegate
            {
                foreach (Model.Task taskc in this.tasks)
                {
                    if (taskc.Period == Model.TaskPeriod.EVERYWEEK)
                    {
                        DayOfWeek dow = DateTime.Now.DayOfWeek;
                        switch(dow)
                        {
                            case DayOfWeek.Monday:
                                if (taskc.DayOfWeeks[0] == 1 && taskc.Time.ToShortTimeString() == DateTime.Now.ToShortTimeString())
                                    NeedToRun(taskc, EventArgs.Empty);
                                break;
                            case DayOfWeek.Tuesday:
                                if (taskc.DayOfWeeks[1] == 1 && taskc.Time.ToShortTimeString() == DateTime.Now.ToShortTimeString())
                                    NeedToRun(taskc, EventArgs.Empty);
                                break;
                            case DayOfWeek.Wednesday:
                                if (taskc.DayOfWeeks[2] == 1 && taskc.Time.ToShortTimeString() == DateTime.Now.ToShortTimeString())
                                    NeedToRun(taskc, EventArgs.Empty);
                                break;
                            case DayOfWeek.Thursday:
                                if (taskc.DayOfWeeks[3] == 1 && taskc.Time.ToShortTimeString() == DateTime.Now.ToShortTimeString())
                                    NeedToRun(taskc, EventArgs.Empty);
                                break;
                            case DayOfWeek.Friday:
                                if (taskc.DayOfWeeks[4] == 1 && taskc.Time.ToShortTimeString() == DateTime.Now.ToShortTimeString())
                                    NeedToRun(taskc, EventArgs.Empty);
                                break;
                            case DayOfWeek.Saturday:
                                if (taskc.DayOfWeeks[5] == 1 && taskc.Time.ToShortTimeString() == DateTime.Now.ToShortTimeString())
                                    NeedToRun(taskc, EventArgs.Empty);
                                break;
                            case DayOfWeek.Sunday:
                                if (taskc.DayOfWeeks[6] == 1 && taskc.Time.ToShortTimeString() == DateTime.Now.ToShortTimeString())
                                    NeedToRun(taskc, EventArgs.Empty);
                                break;
                        }
                    }
                    else if (taskc.Time.ToString() == DateTime.Now.ToString())
                            NeedToRun(taskc, EventArgs.Empty);
                }
            })).Start();
        }
    }
}
