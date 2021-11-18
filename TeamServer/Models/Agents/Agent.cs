using System;

namespace TeamServer.Models.Agents
{
    public class Agent
    {
        public AgentMetadata Metadata { get; set; }


        public DateTime LastSeen { get; set; }

        public Agent(AgentMetadata metadata) {

            Metadata = metadata;
        }

        public void CheckIn() 
        {

            LastSeen = DateTime.UtcNow;
        
        }

        public void GetPendingTasks()
        { 
            
        
        }

    }
}
