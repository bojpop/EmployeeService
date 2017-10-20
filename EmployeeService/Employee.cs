using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace EmployeeService
{
    [MessageContract(IsWrapped = true, WrapperName = "EmployeeRequestObject", WrapperNamespace = "http://MyCompany.com/Employee")]
    public class EmployeeRequest
    {
        [MessageHeader(Namespace = "http://MyCompany.com/Employee")]
        public string LicenseKey { get; set; }

        [MessageBodyMember(Namespace = "http://MyCompany.com/Employee")]
        public int EmployeeId { get; set; }
    }

    [MessageContract(IsWrapped = true, WrapperName = "EmployeeInfoObject", WrapperNamespace = "http://MyCompany.com/Employee")]
    public class EmployeeInfo
    {
        public EmployeeInfo()
        {
            
        }

        public EmployeeInfo(Employee employee)
        {
            
        }

        [MessageBodyMember(Order = 1, Namespace = "http://MyCompany.com/Employee")]
        public int Id { get; set; }
        [MessageBodyMember(Order = 2, Namespace = "http://MyCompany.com/Employee")]
        public string Name { get; set; }
        [MessageBodyMember(Order = 3, Namespace = "http://MyCompany.com/Employee")]
        public string Gender { get; set; }
        [MessageBodyMember(Order = 4, Namespace = "http://MyCompany.com/Employee")]
        public DateTime DOB { get; set; }
        [MessageBodyMember(Order = 5, Namespace = "http://MyCompany.com/Employee")]
        public Employee.EmployeeType Type { get; set; }
        [MessageBodyMember(Order = 6, Namespace = "http://MyCompany.com/Employee")]
        public int AnnualSalary { get; set; }
        [MessageBodyMember(Order = 7, Namespace = "http://MyCompany.com/Employee")]
        public int HourlyPay { get; set; }
        [MessageBodyMember(Order = 8, Namespace = "http://MyCompany.com/Employee")]
        public int HoursWorked { get; set; }
    }

    //[KnownType(typeof(FullTimeEmployee))]
    //[KnownType(typeof(PartTimeEmployee))]
    [DataContract]
    public class Employee
    {
        private int _id;
        private string _name;
        private string _gender;
        private DateTime _dateOfBirth;

        [DataMember]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [DataMember]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        [DataMember]
        public string Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        [DataMember]
        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set { _dateOfBirth = value; }
        }
        [DataMember]
        public EmployeeType Type { get; set; }

        public enum EmployeeType
        {
            FullTimeEmployee = 1,
            PartTimeEmployee = 2
        }
    }
}
