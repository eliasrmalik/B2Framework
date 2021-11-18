using System.Threading.Tasks;
using TeamServer.Services;

namespace TeamServer.Models
{
    public abstract class Listener
    {
        public abstract string Name { get; }

        protected IAgentService AgentService;
        public void init(IAgentService agentService)
        {

            AgentService = agentService;
        }

        public abstract Task Start();

        public abstract void Stop(); 
    }
}
