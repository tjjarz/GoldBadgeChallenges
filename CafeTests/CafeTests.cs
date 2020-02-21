using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoldBadgeAppChallenge;
using System.Linq;
using System.Collections.Generic;

namespace CafeTests
{
    [TestClass]
    public class MenuRepositoryTest
    {
        [TestMethod]
        public void AddMeal_ShouldGetTrueBool()
        {
            Meal content = new Meal();
            MenuRepository repository = new MenuRepository();

            bool addedcontent = repository.NewMeal(content);

            Assert.IsTrue(addedcontent);

        }

        private MenuRepository _repo;
        private Meal _content;

        [TestInitialize]
        public void Arrange()
        {
            _repo = new MenuRepository();
            string[] ingredients = { "vanilla", "ice", "baby", "too cold" };

            _repo.NewMeal(new Meal(1, "FOD", "it's food you idiot", ingredients.ToList()));
            _repo.NewMeal(new Meal(2, "asiojk", "it's food you idiot", ingredients.ToList()));
            _repo.NewMeal(new Meal(3, "fgeh5", "it's food you idiot", ingredients.ToList()));
            _repo.NewMeal(new Meal(6, "MANA", "it's food you idiot", ingredients.ToList()));
            _repo.NewMeal(new Meal(4, "Potion", "it's food you idiot", ingredients.ToList()));
            _repo.NewMeal(new Meal(5, "sword", "it's food you idiot", ingredients.ToList()));
            _repo.NewMeal(new Meal(6, "death", "it's food you idiot", ingredients.ToList()));
            _repo.NewMeal(new Meal(7, "Blade", "it's food you idiot", ingredients.ToList()));
            _repo.NewMeal(new Meal(8, "cats", "it's food you idiot", ingredients.ToList()));
        }

        [TestMethod]
        public void GetMeal_returnContent()
        {
            Meal searchResult = _repo.GetItemByName("FOD");
            Assert.AreEqual(searchResult.Name, "FOD");
        }

        [TestMethod]
        public void updateExistingContent_shouldbeTrue()
        {
            string[] ingredients = { "vanilla", "ice", "baby", "too cold" };

            _content = new Meal(1, "rapper", "it's food you idiot", ingredients.ToList());
            _repo.NewMeal(_content);
            bool wasUpdated = _repo.UpdateMeal("FOD", _content);
            Meal updatedcontent = _repo.GetItemByName("rapper");

            //pretty sure my chicanry worked earlier!
            Console.WriteLine(updatedcontent.Name);

            Assert.IsTrue(wasUpdated);
        }

        [TestMethod]
        public void RemoveContent_shouldtrue()
        {
            bool removeResult = _repo.DeleteExistingItem("FOD");
            Assert.IsTrue(removeResult);
        }


    }
}
