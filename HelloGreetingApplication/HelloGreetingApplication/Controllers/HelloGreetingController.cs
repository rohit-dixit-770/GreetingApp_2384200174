using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Interface;
using ModelLayer.Model;
using RepositoryLayer.Entity;

namespace HelloGreetingApplication.Controllers
{
    /// <summary>
    /// Class providing API for HelloGreeting
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class HelloGreetingController : ControllerBase
    {
        private readonly ILogger<HelloGreetingController> _logger;
        private readonly IGreetingBL _greetingBL;

        public HelloGreetingController(IGreetingBL greetingBL, ILogger<HelloGreetingController> logger)
        {
            _logger = logger;
            _greetingBL = greetingBL;
        }

        /// <summary>
        /// Get method to retrieve the greeting message
        /// </summary>
        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("GET request received to get greeting message");

            ResponseModel<string> responseModel = new()
            {
                Success = true,
                Message = "Hello to Greeting App API Endpoint",
                Data = "Hello, World!"
            };

            return Ok(responseModel);
        }


<<<<<<< HEAD
=======
        /// <summary>
        /// Retrieves a greeting message by its unique identifier
        /// </summary>
        /// <param name="id">unique identifier of greeting message</param>
        /// <returns>Response based on given Id </returns>
        [HttpGet("id")]
        public IActionResult Get(Guid id)
        {
            var greet = _greetingBL.GetGreetingById(id);

            if (greet == null)
            {
                ResponseModel<String> response = new()
                {
                    Success = false,
                    Message = "Not Found",
                    Data = null,
                };

                _logger.LogInformation("Find Greeting by Id Failed");
                return NotFound(response);
            }

            _logger.LogInformation("Find Greeting by Id Successful");

            return Ok(new ResponseModel<String>()
            {
                Success = true,
                Message = "Greeting saved successfully",
                Data = greet.Message,
            });
        }


>>>>>>> UC5
        /// <summary>
        /// Post method to create a new greeting message
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns>responseModel containing greeting message</returns>
        [HttpPost("greet")]
        public IActionResult Post(UserModel userModel)
        {
            _logger.LogInformation("POST request received to generate a greeting message for user: {FirstName} {LastName}",
                userModel.FirstName, userModel.LastName);

            var greetingMessage = _greetingBL.GreetingMessage(userModel);

            ResponseModel<string> responseModel = new()
            {
                Success = true,
                Message = "Request received successfully",
                Data = greetingMessage
            };

            return Ok(responseModel);
        }

        /// <summary>
        /// Post method to add a new greeting message
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns>Returns a success response if message saved</returns>
        [HttpPost("AddGreet")]
        public IActionResult AddGreeting(RequestModel requestModel)
        {
            try
            {
                _logger.LogInformation("POST request received to add a new greeting with Message: {Message}", requestModel.Message);

                var newGreeting = new GreetingEntity { Message = requestModel.Message };
                var savedGreeting = _greetingBL.AddGreeting(newGreeting);

                ResponseModel<string> response = new()
                {
                    Success = true,
                    Message = "Greeting saved successfully.",
                    Data = $"Id: {savedGreeting.Id}, Message: {savedGreeting.Message}" 
                };

                _logger.LogInformation("Greeting successfully saved");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while saving the greeting ");

                ResponseModel<string> response = new()
                {
                    Success = false,
                    Message = "Failed to save greeting.",
                    Data = ex.Message
                };
                return BadRequest(response);
            }
        }

        /// <summary>
        /// Post method to create a new greeting message
        /// </summary>
        [HttpPost]
        public IActionResult Post(RequestModel requestModel)
        {
            _logger.LogInformation("POST request received with Message: {Message}", requestModel.Message);

            ResponseModel<string> responseModel = new()
            {
                Success = true,
                Message = "Request received successfully",
                Data = $"Message: {requestModel.Message}"
            };

            return Ok(responseModel);
        }

        /// <summary>
        /// Put method to update an existing greeting message
        /// </summary>
        [HttpPut]
        public IActionResult Put(RequestModel requestModel)
        {
            _logger.LogInformation("PUT request received to update greeting");

            ResponseModel<string> responseModel = new()
            {
                Success = true,
                Message = "Request updated successfully",
                Data = $" Updated Message: {requestModel.Message}"
            };

            return Ok(responseModel);
        }

        /// <summary>
        /// Patch method to modify a greeting message
        /// </summary>
        [HttpPatch]
        public IActionResult Patch(Guid Id)
        {
            _logger.LogInformation("PATCH request received to modify greeting with ID: {RequestId}", Id);

            ResponseModel<string> responseModel = new()
            {
                Success = true,
                Message = "Request patched successfully",
                Data = $"Patched Id: {Id}"
            };

            return Ok(responseModel);
        }

        /// <summary>
        /// Delete method to remove a greeting message
        /// </summary>
        [HttpDelete]
        public IActionResult Delete(Guid Id)
        {
            _logger.LogInformation("DELETE request received to remove greeting with ID: {RequestId}", Id);

            ResponseModel<string> responseModel = new()
            {
                Success = true,
                Message = "Greeting message deleted successfully",
                Data = $"Deleted Id: {Id}"
            };

            _logger.LogInformation("Greeting successfully deleted with ID: {RequestId}", Id);
            return Ok(responseModel);
        }
    }
}
