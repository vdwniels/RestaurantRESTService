// See https://aka.ms/new-console-template for more information
using RestaurantBL.Model;

Console.WriteLine("Hello, World!");
Location location = new Location(9000, "gent");
location.SetStreet("korenmarkt");
location.SetNumber("33");
User user = new User("bob", "BOB.BAUER@Gmail.com", "+3263978421", location);
Console.WriteLine(user.ToString());
