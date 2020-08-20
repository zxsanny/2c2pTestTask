using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        public async Task<IEnumerable<TransactionInfo>> GetAsync()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("currency/{currency}")]
        public async Task<IEnumerable<TransactionInfo>> GetAsync(string currency)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("daterange/{from}/{to}")]
        public async Task<IEnumerable<TransactionInfo>> GetAsync(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("status/{status}")]
        public async Task<IEnumerable<TransactionInfo>> GetAsync(TransactionStatusEnum status)
        {
            throw new NotImplementedException();
        }

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
            
            var parserResult = parser.ParseFile(file.OpenReadStream());
            var insertResult = parserResult.Success
                ? await _transactionRepository.InsertAsync(parserResult.Transactions)
                : new InsertResult();
            return Ok(new UploadResult(parserResult, insertResult));
        }
    }
}
