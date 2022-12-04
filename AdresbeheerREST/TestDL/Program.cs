// See https://aka.ms/new-console-template for more information
using AdresBeheerBL.Interfaces;
using AdresBeheerDL.repositories;

Console.WriteLine("Hello, World!");
string connectieString = "Data Source=FRENK\\SQLEXPRESS;Initial Catalog=AdresBeheerREST;Integrated Security=True";
IGemeenteRepository repo = new GemeenteRepositoryADO(connectieString);
var g = repo.GeefGemeente(50000);
Console.WriteLine(g);