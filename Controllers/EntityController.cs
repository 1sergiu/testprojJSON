using TestProjJSON.Data.Services;
using TestProjJSON.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using TestProjJSON.Exceptions;

namespace TestProjJSON.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntityController : ControllerBase
    {
        private readonly EntityService _entityService;

        public EntityController(EntityService entityService)
        {
            _entityService = entityService ?? throw new ArgumentNullException(nameof(entityService));
        }

        [HttpGet]
        public IActionResult GetAllEntities()
        {
            var entities = _entityService.GetAllEntities();
            return Ok(entities);
        }

        [HttpGet("{guid}")]
        public IActionResult GetEntityById(Guid guid)
        {
            try
            {
                var entity = _entityService.GetEntityById(guid);
                return Ok(entity);
            }
            catch (GuidNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddEntity([FromBody] EntityVM entityVM)
        {
            try
            {
                _entityService.AddEntity(entityVM);
                return CreatedAtAction(nameof(GetEntityById), new { guid = entityVM.Guid }, entityVM);
            }
            catch (GuidNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{guid}")]
        public IActionResult UpdateEntityById(Guid guid, [FromBody] EntityVM entityVM)
        {
            try
            {
                _entityService.UpdateEntityById(guid, entityVM);
                return Ok();
            }
            catch (GuidNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{guid}")]
        public IActionResult DeleteEntityById(Guid guid)
        {
            try
            {
                _entityService.DeleteEntityById(guid);
                return NoContent();
            }
            catch (GuidNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}