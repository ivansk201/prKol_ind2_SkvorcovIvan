using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace prakt15
{
    public partial class Form1 : Form
    {
        private readonly StudentGroup _studentGroup;
        private Timer _timer;

        public Form1()
        {
            
            InitializeComponent();
            _studentGroup = new StudentGroup("Группа 1");
            _timer = new Timer();
            _timer.Interval = 500;
            _timer.Tick += Timer_Tick;
            _timer.Start();
            button10.Visible = false;
            button9.Visible = false;

            button6.Visible = false;
            label1.Visible = false;
            textBox1.Visible = false;

            button5.Visible = false;
            label2.Visible = false;
            dateTimePicker1.Visible = false;

            button4.Visible = false;
            label3.Visible = false;
            textBox2.Visible = false;

            radioButton1.Visible = false;
            radioButton2.Visible= false;
            radioButton3.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            LoadDataFromFile(); 
            dataGridView1.DataSource = _studentGroup.Students;
            _timer.Start(); 
        }

        private void LoadDataFromFile()
        {
            try
            {
                using (StreamReader reader = new StreamReader("test.txt"))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] studentData = line.Split(';');
                        if (studentData.Length == 6)
                        {
                            string surname = studentData[0];
                            string name = studentData[1];
                            string otchestvo = studentData[2];
                            DateTime dateOfBirth = DateTime.Parse(studentData[3]);
                            string phoneNumber = studentData[4];
                            string adress = studentData[5];
                            Student student = new Student(surname, name, otchestvo, dateOfBirth, phoneNumber, adress);
                            _studentGroup.AddStudent(student);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при чтении файла: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = _studentGroup.Students;
            _timer.Stop(); 
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var form = new AddStudentForm();
            form.FormClosed += Form_FormClosed; 
            var result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                _studentGroup.AddStudent(form.Student);
                SaveDataToFile();
            }
        }

        private void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            _timer.Start();
        }

        private void SaveDataToFile()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("test.txt"))
                {
                    foreach (var student in _studentGroup.Students)
                    {
                        string line = $"{student.Surname};{student.Name};{student.Otchestvo};{student.DateofBirth};{student.PhoneNomber};{student.Addres}";
                        writer.WriteLine(line);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при записи файла: " + ex.Message, "Ошибка", MessageBoxButtons.OK);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            radioButton1.Visible = true;
            radioButton2.Visible = true;
            radioButton3.Visible = true;
            MessageBox.Show("Выберите какой вы поиск хотите сделать", "Поиск");
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                DataGridViewRow selectedRow = dataGridView1.CurrentRow;
                Student student = (Student)selectedRow.DataBoundItem;
                _studentGroup.RemoveStudent(student);
                SaveDataToFile();
                timer1.Interval = 1000;
                timer1.Start();
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            timer1.Stop();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = _studentGroup.Students;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string surname = textBox1.Text.Trim();
            List<Student> result = _studentGroup.SearchBySurname(surname);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = result;

            if (result.Count == 0)
            {
                MessageBox.Show("Нет результатов для указанной фамилии.", "Результат поиска", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DateTime dateOfBirth = dateTimePicker1.Value;
            List<Student> result = _studentGroup.SearchByDateofBirth(dateOfBirth);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = result;

            if (result.Count == 0)
            {
                MessageBox.Show("Нет результатов для указанной даты рождения.", "Результат поиска", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string nomer = textBox2.Text.Trim();
            List<Student> result = _studentGroup.SearchByNomer(nomer);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = result;

            if (result.Count == 0)
            {
                MessageBox.Show("Нет результатов для указанного номера студента.", "Результат поиска", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            button6.Visible = true;
            label1.Visible=true;
            textBox1.Visible = true;

            button5.Visible = false;
            label2.Visible = false;
            dateTimePicker1.Visible = false;

            button4.Visible = false;
            label3.Visible = false;
            textBox2.Visible = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            button6.Visible = false;
            label1.Visible = false;
            textBox1.Visible = false;

            button5.Visible = true;
            label2.Visible = true;
            dateTimePicker1.Visible = true;

            button4.Visible = false;
            label3.Visible = false;
            textBox2.Visible = false;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            button6.Visible = false;
            label1.Visible = false;
            textBox1.Visible = false;

            button5.Visible = false;
            label2.Visible = false;
            dateTimePicker1.Visible = false;

            button4.Visible = true;
            label3.Visible = true;
            textBox2.Visible = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
            button6.Visible = false;
            label1.Visible = false;
            textBox1.Visible = false;
            button5.Visible = false;
            label2.Visible = false;
            dateTimePicker1.Visible = false;
            button4.Visible = false;
            label3.Visible = false;
            textBox2.Visible = false;
            radioButton1.Visible = false;
            radioButton2.Visible = false;
            radioButton3.Visible = false;
            radioButton1.Visible = false;
            radioButton2.Visible = false;
            radioButton3.Visible = false;
            button10.Visible = false;
            button9.Visible = false;
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = _studentGroup.Students;
            _timer.Start();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            button10.Visible = true;
            button9.Visible = true;
            MessageBox.Show("Выберите какая сортировка вам нужна", "Сортировка");

        }

        private void button10_Click(object sender, EventArgs e)
        {
           _studentGroup.SortBySurname(); 
            dataGridView1.DataSource = null; 
            dataGridView1.DataSource = _studentGroup.Students; 
        }

        private void button9_Click(object sender, EventArgs e)
        {
            _studentGroup.SortByDateofBirth();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = _studentGroup.Students;
        }
    }
}
