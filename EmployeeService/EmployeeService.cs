﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EmployeeService" in both code and config file together.
    public class EmployeeService : IEmployeeService
    {
        public Employee GetEmployee(int Id)
        {
            Employee employee = null;
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spGetEmployee", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter parameterId = new SqlParameter();
                parameterId.ParameterName = "@Id";
                parameterId.Value = Id;
                cmd.Parameters.Add(parameterId);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if ((Employee.EmployeeType)reader["EmployeeType"] == Employee.EmployeeType.FullTimeEmployee)
                    {
                        employee = new FullTimeEmployee
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Gender = reader["Gender"].ToString(),
                            DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                            Type = Employee.EmployeeType.FullTimeEmployee,
                            AnnualSalary = Convert.ToInt32(reader["AnnualSalary"])
                        };
                    }
                    else
                    {
                        employee = new PartTimeEmployee
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Gender = reader["Gender"].ToString(),
                            DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                            Type = Employee.EmployeeType.PartTimeEmployee,
                            HourlyPay = Convert.ToInt32(reader["HourlyPay"]),
                            HoursWorked = Convert.ToInt32(reader["HoursWorked"])
                        };
                    }
                }
            }
            return employee;
        }

        public void SetEmployee(Employee employee)
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spSaveEmployee", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter parameterId = new SqlParameter
                {
                    ParameterName = "@Id",
                    Value = employee.Id
                };
                cmd.Parameters.Add(parameterId);

                SqlParameter parameterName = new SqlParameter
                {
                    ParameterName = "@Name",
                    Value = employee.Name
                };
                cmd.Parameters.Add(parameterName);

                SqlParameter parameterGender = new SqlParameter
                {
                    ParameterName = "@Gender",
                    Value = employee.Gender
                };
                cmd.Parameters.Add(parameterGender);

                SqlParameter parameterDateOfBirth = new SqlParameter
                {
                    ParameterName = "@DateOfBirth",
                    Value = employee.DateOfBirth
                };
                cmd.Parameters.Add(parameterDateOfBirth);

                SqlParameter parameterEmployeeType = new SqlParameter
                {
                    ParameterName = "@EmployeeType",
                    Value = employee.Type
                };
                cmd.Parameters.Add(parameterEmployeeType);

                if (employee.GetType()==typeof(FullTimeEmployee))
                {
                    SqlParameter parameterAnnualSalary = new SqlParameter
                    {
                        ParameterName = "@AnnualSalary",
                        Value = ((FullTimeEmployee) employee).AnnualSalary
                    };
                    cmd.Parameters.Add(parameterAnnualSalary);
                }
                else
                {
                    SqlParameter parameterHourlyPay = new SqlParameter
                    {
                        ParameterName = "@HourlyPay",
                        Value = ((PartTimeEmployee)employee).HourlyPay
                    };
                    cmd.Parameters.Add(parameterHourlyPay);

                    SqlParameter parameterHoursWorked = new SqlParameter
                    {
                        ParameterName = "@HoursWorked",
                        Value = ((PartTimeEmployee)employee).HoursWorked
                    };
                    cmd.Parameters.Add(parameterHoursWorked);
                }

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
