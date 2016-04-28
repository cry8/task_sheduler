using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Model
{
    public class Parser
    {
        private XmlDocument xDoc;
        public Parser(string Filename)
        {
            xDoc = new XmlDocument();
            xDoc.Load(Filename);
        }
        public Parser()
        {

        }
        public IEnumerable<object> getTask()
        {
            XmlElement xRoot = xDoc.DocumentElement;
            foreach (XmlElement xnode in xRoot)
            {
                Task task = new Task();
                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    switch(childnode.Name)
                    {
                        case "Status":
                            int value = Convert.ToInt32(childnode.InnerText);
                            if (value >= 0 && value < 5)
                                task.Status = (TaskStatus)value;
                            else
                                task.Status = TaskStatus.UNKNOWN;
                            break;
                        case "Period":
                            int value2 = Convert.ToInt32(childnode.InnerText);
                            if (value2 >= 0 && value2 < 4)
                                task.Period = (TaskPeriod)value2;
                            else
                                task.Period = TaskPeriod.ONCE;
                            break;
                        case "Time":
                            task.Time = Convert.ToDateTime(childnode.InnerText);
                            break;
                        case "File":
                            task.ExecFile = childnode.InnerText;
                            break;
                        case "DayOfWeeks":
                            foreach (XmlNode childnode2 in childnode.ChildNodes)
                            {
                                switch(childnode2.Name)
                                {
                                    case "Monday":
                                        task.DayOfWeeks[0] = Convert.ToInt32(childnode2.InnerText);
                                        break;
                                    case "Tuesday":
                                        task.DayOfWeeks[1] = Convert.ToInt32(childnode2.InnerText);
                                        break;
                                    case "Wednesday":
                                        task.DayOfWeeks[2] = Convert.ToInt32(childnode2.InnerText);
                                        break;
                                    case "Thursday":
                                        task.DayOfWeeks[3] = Convert.ToInt32(childnode2.InnerText);
                                        break;
                                    case "Friday":
                                        task.DayOfWeeks[4] = Convert.ToInt32(childnode2.InnerText);
                                        break;
                                    case "Saturday":
                                        task.DayOfWeeks[5] = Convert.ToInt32(childnode2.InnerText);
                                        break;
                                    case "Sunday":
                                        task.DayOfWeeks[6] = Convert.ToInt32(childnode2.InnerText);
                                        break;
                                }
                            }
                            break;
                    }
                }
                yield return task;
            }
        }


        public void SaveTasks(List<Task> tasks, string filename)
        {
            XmlTextWriter xmlwriter = new XmlTextWriter(filename, Encoding.UTF8);
            xmlwriter.WriteStartDocument();
            xmlwriter.Formatting = Formatting.Indented;
            xmlwriter.IndentChar = '\t';
            xmlwriter.Indentation = 1;
            xmlwriter.WriteStartElement("Tasks");
            foreach (Task task in tasks)
            {
                xmlwriter.WriteStartElement("Task");
                xmlwriter.WriteElementString("Status", ((int)task.Status).ToString());
                xmlwriter.WriteElementString("Period", ((int)task.Period).ToString());
                xmlwriter.WriteElementString("Time", task.Time.ToString());
                xmlwriter.WriteElementString("File", task.ExecFile);
                xmlwriter.WriteStartElement("DayOfWeeks");
                xmlwriter.WriteElementString("Monday", task.DayOfWeeks[0].ToString());
                xmlwriter.WriteElementString("Tuesday", task.DayOfWeeks[1].ToString());
                xmlwriter.WriteElementString("Wednesday", task.DayOfWeeks[2].ToString());
                xmlwriter.WriteElementString("Thursday", task.DayOfWeeks[3].ToString());
                xmlwriter.WriteElementString("Friday", task.DayOfWeeks[4].ToString());
                xmlwriter.WriteElementString("Saturday", task.DayOfWeeks[5].ToString());
                xmlwriter.WriteElementString("Sunday", task.DayOfWeeks[6].ToString());
                xmlwriter.WriteEndElement();
                xmlwriter.WriteEndElement();
            }
            xmlwriter.WriteEndElement();
            xmlwriter.Close();
        }
    }
}
