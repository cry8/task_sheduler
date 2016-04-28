using Microsoft.Win32;
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
    /// <summary>
    /// Interaction logic for TaskBrowser.xaml
    /// </summary>
    public partial class TaskBrowser : Window, View.IBrowser
    {
        public TaskBrowser()
        {
            InitializeComponent();
            PeriodBox.SelectedIndex = 0;
        }
        public string getExecFile()
        {
            return this.EXECPath.Text;
        }

        public event EventHandler PeriodChanged;
        public int getPeriod()
        {
            if (this.PeriodBox.SelectedItem == this.EVERYDAY)
                return 0;
            else if (this.PeriodBox.SelectedItem == this.EVERYWEEK)
                return 1;
            else if (this.PeriodBox.SelectedItem == this.EVERYMONTH)
                return 2;
            else
                return 3;
        }
        public void activateWeekBox()
        {
            this.WeekBox.IsEnabled = true;
            this.BeginBox.IsEnabled = false;
        }
        public void deactivateWeekBox()
        {
            this.WeekBox.IsEnabled = false;
            this.BeginBox.IsEnabled = true;
        }
        public void setBegin(DateTime date)
        {
            this.BeginBox.SelectedDate = date;
        }
        public void setTime(DateTime time)
        {
            this.TimePicker.Value = time;
        }
        public void setCheckboxStatus(int[] checkboxes)
        {
            if (checkboxes[0] == 1)
                this.Monday.IsChecked = true;
            if (checkboxes[1] == 1)
                this.Tuesday.IsChecked = true;
            if (checkboxes[2] == 1)
                this.Wednesday.IsChecked = true;
            if (checkboxes[3] == 1)
                this.Thursday.IsChecked = true;
            if (checkboxes[4] == 1)
                this.Friday.IsChecked = true;
            if (checkboxes[5] == 1)
                this.Saturday.IsChecked = true;
            if (checkboxes[6] == 1)
                this.Sunday.IsChecked = true;
        }
        public void setPeriod(int period)
        {
            switch (period)
            {
                case 0:
                    this.PeriodBox.SelectedItem = this.EVERYDAY;
                    break;
                case 1:
                    this.PeriodBox.SelectedItem = this.EVERYWEEK;
                    break;
                case 2:
                    this.PeriodBox.SelectedItem = this.EVERYMONTH;
                    break;
                default:
                    this.PeriodBox.SelectedItem = this.ONCE;
                    break;
            }
        }
        public void setExecFile(string path)
        {
            this.EXECPath.Text = path;
        }
        public DateTime getBegin()
        {
            if (this.BeginBox.SelectedDate != null)
                return (DateTime)this.BeginBox.SelectedDate;
            else
                throw new Exception("Не выбрана дата");
        }
        public DateTime getTime()
        {
            return (DateTime)this.TimePicker.Value;
        }
        public int[] getCheckboxStatus()
        {
            int[] array = new int[7];
            if ((bool)this.Monday.IsChecked)
                array[0] = 1;
            if ((bool)this.Tuesday.IsChecked)
                array[1] = 1;
            if ((bool)this.Wednesday.IsChecked)
                array[2] = 1;
            if ((bool)this.Thursday.IsChecked)
                array[3] = 1;
            if ((bool)this.Friday.IsChecked)
                array[4] = 1;
            if ((bool)this.Saturday.IsChecked)
                array[5] = 1;
            if ((bool)this.Sunday.IsChecked)
                array[6] = 1;
            return array;
        }
        #region IBrowser Implementation
        public bool LoadWindow()
        {
            return (bool)this.ShowDialog();
        }

        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                this.EXECPath.Text = openFileDialog.FileName;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (CheckFields())
                this.DialogResult = true;
            else
                MessageBox.Show("Заполните все необходимые поля!");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false; 
        }

        private bool CheckFields()
        {
            if (this.EXECPath.Text == "" || this.BeginBox.SelectedDate == null)
                return false;
            else
                return true;
        }

        private void PeriodBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                PeriodChanged(this.getPeriod(), e);
            }
            catch(Exception)
            {

            }
        }
    }
}
