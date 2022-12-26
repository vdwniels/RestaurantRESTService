// See https://aka.ms/new-console-template for more information
using RestaurantBL.Interfaces;
using RestaurantBL.Model;
using RestaurantBL.Services;
using RestaurantDL.Repositories;

string conn = @"Data Source=FRENK\SQLEXPRESS;Initial Catalog=RestaurantRESTdef;Integrated Security=True";

//Console.WriteLine("Hello, World!");
//Table t1 = new Table(1, 1, 2);
//Table t2 = new Table(2, 2, 3);
//Table t3 = new Table(3, 3, 309);
//Table t4 = new Table(4, 4, 4);
//Table t5 = new Table(5, 5, 309);


//List<Table> ts1 = new List<Table>() { t1, t2, t3, t4, t5 };

//int max = ts1.Max(r => r.Seats);


//    Console.WriteLine(max);


// Test AddUser
//IUserRepository repo = new UserRepositoryADO(conn);
//UserService us = new UserService(repo);

//Location l = new Location(9810, "Nazareth");
//l.SetStreet("Schoolstraat");
//User u = new User(45, "Anneke", "Anneke.DeVrieze@gmail.com", "0462311653", l);

//us.UnsubscribeUser(u.CustomerNumber);

//Location l = new Location(9000, "Drongen");
//User u = new User(4, "Aneke", "Aneke.DeVrieze@gmail.com", "0462311664", l);

//us.UpdateUser(u);

IRestautantRepository repo = new RestaurantRepositoryADO(conn);
RestautantService rs = new RestautantService(repo);

//Location l = new Location(9000, "Gent");
//Restaurant r = new Restaurant(1,"IlPunto", l, "It.", "Il.Puntoooo@gmail.com", "0965445319");

//rs.AddRestaurant(r);


//ITableRepository repo = new TableRepositoryADO(conn);
//TableService ts = new TableService(repo);

//Table t = new Table(2, 4, 1);
//ts.AddTable(t);

//rs.UpdateRestaurant(r);
IReadOnlyList<Restaurant> restaurants = rs.SearchRestaurantsWithFreeTables(DateTime.Today.AddDays(2), 2);

foreach (Restaurant r in restaurants)
{
    Console.WriteLine(r.ToString());
}