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
        public async Task<IEnumerable<TransactionInfo>> GetAsync([FromQuery]TransactionFilter filter)
            => await _transactionRepository.GetAsync(filter);

        [HttpGet]
        [Route("currency/{currency}")]
        public async Task<IEnumerable<TransactionInfo>> GetAsync(string currency)
            => await _transactionRepository.GetAsync(new TransactionFilter(currency: currency));

        [HttpGet]
        [Route("daterange/{from}/{to}")]
        public async Task<IEnumerable<TransactionInfo>> GetAsync(DateTime from, DateTime to)
            => await _transactionRepository.GetAsync(new TransactionFilter(from: from, to: to));
        
        [HttpGet]
        [Route("status/{status}")]
        public async Task<IEnumerable<TransactionInfo>> GetAsync(TransactionStatusEnum status)
            => await _transactionRepository.GetAsync(new TransactionFilter(status: status));
        
        [HttpPost, RequestSizeLimit(1050000)]
        [Route("upload")]
        public async Task<IActionResult> UploadAsync()
        {
            var file = Request.Form.Files.FirstOrDefault();
            if (file == null)
            {
                return BadRequest("There is no file uploaded");
            }
            var extension = new FileInfo(file.FileName).Extension.Trim('.').ToLower();
            var parser = _fileParserFactory.CreateTransactionFileParser(extension);
            
            var parserResult = parser.ParseStream(file.OpenReadStream());
            var insertResult = parserResult.Success
                ? await _transactionRepository.InsertAsync(parserResult.Transactions)
                : new InsertResult();
            return Ok(new UploadResult(parserResult, insertResult));
        }
    }
}
