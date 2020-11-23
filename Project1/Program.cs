/// HW No.11 Project No.1
/// File Name:     HW1.cs
/// @author:       Jacques Zwielich
/// Date:          Nov. 23, 2020
///
/// Problem Statement: Create a student class, read items into class, then output stats to a different file
///    
/// Overall Plan (step-by-step, how you want the code to make it happen):
/// 1. Create a class called student, with the instance variables first, last, score
/// 2. Create a single constructor with all the parameters 
/// 3. Create getters and setters
/// 4. Create a compareTo that compares two student objects score instance variables
/// 5. Create a method that calculates the average scores from a list of students
/// 6. Create a method that sorts a list of students based on scores and finds the median, regardless of whether it is odd or even
/// ~~~~~~~~~~~~~~~~
/// MAIN
/// ~~~~~~~~~~~~~~~~
/// 1. Open a file with students first, last, and score
/// 2. Individually store each student in student array
/// 3. close the existing and begin writing a new one
/// 4. In the file being written in write the average and median of the array of students
/// 5. close and write values to the new file.

using System;

namespace Project1
{
    class Program
    {
        //Student class that stores the first and last name of students and their scores
        public class Student
        {
            //First and last name variables and score
            protected string firstName;
            protected string lastName;
            protected int score;
            //Constructor for all elements
            public Student(string first, string last, int score)
            {
                this.firstName = first;
                this.lastName = last;
                this.score = score;
            }
            //getters for instance variables
            public string getFirst()
            {
                return this.firstName;
            }
            public string getLast()
            {
                return this.lastName;
            }
            public int getScore()
            {
                return this.score;
            }
            //Setters for instance variables
            public void setFirst(string first)
            {
                this.firstName = first;
            }
            public void setLast(string last)
            {
                this.lastName = last;
            }
            public void setScore(int score)
            {
                this.score = score;
            }
            //Compares the scores of two students
            public int CompareTo(Student otherStudent)
            {
                if (this.score > otherStudent.getScore())
                {
                    return 1;
                }
                else if (this.score < otherStudent.getScore())
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
        }
        //Finds the median student in a list of students
        public static double findMedian(params Student[] students)
        {
            Student[] tempList = students;
            //Sort list
            for (int i = 0; i < students.Length; i++)
            {
                Student temp;
                for (int j = 0; j < tempList.Length; j++)
                {
                    if (1 == tempList[i].CompareTo(tempList[j]))
                    {
                        temp = tempList[i];
                        tempList[i] = tempList[j];
                        tempList[j] = temp;
                    }
                }
            }
            //If it's even list return one thing. Otherwise return a different calculation
            int length = tempList.Length;
            if (students.Length % 2 == 0)
            {
                Student temp = tempList[(length / 2)];
                return temp.getScore();
            }
            else
            {
                Student tempOne = (tempList[(length - 1) / 2]);
                Student tempTwo = (tempList[(length + 1) / 2]);
                return ((tempOne.getScore() + tempTwo.getScore()) / 2);
            }
        }
        //Finds the average from a list of students and returns a double
        public static double findAverage(params Student[] students)
        {
            double average = 0;
            foreach (Student student in students)
            {
                average += student.getScore();
            }
            return average / students.Length;
        }
        static void Main(string[] args)
        {
            Student[] student = new Student[6];
            //Open file
            System.IO.StreamReader scoreFile = new System.IO.StreamReader(@"C:\Users\Patchz\source\repos\HomeWork 11\Project1\scores.txt");
            string line;
            int count = 0;
            //Read each line and populate the list
            while ((line = scoreFile.ReadLine()) != null)
            {
                string first = "";
                string last = "";
                string scoreStr = "";
                int word = 0;
                //Get first, last and score from file for each student
                foreach (char letter in line)
                {

                    if (letter == ' ')
                    {
                        word++;
                    }
                    else if (word == 0)
                    {
                        first += letter;
                    }
                    else if (word == 1)
                    {
                        last += letter;
                    }
                    else
                    {
                        scoreStr += letter;
                    }
                }
                //Store each acquired student into list
                int score = Int32.Parse(scoreStr);
                Student temp = new Student(first, last, score);
                student[count] = temp;
                count++;
            }
            scoreFile.Close();
            //Write Output file
            System.IO.StreamWriter statOut = new System.IO.StreamWriter(@"C:\Users\Patchz\source\repos\HomeWork 11\Project1\StudentStats.txt");
            statOut.WriteLine("Here are the stats for the class:");
            statOut.WriteLine("Median: " + findMedian(student));
            statOut.WriteLine("Average: " + findAverage(student));
            statOut.Close();
        }
    }
}
