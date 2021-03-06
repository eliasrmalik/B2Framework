using Agent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Agent.Commands
{
    public class CreateDirectory : AgentCommand
    {
        public override string Name => "mkdir";

        public override string Execute(AgentTask task)
        {
            string path;

            if (task.Arguments is null || task.Arguments.Length == 0)
            {
                return "No path provided";

            }
            else
            {
                path = task.Arguments[0];

            }

            var dirInfo = Directory.CreateDirectory(path);
            return $"{dirInfo.FullName} created";

        }
    }
}
