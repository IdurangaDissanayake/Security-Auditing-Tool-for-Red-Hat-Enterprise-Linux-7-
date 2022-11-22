// See https://aka.ms/new-console-template for more information
//ICT/20836
//Dissanayake D.M.I.H

using System;
using System.Text;

public class Methods
{
    public static void LogFile(string action = "INFO", string message = "")
    {
        string txtFile = @"C:\Users\idura\Desktop\Log.txt";
 

        StreamWriter log;
        if (!File.Exists(txtFile))
        {
           log = new StreamWriter(txtFile);
        }
        else
        {
            log = File.AppendText(txtFile);
        }

        log.Write(DateTime.Now);
        log.WriteLine("\t[" + action + "]\t:\t" + message);
        log.Close();
    }
}

class Student
{
    public string name;
    public int total, rank = 0;

    public int[] marks = new int[4];
    public void addData()
    {
        Methods.LogFile("PROCESS", "Students.addData() method loaded successfully");

        Console.Write("Enter Student Name: ");
        name = Console.ReadLine();
        Methods.LogFile("INPUT", "User Entered Name: " + name);

        Console.Write("Enter Maths Marks: ");
        marks[0] = Convert.ToInt32(Console.ReadLine());
        Methods.LogFile("INPUT", "User Entered Maths Marks: " + marks[0].ToString());

        Console.Write("Enter English Marks: ");
        marks[1] = Convert.ToInt32(Console.ReadLine());
        Methods.LogFile("INPUT", "User Entered English Marks: " + marks[1].ToString());

        Console.Write("Enter Science Marks: ");
        marks[2] = Convert.ToInt32(Console.ReadLine());
        Methods.LogFile("INPUT", "User Entered Science Marks: " + marks[2].ToString());

        Console.Write("Enter IT Marks: ");
        marks[3] = Convert.ToInt32(Console.ReadLine());
        Methods.LogFile("INPUT", "User Entered IT Marks: " + marks[3].ToString());

        Console.Write("\n");
    }

    public void calculateTotal()
    {
        Methods.LogFile("PROCESS", "Students.calculateTotal() method loaded successfully");

        total = marks[0] + marks[1] + marks[2] + marks[3];
        Methods.LogFile("PROCESS", "Total marks calculated successfully");
    }

    public string getBestSubject()
    {
        Methods.LogFile("PROCESS", "Students.getBestSubject() method loaded successfully");

        int maxValue = marks.Max();
        int maxIndex = marks.ToList().IndexOf(maxValue);

        if      (maxIndex == 0) return "Maths";
        else if (maxIndex == 1) return "English";
        else if (maxIndex == 2) return "Science";
        else if (maxIndex == 3) return "IT";
        else return "";
    }
}

public class ConsoleApplication
{
    public static void Main(string[] args)
    {
        Methods.LogFile("INFO", "Program started");

        Console.WriteLine("---------------- Student Ranking System ----------------");
        

        string csvFile = @"C:\Users\Tharindu\Documents\C app\Output.csv";

        Console.Write("How many students are there in the class? ");
        int studentCount = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("");

        Methods.LogFile("INPUT", "User Entered Student Count: " + studentCount.ToString());

        Student[] students = new Student[studentCount];
        Methods.LogFile("PROCESS", "Created the 'student' array");

        int[] studentTotalMarks = new int[studentCount];
        Methods.LogFile("PROCESS", "Created the 'studentTotalMarks' array");

        for (int i = 0; i < studentCount; i++)
        {
            students[i] = new Student();
            students[i].addData();
            students[i].calculateTotal();
            studentTotalMarks[i] = students[i].total;
            Methods.LogFile("PROCESS", "New object created for student '" + students[i] + "' in 'students' array");
        }

        Array.Sort(studentTotalMarks);
        Methods.LogFile("PROCESS", "'studentTotalMarks' array sorted successfully");

        for (int i = studentCount - 1; i >= 0; i--)
        {
            for (int j = 0; j < studentCount; j++)
            {
                if (studentTotalMarks[i] == students[j].total)
                {
                    students[j].rank = studentCount - i;
                }
            }
        }
        Methods.LogFile("PROCESS", "Ranks calculated successfully");

        Console.WriteLine("---------------- Data Set ----------------");
        for (int k = 0; k < studentCount; k++)
        {
            Console.WriteLine("{0}\t: {1}, {2}, {3}, {4}",
                students[k].name,
                students[k].marks[0],
                students[k].marks[1],
                students[k].marks[2],
                students[k].marks[3]);
        }

        Console.WriteLine("\n---------------- Ranking ---------------- ");
        for (int l = 0; l < studentCount; l++)
        {
            Console.WriteLine(students[l].name + "\t: " + students[l].rank);
        }

        string separator = ",";
        StringBuilder output = new StringBuilder();
        string[] headings = { "Student Name", "Rank", "Best Subject" };
        output.AppendLine(string.Join(separator, headings));

        foreach (Student student in students)
        {
            string[] newLine = {
                student.name,
                student.rank.ToString(),
                student.getBestSubject().ToString() };
            output.AppendLine(string.Join(separator, newLine));
        }

        try
        {
            File.AppendAllText(csvFile, output.ToString());
        }
        catch (Exception ex)
        {
            Methods.LogFile("Data could not be written to the CSV file");
            return;
        }

        Console.WriteLine("\n\nThe data has been successfully saved in the CSV file");

        Methods.LogFile("EXPORT", "Data has been successfully saved in the CSV file");
    }
}
