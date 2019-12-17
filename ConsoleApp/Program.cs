using System;
using DomainLogic;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine($"The only workflow is: {TopProductWorkflow.WorkflowName()}");
        }
    }
}