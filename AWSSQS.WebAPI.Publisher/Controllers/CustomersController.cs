using AWSSQS.WebAPI.Publisher.Messaging;
using AWSSQS.WebAPI.Publisher.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AWSSQS.WebAPI.Publisher.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CustomersController(
    ISqsMessenger sqsMesenger) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Create(string name, CancellationToken cancellationToken)
    {
        //Db Kayıt işlemi

        Customer customer = new()
        {
            Name = name,
        };

        //kayıt işleminden sonra onay maili gönder

        var response = await sqsMesenger.SendMessageAsync(customer);

        return StatusCode(201);
    }
}
