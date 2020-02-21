using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoldBadgeAppChallenge;

namespace _03_Badges
{
    class ProgramUI
    {
        private readonly BadgeRepository _badges = new BadgeRepository();
        private readonly IConsole _console;

        public ProgramUI(IConsole console) => _console = console;

        static void Main()
        {
            MyConsole console = new MyConsole();
            ProgramUI ui = new ProgramUI(console);
            ui.BadgeManagerUI();

        }

        private void BadgeManagerUI()
        {
            bool running = true;
            while (running)
            {
                _console.Clear();
                _console.Title("Badge Breaker v1.337");
                Console.ForegroundColor = ConsoleColor.Green;

                _console.WriteLine("Hello Security Admin, What would you like to do?");

                Console.ForegroundColor = ConsoleColor.Blue;
                _console.WriteLine( "1. Add a Badge\n" +
                                    "2. Edit a Badge\n" +
                                    "3. List All Badges\n" +
                                    "4. Delete All Doors on a Badge\n" +
                                    "X. Exit");
                string input = _console.ReadLine();

                switch (input)
                {
                    case "1":
                        Create();
                        break;
                    case "2":
                        Update();
                        break;
                    case "3":
                        DisplayAll();
                        break;
                    case "4":
                        DeleteDoors();
                        break;
                    case "x":
                    case "exit":
                    case "5":
                        running = false;
                        break;
                    default:
                        _console.WriteLine("INVALID SELECTION!!!\npress any key to try again");
                        _console.ReadKey();
                        break;
                }
            }
        }
        private void Create()
        {
            Badge newBadge = new Badge();
            string input;
            _console.Write("please enter the new Badge ID number: ");
            newBadge.BadgeID = Console.ReadLine();

            _console.Write("please enter the valid doors for this badge separated by COMMAS: ");
            input = Console.ReadLine().ToUpper();
            newBadge.ValidDoors = input.Split(',').ToList<string>();

            _badges.Add(newBadge);
        }

        private void DisplayAll()
        {
            _console.Clear();
            Dictionary<string, Badge> badges = _badges.GetBadges();
            //GEWGLE the Key Value Pair
            Console.WriteLine("Badge ID\tValid Doors\n");
            foreach (KeyValuePair<string, Badge> entry in badges)
            {
                _console.Write($" {entry.Key} \t\t {entry.Value.DoorsAsString()} \n");
            }
            _console.WriteLine("Press anykey to continue...");
            _console.ReadKey();
        }

        private void Update()
        {
            bool updating = true;
            while (updating)
            {
                _console.Clear();
                _console.WriteLine("What is the badge number to update? ");
                string reqItem = _console.ReadLine();
                Badge tarItem = _badges.GetBadge(reqItem);
                if (tarItem == null) { _console.WriteLine($"{tarItem} could not be Found\nPress Any Key to Continue..."); _console.ReadKey(); }
                else
                {
                    reqItem = reqItem.ToLower();
                    bool updatingDoors = true;
                    while (updatingDoors)
                    {
                        _console.Clear();
                        _console.WriteLine($"{tarItem.BadgeID} has access to these doors: {tarItem.DoorsAsString()}\n1 Remove a Door     2 Add a Door     x Exit");
                        string reqField = _console.ReadLine().ToLower();
                        string door;
                        switch (reqField)
                        {
                            case "1":
                            case "remove":
                                //remove a door from the door list
                                Console.Write($"Please enter a door to remove: ");
                                door = Console.ReadLine().ToUpper();
                                tarItem.ValidDoors.Remove(door);
                                break;
                            case "2":
                            case "add":
                                //add a door to the door list
                                Console.Write($"Please enter a door to add: ");
                                door = Console.ReadLine().ToUpper();
                                tarItem.ValidDoors.Add(door);
                                break;
                            case "x":
                            case "exit":
                                _badges.GetBadge(tarItem.BadgeID).ValidDoors = tarItem.ValidDoors;
                                updatingDoors = false;
                                updating = false;
                                break;
                        }
                    }
                }
            }

        }
        private void DeleteDoors()
        {
            _console.Clear();
            _console.Write("What is the badge number to Remove ALL DOORS from: ");
            string reqItem = _console.ReadLine();
            Badge tarItem = _badges.GetBadge(reqItem);
            _console.WriteLine($"{tarItem.BadgeID} has access to these doors: {tarItem.DoorsAsString()}\n\n " +
                $"ARE YOU SURE YOU WOULD LIKE TO REMOVE ALL DOORS FROM THIS BADGE?\n" +
                $"Type REMOVE to CONFIRM deletion - anything else will return to the previous menu.");
            string input = _console.ReadLine();
            if (input == "REMOVE") 
            {
                tarItem.ValidDoors.Clear();
            }
        }

        private bool Exit()
        {
            _console.WriteLine("Saving data NOT REALLY LOL");
            _console.WriteLine("Press any key to Exit");
            _console.ReadKey();

            return false;


        }
    }
}
