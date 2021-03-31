using DevExpress.ExpressApp;
using System;
using System.Collections.Generic;
using System.Text;
using CarShop.Module.BusinessObjects.Company;
using DevExpress.ExpressApp.Actions;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.SystemModule;

namespace CarShop.Module.Controllers
{
   
    public class OrderController : ViewController
    {
        public OrderController()
        {
                TargetObjectType = typeof(Order);
                TargetViewType = ViewType.ListView;
                SimpleAction markCompletedAction = new SimpleAction (
                     this, "MarkFinished", DevExpress.Persistent.Base.PredefinedCategory.RecordEdit)
                {
                    TargetObjectsCriteria =
                        (CriteriaOperator.Parse("OrderStatus.Status != ?", "'Finished'")).ToString(),
                    ConfirmationMessage =
                        "Are you sure you want to mark the selected order(s) as 'Finished'?",
                    ImageName = "State_Task_Completed"
                };
                markCompletedAction.SelectionDependencyType = SelectionDependencyType.RequireMultipleObjects;
                markCompletedAction.Execute += (s, e) =>
                {   
                    string status = "Finished";
                    OrderStatus orderStatus = ObjectSpace.FindObject<OrderStatus>(CriteriaOperator.Parse("Status=?", status));
                    foreach (Order order in e.SelectedObjects)
                    {
                        order.EndDate = DateTime.Now;
                        order.OrderStatus = orderStatus;
                        View.ObjectSpace.SetModified(order);
                    }
                    View.ObjectSpace.CommitChanges();
                    View.ObjectSpace.Refresh();
                }; 
        }
    } 
}
