using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using CarShop.Module.BusinessObjects.Company;
using DevExpress.ExpressApp.DC;
using CarShop.Module.Gus;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace CarShop.Module.BusinessObjects.Customer
{
    [DeferredDeletion(false)]
    [NavigationItem("Customer")]
    [XafDefaultProperty(nameof(FullName))]
    public class Customer : BaseObject
    {
        public Customer(Session session) : base(session) { }        
        string custNo;        
        public string CustomerNo
        {
            get { return custNo; }
            set { SetPropertyValue(nameof(CustomerNo), ref custNo, value); }
        }
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
        string nipNo;
        public string NipNumber
        {
            get { return nipNo; }
            set { SetPropertyValue(nameof(NipNumber), ref nipNo, value);
                    if (!IsLoading) 
                         ReadXml();
            }
        }        
        string companyName;
        public string CompanyName
        {
            get { return companyName; }
            set { SetPropertyValue(nameof(CompanyName), ref companyName, value); }
        }
        string streetName;
        public string StreetName
        {
            get { return streetName; }
            set { SetPropertyValue(nameof(StreetName), ref streetName, value); }
        }
        string houseNr;
        public string HouseNr
        {
            get { return houseNr; }
            set { SetPropertyValue(nameof(HouseNr), ref houseNr, value); }
        }
        string flatNr;
        public string FlatNr
        {
            get { return flatNr; }
            set { SetPropertyValue(nameof(FlatNr), ref flatNr, value); }
        }
        string postCode;
        public string PostCode
        {
            get { return postCode; }
            set { SetPropertyValue(nameof(PostCode), ref postCode, value); }
        }
        string city;
        public string City
        {
            get { return city; }
            set { SetPropertyValue(nameof(City), ref city, value); }
        }
        string province;
        public string Province
        {
            get { return province; }
            set { SetPropertyValue(nameof(Province), ref province, value); }
        }
        string email;
        public string Email
        {
            get { return email; }
            set { SetPropertyValue(nameof(Email), ref email, value); }
        }
        string phone;
        public string Phone
        {
            get { return phone; }
            set { SetPropertyValue(nameof(Phone), ref phone, value); }
        }
        [Association("Customer-Cars")]
        public XPCollection<Car> Cars
        {
            get { return GetCollection<Car>(nameof(Cars)); }
        }
        public string FullName
        {
            get
            {
                string namePart = string.Format("{0} {1}", FirstName, LastName);
                return namePart;
            }
        }       
        private void SetCustomerData(string data)
        {            
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);
            XmlNode root = doc.DocumentElement;
            XmlNode dane = root["dane"];
            if (dane["ErrorCode"] == null)
            {   CompanyName = dane["Nazwa"].InnerText;
                StreetName = dane["Ulica"].InnerText;
                HouseNr = dane["NrNieruchomosci"].InnerText;
                FlatNr = dane["NrLokalu"].InnerText;
                PostCode = dane["KodPocztowy"].InnerText;
                City = dane["Miejscowosc"].InnerText;
                Province = dane["Wojewodztwo"].InnerText;
            }
        }
        private async System.Threading.Tasks.Task ReadXml()
        {           
            string result = await ServiceData.PobierzDane(NipNumber);
            SetCustomerData(result);
        }
    }
    // ...
    [DeferredDeletion(false)]
    [NavigationItem("Customer")]
    [XafDefaultProperty(nameof(RegNr))]
    public class Car : BaseObject
    {
        public Car(Session session) : base(session) { }
        string brand;        
        public string Brand
        {
            get { return brand; }
            set { SetPropertyValue(nameof(Brand), ref brand, value); }
        }
        string model;
        public string Model
        {
            get { return model; }
            set { SetPropertyValue(nameof(Model), ref model, value); }
        }
        string prodYear;
        [Size(4)]                
        public string ProdYear
        {
            get { return prodYear; }
            set { SetPropertyValue(nameof(ProdYear), ref prodYear, value); }
        }
        string regNr;
        public string RegNr
        {
            get { return regNr; }
            set { SetPropertyValue(nameof(RegNr), ref regNr, value); }
        }
        System.DateTime reviewDate;
        public DateTime ReviewDate
        {
            get { return reviewDate; }
            set { SetPropertyValue(nameof(ReviewDate), ref reviewDate, value); }
        }
        Customer customer;
        [Association("Customer-Cars")]
        public Customer Customer
        {
            get { return customer; }
            set { SetPropertyValue(nameof(Customer), ref customer, value); }
        }
    }
}