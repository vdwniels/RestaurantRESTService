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

        public RestaurantRESTControllerUser(ReservationService reservationService, RestaurantService restaurantService, UserService userService, TableService tableService)
        {
            this.reservationService = reservationService;
            this.restaurantService = restaurantService;
            this.userService = userService;
            this.tableService = tableService;
        }

        [HttpGet("/GetCustomer/{customerId}")]
        public ActionResult<UserRESTInputDTO> GetUser(int customerId)
        {
            try
            {
                User user = userService.GetUser(customerId);
                return Ok(MapFromDomain.MapFromUserDomain(hostURL, user, userService));
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("/PostCustomer/")]
        public ActionResult<UserRESTInputDTO> PostUser([FromBody] UserRESTInputDTO restDTO)
        {
            try
            {
                User user = userService.AddUser(MapToDomain.MapToUserDomain(restDTO));
                return CreatedAtAction(nameof(GetUser), new { UserId = user.CustomerNumber }, MapFromDomain.MapFromUserDomain(hostURL, user, userService));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
