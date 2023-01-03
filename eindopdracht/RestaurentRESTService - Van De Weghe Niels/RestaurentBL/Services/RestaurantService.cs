using RestaurantBL.Exceptions;
using RestaurantBL.Interfaces;
using RestaurantBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBL.Services
{
    public class RestaurantService
    {
        private IRestautantRepository repo;

        public RestaurantService(IRestautantRepository repo)
        {
            this.repo = repo;
        }

        public IReadOnlyList<Restaurant> SearchRestaurantOnLocationAndOrCuisine(int? postalcode, string? cuisine)
        {
            try
            {
                if ((postalcode == null) && (string.IsNullOrEmpty(cuisine))) throw new RestaurantServiceException("RestaurantService - SearchRestaurantOnLocationAndOrCuisine - no entries");
                if ((postalcode != null) && (postalcode.ToString().Length != 4)) throw new RestaurantServiceException("RestaurantService - SearchRestaurantOnLocationAndOrCuisine - postalcode must be 4 digits");
                IReadOnlyList<Restaurant> list = new List<Restaurant>();
                list = repo.SearchRestaurantOnLocationAndOrCuisine(postalcode, cuisine);
                return list;
            }
            catch (RestaurantServiceException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new RestaurantServiceException("SearchRestaurantOnLocationAndOrCuisine", ex);
            }
        }

        public IReadOnlyList<Restaurant> SearchRestaurantsWithFreeTables(DateTime date, int seats)//TODO further testing
        {
            try
            {
                if (date == null) throw new RestaurantServiceException("RestaurantService - GetRestaurantsWithFreeTables - no date entry");
                if (date < DateTime.Now) throw new RestaurantServiceException("RestaurantService - GetRestaurantsWithFreeTables - free tables in the past are expired");
                if (seats <= 0) throw new RestaurantServiceException("RestaurantService - GetRestaurantsWithFreeTables - at least one attendee required");
                IReadOnlyList<Restaurant> list = new List<Restaurant>();
                list = repo.GetRestaurantsWithFreeTables(date, seats);
                if (list.Count == 0) throw new RestaurantServiceException("There are no free tables at this time for this amount of people. However, we may assign you a larger table. Feel free to search for a table with more seats.");
                return list;
            }
            catch (RestaurantServiceException)
            {
                throw;
            }
            catch(Exception ex)
            {
                throw new RestaurantServiceException("GetRestaurantsWithFreeTables", ex);
            }
        }

        public Restaurant AddRestaurant (Restaurant restaurant)
        {
            try
            {
                if (restaurant == null) throw new RestaurantServiceException("RestaurantService - AddRestaurant - no restaurant entry");
                if (repo.RestaurantExists(restaurant.PhoneNumber,restaurant.Email)) throw new RestaurantServiceException("RestaurantService - AddRestaurant - Restaurant with this phonenumber and/or email already exists");
                Restaurant restaurantWithId = repo.AddRestaurant(restaurant);
                return restaurantWithId;
            }
            catch (RestaurantServiceException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new RestaurantServiceException("AddRestaurant", ex);
            }
        }

        public void DeleteRestaurant (int restaurantId) //TODO test when all tables are IsDeleted = 1
        {
            try
            {
                if (!repo.RestaurantExists(restaurantId)) throw new RestaurantServiceException("RestaurantService - DeleteRestaurant - restaurant doesn't exist");
                if (repo.HasTables(restaurantId)) throw new RestaurantServiceException("RestaurantService - DeleteRestaurant - restaurant still has tables");
                repo.DeleteRestaurant(restaurantId);
            }
            catch (RestaurantServiceException)
            {
                throw;
            }
            catch(Exception ex)
            {
                throw new RestaurantServiceException("DeleteRestaurant", ex);
            }
        }

        public void UpdateRestaurant(Restaurant restaurant)
        {
            try
            {
                if (restaurant == null) throw new RestaurantServiceException("RestaurantService - UpdateRestaurant - no restaurant data entry");
                if (!repo.RestaurantExists(restaurant.RestaurantId)) throw new RestaurantServiceException("RestaurantService - UpdateRestaurant - restaurant doesn't exist");
                Restaurant currentRestaurant = repo.GetRestaurant(restaurant.RestaurantId);

                //#region test

                //foreach(Table t in currentRestaurant._tables.Keys)
                //{
                //    Console.WriteLine($"{currentRestaurant.Name} --- {t.TableNumber} - {t.Seats} - {t.RestaurantId}");
                //}

                //#endregion

                if (restaurant == currentRestaurant || currentRestaurant == null) throw new RestaurantServiceException("RestaurantService - UpdateRestaurant - no different values");// operator overload
                repo.UpdateRestaurant(restaurant);
            }
            catch (RestaurantServiceException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new RestaurantServiceException("UpdateRestaurant", ex);
            }
        }

        public Restaurant GetRestaurant(int restaurantId)
        {
            try
            {
                if (!repo.RestaurantExists(restaurantId)) throw new RestaurantServiceException("RestaurantService - GetRestaurant - restaurant doesn't exist");
                return repo.GetRestaurant(restaurantId);
            }
            catch (RestaurantServiceException)
            {
                throw;
            }
            catch(Exception ex)
            {
                throw new RestaurantServiceException("GetRestaurant", ex);
            }
        }

        public void DeleteAllTables(int restaurantId)
        {
            try
            {
                if (!repo.RestaurantExists(restaurantId)) throw new RestaurantServiceException("RestaurantService - DeleteAllTables - restaurant doesn't exist");
                repo.DeleteAllTables(restaurantId);
            }
            catch (TableServiceException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new TableServiceException("DeleteTable", ex);
            }
        }
    }
}
