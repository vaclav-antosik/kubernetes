using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace MeAddOne.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MathController : ControllerBase
    {
        private readonly ILogger<MathController> _logger;

        public MathController(ILogger<MathController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public int Get(int number)
        {
            string log = string.Empty;
            var file = System.IO.DriveInfo.GetDrives()
                .SingleOrDefault(x => x.VolumeLabel == "/maodata").RootDirectory.FullName + "/log.txt";

            if (file != null)
            {
                System.IO.File.AppendAllLines(file, new[] { $"Request to add 1 to {number}" });
                var output = System.IO.File.ReadAllLines(file);
                log += $"Log file is {file}";
                log += Environment.NewLine;
                log += "Log file summary:";
                foreach (string line in output)
                {
                    log += Environment.NewLine;
                    log += line;
                }
                _logger.LogInformation(log);
            }

            return number + 1;
        }
    }
}
