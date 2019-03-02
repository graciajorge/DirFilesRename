/**
 * Look for direct files in directory,
 * and change the file name by looking for
 * a token and replacing it with some string.
 * 
 * Date: 3/1/2019
 * Author: JG
 */
using System;
using CommandLine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DirFilesRename
{
    class Options
    {
        [Option(
            "dir",
            Required = true,
            HelpText = "Absolute directory contaning files"
            )]
        public string dirname { get; set; }

        [Option(
            "token",
            Required = true,
            HelpText = "String token in filename to be repaced"
            )]
        public string token { get; set; }
        [Option(
            "rpl",
            HelpText = "Replacement text that will replace token in filename",
            Default = ""
            )]
        public string rpl { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write(
                "*****************************\n" +
                "* File(s) token replacement *\n" +
                "* Author: JG                *\n" +
                "* Version: v1.0.0           *\n" +
                "*****************************\n");

            CommandLine.Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(opts => {
                    // successfully have required arguments passed :)
                    Console.Out.WriteLine("--found arguments");
                    Console.Out.WriteLine("--dir" + opts.dirname);
                    Console.Out.WriteLine("--token: " + opts.token);
                    Console.Out.WriteLine("--replacement text" + opts.rpl);

                    DirectoryInfo dir = new DirectoryInfo(opts.dirname);
                    FileInfo[] files = dir.GetFiles();
                    foreach(FileInfo file in files)
                    {
                        try
                        {
                            //move the file with the new name
                            Console.Out.WriteLine("--replace filename '" + file.Name + "' with '" + file.Name.Replace(opts.token, opts.rpl)+"'");
                            file.MoveTo(file.DirectoryName + "\\" + file.Name.Replace(opts.token, opts.rpl));
                        }
                        catch (Exception)
                        {
                            Console.Out.WriteLine("--uh oh! failed to overwrite some file: '" + file.Name+"' :(");
                        }

                    }
                });
        }
    }
}
