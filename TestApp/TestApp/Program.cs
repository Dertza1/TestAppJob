using TestApp.Model;
using TestApp.Services;

namespace TestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FunctionSelection();

            static void FunctionSelection()
            {
                Console.Clear();
                Console.WriteLine(
                    "Выберите функцию:\n" +
                    "- - - - - - - - - - - - - - " +
                    "\n1 - Просмотр всех сотрудников" +
                    "\n2 - Выбор сотрудника по ключу" +
                    "\n3 - Добавить нового сотрудника" +
                    "\n4 - Редактировать сотрудника" +
                    "\n5 - Удалить сотрудника");

                int functionID = 0;

                try
                {
                    functionID = Convert.ToInt32(Console.ReadLine().Trim());
                }
                catch
                {
                    Console.WriteLine("Выберите функцию из списка по ключу");
                    Console.ReadKey();
                    
                    FunctionSelection();
                }

                switch (functionID)
                {
                    case 1:
                        GetAllEmployees();
                        break;

                    case 2:
                        GetEmployeeByID();
                        break;

                    case 3:
                        CreateNewEmployee();
                        break;

                    case 4:
                        EditEmployee();
                        break;

                    case 5:
                        DeleteEmployee();
                        break;

                    default:
                        Console.WriteLine("Вам доступно 5 функций");
                        Console.ReadKey();

                        FunctionSelection();
                        break;
                }

            }

            static void GetAllEmployees()
            {
                Console.Clear();

                var allEmployees = FileManager.GetAllEmployees();

                if (allEmployees is null)
                {
                    Console.WriteLine("Сотрудники не найдены");
                    Console.WriteLine("- - - - - - - - - - - - - - ");
                }
                else
                {
                    Console.WriteLine("Сотрудники");
                    Console.WriteLine("- - - - - - - - - - - - - - ");
                    foreach (var emp in allEmployees)
                    {
                        if (!string.IsNullOrEmpty(emp.MiddleName))
                        {
                            Console.WriteLine($"Id - {emp.EmployeeID}," +
                                $" LastName - {emp.LastName}," +
                                $" FirstName - {emp.FirstName}," +
                                $" MiddleName - {emp.MiddleName}," +
                                $" SalaryPerHour - {emp.SalaryPerHour}");
                        }
                        else
                        {
                            Console.WriteLine($"Id - {emp.EmployeeID}," +
                               $" LastName - {emp.LastName}," +
                               $" FirstName - {emp.FirstName}," +
                               $" SalaryPerHour - {emp.SalaryPerHour}");
                        }
                    }

                }

                Console.WriteLine("- - - - - - - - - - - - - - ");
                Console.WriteLine("Чтобы вернуться назад, нажмите любую клавишу..");
                Console.ReadKey();

                FunctionSelection();
            }

            static void GetEmployeeByID()
            {
                int employeeID = 0;

                Console.Clear();
                Console.Write("Для выбора сотрудника введите ключ (ID):");

                try
                {
                    employeeID = Convert.ToInt32(Console.ReadLine().Trim());
                }
                catch
                {
                    Console.WriteLine("Необходимо ввести корректный данные");
                    Console.ReadKey();

                    GetEmployeeByID();
                }

                Employee? employee = FileManager.GetEmployeeById(employeeID);

                Console.WriteLine("- - - - - - - - - - - - - - ");

                if (employee is null)
                    Console.WriteLine($"Сотрудник[{employeeID}] не был найден");
                else
                {
                    if (!string.IsNullOrEmpty(employee.MiddleName))
                    {
                        Console.WriteLine($"Id - {employee.EmployeeID}," +
                            $" LastName - {employee.LastName}," +
                            $" FirstName - {employee.FirstName}," +
                            $" MiddleName - {employee.MiddleName}," +
                            $" SalaryPerHour - {employee.SalaryPerHour}");
                    }
                    else
                    {
                        Console.WriteLine($"Id - {employee.EmployeeID}," +
                            $" LastName - {employee.LastName}," +
                            $" FirstName - {employee.FirstName}," +
                            $" SalaryPerHour - {employee.SalaryPerHour}");
                    }
                }

                Console.WriteLine("- - - - - - - - - - - - - - \n" +
                    "1 - Повторить\n" +
                    "2 - Вернуться назад");

                string functionId = Console.ReadLine();

                switch (functionId)
                {
                    case "1":
                        GetEmployeeByID();
                        break;
                    case "2":
                        FunctionSelection();
                        break;
                    default:
                        FunctionSelection();
                        break;
                }
            }

            static void CreateNewEmployee()
            {
                Console.Clear();

                Console.WriteLine("1 - Добавить нового пользователя\n2 - Вернуться назад");
                Console.Write("Функция: ");

                string functionId = Console.ReadLine();

                if (functionId == "1")
                {
                    Console.Clear();
                    Console.WriteLine("Добавление нового сотрудника\n- - - - - - - - - - - - - - ");

                    Console.Write("Введите фамилию:");
                    string lastName = Console.ReadLine().Trim();

                    Console.Write("Введите имя:");
                    string firstName = Console.ReadLine().Trim();

                    Console.Write("Введите отчество (необязательно):");
                    string? middleName = Console.ReadLine().Trim();

                    Console.Write("Введите зарплату в час:");
                    string salaryPerHour = Console.ReadLine();

                    if (string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(salaryPerHour))
                    {
                        Console.WriteLine("Необходимо заполнить все поля (исключением может быть отчество)");
                        Console.ReadKey();

                        CreateNewEmployee();
                    }

                    try
                    {
                        Employee newEmployee = new Employee(FileManager.GetNewID(), lastName, firstName, middleName, Convert.ToDecimal(salaryPerHour));

                        if (FileManager.CreateNewEmployee(newEmployee))
                        {
                            Console.WriteLine("- - - - - - - - - - - - - - \n" +
                                "Успешное добавление нового сотрудника");
                        }
                        else
                        {
                            Console.WriteLine("- - - - - - - - - - - - - - \n" +
                                "Не удалось добавить нового сотрудника");
                        }

                        Console.WriteLine("1 - Повторить\n" +
                            "2 - Вернуться назад");

                        functionId = Console.ReadLine();

                        switch (functionId)
                        {
                            case "1":
                                CreateNewEmployee();
                                break;
                            case "2":
                                FunctionSelection();
                                break;
                            default:
                                FunctionSelection();
                                break;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("- - - - - - - - - - - - - - \n" +
                            "Введите корректные данные");
                        Console.ReadKey();

                        CreateNewEmployee();
                    }
                }
                else
                    FunctionSelection();



            }

            static void EditEmployee()
            {

                Console.Clear();

                Console.WriteLine("1 - Редактирование выбранного пользователя\n2 - Вернуться назад");
                Console.Write("Функция: ");

                string functionId = Console.ReadLine();

                if (functionId == "1")
                {
                    Console.Clear();
                    Console.Write("Для редактирования сотрудника введите ключ (ID): ");

                    int employeeID = 0;

                    try
                    {
                        employeeID = Convert.ToInt32(Console.ReadLine().Trim());
                    }
                    catch
                    {
                        Console.WriteLine("Необходимо ввести корректный данные");
                        Console.ReadKey();

                        EditEmployee();
                    }

                    Employee? employee = FileManager.GetEmployeeById(employeeID);

                    Console.WriteLine("- - - - - - - - - - - - - - ");

                    if (employee is null)
                    {
                        Console.WriteLine($"Сотрудник[{employeeID}] не был найден");

                        Console.WriteLine("1 - Повторить\n2 - Вернуться назад");
                        Console.Write("Функция: ");

                        functionId = Console.ReadLine();

                        switch (functionId)
                        {
                            case "1":
                                EditEmployee();
                                break;

                            case "2":
                                FunctionSelection();
                                break;

                            default:
                                FunctionSelection();
                                break;
                        }
                    }

                    EditEmployeeFunctions(employee);
                }  
                else
                    FunctionSelection();
            }

            static void EditEmployeeFunctions(Employee editEmployee)
            {
                Console.Clear();

                if (!string.IsNullOrEmpty(editEmployee.MiddleName))
                {
                    Console.WriteLine($"Id - {editEmployee.EmployeeID}," +
                        $" LastName - {editEmployee.LastName}," +
                        $" FirstName - {editEmployee.FirstName}," +
                        $" MiddleName - {editEmployee.MiddleName}," +
                        $" SalaryPerHour - {editEmployee.SalaryPerHour}");
                }
                else
                {
                    Console.WriteLine($"Id - {editEmployee.EmployeeID}," +
                        $" LastName - {editEmployee.LastName}," +
                        $" FirstName - {editEmployee.FirstName}," +
                        $" SalaryPerHour - {editEmployee.SalaryPerHour}");
                }

                Console.WriteLine("- - - - - - - - - - - - - - \n" +
                    "Выберите поле редактирования:\n" +
                    "1 - Фамилия\n" +
                    "2 - Имя\n" +
                    "3 - Отчество\n" +
                    "4 - Зарплата в час\n" +
                    "5 - Сохранить изменения\n" +
                    "6 - Отменить редактирование");

                string functionId = Console.ReadLine();

                switch (functionId)
                {
                    case "1":
                        Console.Write("Введите фамилию: ");
                        editEmployee.LastName = Console.ReadLine();

                        EditEmployeeFunctions(editEmployee);
                        break;

                    case "2":
                        Console.Write("Введите имя: ");
                        editEmployee.FirstName = Console.ReadLine();

                        EditEmployeeFunctions(editEmployee);
                        break;

                    case "3":
                        Console.Write("Введите Отчество: ");
                        editEmployee.MiddleName = Console.ReadLine();

                        EditEmployeeFunctions(editEmployee);
                        break;

                    case "4":
                        Console.Write("Введите зарплату в час: ");
                        editEmployee.SalaryPerHour = Convert.ToDecimal(Console.ReadLine());

                        EditEmployeeFunctions(editEmployee);
                        break;

                    case "5":

                        if (FileManager.UpdateEmployee(editEmployee))
                        {
                            Console.WriteLine("- - - - - - - - - - - - - - \n" +
                                "Редактирование прошло успешно\n" +
                                "Чтобы продолжить нажмите любую клавишу..");
                            Console.ReadKey();

                            FunctionSelection();
                        }
                        else
                        {
                            Console.WriteLine("- - - - - - - - - - - - - - \n" +
                                "Редактирование не удалось\n" +
                                "Чтобы продолжить нажмите любую клавишу..");
                            Console.ReadKey();

                            FunctionSelection();
                        }
                        break;

                    case "6":
                        EditEmployee();
                        break;

                    default:
                        EditEmployee();
                        break;
                }
            }

            static void DeleteEmployee()
            {
                Console.Clear();

                Console.WriteLine("1 - Выбрать пользователя для удаления\n2 - Вернуться назад");
                Console.Write("Функция: ");

                string functionId = Console.ReadLine();

                if (functionId == "1")
                {
                    Console.Write("- - - - - - - - - - - - - - \nДля удаления сотрудника введите ключ (ID): ");

                    int employeeID = 0;

                    try
                    {
                        employeeID = Convert.ToInt32(Console.ReadLine().Trim());
                    }
                    catch
                    {
                        Console.WriteLine("Необходимо ввести корректный данные");
                        Console.ReadKey();

                        DeleteEmployee();
                    }

                    var deleteEmployee = FileManager.GetEmployeeById(employeeID);

                    if (deleteEmployee is null)
                    {
                        Console.WriteLine("Сотрудник с таким ключом (ID) не найден\nДля продолжение нажмите любую клавишу..");
                        Console.ReadKey();

                        DeleteEmployee();
                    }

                    if (!string.IsNullOrEmpty(deleteEmployee.MiddleName))
                    {
                        Console.WriteLine($"Id - {deleteEmployee.EmployeeID}," +
                            $" LastName - {deleteEmployee.LastName}," +
                            $" FirstName - {deleteEmployee.FirstName}," +
                            $" MiddleName - {deleteEmployee.MiddleName}," +
                            $" SalaryPerHour - {deleteEmployee.SalaryPerHour}");
                    }
                    else
                    {
                        Console.WriteLine($"Id - {deleteEmployee.EmployeeID}," +
                           $" LastName - {deleteEmployee.LastName}," +
                           $" FirstName - {deleteEmployee.FirstName}," +
                           $" SalaryPerHour - {deleteEmployee.SalaryPerHour}");
                    }

                    Console.WriteLine("- - - - - - - - - - - - - - \nПодтверждаю удаление\n1 - Да\n2 - Нет");

                    string deleteConfirmation = Console.ReadLine();

                    switch (deleteConfirmation)
                    {
                        case "1":

                            if (FileManager.DeleteEmployee(deleteEmployee.EmployeeID))
                            {
                                Console.WriteLine("- - - - - - - - - - - - - - \n" +
                                    "Удаление прошло успешно\n" +
                                    "Для продолжения нажмите любую клавишу..");

                                Console.ReadKey();

                                FunctionSelection();
                            }
                            else
                            {
                                Console.WriteLine("- - - - - - - - - - - - - - \n" +
                                    "Удаление не удалось\n" +
                                    "Для продолжения нажмите любую клавишу..");

                                Console.ReadKey();

                                FunctionSelection();
                            }
                            break;

                        case "2":
                            Console.WriteLine("- - - - - - - - - - - - - - \n" +
                                "Удаление отменено\n" +
                                "Для продолжения нажмите любую клавишу..");
                            Console.ReadKey();

                            FunctionSelection();
                            break;

                        default:
                            Console.WriteLine("- - - - - - - - - - - - - - \n" +
                                "Удаление отменено\n" +
                                "Для продолжения нажмите любую клавишу..");
                            Console.ReadKey();

                            FunctionSelection();
                            break;
                    }
                }
                else
                    FunctionSelection();

            }
        }
    }
}