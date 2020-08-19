using System;
using System.Collections.Generic;
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
    public class TransactionsController : ControllerBase
    {
        private readonly ILogger<TransactionsController> _logger;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ITransactionFileParserFactory _fileParserFactory;

        public TransactionsController(ILogger<TransactionsController> logger, ITransactionRepository transactionRepository, ITransactionFileParserFactory fileParserFactory)
        {
            _logger = logger;
            _transactionRepository = transactionRepository;
            _fileParserFactory = fileParserFactory;
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

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadAsync(List<IFormFile> files)
        {
            var result = await Task.WhenAll(files.Select(async f => {
                var parser = _fileParserFactory.CreateTransactionFileParser(f.FileName);
                var parserResult = await parser.ParseFileAsync(f.OpenReadStream());
                if (parserResult.Success)
                {
                    var insertResult = await _transactionRepository.InsertAsync(parserResult.Transactions);
                    return new UploadResult(parserResult, insertResult);
                }
                else
                {
                    return new UploadResult(parserResult);
                }
            }));
            return Ok(result);
        }
    }
}
