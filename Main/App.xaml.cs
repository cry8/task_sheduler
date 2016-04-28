using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Model;
using Presenter;
using View;

namespace Main
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            IView mview = new MView();
            IModel model = new Model.Model();
            Presenter.Presenter presenter = new Presenter.Presenter(mview, model);
        }
    }
}
