using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Game
{
    class Person
    {
        public static Person Paul;

        public static PersonList All = new PersonList();

        public string name { get; private set; }
        public string description = "";
        public Inventory items;

        public Person(string name)
        {
            this.name = name;
            this.items = new Inventory(name + "'s Inventory", Inventory.MAX);
            All.Add(this);
        }

        public delegate void del(Game g);
        public del Talk;
    }

    class PersonList
    {
        private List<Person> people;

        public PersonList() => people = new List<Person>();

        public int Count() => people.Count;

        public List<Person> All() => new List<Person>(people);

        public void Add(Person person) => people.Add(person);

        public bool Remove(Person person) => people.Remove(person);

        public bool Remove(string person)
        {
            person = person.Trim().ToLower();
            foreach (Person p in people)
                if (p.name.ToLower() == person)
                    return people.Remove(p);
            return false;
        }

        public Person Find(Person person)
        {
            foreach (Person p in people)
                if (p == person)
                    return p;
            return null;
        }

        public Person Find(string person)
        {
            person = person.Trim().ToLower();
            foreach (Person p in people)
                if (p.name.ToLower() == person)
                    return p;
            return null;
        }

        public bool Contains(Person person)
        {
            foreach (Person p in people)
                if (p == person)
                    return true;
            return false;
        }

        public bool Contains(string person)
        {
            person = person.Trim().ToLower();
            foreach (Person p in people)
                if (p.name.ToLower() == person)
                    return true;
            return false;
        }
    }
}
