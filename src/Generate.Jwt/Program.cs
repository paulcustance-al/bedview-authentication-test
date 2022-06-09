using CommandLine;
using System;
using System.Collections.Generic;

namespace Generate.Jwt
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<Options>(args)
                .WithParsed(RunOptions)
                .WithNotParsed(HandleParseError);
        }

        static void RunOptions(Options opts)
        {
            //handle options
        }

        static void HandleParseError(IEnumerable<Error> errs)
        {
            Console.WriteLine("Invalid argument(s)");

            foreach (var error in errs)
            {
                Console.WriteLine(error);
            }
        }
    }

    public class Options
    {
        [Option('s', "signingKey", Required = true, HelpText = "Signing key used to sign the token")]
        public string SigningKey { get; set; }

        [Option('e', "encryptionKey", Required = true, HelpText = "Encryption key used to encrypt the token")]
        public string EncryptionKey { get; set; }

        [Option('i', "issuer", Required = true, HelpText = "Authority issuing the token")]
        public string Issuer { get; set; }

        [Option('a', "audience", Required = true, HelpText = "Intended audience for the token")]
        public string Audience { get; set; }

        [Option('c', "claims", Required = true, HelpText = "Claims, e.g. nameid:geoff")]
        public IEnumerable<string> Claims { get; set; }
    }
}
