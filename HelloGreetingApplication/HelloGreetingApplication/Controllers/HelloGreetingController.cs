using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Interface;
using ModelLayer.Model;

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
        public HelloGreetingController(ILogger<HelloGreetingController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Get method to get the greeting message
        /// </summary>
        /// <returns>"Hello, World!"</returns>
        [HttpGet]
        public IActionResult Get() 
        {
            _logger.LogInformation("GET request received");

            ResponseModel<string> responseModel = new()
            {
                Success = true,
                Message = "Hello to Greeting App API Endpoint",
                Data = "Hello, World!"
            };

            return Ok(responseModel);
        }

        /// <summary>
        /// Post method to create a new greeting message
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns> responseModel </returns>
        [HttpPost]
        public IActionResult Post(RequestModel requestModel) 
        {
            _logger.LogInformation($"POST request received with Key: {requestModel.Key}");

            ResponseModel<string> responseModel = new()
            {
                Success = true,
                Message = "Request received successfully",
                Data = $"Key: {requestModel.Key}, Value: {requestModel.Key}"
            };

            return Ok(responseModel);
        }

        /// <summary>
        /// Put method to update a new greeting message
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns> responseModel </returns>
        [HttpPut]
        public IActionResult Put(RequestModel requestModel) 
        {
            _logger.LogInformation($"PUT request received with Key: {requestModel.Key}");

            ResponseModel<string> responseModel = new()
            {
                Success = true,
                Message = "Request updated successfully",
                Data = $"Updated Key: {requestModel.Key} , Updated Value: {requestModel.Value}"
            };

            return Ok(responseModel);
        }

        /// <summary>
        /// Patch method to patch a greeting message
        /// </summary>
        /// <param name="Key"></param>
        /// <returns> responseModel </returns>
        [HttpPatch]
        public IActionResult Patch(string Key) 
        {
            _logger.LogInformation($"PATCH request received with Key: {Key}");

            ResponseModel<string> responseModel = new()
            {
                Success = true,
                Message = "Request patched successfully",
                Data = $" Patched Key: {Key}"
            };
            return Ok(responseModel);
        }

        /// <summary>
        /// Delete method to Delete a greeting message
        /// </summary>
        /// <param name="Key"></param>
        /// <returns> responseModel </returns>
        [HttpDelete]
        public IActionResult Delete(string Key) 
        {
            _logger.LogInformation($"DELETE request received with Key: {Key}");

            ResponseModel<string> responseModel = new()
            {
                Success = true,
                Message = "Greeting message deleted successfully",
                Data = $"Deleted Key: {Key}"
            };

            return Ok(responseModel);
        }
    }
}
