using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.Extensions.Logging;

namespace ApiLog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogger _logger;
        private const string ReturnMessage = "Check Logs for output";

        public LogController(ILogger<LogController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            MakeSomeNoise("HttpGet");
            return ReturnMessage;
        }

        [HttpPost]
        public ActionResult<string> Post([FromBody] string value)
        {
            MakeSomeNoise("Post");
            return ReturnMessage;
        }

        [HttpPut("{id}")]
        public ActionResult<string> Put(int id, [FromBody] string value)
        {
            MakeSomeNoise("HttpPut");
            return ReturnMessage;
        }

        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            MakeSomeNoise("HttpDelete");
            return ReturnMessage;
        }

        private void MakeSomeNoise(string source = "")
        {
            foreach (LogLevel customerType in Enum.GetValues(typeof(LogLevel)))
                WriteSomeLogs(customerType, source);
        }

        private void WriteSomeLogs(LogLevel level, string source = "")
        {
            _logger.Log(level, $"Basic Log Message without any objects with content from {source}");
            _logger.Log(level, "Simple Structured Log Message without any objects with content from {Source}, {Bool}, {Int}, {Decimal}, {Float}", source, false, 1, 1.1M, 1 / (float)3);
            _logger.Log(level, "FlatObject Splatted Structured Log Message without any objects with content from {Source}, {@FlatObject}", source, new FlatObject());
            _logger.Log(level, "ComplexObject Splatted Structured Log Message without any objects with content from {Source}, {@ComplexObject}", source, new ComplexObject());
            _logger.Log(level, "NestedComplexObject Splatted Structured Log Message without any objects with content from {Source}, {@NestedComplexObject}", source, new NestedComplexObject());
        }
    }

    public class FlatObject
    {
        public bool SomeBoolValue { get; set; } = true;
        public string SomeStringValue { get; set; } = "A short string";
        public int SomeIntValue { get; set; } = 9001;
        public decimal SomeDecimalValue { get; set; } = 3.50M;
        public float SomeFloatValue { get; set; } = 1 / (float)3;
    }

    public class ComplexObject
    {
        public FlatObject SingleFlatObject { get; set; } = new FlatObject();

        public IEnumerable<FlatObject> ObjectListFlatObjects { get; set; } = new[]
        {
            new FlatObject(),
            new FlatObject
            {
                SomeBoolValue = false,
                SomeDecimalValue = 1,
                SomeFloatValue = 2,
                SomeIntValue = 3,
                SomeStringValue = "Another String"
            }
        };
    }

    public class NestedComplexObject
    {
        public ComplexObject SingleComplexObject { get; set; } = new ComplexObject();
    }
}
