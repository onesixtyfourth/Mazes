using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommandLine;
using Microsoft.Extensions.Logging;

namespace MazeConsole
{
    public class ConsoleApp 
    {
        private readonly ILogger<ConsoleApp> log;
        private const string _readPrompt = "maze-> ";

        public ConsoleApp(ILogger<ConsoleApp> logger)
        {
            log = logger;
        }

        public Task Run()
        {
            while(true)
            {
                var input = ReadFromConsole();
                if(string.IsNullOrWhiteSpace(input))
                {
                    continue;
                }
                else if (input.ToLower().Equals("exit"))
                {
                    break;
                }
                else
                {
                    Parser.Default.ParseArguments<MazeOptions>(input.Split(" "))
                    .WithParsed<MazeOptions>(async options => await options.DoWork())
                    .WithNotParsed(HandleParseError);
                }
            }
            return Task.CompletedTask;
        }

        private void HandleParseError(IEnumerable<Error> errs)
        {
            if (errs.IsVersion())
            {
                log.LogInformation("Version Request");
                return;
            }

            if (errs.IsHelp())
            {
                log.LogInformation("Help Request");
                return;
            }
            log.LogInformation("Parser Fail");
        }

        
        public static string ReadFromConsole(string promptMessage = "")
        {
            // Show a prompt, and get input:
            Console.Write(_readPrompt + promptMessage);
            return Console.ReadLine();
        }

        public static void WriteToConsole(string message = "")
        {
            if (message.Length > 0)
            {
                Console.WriteLine(message);
            }
        }
    }
}