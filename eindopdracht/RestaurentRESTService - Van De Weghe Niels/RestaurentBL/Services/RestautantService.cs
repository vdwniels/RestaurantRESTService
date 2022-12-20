﻿using RestaurantBL.Exceptions;
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
    public class RestautantService
    {
        private IRestautantRepository repo;

        public RestautantService(IRestautantRepository repo)
        {
            this.repo = repo;
        }

        public IReadOnlyList<Restaurant> SearchRestaurantOnLocationAndOrCuisine(int? postalcode, string? cuisine)
        {
            try
            {
                if (cuisine.Trim() == "") cuisine = null;
                if ((postalcode == null) && (cuisine == null)) throw new RestaurantServiceException("RestaurantService - SearchRestaurantOnLocationAndOrCuisine - no entries");
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

        public IReadOnlyList<Restaurant> GetRestaurantsWithFreeTables(DateTime date, int seats)
        {
            try
            {
                if (date == null) throw new RestaurantServiceException("RestaurantService - GetRestaurantsWithFreeTables - no date entry");
                if (date < DateTime.Now) throw new RestaurantServiceException("RestaurantService - GetRestaurantsWithFreeTables - free tables in the past are expired");
                if (seats <= 0) throw new RestaurantServiceException("RestaurantService - GetRestaurantsWithFreeTables - at least one attendee required");
                IReadOnlyList<Restaurant> list = new List<Restaurant>();
                list = repo.GetRestaurantsWithFreeTables(date, seats);
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
    }
}
