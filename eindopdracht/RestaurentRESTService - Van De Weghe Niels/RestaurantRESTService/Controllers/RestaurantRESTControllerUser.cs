using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantBL.Model;
using RestaurantBL.Services;
using RestaurantRESTServiceUser.Mappers;
using RestaurantRESTServiceUser.Model.input;

namespace RestaurantRESTServiceUser.Controllers
{
    [Route("api/[controller]/user")]
    [ApiController]
    public class RestaurantRESTControllerUser : ControllerBase
    {
        private string hostURL = @"http://localhost:5022/api/user/Restaurantbeheer";
        private ReservationService reservationService;
        private RestaurantService restaurantService;
        private UserService userService;
        private TableService tableService;
        private readonly ILogger logger;
        public RestaurantRESTControllerUser(ReservationService reservationService, RestaurantService restaurantService, UserService userService, TableService tableService, ILoggerFactory logger)
        {
            this.reservationService = reservationService;
            this.restaurantService = restaurantService;
            this.userService = userService;
            this.tableService = tableService;
            this.logger = logger.AddFile("LoggingUser").CreateLogger<RestaurantRESTControllerUser>();
        }

        [HttpGet("/GetCustomer/{customerId}")]
        public ActionResult<UserRESTInputDTOUser> GetUser(int customerId)
        {
            try
            {
                User user = userService.GetUser(customerId);
                return Ok(MapFromDomainUser.MapFromUserDomain(hostURL, user, userService));
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("/PostCustomer/")]
        public ActionResult<UserRESTInputDTOUser> PostUser([FromBody] UserRESTInputDTOUser restDTO)
        {
            try
            {
                User user = userService.AddUser(MapToDomainUser.MapToUserDomain(restDTO));
                return CreatedAtAction(nameof(GetUser), new { customerId = user.CustomerNumber }, MapFromDomainUser.MapFromUserDomain(hostURL, user, userService));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("/PutUser/{customerId}")]
        public IActionResult PutUser(int customerId, [FromBody] UserRESTInputDTOUser restDTO)
        {
            try
            {
                User user = MapToDomainUser.MapToUserDomain(restDTO);
                user.SetCustomerNumber(customerId);
                userService.UpdateUser(user);
                return CreatedAtAction(nameof(GetUser), new { customerId = customerId },MapFromDomainUser.MapFromUserDomain(hostURL, user, userService));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("/UnsubscribeUser/{customerId}")]
        public IActionResult UnsubscribeUser(int customerId)
        {
            try
            {
                userService.UnsubscribeUser(customerId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("/SearchRestaurantOnLocationAndOrCuisine/")]
        public ActionResult<UserRESTInputDTOUser> SearchRestaurantOnLocationAndOrCuisine(int? postalcode, string? cuisine)
        {
            try
            {
                IReadOnlyList<Restaurant> restaurants = restaurantService.SearchRestaurantOnLocationAndOrCuisine(postalcode, cuisine);
                return Ok(MapFromDomainUser.MapFromSearchRestaurantFreeTableDomain(restaurants));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("/SearchRestaurantsWithFreeTables/{date}/{seats}")]
        public ActionResult<UserRESTInputDTOUser> SearchRestaurantsWithFreeTables(DateTime date, int seats)
        {
            try
            {
                IReadOnlyList<Restaurant> restaurants = restaurantService.SearchRestaurantsWithFreeTables(date,seats);
                return Ok(MapFromDomainUser.MapFromSearchRestaurantFreeTableDomain(restaurants));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("/GetReservation/{reservationId}")]
        public ActionResult<UserRESTInputDTOUser> GetReservation(int reservationId)
        {
            try
            {
                Reservation reservation = reservationService.GetReservation(reservationId);
                return Ok(MapFromDomainUser.MapFromReservationDomain(reservation));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("/PostReservation/")]
        public ActionResult<UserRESTInputDTOUser> PostReservation([FromBody] ReservationRESTInputDTOUser restDTO)
        {
            try
            {
                Reservation reservation = reservationService.AddReservation(MapToDomainUser.MapToReservationDomain(restDTO, restaurantService, userService));
                return CreatedAtAction(nameof(GetReservation), new { ReservationId = reservation.ReservationNumber, TableNumber = reservation.Tablenumber}, MapFromDomainUser.MapFromReservationDomain(reservation));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("/PutReservation/{reservationId}")]
        public IActionResult PutReservation(int reservationId, [FromBody] UpdateReservationRESTInputDTOUser restDTO)
        {
            try
            {
                Reservation currentReservation = reservationService.GetReservation(reservationId);
                Reservation newReservation = MapToDomainUser.MapToReservationDomain(restDTO,reservationId,currentReservation);
                newReservation = reservationService.UpdateReservation(newReservation,currentReservation);
                return CreatedAtAction(nameof(GetReservation), new { ReservationId = newReservation.ReservationNumber, TableNumber = newReservation.Tablenumber }, MapFromDomainUser.MapFromPutReservationDomain(newReservation));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("/CancelReservation/{reservationId}")]
        public IActionResult CancelReservation(int reservationId)
        {
            try
            {
                reservationService.CancelReservation(reservationId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("/SearchreservationsOfCustomer/{customerId}")]
        public ActionResult<UserRESTInputDTOUser> SearchreservationsOfCustomer(int customerId, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                List<Reservation> reservations = reservationService.SearchReservations(customerId,startDate,endDate);
                return Ok(MapFromDomainUser.MapFromSearchReservationDomain(reservations));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
