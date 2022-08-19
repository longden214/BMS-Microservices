using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using User.API.Entities;
using User.API.Repository;

namespace User.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _repository;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountRepository repository, ILogger<AccountController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Account>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            var accounts = await _repository.GetAccounts();

            return Ok(accounts);
        }

        [HttpGet("{name}", Name = "GetAccountsByName")]
        [ProducesResponseType(typeof(IEnumerable<Account>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccountsByName(string name)
        {
            var accounts = await _repository.GetAccountsByName(name);

            return Ok(accounts);
        }

        [HttpGet("{id:length(24)}", Name = "GetAccount")]
        [ProducesResponseType(typeof(Account), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Account>> GetAccount(string id)
        {
            var account = await _repository.GetAccount(id);

            if (account == null)
            {
                _logger.LogError($"Account with id: {id}, not found.");
                return NotFound();
            }

            return Ok(account);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Account), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Account>> CreateAccount([FromBody] Account account)
        {
            await _repository.CreateAccount(account);

            return CreatedAtRoute("GetAccount", new { id = account.Id }, account);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Account), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateAccount([FromBody] Account account)
        {
            return Ok(await _repository.UpdateAccount(account));
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteAccount")]
        [ProducesResponseType(typeof(Account), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteAccount(string id)
        {
            return Ok(await _repository.DeleteAccount(id));
        }
    }
}
