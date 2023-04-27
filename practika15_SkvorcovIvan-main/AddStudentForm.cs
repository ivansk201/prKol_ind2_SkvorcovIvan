using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace prakt15
{
    public partial class AddStudentForm : Form
    {
        public Student Student { get; set; }
        public AddStudentForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!IsRussianName(textBox1.Text))
            {
                MessageBox.Show("Фамилия должна быть на русском языке и начинаться с заглавной буквы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!IsRussianName(textBox2.Text))
            {
                MessageBox.Show("Имя должно быть на русском языке и начинаться с заглавной буквы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!IsRussianName(textBox3.Text))
            {
                MessageBox.Show("Отчество должно быть на русском языке и начинаться с заглавной буквы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!textBox4.Text.StartsWith("+7") || textBox4.Text.Length != 12 || !textBox4.Text.Substring(1).All(char.IsDigit))
            {
                MessageBox.Show("Номер телефона должен начинаться с '+7' и содержать 11 цифр.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var student = new Student(
                textBox1.Text,
                textBox2.Text,
                textBox3.Text,
                dateTimePicker1.Value,
                textBox4.Text,
                textBox5.Text
            );
            Student = student;
            DialogResult = DialogResult.OK;
            File.AppendAllText("test.txt", student.Surname + ";" + student.Name + ";" + student.Otchestvo + ";" + student.DateofBirth.ToString() + ";" + student.PhoneNomber + ";" + student.Addres + Environment.NewLine);
        }

        private bool IsRussianName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            if (!char.IsUpper(name[0]))
                return false;

            foreach (char c in name)
            {
                if (!char.IsLetter(c) || !char.IsUpper(c) && !char.IsLower(c) && c != '-' || !IsRussianLetter(c))
                    return false;
            }
            return true;
        }

        private bool IsRussianLetter(char c)
        {
            string russianLetters = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
            return russianLetters.Contains(c);
        }


        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}

