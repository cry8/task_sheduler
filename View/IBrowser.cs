using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View
{
    public interface IBrowser
    {
        event EventHandler PeriodChanged;
        bool LoadWindow();
        string getExecFile();
        int getPeriod();
        DateTime getBegin();
        void setBegin(DateTime date);
        void setTime(DateTime time);
        void setCheckboxStatus(int[] checkboxes);
        void setPeriod(int period);
        void setExecFile(string path);
        DateTime getTime();
        int[] getCheckboxStatus();
        void activateWeekBox();
        void deactivateWeekBox();
    }
}
