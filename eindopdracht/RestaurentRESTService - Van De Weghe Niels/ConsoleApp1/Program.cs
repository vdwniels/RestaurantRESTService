// See https://aka.ms/new-console-template for more information
using RestaurantBL.Model;

//Location l1 = new Location(9880, "Aalter");
//Restaurant r = new Restaurant("b", l1, "b", "a.a@gmail.com", "0493493718");
//Table t1 = new Table(1, 6);
//Table t2 = new Table(2, 2);
//r.AddTable(t1);
//r.AddTable(t2);
//ContactDetails cd = new ContactDetails("b.b@gmail.com","0456324578");
//Reservation reser = new Reservation(r, 1, cd, 2, 2, DateTime.Now);
//reser.SetSeats(3);

//string s = "18:00-19:30 20:00-21:30";
//string[] sub1 = s.Split('-', ' ');

//foreach (string st in sub1)
//{
//    DateTime dt = DateTime.ParseExact(st, "HH:mm", null);
//    Console.WriteLine(dt.Hour + " " + dt.Minute);
//}

//DateTime dt1 = DateTime.ParseExact("20:30", "HH:mm", null);
//DateTime dt2 = DateTime.ParseExact("23/12/2022", "dd/MM/yyyy", null);
//string date = $"{dt2.Day}/{dt2.Month}/{dt2.Year}";
//Console.WriteLine(dt1.Hour + "        " + dt1.Minute + "            " + date);
//string hour = dt1.ToString("HH:mm");
//string datee = dt2.ToString("dd/MM/yyyy");
//Console.WriteLine(datee +"         "+hour);
//DateTime dt3 = DateTime.ParseExact("21:30", "HH:mm", null);
//DateTime dt4 = DateTime.ParseExact("20:00", "HH:mm", null);

//Console.WriteLine(dt1 > dt3);

Table t1 = new Table(1, 1, 2);
Table t2 = new Table(2, 2, 3);
Table t3 = new Table(3, 3, 6);
Table t4 = new Table(4, 4, 4);
Table t5 = new Table(5, 5, 1);


List<Table> ts1 = new List<Table>() { t1,t2, t3, t4, t5};
List<Table> ts2 = new List<Table>() { t2, t4};

List<Table> ts3 = ts1.Except(ts2).ToList();

foreach (Table t in ts3)
{
    Console.WriteLine(t.TableNumber);
}











