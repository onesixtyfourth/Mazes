using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommandLine;
using Microsoft.Extensions.Logging;

namespace cmdLinePoc
{
    public class ConsoleApp 
    {
        private readonly ILogger<ConsoleApp> log;
        private const string _readPrompt = "console-> ";

        public ConsoleApp(ILogger<ConsoleApp> logger)
        {
            log = logger;
        }

        public Task Run()
        {
            while(true)
            {
                var t = ReadFromConsole();
                if(string.IsNullOrWhiteSpace(t))
                {
                    continue;
                }

                Parser.Default.ParseArguments<DeleteOptions, ConcatOptions>(t.Split(" "))
                   .WithParsed<DeleteOptions>(async options => await options.Dowork())
                   .WithParsed<ConcatOptions>(async options => await options.Dowork())
                   .WithNotParsed(HandleParseError);
            }
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