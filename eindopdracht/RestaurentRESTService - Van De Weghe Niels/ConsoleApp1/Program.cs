// See https://aka.ms/new-console-template for more information
using RestaurantBL.Model;

Location l1 = new Location(9880, "Aalter");
Restaurant r = new Restaurant("b", l1, "b", "a.a@gmail.com", "0493493718");
Table t1 = new Table(1, 6);
Table t2 = new Table(2, 2);
r.AddTable(t1);
r.AddTable(t2);
ContactDetails cd = new ContactDetails("b.b@gmail.com","0456324578");
Reservation reser = new Reservation(r, 1, cd, 2, 2, DateTime.Now);
reser.SetSeats(3);