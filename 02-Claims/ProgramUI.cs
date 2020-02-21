using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoldBadgeAppChallenge;

namespace _02_Claims
{
    class ProgramUI
    {
        private readonly ClaimRepository _claims = new ClaimRepository();
        private readonly IConsole _console;
        //FOR TESTING ONLY
        public ProgramUI(IConsole console) => _console = console;


        static void Main(string[] args)
        {
            MyConsole console = new MyConsole();
            ProgramUI ui = new ProgramUI(console);

            //FOR TESTING ONLY
            ui._claims.Filler();
            //END TESTING STUFF

            ui.ClaimsManager();
        }

        private void ClaimsManager()
        {
            bool running = true;
            _console.Title("Claim Cracker v1.337");
            while (running)
            {
                _console.Clear();

                Console.ForegroundColor = ConsoleColor.Green;
                _console.WriteLine("Welcome [Valued] Claims Agent");
                Console.ForegroundColor = ConsoleColor.Cyan;
                _console.WriteLine("Please choose from the following options by typing the number or task name: \n" +
                                    "1 - See All Claims\n" +
                                    "2 - Take Care of Next Claim\n" +
                                    "3 - Enter New Claim\n" +
                                    "X - to exit\n");
                string input = _console.ReadLine();

                switch (input)
                {
                    case "1":
                        DisplayAll();
                        _console.ReadKey();
                        break;
                    case "2":
                        Process();
                        _console.ReadKey();
                        break;
                    case "3":
                        Add();
                        _console.ReadKey();
                        break;
                    case "x":
                    case "q":
                    case "exit":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("INVALID SELECTION");
                        _console.ReadKey();
                        break;
                }
            }
        }

        public void DisplayAll()
        {
            List<Claim> claims = _claims.ReadAll();

            _console.Clear();
            _console.WriteLine("ClaimID\tType\tAmount\tDateOfAccident\tDateOfClaim\tIsValid\tDescription");
            foreach (Claim claim in claims)
            {
                _console.WriteLine($"{claim}");
            }
        }

        public void Process()
        {
            Claim claim = _claims.Peek();
            _console.Clear();
            _console.WriteLine($"Here are the details for the next claim to be handled: \n" +
                $"ClaimID: {claim.ClaimID}\n" +
                $"Type: {claim.ClaimType}\n" +
                $"Description: {claim.Description}\n" +
                $"Amount: {claim.amount}\n" +
                $"Date of Accident: {claim.DateofIncident}\n" +
                $"Date of Claim: {claim.DateofClaim}\n" +
                $"Claim is Valid?: {claim.IsValid}\n" +
                $"\nDo you want to deal with this claim now(y/n)?");
            var input = _console.ReadKey().KeyChar;
            if (input == 'y')
            {
                _claims.Dequeue();
                Console.WriteLine("\nThe Claim has been DEQUEUED!\nPress any key to continue...");
                return;
            }
            else
            {
                Console.WriteLine("yeah probably best to leave it for someone else to deal with...\n Press any key to return to the main menu");
                _console.ReadKey();
                return;
            }
        }

        public void Add()
        {
            //given this perenial problem of interfacing, it makes sense to create a set; overload
            Claim newclaim = new Claim();
            string input= "";
            int year;
            int month;
            int day;
            Console.Write($"Enter Claim ID: ");
            newclaim.ClaimID = _console.ReadLine();
            Console.Write($"Enter Description: ");
            newclaim.Description = _console.ReadLine();
            Console.Write($"Enter Amount: ");
            input = _console.ReadLine();
            if (input.StartsWith("$")) { input = input.Replace("$", ""); }
            Double.TryParse(input, out double result);
            newclaim.amount = result;

            //set up date of incident
            Console.Write($"Enter the Year of Incident: ");
            input = _console.ReadLine();
            Int32.TryParse(input, out year);
            Console.Write($"Enter the Month of Incident: ");
            input = _console.ReadLine();
            Int32.TryParse(input, out month);
            Console.Write($"Enter the Day of Incident: ");
            input = _console.ReadLine();
            Int32.TryParse(input, out day);
            newclaim.DateofIncident = new DateTime(year, month, day);

            //set up date of claim
            Console.Write($"Enter the Year of Claim: ");
            input = _console.ReadLine();
            Int32.TryParse(input, out year);
            Console.Write($"Enter the Month of Claim: ");
            input = _console.ReadLine();
            Int32.TryParse(input, out month);
            Console.Write($"Enter the Day of Claim: ");
            input = _console.ReadLine();
            Int32.TryParse(input, out day);
            newclaim.DateofClaim = new DateTime(year, month, day);

            
            _console.WriteLine($"add this new claim:\n {newclaim} \nto the database? (y/n)");
            //yes or no, makes no difference to me!!!
            _claims.Add(newclaim);
        }
        /*
        public DateTime DateBuilder(List<string> strings)
        {
            Console.Write($"Enter the Year: ");
            input = _console.ReadLine();
            Int32.TryParse(input, out year);
            Console.Write($"Enter the Month ");
            input = _console.ReadLine();
            Int32.TryParse(input, out month);
            Console.Write($"Enter the Day: ");
            input = _console.ReadLine();
            Int32.TryParse(input, out day);
            newclaim.DateofIncident = new DateTime(year, month, day);
        }*/
    }
}
