using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MPI;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            using (new MPI.Environment(ref args))
            {
                Intracommunicator com = MPI.Communicator.world;
                
                int lider = 0;
                int rand = random.Next(100 + com.Rank);
                Console.WriteLine("Firul " + com.Rank + " genereaza: " + rand);


                if (com.Rank == 0)
                {
                   
                    for (int i = 1; i < 10; i++)
                    {
                        int rand1 = com.Receive<int>(i, 0);
                        if (rand <= rand1)
                        {
                            lider = i;
                            rand = rand1;
                        }
                    }

                    for (int i = 1; i < 10; i++)
                        com.Send<int>(lider, i, 0);
                    Console.WriteLine("Liderul este" + lider);
                }
                else
                {
                    
                    com.Send<int>(rand, 0, 0);
                    com.Receive(0, 0,out lider);
                    
                }

                com.Dispose();
            }
        }
    }
}
