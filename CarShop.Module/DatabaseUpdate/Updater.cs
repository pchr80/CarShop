using System;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using CarShop.Module.BusinessObjects.Company;

namespace CarShop.Module.DatabaseUpdate {
    // For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Updating.ModuleUpdater
    public class Updater : ModuleUpdater {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion) {
        }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();
            string status = "Planned";
            OrderStatus orderStatus = ObjectSpace.FindObject<OrderStatus>(CriteriaOperator.Parse("Status=?", status));
            if (orderStatus == null) {
                orderStatus = ObjectSpace.CreateObject<OrderStatus>();
                orderStatus.Status = status;
            }
            status = "Started";
            orderStatus = ObjectSpace.FindObject<OrderStatus>(CriteriaOperator.Parse("Status=?", status));
            if (orderStatus == null)
            {
                orderStatus = ObjectSpace.CreateObject<OrderStatus>();
                orderStatus.Status = status;
            }
            status = "Finished";
            orderStatus = ObjectSpace.FindObject<OrderStatus>(CriteriaOperator.Parse("Status=?", status));
            if (orderStatus == null)
            {
                orderStatus = ObjectSpace.CreateObject<OrderStatus>();
                orderStatus.Status = status;
            }
            ObjectSpace.CommitChanges();                //Uncomment this line to persist created object(s).
        }
        public override void UpdateDatabaseBeforeUpdateSchema() {
            base.UpdateDatabaseBeforeUpdateSchema();
            //if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
            //    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
            //}
        }
    }
}
