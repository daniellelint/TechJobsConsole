using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TechJobsConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create two Dictionary vars to hold info for menu and data

            // Top-level menu options
            Dictionary<string, string> actionChoices = new Dictionary<string, string>();
            actionChoices.Add("search", "Search");
            actionChoices.Add("list", "List");

            // Column options
            Dictionary<string, string> columnChoices = new Dictionary<string, string>();
            columnChoices.Add("core competency", "Skill");
            columnChoices.Add("employer", "Employer");
            columnChoices.Add("location", "Location");
            columnChoices.Add("position type", "Position Type");
            columnChoices.Add("all", "All");

            // 1
            Console.WriteLine("Welcome to LaunchCode's TechJobs App!");

            // Allow user to search/list until they manually quit with ctrl+c
            while (true)
            {

                string actionChoice = GetUserSelection("View Jobs", actionChoices);

                if (actionChoice.Equals("list"))
                {
                    string columnChoice = GetUserSelection("List", columnChoices);

                    if (columnChoice.Equals("all"))
                    {
                        PrintJobs(JobData.FindAll());
                    }
                    else
                    {
                        List<string> searchBy = JobData.FindAll(columnChoice);
                        searchBy.Sort(); //--
                        Console.WriteLine("\n*** All " + columnChoices[columnChoice] + " Values ***");
                        foreach (string item in searchBy)
                        {
                            Console.WriteLine(item);
                        }
                    }
                }
                else // choice is "search"
                {
                    // How does the user want to search (e.g. by skill or employer)
                    string columnChoice = GetUserSelection("Search", columnChoices);

                    // What is their search term?
                    Console.WriteLine("\nSearch term: ");
                    string searchTerm = Console.ReadLine();
                    searchTerm = searchTerm.ToLower();

                    List<Dictionary<string, string>> userSearch;

                    // Fetch results
                    if (columnChoice.Equals("all"))
                    {
                        userSearch = JobData.FindByValue(searchTerm);//--
                        PrintJobs(userSearch);//--
                        //Console.WriteLine("Search all fields not yet implemented.");
                    }
                    else
                    {
                        userSearch = JobData.FindByColumnAndValue(columnChoice, searchTerm);
                        PrintJobs(userSearch);
                    }
                }
            }
        }

        
         // Returns the key of the selected item from the choices Dictionary
         
        private static string GetUserSelection(string choiceHeader, Dictionary<string, string> choices)
        {
            int choiceIdx;
            bool isValidChoice = false;
            string[] choiceKeys = new string[choices.Count];

            int i = 0;
            foreach (KeyValuePair<string, string> choice in choices)
            {
                choiceKeys[i] = choice.Key;
                i++;
            }

            do
            {
                // 2
                Console.WriteLine("\n" + choiceHeader + " by:");

                for (int j = 0; j < choiceKeys.Length; j++)
                {
                    Console.WriteLine(j + " - " + choices[choiceKeys[j]]);
                }

                string input = Console.ReadLine();
                choiceIdx = int.Parse(input);

                if (choiceIdx < 0 || choiceIdx >= choiceKeys.Length)
                {
                    Console.WriteLine("Invalid choices. Try again.");
                }
                else
                {
                    isValidChoice = true;
                }

            } while (!isValidChoice);

            return choiceKeys[choiceIdx];
        }


        private static void PrintJobs(List<Dictionary<string, string>> listOfJobs)
        {
            //Console.WriteLine("printJobs is not implemented yet");
            int counter = listOfJobs.Count;
            if (counter == 0)
                Console.WriteLine(" No data found with the search");
            else
            {
                foreach (Dictionary<string, string> job in listOfJobs)
                {
                    Console.WriteLine("\n****");
                    foreach (KeyValuePair<string, string> specificJob in job)
                    {
                        if (specificJob.Key != "")
                            Console.WriteLine(specificJob.Key + " : " + specificJob.Value);
                    }
                    Console.WriteLine("****\n\n");
                }

            }
        }
    }
}
