using Newtonsoft.Json;
using TestApp.Model;

namespace TestApp.Services
{
    public static class FileManager
    {
        private static string filePath = Environment.CurrentDirectory + "\\Employees.json";
        //private static string filePath = "C:\\Users\\ukukk\\Desktop\\Job\\iConText Group\\TestApp\\TestApp\\bin\\Debug\\net7.0\\EmployeesTest.json";

        public static bool CreateNewEmployee(Employee newEmployee)
        {
            if (File.Exists(filePath))// Если файл существует
            {
                try
                {
                    var allEmployees = GetAllEmployees();
                    allEmployees.Add(newEmployee); // добавляет в текущую коллекцию сотрудников нового сотрудника

                    var jsonNewEmployee = JsonConvert.SerializeObject(allEmployees);

                    File.WriteAllText(filePath, jsonNewEmployee); // перезаписываем обновновленный json

                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                try
                {
                    List<Employee> employees = new List<Employee>();
                    employees.Add(newEmployee); // если файл не найден, создаем новую коллекцию, добавляем нового сотрудника

                    var jsonNewEmployee = JsonConvert.SerializeObject(employees);

                    filePath = Environment.CurrentDirectory + ("\\EmployeesCopy.json");

                    File.WriteAllText(filePath, jsonNewEmployee); // записываем в новый json

                    return true;
                }
                catch
                {
                    return false;
                }

            }
        }
        public static Employee? GetEmployeeById(int ID)
        {
            var allEmployees = GetAllEmployees();

            if (allEmployees is null)
            {
                return null;
            }

            Employee? employee = allEmployees.FirstOrDefault(emp => emp.EmployeeID == ID);
            return employee;
        }
        public static List<Employee>? GetAllEmployees()
        {
            if (File.Exists(filePath))
            {
                try
                {
                    string jsonContent = File.ReadAllText(filePath);  // Чтение из json-файла

                    var employeesForFile = JsonConvert.DeserializeObject<List<Employee>>(jsonContent);// Десериализация json в List<Employee>

                    return employeesForFile;
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        public static int GetNewID()
        {
            var allEmployees = GetAllEmployees();

            if (allEmployees is null)
            {
                return 1;
            }

            int ID = allEmployees.Any() ? allEmployees.Max(emp => emp.EmployeeID) + 1 : 1;

            return ID;
        }
        public static bool DeleteEmployee(int ID)
        {
            try
            {
                var allEmployees = GetAllEmployees();

                allEmployees.Remove(allEmployees.FirstOrDefault(x=>x.EmployeeID == ID));

                var jsonEmployees = JsonConvert.SerializeObject(allEmployees);

                File.WriteAllText(filePath, jsonEmployees);

                return true;
            }
            catch 
            {
                return false;
            }
        }
        public static bool UpdateEmployee(Employee editEmployee)
        {
            var allEmployees = GetAllEmployees();

            var employee = allEmployees?.FirstOrDefault(x => x.EmployeeID == editEmployee.EmployeeID);

            if (employee is null)
            {
                return false;
            }

            employee.LastName = editEmployee.LastName;
            employee.FirstName = editEmployee.FirstName;
            employee.MiddleName = editEmployee.MiddleName;
            employee.SalaryPerHour = editEmployee.SalaryPerHour;

            var jsonEmployees = JsonConvert.SerializeObject(allEmployees);

            File.WriteAllText(filePath, jsonEmployees);

            return true;
        }
    }
}
