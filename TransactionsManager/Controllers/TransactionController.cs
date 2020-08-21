using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TransactionManager.Common.DTO;
using TransactionManager.Common.Entities;
using TransactionManager.Parsers;
using TransactionManager.Repository;
using TransactionsManager.DTO;

namespace TransactionsManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ITransactionFileParserFactory _fileParserFactory;

        public TransactionController(ILogger<TransactionController> logger, ITransactionRepository transactionRepository, ITransactionFileParserFactory fileParserFactory)
        {
            _logger = logger;
            _transactionRepository = transactionRepository;
            _fileParserFactory = fileParserFactory;
        }

        [HttpGet]
        public async Task<IEnumerable<TransactionView>> GetAsync([FromQuery]TransactionFilter filter = null)
            => await Get(filter);

        [HttpGet]
        [Route("currency/{currency}")]
        public async Task<IEnumerable<TransactionView>> GetAsync(string currency)
            => await Get(new TransactionFilter(currency: currency));

        [HttpGet]
        [Route("daterange/{from}/{to}")]
        public async Task<IEnumerable<TransactionView>> GetAsync(DateTime from, DateTime to)
            => await Get(new TransactionFilter(from: from, to: to));
        
        [HttpGet]
        [Route("status/{status}")]
        public async Task<IEnumerable<TransactionView>> GetAsync(TransactionStatusEnum status)
            => await Get(new TransactionFilter(status: status));
        
        private async Task<IEnumerable<TransactionView>> Get(TransactionFilter filter) 
            => (await _transactionRepository.GetAsync(filter)).Select(x => new TransactionView(x)).ToList();

        [HttpPost, RequestSizeLimit(1050000)]
        [Route("upload")]
        public async Task<IActionResult> UploadAsync()
        {
            var file = Request.Form.Files.FirstOrDefault();
            if (file == null)
            {
                return BadRequest("There is no file uploaded");
            }
            //Create appropriate parser by file's extension 
            var extension = new FileInfo(file.FileName).Extension.Trim('.').ToLower();
            ITransactionFileParser parser;
            try
            {
                parser = _fileParserFactory.CreateTransactionFileParser(extension);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            
            var parserResult = parser.ParseStream(file.OpenReadStream());

            //If data was parsed successfully, insert the data and return insert result
            if (!parserResult.Success)
            {
                return BadRequest(string.Join(", ", parserResult.Errors));
            }
            var insertResult = await _transactionRepository.InsertAsync(parserResult.Transactions);
            if (!insertResult.Success)
            {
                return BadRequest(insertResult.Error);
            }

            return Ok(new UploadResult(parserResult, insertResult));
        }
    }
}
