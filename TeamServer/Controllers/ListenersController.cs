using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ListenersController : ControllerBase
    {
        private readonly IListenerService _listeners;

        public ListenersControllewr(IListenerService listeners)
        {
            _listeners = listeners;
        }

        [HttpGet]
        public IActionResult GetListeners()
        { 
            var listeners = _listeners.GetListeners();
            return Ok(listeners);
        }

        [HttpGet("{name}")]
        public IActionResult GetListener(string name) {
        
            var listeners = _listeners.GetListener(name);
            if (listeners is null) return NotFound();

            return Ok(listener);
        
        }

        [HttpPost]
        public IActionResult StartListener([FromBody] StartHttpListenerRequest request)
        {
            var listener = new HttpListener(request.Name, request.BindPort);
            listener.Start();

            _listeners.AddLisenter(listener);

            var root = $"{ HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
            var path = $"{root}/{listener.Name}";

            return Created(path, listener);

        }


    }
}
