using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BoatRentalSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
 
        [HttpGet("Enqueue")]
        public IActionResult Enqueue()
        {
           
            BackgroundJob.Enqueue(() => sendMSg());
            return Ok($"{DateTime.Now} Enqueue");
        }
        [HttpGet("Schedule")]
        public IActionResult Schedule()
        {
            BackgroundJob.Schedule(()=>sendMSg(),TimeSpan.FromSeconds(20));
            return Ok($"{DateTime.Now} Schedule");
        }
        [HttpGet("Recurring")]
        public IActionResult Recurring()
        {
            RecurringJob.AddOrUpdate(()=>sendMSg(),Cron.Minutely);
            return Ok($"{DateTime.Now} Recurring");
        }

        [ApiExplorerSettings(IgnoreApi =true)]
        public void sendMSg() {
            Console.WriteLine($"{DateTime.Now} || Msg Sent");
                }
    }
}
