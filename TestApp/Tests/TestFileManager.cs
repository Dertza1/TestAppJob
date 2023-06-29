using TestApp.Model;
using TestApp.Services;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        //��� ������������ ���� � ����� �������� ���������, ������������� �� ����� :(

        [Test]
        public void UpdateEmployee_ReturnTrue()// ���������� ������ (��������� � id 3) � ����� EmployeesTest.json
        {

            var employee = new Employee(3, "�������", "���", "��������", 200);

            var result = FileManager.UpdateEmployee(employee);

            Assert.IsTrue(result);

            var allEmployees = FileManager.GetAllEmployees();
            var updatedEmployee = allEmployees.FirstOrDefault(x => x.EmployeeID == employee.EmployeeID);

            Assert.IsNotNull(updatedEmployee);
            Assert.AreEqual(employee.LastName, updatedEmployee.LastName);
            Assert.AreEqual(employee.FirstName, updatedEmployee.FirstName);
            Assert.AreEqual(employee.MiddleName, updatedEmployee.MiddleName);
            Assert.AreEqual(employee.SalaryPerHour, updatedEmployee.SalaryPerHour);
        }

        [Test]
        public void UpdateEmployee_ReturnFalse()// ���������� ������ (��������� � id 99) � ����� EmployeesTest.json
        {

            var employee = new Employee(99, "�������", "���", "��������", 200); //!any id

            var result = FileManager.UpdateEmployee(employee);

            Assert.IsFalse(result);
        }

        [Test]
        public void DeleteEmployee_ReturnTrue()// �������� �� ������� �� ����� EmployeesTest.json
        {
            int employeeID = 7;

            var result = FileManager.DeleteEmployee(employeeID);

            var employee = FileManager.GetAllEmployees().FirstOrDefault(x=>x.EmployeeID == employeeID);

            Assert.IsTrue(result);
            Assert.IsNull(employee);
        }

        [Test]
        public void GetAllEmployees_ReturnTrue()// �������� �� ������� �� ����� EmployeesTest.json
        {
            int countExpected = 8;

            var employees = FileManager.GetAllEmployees();

            var countActual = employees.Count;

            Assert.AreEqual(countExpected, countActual);
            
            Assert.IsTrue(employees.Any());
        }
    }
}