
/**
Developer Name: Joseph Austin Phipps
Email: austinphipps@gmail.com
Date: 6/1/2024

Purpose: To create a .NET console application that searches through a directory to fine a CSV file given by the enduser and validates the given email
formats, seperating them into lists of valid and invalid email formats.

Organizations: GDC IT
**/

namespace technical_assignment;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
class Program
{
    static void Main(string[] args)    //Calling the subsequent methods in order to 
    {
        Console.WriteLine("What is your CSV file name?");
        string file = Console.ReadLine();
        string directory = "C:\\Contact_List";    //Assuming that this file is going to be in X directory

        SearchFile(directory, file);
        FileOutput(directory, file);
    }
    static void SearchFile(string directoryPath, string fileNameToSearch)    //Searches through directory in order to locate CSV file.
    {
        try
        {
            // Check if the directory exists
            if (Directory.Exists(directoryPath))
            {
                // Get all files in the directory
                string[] files = Directory.GetFiles(directoryPath);
                // Search for the file
                foreach (string file in files)
                {
                    if (Path.GetFileName(file).Equals(fileNameToSearch, StringComparison.OrdinalIgnoreCase))
                    {
                        return;
                    }
                }
                Console.WriteLine("File not found.");
            }
            else
            {
                Console.WriteLine("Directory does not exist.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
    static void FileOutput(string directoryPath, string fileNameToSearch)    //Parses the contents of the CSV file into the desired format
    {
        string filePath = Path.Combine(directoryPath, fileNameToSearch);
        try
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                // Skip the header line
                string headerLine = sr.ReadLine();
                string line;
                List<string> emails = new List<string>();
                while ((line = sr.ReadLine()) != null)
                {
                    // Split the line into columns
                    string[] columns = line.Split(',');
                    // Assuming the columns are in the order: First Name, Last Name, Email
                    if (columns.Length >= 3)
                    {
                        string firstName = columns[0];
                        string lastName = columns[1];
                        string email = columns[2];
                        // Process each column
                        //Console.WriteLine($"First Name: {firstName}, Last Name: {lastName}, Email: {email}");
                        emails.Add(email);

                        
                    }
        else
                    {
                        Console.WriteLine("Invalid line format");    
                    }
                }
                ValidateEmail(emails);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
    }

    //Checks the format of each user's email to validate that they are in a valid email format and categorizes them into the corresponding list.
    public static void ValidateEmail(List<string> emails){
        Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", RegexOptions.IgnoreCase);    //Compares enduser's email to regex for validation.
        
        List<string> valid = new List<string>();    //Declares the lists for valid and invalid emails to be stored in.
        List<string> invalid = new List<string>();

        foreach (string email in emails){    //Iterates through each email listed in the CSV file.
            bool isValid = emailRegex.IsMatch(email);
        if (isValid == true){    //If the email format is found to be valid, it is then stored in the "valid" list.
            valid.Add($"{email}");
        }

        else{
            invalid.Add($"{email}");    //If the email format is found to be invalid, it is stored in the "invalid" list.
        }
        }
        Console.WriteLine();    //Extra lines added to make the console results more spaced out and easier to read.
        Console.WriteLine();

        Console.WriteLine("Valid emails:");    //Writes out all the contents of the "valid" list.

        foreach (string email in valid){    //Iterates through the "valid" list and prints them out in the console.
        Console.WriteLine(email);
        }

        Console.WriteLine();    //Extra line added to space out console results.

        Console.WriteLine("Invalid emails:");    //Writes out all the contents of the "invalid" list.

        foreach (string email in invalid){
        Console.WriteLine(email);    //Iterates through the "invalid" list and prints them out on the console.
        }

    }

    
    
}