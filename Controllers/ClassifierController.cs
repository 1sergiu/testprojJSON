using TestProjJSON.Data.Services;
using TestProjJSON.Data.ViewModels;
using TestProjJSON.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace TestProjJSON.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassifierController : ControllerBase
    {
        private readonly ClassifierService _classifierService;

        public ClassifierController(ClassifierService classifierService)
        {
            _classifierService = classifierService ?? throw new ArgumentNullException(nameof(classifierService));
        }

        [HttpGet]
        public IActionResult GetAllClassifiers()
        {
            var allClassifiers = _classifierService.GetAllClassifiers();
            return Ok(allClassifiers);
        }

        [HttpGet("{guid}")]
        public IActionResult GetClassifierById(Guid guid)
        {
            try
            {
                var classifier = _classifierService.GetClassifierById(guid);
                return Ok(classifier);
            }
            catch (GuidNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddClassifier([FromBody] ClassifierVM classifier)
        {
            try
            {
                _classifierService.AddClassifier(classifier);
                return Ok();
            }
            catch (GuidNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{guid}")]
        public IActionResult UpdateClassifierById(Guid guid, [FromBody] ClassifierVM classifier)
        {
            try
            {
                _classifierService.UpdateClassifierById(guid, classifier);
                return Ok();
            }
            catch (GuidNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{guid}")]
        public IActionResult DeleteClassifierById(Guid guid)
        {
            try
            {
                _classifierService.DeleteClassifierById(guid);
                return Ok();
            }
            catch (GuidNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
