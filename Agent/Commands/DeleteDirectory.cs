using Agent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Agent.Commands
{
    public class DeleteDirectory : AgentCommand
    {
        public override string Name => "rmdir";

        public override string Execute(AgentTask task)
        {
            if (task.Arguments is null || task.Arguments.Length == 0)
            {
                return "No path provided";

            }

            var path = task.Arguments[0];

            // Check to see if arg is bool and also then need to check if that array is actually filled or excepton will throw
            // var recurse = bool.Parse(task.Arguments[1]);

            Directory.Delete(path, true);

            if (!Directory.Exists(path))
            {
                return $"{path} deleted";
            
            }

            return $"Failed to delete {path}";
        }
    }
}
