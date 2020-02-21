using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldBadgeAppChallenge
{
    public class ProgramUI
    {
        private readonly MenuRepository _menu = new MenuRepository();
        private readonly IConsole _console;

        public ProgramUI(IConsole console) => _console = console;

        static void Main()
        {
            MyConsole console = new MyConsole();
            ProgramUI ui = new ProgramUI(console);
            ui.MenuUI();
        }

        private void MenuUI()
        {
            bool running = true;
            while (running)
            {
                _console.Clear();

                //Console.BackgroundColor = ConsoleColor.DarkGreen;
                _console.Title("Cafe Manager v.1.337");
                Console.ForegroundColor = ConsoleColor.Green;
                
                _console.WriteLine("Hello! -Valued Cafe Management Person-");

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                _console.WriteLine("Please select from the following options - which have recently changed:\n" +
                    "1. Display Current Menu\n" +
                    "2. Display Menu by Food Restrictions\n" +
                    "3. Add New Menu Item\n" +
                    "4. Update Existing Menu Item\n" +
                    "5. Delete Existing Menu Item\n" +
                    "6. Report Suspected Union Activity\n" +
                    "X. Exit");
                string input = _console.ReadLine();

                switch (input.ToLower())
                {
                    case "1":
                        //display Menu
                        DisplayAll();
                        break;
                    case "2":
                        //display by food restriction
                        _console.WriteLine("This feature is not currently implimented, implementation planned NO LATER THAN July 2014");
                        break;
                    case "3":
                        //Add new menu item
                        Create();
                        break;
                    case "4":
                        //Update Menu Item
                        Update();
                        break;
                    case "5":
                        //delete existing menu item
                        Delete();
                        break;
                    case "6":
                        //bust the trusts!
                        _console.WriteLine("Suspected Activity has been reported to Corporate: Anti-Union team will dispatch in 1 hour.");
                        Console.ForegroundColor = ConsoleColor.Red;
                        _console.WriteLine("Await further instructions and TELL NO ONE!\n\n\n\n This terminal will self destruct in five seconds . . .\n Good Luck Jim");
                        _console.ReadLine();
                        running = false;
                        break;
                    case "x":
                        running = Exit();
                        break;
                    default:
                        _console.WriteLine($"{input} is not a valid selection!");
                        _console.ReadKey();
                        break;
                }
                
            }
        }

        private void Create()
        {
            Meal newMeal = new Meal();
            double newPrice;
            string input;
            _console.Write("please enter the Name of this meal: ");
            newMeal.Name = Console.ReadLine();

            _console.Write("please enter a Menu Number for this meal: ");
            newMeal.ItemNum = Convert.ToInt32(Console.ReadLine());

            _console.Write("please enter a Description for this meal: ");
            newMeal.Description = Console.ReadLine();

            _console.Write("please enter a Price for this meal: ");
            input = Console.ReadLine();
            if(Double.TryParse(input, out newPrice)) { newMeal.Price = newPrice; }
            else { _console.WriteLine("You did not enter a valid number. Shame on you."); }


            _console.Write("please enter the Ingredients of this meal separated by COMMAS: ");
            input = Console.ReadLine();
            newMeal.Ingredients = input.Split(',').ToList<string>();

            _menu.NewMeal(newMeal);
        }

        /*private void Read()
        {
            _menu.GetMenu();
        }*/

        private void DisplayAll()
        {
            _console.Clear();
            List<Meal> directory = _menu.GetMenu();

            foreach (Meal item in directory)
            {
                _console.WriteLine($"{item.ItemNum}) {item.Name} - {item.Description}   ${item.Price}");
                _console.Write("Ingredients: ");
                //int i = 1;

                for (int i = 0; i < item.Ingredients.Count; i++)
                {
                    _console.Write($"{item.Ingredients[i]}");
                    if (i+1 == item.Ingredients.Count) { _console.Write(".\n\n "); }
                    else { _console.Write(", "); }
                }
                /*
                foreach (string ingredient in item.Ingredients)
                { 
                    if (i == item.Ingredients.Count)
                    _console.Write($"{ingredient} "); 
                }*/
                _console.Write("\n");

            }
            _console.WriteLine("Press anykey to continue...");
            _console.ReadKey();
        }

        private void Update()
        {
            _console.Clear();
            _console.WriteLine("Enter title you'd like to update, ye scaberous dog!");
            string reqItem = _console.ReadLine();

            //locate existing content
            Meal reqMeal = _menu.GetItemByName(reqItem);
            if (reqMeal == null) { _console.WriteLine($"{reqMeal} could not be Found\nPress Any Key to Continue..."); _console.ReadKey(); return; }
            bool updatingItem = true;
            while (updatingItem)
            {

                _console.WriteLine($"Selected Meal found, please indicate which field you would like to update:\n" +
                                   $"1: Menu Number:{reqMeal.ItemNum} | 2: Name [{reqMeal.Name}] | 3: Desc [{reqMeal.Description}] | 4: Price [${reqMeal.Price}]\n" +
                                   $"5: - Ingredients: {reqMeal.Ingredients}");
                string reqField = _console.ReadLine();
                reqField = reqField.ToLower();
                string mealEdit = "";
                int newNum;
                double newPrice;
                //create updated version

                switch (reqField)
                {
                    case "1":
                    case "number":
                    case "menu number":
                        _console.Write($"Enter new Menu Number: ");
                        mealEdit = _console.ReadLine();
                        if (Int32.TryParse(mealEdit, out newNum)) { reqMeal.ItemNum = newNum; }
                        else { _console.WriteLine("You did not enter a valid number. Shame on you."); }
                        break;
                    case "2":
                    case "name":
                        _console.Write($"Enter new Name: ");
                        mealEdit = _console.ReadLine();
                        reqMeal.Name = mealEdit;
                        break;
                    case "3":
                    case "description":
                        _console.Write($"Enter new Description: ");
                        mealEdit = _console.ReadLine();
                        reqMeal.Description = mealEdit;
                        break;
                    case "4":
                    case "price":
                        _console.Write($"Enter new Price:");
                        mealEdit = _console.ReadLine();
                        if (Double.TryParse(mealEdit, out newPrice)) { reqMeal.Price = newPrice; }
                        else { _console.WriteLine("You did not enter a valid number. Shame on you."); }
                        break;
                    case "5":
                    case "ingredients":
                        _console.Write("please enter the Ingredients of this meal separated by COMMAS: ");
                        mealEdit = _console.ReadLine();
                        reqMeal.Ingredients = mealEdit.Split(',').ToList<string>();
                        break;
                    case "exit":
                    case "x":
                        updatingItem = false;
                        break;
                    default:
                        _console.WriteLine("invalid selection!");
                        break;
                }
            }
        }
        private void Delete()
        {
            _console.Clear();
            _console.WriteLine("REMOVE ye content!");
            string input = _console.ReadLine();
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
