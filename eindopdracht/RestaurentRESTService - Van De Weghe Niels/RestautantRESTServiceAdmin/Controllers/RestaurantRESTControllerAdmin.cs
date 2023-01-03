using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantBL.Model;
using RestaurantBL.Services;
using RestaurantRESTServiceAdmin.Model.input;
using RestaurantRESTServiceUser.Mappers;
using RestautantRESTServiceAdmin.Model.input;
using System.Xml.Serialization;

namespace RestautantRESTServiceAdmin.Controllers
{
    [Route("api/[controller]/admin")]
    [ApiController]
    public class RestaurantRESTControllerAdmin : ControllerBase
    {
        private string hostURL = @"http://localhost:5022/api/user/Restaurantbeheer";
        private ReservationService reservationService;
        private RestaurantService restaurantService;
        private UserService userService;
        private TableService tableService;

        public RestaurantRESTControllerAdmin(ReservationService reservationService, RestaurantService restaurantService, UserService userService, TableService tableService)
        {
            this.reservationService = reservationService;
            this.restaurantService = restaurantService;
            this.userService = userService;
            this.tableService = tableService;
        }

        [HttpGet("/GetRestaurant/{restaurantId}")]
        public ActionResult<RestaurantRESTInputDTOAdmin> GetRestaurant(int restaurantId)
        {
            try
            {
                Restaurant restaurant = restaurantService.GetRestaurant(restaurantId);
                return Ok(MapFromDomainAdmin.MapFromRestaurantDomain(restaurant));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("/PostRestaurant/")]
        public ActionResult<RestaurantRESTInputDTOAdmin> PostRestaurant([FromBody] RestaurantRESTInputDTOAdmin restDTO)
        {
            try
            {
                Restaurant restaurant = restaurantService.AddRestaurant(MapToDomainAdmin.MapToRestaurantDomain(restDTO));
                return CreatedAtAction(nameof(GetRestaurant), new { RestaurantId = restaurant.RestaurantId }, MapFromDomainAdmin.MapFromRestaurantDomain(restaurant));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("/Deleterestaurant/{restaurantId}")]
        public IActionResult Deleterestaurant(int restaurantId)
        {
            try
            {
                restaurantService.DeleteRestaurant(restaurantId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("/PutRestaurant/{restaurantId}")]
        public IActionResult PutRestaurant(int restaurantId, [FromBody] RestaurantRESTInputDTOAdmin restDTO)
        {
            try
            {
                Restaurant restaurant = MapToDomainAdmin.MapToRestaurantDomain(restDTO);
                restaurant.SetRestaurantId(restaurantId);
                restaurantService.UpdateRestaurant(restaurant);
                return CreatedAtAction(nameof(GetRestaurant), new { restaurantId = restaurant.RestaurantId }, MapFromDomainAdmin.MapFromRestaurantWithoutTablesDomain(restaurant));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #region table 
        [HttpGet("/GetTable/{tableId}")]
        public ActionResult<TableRESTInputDTOAdmin> GetTable(int tableId)
        {
            try
            {
                Table table = tableService.GetTable(tableId);
                return Ok(MapFromDomainAdmin.MapFromTableDomain(table));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("/PostTable/")]
        public ActionResult<TableRESTInputDTOAdmin> PostTable([FromBody] TableRESTInputDTOAdmin restDTO)
        {
            try
            {
                Table table = tableService.AddTable(MapToDomainAdmin.MapToTableDomain(restDTO));
                return CreatedAtAction(nameof(GetTable), new { TableId = table.TableId }, MapFromDomainAdmin.MapFromTableDomain(table));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("/DeleteTable/{tableId}")]
        public IActionResult DeleteTable(int tableId)
        {
            try
            {
                tableService.DeleteTable(tableId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("/DeleteAllTables/{restaurantId}")]
        public IActionResult DeleteAllTables(int restaurantId)
        {
            try
            {
                restaurantService.DeleteAllTables(restaurantId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("/PutTable/{tableId}")]
        public IActionResult PutTable(int tableId, [FromBody] TableRESTInputDTOAdmin restDTO)
        {
            try
            {
                Table table = MapToDomainAdmin.MapToTableDomain(restDTO);
                table.SetTableId(tableId);
                tableService.UpdateTable(table);
                return CreatedAtAction(nameof(GetTable), new { tableId = table.TableId}, MapFromDomainAdmin.MapFromTableDomain(table));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion

        [HttpGet("/GetRestaurantReservationsOnPeriod/{restaurantId}")]
        public ActionResult<ReservationRESTInputDTOAdmin> GetRestaurantReservationsOnPeriod(int restaurantId,DateTime? startDate, DateTime? endDate)
        {
            try
            {
                List<Reservation> reservations = reservationService.GetReservationsOnPeriod(restaurantId, startDate, endDate);
                return Ok(MapFromDomainAdmin.MapFromReservationDomain(reservations));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("/GetRestaurantReservationsOnSpecificDay/{restaurantId}/{date}")]
        public ActionResult<ReservationRESTInputDTOAdmin> GetRestaurantReservationsOnSpecificDay(int restaurantId, DateTime date)
        {
            try
            {
                List<Reservation> reservations = reservationService.GetReservationsOnSpecificDay(restaurantId, date);
                return Ok(MapFromDomainAdmin.MapFromReservationDomain(reservations));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
