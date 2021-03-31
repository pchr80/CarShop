using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.ExpressApp.Actions;
using CarShop.Module.BusinessObjects.Company;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;

namespace CarShop.Module.Blazor.Controllers
{
    public class MyModController : DevExpress.ExpressApp.Blazor.SystemModule.BlazorModificationsController
    {           
        protected override void Save(SimpleActionExecuteEventArgs e)
        {            
            base.Save(e);
            View view = View;

            if ((view != null) && (view.ObjectTypeInfo.Type == typeof(Order)))
            {
                view.Refresh(true);
            }
            if ((view != null) && (view.ObjectTypeInfo.Type == typeof(Payment)) && (view is DetailView))
            {                           
                Application.MainWindow.View.Refresh(true);
            }
        }
    }    
}
