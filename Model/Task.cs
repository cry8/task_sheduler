using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public enum TaskPeriod
    {
        EVERYDAY,
        EVERYWEEK,
        EVERYMONTH,
        ONCE
    }

    public enum TaskStatus
    {
        COMPLETED,
        FAILED,
        WAITING,
        PROCESSING,
        UNKNOWN
    }

    public class Task
    {
        public Task()
        {
            Period = TaskPeriod.EVERYDAY;
            Status = TaskStatus.UNKNOWN;
            Time = new DateTime();
            DayOfWeeks = new int[7];
            ExecFile = "";
        }
        public TaskStatus Status { get; set; }
        public TaskPeriod Period { get; set; }
        public DateTime Time { get; set; }
        public int[] DayOfWeeks { get; set; }
        public string ExecFile { get; set; }

        public string getBeginDate()
        {
            return Time.ToShortDateString();
        }

        public string getTime()
        {
            return Time.ToShortTimeString();
        }

        public override string ToString()
        {
            return this.ExecFile + "\t|\t" + this.Period.ToString()+"\t|\t"+this.Status.ToString();
        }
    }
}
