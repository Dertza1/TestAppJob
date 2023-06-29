namespace TestApp.Model
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; } 
        public string? MiddleName { get; set; } 
        public decimal SalaryPerHour { get; set; } 

        public Employee(int employeeID, string lastName, string firstName, string? middleName, decimal salaryPerHour)
        {
            EmployeeID = employeeID;
            LastName = lastName;
            FirstName = firstName;
            MiddleName = middleName;
            SalaryPerHour = salaryPerHour;
        }
    }
}
