using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyAnimalFinder.Controllers;

namespace MyAnimalFinder.Models
{
    public class Animal
    {
        public string name { get; set; }
        public Animal(string newName = "")
        {
            name = newName;
        }
    }
    public class Pig : Animal
    {
        public Pig(string newName = "Pig") : base(newName) { }
    }
    public class Chicken : Animal
    {
        public Chicken(string newName = "Chicken") : base(newName) { }
    }
    public class Snek : Animal
    {
        public Snek(string newName = "Snek") : base(newName) { }
    }

    public class Fort : Location
    {
        public Fort(char nRow, int nCol, string nName, List<Animal> nullAnimals=null)
            : base(nRow, nCol, nName, nullAnimals) {}
    }
    public class Location
    {
        public int row { get; set; }
        public int col { get; set; }
        public bool isIsland { get; set; }
        public char rowChar { get; set; }
        public string fullRowCol { get; set; }
        public string name { get; set; }
        public List<Animal> animals = new List<Animal>();

        public Location(char nRow, int nCol, string nName, List<Animal> nAnimals)
        {
            rowChar = nRow;
            row = (int)(rowChar - 'A');
            col = nCol;
            name = nName;
            fullRowCol = (rowChar.ToString() + col.ToString());
            animals = nAnimals;
        }
        public bool HasAnimal(object animal)
        { //use object type to support anything/everything
            if (animal is Chicken) //use is to check type of object
            {
                foreach (Animal a in animals)
                    if (a is Chicken) //search animals for chicken
                        return true;
                return false;
            }
            else if (animal is Pig)
            {
                foreach (Animal a in animals)
                    if (a is Pig) //search animals for pig
                        return true;
                return false;
            }
            else if (animal is Snek)
            {
                foreach (Animal a in animals)
                    if (a is Snek) //search animals for snek
                        return true;
                return false;
            }
            return false; //unsupported animal
        }
    }
    

}