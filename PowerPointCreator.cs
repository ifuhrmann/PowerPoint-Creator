using System;

namespace PowerPointCreator
{
    public class PowerPoint_Maker
    {
        // Main Method 
        static void Main(string[] args) { 

            Console.WriteLine("Enter a suggested search\n");
            string search = Console.ReadLine();
            ImageGetter.getRelatedImages(search,3);

            // To prevents the screen from  
            // running and closing quickly 
            Console.ReadKey(); 
        } 
    }
}