using HotChocolate.Subscriptions;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ITopicEventSender topicEventSender;

        public TestController(ITopicEventSender topicEventSender)
        {
            this.topicEventSender = topicEventSender;
        }

        [HttpGet]
        public void Get()
        {
            topicEventSender.SendAsync("test", new Test
            {
                Name = "Hello"
            });
        }
    }

    public class Test
    {
        public string Name { get; set; }
    }
    
}
