using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prakt15
{
    public class Student
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Otchestvo { get; set; }
        public DateTime DateofBirth { get; set; }
        public string PhoneNomber { get; set; }
        public string Addres { get; set; }


        public Student(string surname, string name, string otchestvo, DateTime dateOfBirth, string phoneNumber, string addres)
        {
            Surname = surname;
            Name = name;
            Otchestvo = otchestvo;
            DateofBirth = dateOfBirth;
            PhoneNomber = phoneNumber;
            Addres = addres;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4} {5}", Surname, Name, Otchestvo, DateofBirth, PhoneNomber, Addres);
        }
    }
}
