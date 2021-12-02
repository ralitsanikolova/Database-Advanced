using SoftUni.Data;
using SoftUni.Models;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SoftUni
{
    public class StartUp
    {
        static void Main(string[] args)
        {
        }
        public static string GetEmployeesFullInformation(SoftUniContext context)
        {

            var employees = context.Employees
                .OrderBy(e => e.EmployeeId)
                .Select(e => new { e.FirstName, e.LastName, e.MiddleName, e.JobTitle, e.Salary })
                .ToList();

            StringBuilder stringBuilder = new StringBuilder();
            foreach (var employee in employees)
            {
                stringBuilder.Append($"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {employee.Salary:f2}");
            }

            return stringBuilder.ToString();
        }

        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            var employees = context.Employees
                .Where(e => e.Salary > 5000)
                .Select(e => new { e.FirstName, e.Salary })
                .ToList();

            StringBuilder stringBuilder = new StringBuilder();
            foreach (var employee in employees)
            {
                stringBuilder.Append($"{employee.FirstName} - {employee.Salary:F2}");
                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }

        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            var employees = context.Employees
                .OrderBy(e => e.Salary)
                .ThenByDescending(e => e.FirstName)
                 .Where(e => e.Department.Name == "Research and Development")
                .Select(e => new { e.FirstName, e.LastName, Department = e.Department.Name, e.Salary });

            StringBuilder stringBuilder = new StringBuilder();
            foreach (var employee in employees)
            {
                stringBuilder.Append($"{employee.FirstName} {employee.LastName} from {employee.Department} - {employee.Salary:F2}");
                stringBuilder.AppendLine();
            }
            return stringBuilder.ToString();
        }

        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            Address newAddress = new Address()
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };

            context.Employees
                .Single(e => e.LastName == "Nakov")
                .Address = newAddress;
            context.SaveChanges();

            var employees = context.Employees
                .Take(10)
                .OrderByDescending(e => e.Address.AddressId)
                .Select(e => new { e.Address.AddressText, e.Address.AddressId })
                .ToList();

            StringBuilder stringBuilder = new StringBuilder();

            foreach (var employee in employees)
            {
                stringBuilder.AppendLine($"{employee.AddressText}");
            }
            return stringBuilder.ToString();
        }

        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            var employees = context.Employees
                .Where(e => e.EmployeesProjects.Any(
                    epr => epr.Project.StartDate.Year >= 2001 && epr.Project.StartDate.Year <= 2003))
                .Take(10)
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    ManagerFirstName = e.Manager.FirstName,
                    ManagerLastName = e.Manager.LastName,
                    Project = e.EmployeesProjects
                        .Select(epr => new
                        {
                            epr.Project,
                            ProjectName = epr.Project.Name,
                            StartDate = epr.Project.StartDate,
                            EndDate = epr.Project.EndDate
                        })
                })
                .ToList();

            StringBuilder stringBuilder = new StringBuilder();

            foreach (var employee in employees)
            {
                stringBuilder.AppendLine($"{employee.FirstName} {employee.LastName} - Manager: {employee.ManagerFirstName} {employee.ManagerLastName}");

                foreach (var projects in employee.Project)
                {
                    string startDate = projects.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                    string endDate = projects.EndDate.ToString();

                    stringBuilder.AppendLine($"--{projects.ProjectName} - {startDate} - {endDate}");

                    if (projects.EndDate != null)
                    {
                        stringBuilder.Append($"- not finished");
                    }
                }
            }
            return stringBuilder.ToString();
        }

        public static string GetAddressesByTown(SoftUniContext context)
        {
            var employees = context.Addresses
                .OrderByDescending(e => e.Employees)
                .ThenBy(e => e.Town.Name)
                .ThenBy(e => e.AddressText)
                .Select(e => new
                {
                    e.AddressText,
                    TownName = e.Town.Name,
                    EmployeeCount = e.Employees.Count()
                })
                .Take(10)
                .ToList();
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var employee in employees)
            {
                stringBuilder.AppendLine($"{employee.AddressText}, {employee.TownName}-{employee.EmployeeCount}");
            }
            return stringBuilder.ToString();
        }

        public static string GetEmployee147(SoftUniContext context)
        {
            var emloyees = context.Employees
                .Where(e => e.EmployeeId == 147)
                .OrderBy(e => e.FirstName)
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.JobTitle,
                    Projects = e.EmployeesProjects
                        .Select(ep => ep.Project.Name)
                        .ToList()
                })
                .First();

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"{emloyees.FirstName} {emloyees.LastName} - {emloyees.JobTitle}");
            stringBuilder.AppendLine($"{emloyees.Projects}");

            return stringBuilder.ToString();
        }

        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            var departments = context.Departments
                .OrderBy(d => d.Employees.Count)
                .ThenBy(d => d.Name)
                .Where(d => d.Employees.Count > 5)
                .Select(d => new
                {
                    d.Name,
                    d.Manager.FirstName,
                    d.Manager.LastName,
                    Employees = d.Employees
                   .Select(e => new
                   {
                       e.FirstName,
                       e.LastName,
                       e.JobTitle
                   })
                })
                .ToList();

            StringBuilder stringBuilder = new StringBuilder();
            foreach (var depart in departments)
            {
                stringBuilder.AppendLine($"{depart.FirstName} {depart.LastName}");
                foreach (var emp in depart.Employees)
                {
                    stringBuilder.AppendLine($"{emp.FirstName} {emp.LastName} {emp.JobTitle}");
                }
            }
            return stringBuilder.ToString();
        }

        public static string GetLatestProjects(SoftUniContext context)
        {
            var projects = context.Projects
                .OrderByDescending(p => p.StartDate)
                .Take(10)
                .Select(p => new
                {
                    p.Name,
                    p.Description,
                    p.StartDate
                })
                .OrderBy(p => p.Name)
                .ToList();

            StringBuilder stringBuilder = new StringBuilder();
            foreach (var project in projects)
            {
                var startDate = project.StartDate.ToString("M/d/yyyy h:mm:ss tt");
                stringBuilder.AppendLine($"{project.Name}");
                stringBuilder.AppendLine($"{project.Description}");
                stringBuilder.AppendLine($"{startDate}");
            }
            return stringBuilder.ToString();
        }

        public static string IncreaseSalaries(SoftUniContext context)
        {
            var employees = context.Employees
                 .Where(e => e.Department.Name.Contains(e.Department.Name))
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    Salary = e.Salary + e.Salary * 0.12m
                })
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName);


            StringBuilder stringBuilder = new StringBuilder();
            foreach (var employee in employees)
            {
                stringBuilder.AppendLine($"{employee.FirstName} {employee.LastName}(${employee.Salary:F2}");
            }
            return stringBuilder.ToString();
        }

        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            var employees = context.Employees
                .Where(e => e.FirstName.StartsWith("Sa"))
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.Salary
                });

            StringBuilder stringBuilder = new StringBuilder();
            foreach (var employee in employees)
            {
                stringBuilder.AppendLine($"{employee.FirstName} - {employee.LastName} - (${employee.Salary:F2})");
            }
            return stringBuilder.ToString();
        }

        public static string DeleteProjectById(SoftUniContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var employeeProjects = context.EmployeesProjects
                .Where(x => x.ProjectId == 2)
                .ToList();
            foreach (var item in employeeProjects)
            {
                context.Remove(item);
                context.SaveChanges();
            }

            context.Remove(context.Projects.Where(x => x.ProjectId == 2).FirstOrDefault());
            context.SaveChanges();

            var leftProjects = context.Projects
                .Take(10)
                .ToList();

            foreach (var item in leftProjects)
            {
                stringBuilder.AppendLine(item.Name);
            }
            return stringBuilder.ToString();
        }
    }

}





