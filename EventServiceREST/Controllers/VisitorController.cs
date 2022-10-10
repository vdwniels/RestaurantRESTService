using EventBL.Managers;
using EventBL.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventServiceREST.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VisitorController : ControllerBase
    {
        private VisitorManager VM;

        public VisitorController(VisitorManager vM)
        {
            VM = vM;
        }
        [HttpGet("{id}")]
        public ActionResult<Visitor> Get(int id)
        {
            try
            {
                return Ok(VM.GetVisitor(id));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public ActionResult<Visitor> PostVisitor([FromBody] Visitor visitor)
        {
            if (visitor == null) return BadRequest();
            try
            {
                Visitor v = VM.RegisterVisitor(visitor);
                VM.SubscribeToList(v);
                return CreatedAtAction(nameof(Get), new { id = v.Id }, v);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public ActionResult<List<Visitor>> GetAll()
        {
            try
            {
                return Ok(VM.GetVisitors());
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                VM.UnsubscribeFromList(VM.GetVisitor(id));
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id,[FromBody] Visitor visitor)
        {
            if (visitor==null) return BadRequest();
            try
            {
                VM.UpdateVisitor(visitor);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
