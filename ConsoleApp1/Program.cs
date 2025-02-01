using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Search Search = new Search();
            Medicine[] MedicationAvailable = new Medicine[10];

            for (int i = 0; i < MedicationAvailable.Length; i++)
            {
                MedicationAvailable[i] = new Medicine(new MedicineFeatureSet());
            }
            for (int i = 0; i < MedicationAvailable.Length; i++)
            {
                MedicationAvailable[i].MedicalDisplay();
            }

            Search.SearchProgram(MedicationAvailable);

            Console.ReadLine();
        }
    }
}

