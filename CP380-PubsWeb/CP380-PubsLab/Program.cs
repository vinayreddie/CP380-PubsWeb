using System;
using System.Linq;

namespace CP380_PubsLab
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dbcontext = new Models.PubsDbContext())
            {
                if (dbcontext.Database.CanConnect())
                {
                    Console.WriteLine("Yes, I can connect");
                }

                // 1:Many
                var emp = dbcontext.Employee.ToList();
                var j = dbcontext.Jobs.ToList();

                Console.WriteLine("Employee List");
                foreach (var employee in emp)
                {
                    Console.WriteLine("\t> " + employee.fname + " " + employee.lname + " (" + dbcontext.Jobs.First(j => j.job_id == employee.job_id).job_desc + ")");
                }

                Console.WriteLine("\n\nJob List");
                foreach (var job in j)
                {
                    Console.WriteLine("\t" + job.job_desc);
                   
                    var e = dbcontext.Employee.Where(e => e.job_id == job.job_id).ToList();
                    foreach (var employee in e)
                    {
                        Console.WriteLine("\t\t" + employee.fname + " " + employee.lname);
                    }
                }

                // Many:Many
                var stores = dbcontext.Stores.ToList();
                var titles = dbcontext.Titles.ToList();
                var sales = dbcontext.Sales.ToList();

                Console.WriteLine("\n\nStores");
                foreach (var store in stores)
                {
                    Console.Write("\t" + store.stor_name + " => ");
                    var tempSales = sales.Where(s => s.stor_id == store.stor_id).ToList();
                    foreach(var sale in tempSales)
                    {
                        Console.Write(titles.First(t => t.title_id == sale.title_id).title + ", ");
                    }
                    Console.WriteLine("\n");
                }

                Console.WriteLine("\n\nBooks");
                foreach (var title in titles)
                {
                    Console.Write("\t" + title.title + " => ");
                    var tempSales = sales.Where(s => s.title_id == title.title_id).ToList();
                    foreach (var sale in tempSales)
                    {
                        Console.Write(stores.First(t => t.stor_id == sale.stor_id).stor_name + ", ");
                    }
                    Console.WriteLine("\n");
                }
            }
        }
    }
}
