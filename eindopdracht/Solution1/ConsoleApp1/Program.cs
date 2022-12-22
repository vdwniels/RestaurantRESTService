// See https://aka.ms/new-console-template for more information
using RestaurantBL.Model;

Console.WriteLine("Hello, World!");
Table t1 = new Table(1, 1, 2);
Table t2 = new Table(2, 2, 3);
Table t3 = new Table(3, 3, 6);
Table t4 = new Table(4, 4, 4);
Table t5 = new Table(5, 5, 1);


List<Table> ts1 = new List<Table>() { t1, t2, t3, t4, t5 };
List<Table> ts2 = new List<Table>() { t2, t4 };

List<Table> ts3 = ts1.Except(ts2).ToList();

foreach (Table t in ts3)
{
    Console.WriteLine(t.TableNumber);
}
