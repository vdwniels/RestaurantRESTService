using EventBL.Managers;
using EventBL.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OpdrachtEvents.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class VisitorController : ControllerBase {
        private VisitorManager _vM;

        public VisitorController(VisitorManager vm) {
            _vM = vm;
        }

        [HttpGet("{id}")]
        public ActionResult<Visitor> Get(int id) {
            try {
                return Ok(_vM.GetVisitor(id));
            } catch (Exception ex) {
                return BadRequest(); // Of not found
            }
        }

        [HttpPost]
        public ActionResult<Visitor> PostVisitor([FromBody] Visitor visitor) {
            if (visitor == null) { return BadRequest(); }
            try {
                Visitor v = _vM.RegisterVisitor(visitor);
                _vM.SubscribeVisitor(v);
                return CreatedAtAction(nameof(Get), new { id = v.Id }, v);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<List<Visitor>> GetAll() {
            try {
                return Ok(_vM.GetVisitors());
            } catch (Exception ex) {
                return BadRequest();
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id) {
            try {
                _vM.UnsubscribeVisitor(_vM.GetVisitor(id));
                return NoContent();
            } catch (Exception ex) {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult Put(int id, [FromBody] Visitor visitor) {
            if (visitor == null) { return BadRequest(); }
            try {
                _vM.UpdateVisitor(visitor);
                return NoContent();
            } catch (Exception ex) {
                return BadRequest();
            }
        }
    }
}
