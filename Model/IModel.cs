using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface IModel
    {
        List<Task> Tasks { get; set; }

        void AddTask(Task task);
        void DeleteTask(Task task);
        void SaveData();
        void LoadData();
    }
}
