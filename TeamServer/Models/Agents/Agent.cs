using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace TeamServer.Models.Agents
{
    public class Agent
    {
        public AgentMetadata Metadata { get; set; }
        public DateTime LastSeen { get; set; }

        private readonly ConcurrentQueue<AgentTask> _pendingTasks = new();
        private readonly List<AgentTaskResult> _taskResults = new();

        public Agent(AgentMetadata metadata) {

            Metadata = metadata;
        }

        public void CheckIn() 
        {

            LastSeen = DateTime.UtcNow;
        
        }

        public void QueueTask(AgentTask task)
        { 
            _pendingTasks.Enqueue(task);
        
        }

        public IEnumerable<AgentTask> GetPendingTasks()
        {
            List<AgentTask> tasks = new();
            while (_pendingTasks.TryDequeue(out var task))
            {

                // Keep trying to dequeue and add this item to the list
                tasks.Add(task);
            
            }

            return tasks;

        
        }

        public AgentTaskResult GetTaskResult(string taskId)
        {

            return GetTaskResults().FirstOrDefault(r => r.Id.Equals(taskId));


        }

        public IEnumerable<AgentTaskResult> GetTaskResults()
        {
            return _taskResults;
        }

        public void AddTaskResults(IEnumerable<AgentTaskResult> results)
        {
            _taskResults.AddRange(results);
        
        }
    }
}
