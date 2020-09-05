using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GradeBook
{

    public class NamedObjects
    {
        public NamedObjects(string name)
        {
            Name = name;
        }

        public string Name
        {
            get;
            set;
        }
    }

    public interface IBook
    {
        void AddGrade(double grade);
        Statistics GetStats();
        string Name { get; }
        event GradeAddedDelegate GradeAdded;


    }

    public abstract class Book :NamedObjects, IBook
    {
        public Book(string name) : base(name)
        {
        }

        public abstract event GradeAddedDelegate GradeAdded;

        public abstract void AddGrade(double grade);
        public abstract Statistics GetStats();
    }

    public delegate void GradeAddedDelegate(object sender, EventArgs args);
    public class InMemoryBook : Book
    {
        public InMemoryBook (string name):base(name)
        {
            Grades = new List<double>();
            this.Name = name;
        }
        public override void AddGrade(double grade)
        {
            if (grade <= 100 && grade >= 0)
            {
                Grades.Add(grade);
                if (GradeAdded != null)
                    GradeAdded(this, new EventArgs());
            }
            else
            {
                throw new ArgumentException($"Invalid Grade{nameof(grade)}");
                //Console.WriteLine("Please enter a valid grade!");
            }
        }

        public void AddGrade(char letter)
        {
            switch (letter)
            {
                case 'A':
                    AddGrade(90);
                    break;
                case 'B':
                    AddGrade(80);
                    break;
                case 'C':
                    AddGrade(70);
                    break;
                case 'D':
                    AddGrade(60);
                    break;
                default:
                    AddGrade(0);
                    break;
            }


        }

        public void ShowStats()
        {
            var highestGrade = double.MinValue;
            var lowestGrade = double.MaxValue;

            var result = 0.0;

            foreach (double number in Grades)
            {
                if (number > highestGrade)
                    highestGrade = number;
                if (number < lowestGrade)
                    lowestGrade = number;
                result += number;
            }
            Console.WriteLine($"Lowest Grade is {lowestGrade}");
            Console.WriteLine($"Highest Grade is {highestGrade}");
            Console.WriteLine($"Sum of Grades is {result}");
            Console.WriteLine($"Average of Grades is {(result / Grades.Count):N1}");

        }


        public override Statistics GetStats()
        {
            var result = new Statistics();
            
            

            foreach (double grade in Grades)
            {
                result.Add(grade);
                
            }
            
            

            return result;
        }

        List<double> Grades;

        

        public override event GradeAddedDelegate GradeAdded;
        
    }
    public class DiskBook : Book
    {
        public DiskBook(string name) : base(name)
        {
        }

        public override event GradeAddedDelegate GradeAdded;

        public override void AddGrade(double grade)
        {
            using (var writer = File.AppendText($"{Name}.txt"))
            {
                writer.WriteLine(grade);
                if (GradeAdded != null)
                    GradeAdded(this, new EventArgs());
            }
        }

        public override Statistics GetStats()
        {
            var result = new Statistics();
            using (var reader = File.OpenText($"{Name}.txt"))
            {
                var line = reader.ReadLine();
                while (line!=null)
                {
                    var grade = double.Parse(line);
                    result.Add(grade);
                    line = reader.ReadLine();
                }
            }
            return result;
        }
    }
}
