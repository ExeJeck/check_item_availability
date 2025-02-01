using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp1
{
    class MedicineFeatureSet
    {
        public MedicineFeatureSet()
        {

        }
        public string[] Medical = { "Aspirin", "Paracetamol", "Ibuprofen", "Amoxicillin", "Cetirizine", "Omeprazole", "Simastatin", "Metformin" };
        public string[] SetAppointment = { "against pain", "antipyretic", "against inflammation", "antibiotic", "against allergies", "against ulcers", "against cholesterol", "against diabetes" };
        public string[] SetForm = { "tablet", "capsule", "syrup", "gel", "ointment", "solution", "plaster" };
        public string[] SetCountries = { "Ukraine", "USA", "Germany", "India", "China", "Japan" };
        public void ShowAllVariable()
        {
            Console.Write("Name:");
            for (int i = 0; i < Medical.Length; i++)
            {
                Console.Write($"{Medical[i]}, ");
            }
            Console.WriteLine();
            Console.Write("Appointment:");
            for (int i = 0; i < SetAppointment.Length; i++)
            {
                Console.Write($"{SetAppointment[i]}, ");
            }
            Console.WriteLine();
            Console.Write("Form:");
            for (int i = 0; i < SetForm.Length; i++)
            {
                Console.Write($"{SetForm[i]}, ");
            }
            Console.WriteLine();
            Console.Write("Producing country:");
            for (int i = 0; i < SetCountries.Length; i++)
            {
                Console.Write($"{SetCountries[i]}, ");
            }
            Console.WriteLine("Medicine prices range from 10 to 50");
        }
    }

    internal class Medicine
    {
        public string Name { get; set; }
        public string Appointment { get; set; }
        public string Form { get; set; }
        public string ProducingCountry { get; set; }
        public int Price { get; set; }

        public Medicine(MedicineFeatureSet MedecineFeatureSet)
        {
            Random random = new Random();

            Price = random.Next(10, 51);
            Name = MedecineFeatureSet.Medical[random.Next(0, 8)];
            Appointment = MedecineFeatureSet.SetAppointment[random.Next(0, 8)];
            Form = MedecineFeatureSet.SetForm[random.Next(0, 7)];
            ProducingCountry = MedecineFeatureSet.SetCountries[random.Next(0, 6)];

            Thread.Sleep(100);
        }

        public void MedicalDisplay()
        {
            Console.OutputEncoding = System.Text.Encoding.Default;
            Console.WriteLine($"{Name}, {Appointment}, {Form}, {Price}, {ProducingCountry}");
        }
    }
    class SelectedFilters
    {
        public List<string> SelectedName { get; set; }
        public List<string> SelectedAppointment { get; set; }
        public List<string> SelectedForm { get; set; }
        public List<string> SelectedProducingCountry { get; set; }
        public int? SelectedPriceFrom { get; set; }
        public int? SelectedPriceTo { get; set; }
        public bool CheckSetFilltrePrice { get; set; }
        public SelectedFilters()
        {
            SelectedName = new List<string>();
            SelectedAppointment = new List<string>();
            SelectedForm = new List<string>();
            SelectedProducingCountry = new List<string>();
        }
        public void ResetFilters()
        {
            SelectedName.Clear();
            SelectedAppointment.Clear();
            SelectedForm.Clear();
            SelectedProducingCountry.Clear();
            SelectedPriceFrom = null;
            SelectedPriceTo = null;
            CheckSetFilltrePrice = false;
        }
    }
    class Search 
    {
        public SelectedFilters selectedFilters = new SelectedFilters();
        public MedicineFeatureSet medicationOptions = new MedicineFeatureSet();
        public List<Medicine> suitableMedicines;
        public bool WhetherAddNewElementToFilter = true;
        public void SearchProgram(Medicine[] allMedicine)
        {
            Console.WriteLine();
            Console.WriteLine("Search program is running...");
            Console.WriteLine("You can search for medicine by Name, Appointment, Form, Price, Producing country");
            Console.WriteLine();

            medicationOptions.ShowAllVariable();

            Filter(allMedicine);
            Console.WriteLine("Entering parameters into the filter is over");
            ChoosingTheRightMedicine(new ShowSuitableMedicine(), selectedFilters, allMedicine);
            ShowSuitableMedicine();
            ResetFilterOptionsPrompt(allMedicine);


        }
        public void Filter(Medicine[] allMedicine)
        {
            Console.WriteLine();
            {
                if (!selectedFilters.CheckSetFilltrePrice)
                {
                    Console.WriteLine($"Why do you need to search? Write: Name, Appointment, Form, Price, Producing country");
                }
                else
                {
                    Console.WriteLine($"Why do you need to search? Write: Name, Appointment, Form, Producing country");
                }
            }
            if (WhetherAddNewElementToFilter)
            {
                WhetherAddNewElementToFilter = AddingVariablesToFilter(new AddingVariablesToFilter(), medicationOptions, selectedFilters, allMedicine);
                if(WhetherAddNewElementToFilter)
                {
                    Filter(allMedicine);
                }
            }
        }
        public bool AddingVariablesToFilter(AddingVariablesToFilter addingVariablesToFilter, MedicineFeatureSet medicationOptions, SelectedFilters selectedFilters, Medicine[] allMedicine)
        {
            if(!selectedFilters.CheckSetFilltrePrice)
            {
                return addingVariablesToFilter.AddingVariablesToFilterWithPrice(medicationOptions, selectedFilters, allMedicine);
            }
            else
            {
                return addingVariablesToFilter.AddingVariablesToFilterWithoutPrice(medicationOptions, selectedFilters, allMedicine);
            }
        }
        public void ChoosingTheRightMedicine(ShowSuitableMedicine filteredMedicine, SelectedFilters selectedFilters, Medicine[] allMedicine)
        {
            suitableMedicines = filteredMedicine.FilteredMedicine(selectedFilters, allMedicine);
        }
        public void ShowSuitableMedicine()
        {
            foreach (Medicine medicine in suitableMedicines)
            {
                Console.WriteLine($"{medicine.Name}, {medicine.Appointment}, {medicine.Form}, {medicine.ProducingCountry}, {medicine.Price}");
            } 
        }
        public void ResetFilterOptions()
        {
            WhetherAddNewElementToFilter = true;
            selectedFilters.ResetFilters();

        }
        public void FilterRestart(Medicine[] allMedicine)
        {
            ResetFilterOptions();
            SearchProgram(allMedicine);
        }
        public void ResetFilterOptionsPrompt(Medicine[] allMedicine)
        {
            Console.WriteLine("find the right medicine? Yes / No");
            string UserChange = Console.ReadLine();
            if(UserChange == "Yes")
            {
                FilterRestart(allMedicine);
            }
            else { }
        }

    }
    class AddingVariablesToFilter
    {
        public AddMoreElementsToFilter addMoreElementsToFilter = new AddMoreElementsToFilter();
        public AddingVariablesToFilter()
        {
        }
        public bool AddingVariablesToFilterWithPrice(MedicineFeatureSet medicationOptions, SelectedFilters selectedFilters, Medicine[] allMedicine)
        {
            string MedicineSearchOption = Console.ReadLine();
            switch (MedicineSearchOption)
            {
                case "Name":
                    AddingOptionsToFilter(medicationOptions.Medical, selectedFilters.SelectedName, MedicineSearchOption);
                    break;
                case "Appointment":
                    AddingOptionsToFilter(medicationOptions.SetAppointment, selectedFilters.SelectedAppointment, MedicineSearchOption);
                    break;
                case "Form":
                    AddingOptionsToFilter(medicationOptions.SetForm, selectedFilters.SelectedForm, MedicineSearchOption);
                    break;
                case "Producing country":
                    AddingOptionsToFilter(medicationOptions.SetCountries, selectedFilters.SelectedProducingCountry, MedicineSearchOption);
                    break;
                case "Price":
                    AddingPriceOptionsToFilter(selectedFilters);
                    break;
            }
            return ResultAddingNewElementToFilter(allMedicine);
        }
        public bool AddingVariablesToFilterWithoutPrice(MedicineFeatureSet medicationOptions, SelectedFilters selectedFilters, Medicine[] allMedicine)
        {
            string MedicineSearchOption = Console.ReadLine();
            switch (MedicineSearchOption)
            {
                case "Name":
                    AddingOptionsToFilter(medicationOptions.Medical, selectedFilters.SelectedName, MedicineSearchOption);
                    break;
                case "Appointment":
                    AddingOptionsToFilter(medicationOptions.SetAppointment, selectedFilters.SelectedName, MedicineSearchOption);
                    break;
                case "Form":
                    AddingOptionsToFilter(medicationOptions.SetForm, selectedFilters.SelectedName, MedicineSearchOption);
                    break;
                case "Producing country":
                    AddingOptionsToFilter(medicationOptions.SetCountries, selectedFilters.SelectedName, MedicineSearchOption);
                    break;
                default:
                    Console.WriteLine("unknown category");
                    break;
            }
            return ResultAddingNewElementToFilter(allMedicine);
        }
        public void AddingOptionsToFilter(string[] SetMedicineOption, List<string> selectedFilters, string MedicineSearchOption)
        {
            Console.WriteLine($"Change {MedicineSearchOption} among those offered");
            for (int i = 0; i < SetMedicineOption.Length; i++)
            {
                Console.Write($"{SetMedicineOption[i]}, ");
            }
            Console.WriteLine();
            string SelectedName = Console.ReadLine();
            bool SpellChecking = false;
            for (int i = 0; i < SetMedicineOption.Length && SpellChecking == false; i++)
            {
                SpellChecking = SelectedName.Equals(SetMedicineOption[i]);
            }
            if (SpellChecking)
            {
                Console.WriteLine("the selected title has been successfully added to the filter");
                selectedFilters.Add(SelectedName);
            }
            else
            {
                Console.WriteLine("It seems that you have entered the name of a medicine that is not available in our pharmacy, try selecting the filters again");
            }
        }
        public void AddingPriceOptionsToFilter(SelectedFilters selectedFilters)
        {
            Console.WriteLine("Choose a search option by price from among the ones offered: From, To, From - To");
            string SelectedOptionAskForPrice = Console.ReadLine();
            int PriceFrom;
            int PriceUpTo;
            switch (SelectedOptionAskForPrice)
            {
                case "From":
                    Console.WriteLine("\r\nSet the price from (Prices for Medicines start from 10, but the value of the price from can be set to less than 10)");
                    PriceFrom = Convert.ToInt32(Console.ReadLine());
                    selectedFilters.SelectedPriceFrom = PriceFrom;
                    selectedFilters.CheckSetFilltrePrice = true;
                    break;
                case "To":
                    Console.WriteLine("Set the price to (Maximum medicine price 50)");
                    PriceUpTo = Convert.ToInt32(Console.ReadLine());
                    selectedFilters.SelectedPriceTo = PriceUpTo;
                    selectedFilters.CheckSetFilltrePrice = true;
                    break;
                case "From - To":
                    Console.WriteLine("Set the price from - to");
                    Console.WriteLine("From");
                    PriceFrom = Convert.ToInt32(Console.ReadLine());
                    selectedFilters.SelectedPriceFrom = PriceFrom;
                    Console.WriteLine("To");
                    PriceUpTo = Convert.ToInt32(Console.ReadLine());
                    selectedFilters.SelectedPriceTo = PriceUpTo;
                    selectedFilters.CheckSetFilltrePrice = true;
                    break;
                default:
                    Console.WriteLine("Unknown price search option");
                    break;
            }
        }
        public bool ResultAddingNewElementToFilter(Medicine[] allMedicine)
        {
            return addMoreElementsToFilter.AddOrNotMoreElementsToFilter(allMedicine);
        }
    }
    class AddMoreElementsToFilter
    {
        public AddMoreElementsToFilter()
        {

        }
        public bool AddOrNotMoreElementsToFilter(Medicine[] allMedicine)
        {
            Console.WriteLine("Want to add more elements to the filter ( Yes / No ) || Remove filter settings ( Reset )");
            string UserChange = Console.ReadLine();
            bool ResultQuestionsAboutAdding;

            if (UserChange == "Yes")
            {
                ResultQuestionsAboutAdding = true;
            }
            else if(UserChange == "Reset")
            {
                SearchRestart(new Search(), allMedicine);
                return true;
            }
            else
            {
                ResultQuestionsAboutAdding = false;
            }
            return ResultQuestionsAboutAdding;
        }
        public void SearchRestart(Search search, Medicine[] allMedicine)
        {
            search.FilterRestart(allMedicine);
        }
    }
    class ShowSuitableMedicine
    {
        public ShowSuitableMedicine()
        {

        }
        public List<Medicine> FilteredMedicine(SelectedFilters selectedFilters, Medicine[] AllMedicine)
        {
            List <Medicine> suitableMedicine = new List<Medicine> ();
            if (selectedFilters.SelectedName.Count == 0 && selectedFilters.SelectedAppointment.Count == 0 && selectedFilters.SelectedForm.Count == 0 && selectedFilters.SelectedProducingCountry.Count == 0 && selectedFilters.SelectedPriceFrom == null && selectedFilters.SelectedPriceTo == null)
            {
                Console.WriteLine("You have not specified any parameters for the filter\r\n");
            }
            else
            {
                for (int i = 0; i < AllMedicine.Length; i++)
                {
                    bool isMatch = true;
                    if (selectedFilters.SelectedName.Count() != 0)
                    {
                        if (isMatch)
                        {
                            isMatch = CheckParametersForMedicineCompliance(selectedFilters.SelectedName, AllMedicine[i].Name, isMatch);
                        }
                    }
                    if (selectedFilters.SelectedAppointment.Count() != 0)
                    {
                        if (isMatch)
                        {
                            isMatch = CheckParametersForMedicineCompliance(selectedFilters.SelectedAppointment, AllMedicine[i].Appointment, isMatch);
                        }
                    }
                    if (selectedFilters.SelectedForm.Count() != 0)
                    {
                        if (isMatch)
                        {
                            isMatch = CheckParametersForMedicineCompliance(selectedFilters.SelectedForm, AllMedicine[i].Form, isMatch);
                        }
                    }
                    if (selectedFilters.SelectedProducingCountry.Count() != 0)
                    {
                        if (isMatch)
                        {
                            isMatch = CheckParametersForMedicineCompliance(selectedFilters.SelectedProducingCountry, AllMedicine[i].ProducingCountry, isMatch);
                        }
                    }
                    if(selectedFilters.SelectedPriceFrom != null || selectedFilters.SelectedPriceTo != null && isMatch)
                    {
                        if (selectedFilters.SelectedPriceFrom != null && selectedFilters.SelectedPriceTo == null)
                        {
                            isMatch = CheckPriceFrom(selectedFilters, AllMedicine[i].Price, isMatch);
                        }
                        else if (selectedFilters.SelectedPriceFrom != null && selectedFilters.SelectedPriceTo == null)
                        {
                            if (AllMedicine[i].Price <= selectedFilters.SelectedPriceTo)
                            {
                                isMatch = CheckPriceTo(selectedFilters, AllMedicine[i].Price, isMatch);
                            }
                            else isMatch = false;
                        }
                        else if (selectedFilters.SelectedPriceTo != null && selectedFilters.SelectedPriceFrom == null)
                        {
                            if (AllMedicine[i].Price >= selectedFilters.SelectedPriceFrom && AllMedicine[i].Price <= selectedFilters.SelectedPriceTo)
                            {
                                isMatch = CheckPriceFromTo(selectedFilters, AllMedicine[i].Price, isMatch);
                            }
                            else isMatch = false;
                        }
                    }
                    if(isMatch)
                    {
                        suitableMedicine.Add(AllMedicine[i]);
                    }
                }
                if (suitableMedicine.Count() != 0)
                {
                    Console.WriteLine("Our pharmacy has what suits you");
                }
                else
                {
                    Console.WriteLine("Unfortunately, the medicines you need are not available");
                }
            }
            return suitableMedicine;
        }
        public bool CheckParametersForMedicineCompliance(List<string> selectedFilters, string medicineParameter, bool isMath)
        {
            bool checkingForCorrectness = isMath;
            for (int j = 0; j < selectedFilters.Count(); j++)
            {
                

                if (medicineParameter == selectedFilters[j])
                {
                    checkingForCorrectness = true;
                    break;
                }
                else
                {
                    checkingForCorrectness = false;
                }
            }
            return checkingForCorrectness;
        }
        public bool CheckPriceFrom(SelectedFilters selectedFilters, int MedicinePrice, bool isMatch)
        {
            if (MedicinePrice >= selectedFilters.SelectedPriceFrom)
            {
            }
            else isMatch = false;
            return isMatch;
        }
        public bool CheckPriceTo(SelectedFilters selectedFilters, int MedicinePrice, bool isMatch)
        {
            if (MedicinePrice <= selectedFilters.SelectedPriceTo)
            {
            }
            else isMatch = false;
            return isMatch;
        }
        public bool CheckPriceFromTo(SelectedFilters selectedFilters, int MedicinePrice, bool isMatch)
        {
            if (MedicinePrice >= selectedFilters.SelectedPriceFrom && MedicinePrice <= selectedFilters.SelectedPriceTo)
            {
            }
            else isMatch = false;
            return isMatch;
        }
    }
}