// See https://aka.ms/new-console-template for more information
using RestaurantBL.Interfaces;
using RestaurantBL.Model;
using RestaurantBL.Services;
using RestaurantDL.Repositories;
using System.Collections.Generic;

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

IRestautantRepository restorepo = new RestaurantRepositoryADO(conn);
RestaurantService restoserv= new RestaurantService(restorepo);

//Location l = new Location(9000, "Gent");
//Restaurant r = new Restaurant(1,"IlPunto", l, "It.", "Il.Puntoooo@gmail.com", "0965445319");

//rs.AddRestaurant(r);


//ITableRepository repo = new TableRepositoryADO(conn);
//TableService ts = new TableService(repo);

//Table t = new Table(2, 4, 1);
//ts.AddTable(t);

//rs.UpdateRestaurant(r);
//IReadOnlyList<Restaurant> restaurants = rs.SearchRestaurantsWithFreeTables(DateTime.Today.AddDays(2), 2);

//foreach (Restaurant r in restaurants)
//{
//    Console.WriteLine(r.ToString());
//}

ITableRepository tablerepo = new TableRepositoryADO(conn);
TableService ts = new TableService(tablerepo);

//Table t = new Table(1004,10, 9, 4);

//ts.AddTable(t);
//List<Table> tables = ts.GetAllTablesOfRestaurant(2);
//foreach (Table table in tables)
//{
//    Console.WriteLine(table.ToString());
//}

//ts.UpdateTable(t);

IReservationRepository resRepo = new ReservationRepositoryADO(conn);

ReservationService rs = new ReservationService(resRepo,ts);

//ReservationRepositoryADO a = new ReservationRepositoryADO(conn);
//List<Table> t = a.SelectFreeTables(1, new DateTime(2022, 12, 27, 18, 30, 00));

//foreach (Table t2 in t)
//{
//    Console.WriteLine(t2.ToString());
//}

//Location l = new Location(9810, "Nazareth");
//l.SetStreet("Warandestraat");
//Restaurant r = new Restaurant(1, "IlPunto", l, "It.", "il.puntoooo@gmail.com", "0965445319");


//User u = new User(2, "Ann", "ann.desmet@gmail.com", "0462322353", l);

//Reservation reserv = new Reservation(r, u, 2, new DateTime(2022, 12, 29, 20, 30, 00));
//rs.AddReservation(reserv);

//int t = rs.SelectFreeTable(null, 1, new DateTime(2022, 12, 27, 20, 00, 00), 1);
//Console.WriteLine(t);

UserRepositoryADO userRepo = new UserRepositoryADO(conn);
User u = userRepo.GetUser(1);
Console.WriteLine(u.Name);