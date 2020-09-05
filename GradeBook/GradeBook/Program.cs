using System;
using System.Collections.Generic;

namespace GradeBook
{
    class Program
    {
        
        static void Main(string[] args)

        {
            IBook book = new DiskBook ("Prashant's Grade book");
            book.GradeAdded += OnGradeAdded;
            EnterGrades(book);
            var result = book.GetStats();

            Console.WriteLine($"For the book named {book.Name}");
            Console.WriteLine($"Lowest Grade is {result.LowestGrade}");
            Console.WriteLine($"Highest Grade is {result.HighestGrade}");
            Console.WriteLine($"Average of Grades is {result.Average:N1}");
            Console.WriteLine($"The Letter Grade is {result.LetterGrade}");


        }

        private static void EnterGrades(IBook  book)
        {
            
            while (true)
            {
                Console.WriteLine("Please enter a grade or q to quit: ");
                var input = Console.ReadLine();
                if (input == "q" || input == "Q")
                {
                    break;
                }
                try
                {
                    var grade = double.Parse(input);
                    book.AddGrade(grade);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Invalid input!!");
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Invalid input!!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Invalid input!!");
                }
                finally
                {
                    //do something that always needs to be done
                }
            }
        }

        static void OnGradeAdded(object sender, EventArgs e)
        {
            Console.WriteLine("A new grade is added the class");
        }
    }
}
