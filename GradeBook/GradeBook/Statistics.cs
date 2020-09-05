using System;
using System.Collections.Generic;
using System.Text;

namespace GradeBook
{
    public class Statistics
    {
        public Statistics()
        {
            HighestGrade = double.MinValue;
            LowestGrade = double.MaxValue;
            sum = 0.0;
            count = 0;
        }

        public void Add(double number)
        {
            count++;
            sum += number;
            LowestGrade = Math.Min(LowestGrade, number);
            HighestGrade = Math.Max(HighestGrade, number);
        }

        public int count { get; set; }
        public double sum { get; set; }
        public double HighestGrade{ get; set; }
        public double LowestGrade { get; set; }
        public double Average
        {
            get
            {
                return (sum / count);
            }
        }
        public Char LetterGrade
        {
            get
            {
                switch (Average)
                {
                    case var d when d >= 90.0:
                        return 'A';


                    case var d when d >= 80.0:
                        return 'B';


                    case var d when d >= 70:
                        return 'C';


                    case var d when d >= 60:
                        return 'D';


                    default:
                        return 'F';

                }
            }
        }
    }
}
