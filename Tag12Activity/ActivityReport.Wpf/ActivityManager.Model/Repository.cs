using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityManager.Model
{
    public class Repository
    {
        List<Employee> emps;

        private static Repository instance;

        List<Activity> activs;

        private Repository()
        {
            emps = new List<Employee>();
            activs = new List<Activity>();
            LoadActivs();
            LoadEmps();
        }

        public static Repository GetInstance()
        {
            if (instance == null)
                instance = new Repository();

            return instance;
        }

        private void LoadActivs()
        {
            activs.Add(new Activity() { Id = (activs.Count + 1), Datum = new DateTime(2017, 10, 12), From = "12:00", To = "14:00", Description = "Vorbereitung Schulung" });
            activs.Add(new Activity() { Id = (activs.Count + 1), Datum = new DateTime(2017, 10, 12), From = "14:00", To = "17:00", Description = "Durchführung Schulung" });
        }
        private void LoadEmps()
        {
            emps.Add(new Employee() { Id = (emps.Count + 1), FirstName = "Sleepy", LastName = "Ash", Activities= activs});
            emps.Add(new Employee() { Id = (emps.Count + 1), FirstName = "Nya", LastName = "Nyan" });
        }

        public List<Employee> GetAllEmployees()
        {
            return emps.OrderBy(x => x.LastName).ToList(); 
        }

        public void AddEmployee(Employee emp)
        {
            emps.Add(emp);
        }

        public void DeleteEmployee(Employee emp)
        {
            emps.Remove(emp);
        }

        public void Update(Employee emp)
        {
            emps.Find(e => e.Id == emp.Id).FirstName = emp.FirstName;
            emps.Find(e => e.Id == emp.Id).LastName = emp.LastName;
        }

        public List<Activity> GetActivities(Employee emp)
        {
            return emps.Find(a => a.Id == emp.Id).Activities;
        }
    }
}
