using System;
using System.Collections;

namespace prakt15
{
    public class StudentGroup
    {
        public string GroupName { get; set; }
        public ArrayList Students { get; set; }

        public StudentGroup(string groupName)
        {
            GroupName = groupName;
            Students = new ArrayList();
        }

        public void AddStudent(Student student)
        {
            Students.Add(student);
        }

        public void RemoveStudent(Student student)
        {
            Students.Remove(student);
        }

        public void SortBySurname()
        {
            Students.Sort(new StudentSurnameComparer());
        }

        public void SortByDateofBirth()
        {
            Students.Sort(new StudentDateofBirthComparer());
        }

        public ArrayList SearchBySurname(string surname)
        {
            ArrayList result = new ArrayList();
            foreach (Student student in Students)
            {
                if (student.Surname == surname)
                {
                    result.Add(student);
                }
            }
            return result;
        }

        public ArrayList SearchByDateofBirth(DateTime dateofbirth)
        {
            ArrayList result = new ArrayList();
            foreach (Student student in Students)
            {
                if (student.DateofBirth == dateofbirth)
                {
                    result.Add(student);
                }
            }
            return result;
        }

        public ArrayList SearchByNomer(string nomer)
        {
            ArrayList result = new ArrayList();
            foreach (Student student in Students)
            {
                if (student.PhoneNomber == nomer)
                {
                    result.Add(student);
                }
            }
            return result;
        }
    }

    public class StudentSurnameComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            Student s1 = (Student)x;
            Student s2 = (Student)y;
            return string.Compare(s1.Surname, s2.Surname);
        }
    }

    public class StudentDateofBirthComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            Student s1 = (Student)x;
            Student s2 = (Student)y;
            return DateTime.Compare(s1.DateofBirth, s2.DateofBirth);
        }
    }
}
