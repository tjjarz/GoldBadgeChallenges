using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldBadgeAppChallenge
{
    public class Meal
    {
        public int ItemNum { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Ingredients { get; set; }
        public double Price { get; set; }

        public Meal(int itemnum,string name,string desc,List<string> ingredients)
        {
            ItemNum = itemnum;
            Name = name;
            Description = desc;
            Ingredients = ingredients;
        }

        public Meal() { }
    }

    public class MenuRepository
    {
        List<Meal> _menu = new List<Meal>();


        public bool NewMeal(Meal meal)
        {
            int dirLength = _menu.Count();
            _menu.Add(meal);
            bool wasAdded = _menu.Count() == dirLength + 1;
            return wasAdded;
        }

        public List<Meal> GetMenu()
        {
            return _menu;
        }

        public Meal GetItemByName(string name)
        {
            foreach (Meal meal in _menu)
            {
                if (meal.Name.ToLower() == name.ToLower())
                {
                    return meal;
                }

            }
            return null;
        }
        public bool UpdateMeal(string orgmeal, Meal newmeal)
        {

            Meal oldmeal = GetItemByName(orgmeal);

            if (oldmeal != null)
            {
                int index = _menu.IndexOf(oldmeal);
                _menu[index] = newmeal;
                return true;
            }
            return false;
        }

        public bool DeleteExistingItem(string name)
        {
            Meal foundmeal = GetItemByName(name);
            bool result = _menu.Remove(foundmeal);
            return result;
        }

    }
}
