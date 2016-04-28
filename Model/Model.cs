using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Model: IModel
    {
        public Model()
        {
            Tasks = new List<Task>();
        }
        #region IModel Implementation
        public List<Task> Tasks { get; set; }

        public void AddTask(Task task)
        {
            if (!Tasks.Contains(task))
                Tasks.Add(task);
        }
        public void DeleteTask(Task task)
        {
            if (Tasks.Contains(task))
                Tasks.Remove(task);
        }
      
        public void SaveData()
        {
            Parser parser = new Parser();
            parser.SaveTasks(Tasks, "store.xml");
        }
        public void LoadData()
        {
            Parser parser = new Parser("store.xml");
            foreach(Task task in parser.getTask())
            {
                this.Tasks.Add(task);
            }
        }
        #endregion
    }
}
