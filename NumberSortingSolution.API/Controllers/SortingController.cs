using Microsoft.AspNetCore.Mvc;
using NumberSortingSolution.BusinessLogic.Interfaces;
using NumberSortingSolution.API.Validators;

namespace NumberSortingSln.API.Controllers
{
    [Route("api/numbers")]
    [ApiController]
    public class NumbersController : ControllerBase
    {
        private readonly ISortingService _sortingService;
        private readonly ILogger<NumbersController> _logger;
        private readonly IFileService _fileService;
        private readonly IPerformanceService _performanceService;

        public NumbersController(ISortingService sortingService, ILogger<NumbersController> logger, IFileService fileService, IPerformanceService performanceService)
        {
            _sortingService = sortingService;
            _logger = logger;
            _fileService = fileService;
            _performanceService = performanceService;
        }

        /// <summary>
        /// Sorts a list of numbers using split sequence algorithm and saves it to a .txt file in your Downloads folder
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Server side error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Route("split/sorted-list")]
        public async Task<IActionResult> SplitSortNumbersListAsync([FromBody] List<int> numbers)
        {
            var validator = new NumbersListValidator();
            var validationResults = await validator.ValidateAsync(numbers);

            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults.Errors.Select(error => error.ErrorMessage));
            }

            try
            {
                var sortedList = await _sortingService.SplitSortAndWriteToFileAsync(numbers);
                return Ok($"Sorted numbers list {string.Join(", ", sortedList)} saved to a .txt file in your Downloads folder successfully.");
            }
            catch (IOException ex)
            {
                _logger.LogError(ex, "Failed input output file write operation.");
                return StatusCode(500, "Internal server error occurred.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error occurred.");
                return StatusCode(500, "Internal server error occurred.");
            }
        }

        /// <summary>
        /// Sorts a list of numbers using bubble sequence algorithm and saves it to a .txt file in your Downloads folder
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Server side error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Route("bubble/sorted-list")]
        public async Task<IActionResult> BubbleSortNumbersListAsync([FromBody] List<int> numbers)
        {
            var validator = new NumbersListValidator();
            var validationResults = await validator.ValidateAsync(numbers);

            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults.Errors.Select(error => error.ErrorMessage));
            }

            try
            {
                var sortedList = await _sortingService.BubbleSortAndWriteToFileAsync(numbers);
                return Ok($"Sorted numbers list {string.Join(", ", sortedList)} saved to a .txt file in your Downloads folder successfully.");
            }
            catch(IOException ex)
            {
                _logger.LogError(ex, "Failed input output file write operation.");
                return StatusCode(500, "Internal server error occurred.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error occurred.");
                return StatusCode(500, "Internal server error occurred.");
            }
        }

        /// <summary>
        /// Retrieves the last sorted numbers list file from the Downloads folder
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="404">No found</response>
        /// <response code="500">Server side error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("sorted-list/last")]
        public async Task<IActionResult> GetLastSortedListFileAsync()
        {
            try
            {
                var lastFile = await _fileService.GetLatestSortingResultFile();
                return Ok($"Last file name: {lastFile.Name}. Content: {lastFile.Content}");
            }
            catch (FileNotFoundException e)
            {
                _logger.LogError($"No sorted list files were found in your Downloads folder: {e.Message}");
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Internal server error occurred.");
                return StatusCode(500, "Internal server error occurred.");
            }
        }

        /// <summary>
        /// Checks and returns performance results of two sorting algorithms
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Server side error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("performance-check")]
        public async Task<IActionResult> CompareAlgorithmPerformanceAsync([FromQuery] List<int> numbers)
        {
            var validator = new NumbersListValidator();
            var validationResults = await validator.ValidateAsync(numbers);

            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults.Errors.Select(error => error.ErrorMessage));
            }

            try
            {
                IEnumerable<string> performanceResult = _performanceService.MeasureAlgorithmPerformance(numbers);
                return Ok(performanceResult);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Internal server error occurred.");
                return StatusCode(500, "Internal server error occurred.");
            }
        }
    }
}
