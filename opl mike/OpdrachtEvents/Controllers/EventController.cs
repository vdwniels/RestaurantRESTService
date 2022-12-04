using EventBL.Managers;
using EventBL.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace OpdrachtEvents.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private EventManager EM;
        private VisitorManager VM;

        public EventController(EventManager eM, VisitorManager vM)
        {
            EM = eM;
            VM = vM;
        }

        [HttpPost]
        public ActionResult<Event> AddEvent([FromBody] Event ev)
        {
            try
            {
                if (ev == null) return BadRequest();
                EM.AddEvent(ev);
                return CreatedAtAction(nameof(Get), new { name = ev.Name }, ev);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{name}")]
        public IActionResult RemoveEvent(string name)
        {
            try
            {
                if (!EM.ExistsEvent(name)) return NotFound();
                EM.RemoveEvent(EM.GetEventByName(name));
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpPut("{name}")]
        public IActionResult UpdateEvent(string name, [FromBody] Event ev)
        {
            try
            {
                if (!name.Equals(ev.Name)) return BadRequest("names do not match");
                if (!EM.ExistsEvent(name)) return BadRequest("does not exist");
                EM.UpdateEvent(ev);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{name}")]
        public ActionResult<Event> Get(string name)
        {
            try
            {
                return Ok(EM.GetEventByName(name));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public ActionResult<List<Event>> GetAllEvents()
        {
            try
            {
                return Ok(EM.GetEvents());
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("date/{date}")]
        public ActionResult<List<Event>> GetEventsForDate(string date)
        {
            try
            {
                return Ok(EM.GetEventsForDate(DateTime.Parse(date)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("location/{location}")]
        public ActionResult<List<Event>> GetEventsForLocation(string location)
        {
            try
            {
                return Ok(EM.GetEventsForLocation(location));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("{EventName}/Visitor")]
        public ActionResult<Event> SubscribeVisitorForEvent(string EventName,[FromBody]int visitorId)
        {
            try
            {
                Visitor v = VM.GetVisitor(visitorId);
                Event ev = EM.GetEventByName(EventName) ;
                EM.SubscribeVisitor(v, ev);
                return CreatedAtAction(nameof(Get),new {name=EventName},ev);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("{EventName}/Visitor")]
        public IActionResult UnsubscribeVisitorForEvent(string EventName, [FromBody] int visitorId) 
        {
            try
            {
                Visitor v = VM.GetVisitor(visitorId);
                Event ev = EM.GetEventByName(EventName);
                EM.UnSubscribeVisitor(v, ev);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
