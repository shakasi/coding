using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Person
    {
        public Person(string name, int age, char gender)
        {
            this.Name = name;
            this.Age = age;
            this.Gender = gender;
        }

        public string Name { get; set; }
        public int Age { get; set; }
        public char Gender { get; set; }
    }
}