using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ExperimentSimpleBkLibInvTool.Views
{
    public abstract class BaseViewObject : ObservableViewObject
    {
        protected readonly App TheApp = Application.Current as App;
    }
}
