// See https://aka.ms/new-console-template for more information
using System;



namespace Hotel_Database_aflevering

{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceFacility theCode = new ServiceFacility();
            theCode.Run();

            Console.WriteLine();
            Console.WriteLine("Press any key to close the program...");

            Console.ReadKey();

        }
    }
}
