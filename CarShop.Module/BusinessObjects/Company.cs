using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Text;
using CarShop.Module.BusinessObjects.Customer;
using DevExpress.ExpressApp.DC;

namespace CarShop.Module.BusinessObjects.Company
{
    [DeferredDeletion(false)]
    [NavigationItem("Company")]
    [XafDefaultProperty(nameof(FullName))]
    public class Employee : BaseObject
    {   
        public Employee(Session session) : base(session) { }
        string firstName;
        public string FirstName
        {
            get { return firstName; }
            set { SetPropertyValue(nameof(FirstName), ref firstName, value); }
        }
        string lastName;
        public string LastName
        {
            get { return lastName; }
            set { SetPropertyValue(nameof(LastName), ref lastName, value); }
        }
        public string FullName
        {
            get
            {
                string namePart = string.Format("{0} {1}", FirstName, LastName);
                return namePart;
            }
        }
        string phone;
        public string Phone
        {
            get { return phone; }
            set { SetPropertyValue(nameof(Phone), ref phone, value); }
        }
        string note;
        public string Note
        {
            get { return note; }
            set { SetPropertyValue(nameof(Note), ref note, value); }
        }
        decimal manHour;
        public decimal ManHour
        {
            get { return manHour; }
            set { SetPropertyValue(nameof(ManHour), ref manHour, value); }
        }
        
    }
    [DeferredDeletion(false)]
    [NavigationItem("Company")]
    [XafDefaultProperty(nameof(OrderNo))]    
    public class Order : BaseObject
    {
        public Order(Session session) : base(session) {}      
        
        [Persistent("OrderNo")]        
        string orderNo;
        [PersistentAlias(nameof(orderNo))]
        public string OrderNo
        {
            get { return orderNo; }
            // set { SetPropertyValue(nameof(OrderNo), ref orderNo, value); }
        }

        Car car; 
        public Car Car
        {
            get { return car; }
            set { SetPropertyValue(nameof(Car), ref car, value); }
        }        
        Employee employee;
        public Employee Employee
        {
            get { return employee; }
            set { SetPropertyValue(nameof(Employee), ref employee, value); }
        }        
        System.DateTime receiveDate;
        public DateTime ReceiveDate
        {
            get { return receiveDate; }
            set { SetPropertyValue(nameof(ReceiveDate), ref receiveDate, value); }
        }
        System.DateTime plannedStartDate;
        public DateTime PlannedStartDate
        {
            get { return plannedStartDate; }
            set { SetPropertyValue(nameof(PlannedStartDate), ref plannedStartDate, value); }
        }
        System.DateTime startDate;
        public DateTime StartDate
        {
            get { return startDate; }
            set { SetPropertyValue(nameof(StartDate), ref startDate, value); }
        } 
        string problemDesc;
        [Size(4000)]
        public string ProblemDesc
        {
            get { return problemDesc; }
            set { SetPropertyValue(nameof(ProblemDesc), ref problemDesc, value); }
        }
        string repairDesc;
        [Size(4000)]
        public string RepairDesc
        {
            get { return repairDesc; }
            set { SetPropertyValue(nameof(RepairDesc), ref repairDesc, value); }
        }        
        OrderStatus orderStatus;        
        public OrderStatus OrderStatus
        {
            get { return orderStatus; }
            set { SetPropertyValue(nameof(OrderStatus), ref orderStatus, value); }
        } 
        decimal? repairCost;
        public decimal? RepairCost
        {
            get { return repairCost; }
            set { SetPropertyValue(nameof(RepairCost), ref repairCost, value); }
        }
        decimal? partsCost;
        public decimal? PartsCost
        {
            get { return partsCost; }
            set { SetPropertyValue(nameof(PartsCost), ref partsCost, value); }
        }        
        public decimal? ManHour
        {
            get {
                if (RepairCost == null)
                    return null;
                else if (WorkHours == 0 || WorkHours == null)
                    return null;
                else
                    return RepairCost/Convert.ToDecimal(WorkHours); 
                }
            // set { SetPropertyValue(nameof(ManHour), ref manHour, value); }
        } 
        double? workHours;
        public double? WorkHours
        {
            get { return workHours; }
            set { SetPropertyValue(nameof(WorkHours), ref workHours, value); }
        }
        System.DateTime endDate;
        public DateTime EndDate
        {
            get { return endDate; }
            set { SetPropertyValue(nameof(EndDate), ref endDate, value); }
            
        }               
        decimal sumOfPayments;
        [NonPersistent]
        public decimal SumOfPayments
        {
            get { return getSumPayments(); }
            // set { SetPropertyValue(nameof(SumOfPayments), ref sumOfPayments, value); }
        }        
        [Association("Order-Pays")]        
        public XPCollection<Payment> Payments
        {
            get { return GetCollection<Payment>(nameof(Payments)); }
        }
        private decimal getSumPayments()
        {
            decimal sum = 0;
            foreach (Payment p in Payments)
            {
                sum += p.PaymentAmount;
            }
            return sum;
        }
    }
    [DeferredDeletion(false)]
    [NavigationItem("Company")]
    [XafDefaultProperty(nameof(Status))]
    public class OrderStatus : BaseObject
    {
        public OrderStatus(Session session) : base(session) { }
        string status;
        public string Status
        {
            get { return status; }
            set { SetPropertyValue(nameof(Status), ref status, value); }
        }
    }
    [DeferredDeletion(false)]
    [NavigationItem("Company")]
    public class Payment : BaseObject
    {
        public Payment(Session session) : base(session) { }
        string payNo;
        public string PaymentNo
        {
            get { return payNo; }
            set { SetPropertyValue(nameof(PaymentNo), ref payNo, value); }
        }
        string payDesc;
        public string PaymentDesc
        {
            get { return payDesc; }
            set { SetPropertyValue(nameof(PaymentDesc), ref payDesc, value); }
        }
        decimal payAmount;
        public decimal PaymentAmount
        {
            get { return payAmount; }
            set { SetPropertyValue(nameof(PaymentAmount), ref payAmount, value); }
        }        
        Order order;        
        [Association("Order-Pays")]
        public Order Order
        {
            get { return order; }
            set { SetPropertyValue(nameof(Order), ref order, value); }
        }        
    }
}
