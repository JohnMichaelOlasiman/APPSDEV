﻿using System;
using System.IO;

public class ComplexObject
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Value { get; set; }

    private readonly string sourcePath = @"Z:\II128\OLASIMAN, John Michael\3_Semifinal\Activity5\Activity5\bin\Debug\net8.0\complexObject.bin";
    private readonly string destinationPath = @"Z:\II128\OLASIMAN, John Michael\3_Semifinal\Activity5\complexObject.bin";

    private void TransferFile()
    {
        try
        {
            if (File.Exists(sourcePath))
            {
                File.Move(sourcePath, destinationPath, true);
                Console.WriteLine("File transferred successfully.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error transferring file: {ex.Message}");
        }
    }

    public void HandleBinaryFile(string action)
    {
        TransferFile();
        string filePath = destinationPath;

        switch (action.ToLower())
        {
            case "write":
                try
                {
                    Console.Write("Enter Id (integer): ");
                    int id = int.Parse(Console.ReadLine());
                    Console.Write("Enter Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter Value (decimal): ");
                    double value = double.Parse(Console.ReadLine());

                    using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Append)))
                    {
                        writer.Write(id);
                        writer.Write(name);
                        writer.Write(value);
                    }
                    Console.WriteLine("Object appended to complexObject.bin");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input! Please enter correct data.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                break;

            case "read":
                try
                {
                    if (File.Exists(filePath))
                    {
                        using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
                        {
                            while (reader.BaseStream.Position != reader.BaseStream.Length)
                            {
                                int readId = reader.ReadInt32();
                                string readName = reader.ReadString();
                                double readValue = reader.ReadDouble();
                                Console.WriteLine($"Id: {readId}, Name: {readName}, Value: {readValue}");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("File not found. Please write to the file first.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while reading file: {ex.Message}");
                }
                break;

            case "exit":
                Console.WriteLine("Exiting program...");
                break;

            default:
                Console.WriteLine("Invalid action. Use 'write', 'read', or 'exit'.");
                break;
        }
    }
}

class Program
{
    static void Main()
    {
        ComplexObject obj = new ComplexObject();
        string action = string.Empty;

        while (action.ToLower() != "exit")
        {
            try
            {
                Console.Write("Enter action (write/read/exit): ");
                action = Console.ReadLine();
                obj.HandleBinaryFile(action);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }
    }
}
