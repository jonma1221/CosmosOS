using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using Sys = Cosmos.System;

namespace CosmosKernel1
{
    public class Kernel : Sys.Kernel
    {
        List<string> lValue = new List<string>();
        List<int> rValue = new List<int>();
        //List<double> rValue = new List<double>();
        List<File> files = new List<File>();
        
        public static int baseFileID = 0;
        protected override void BeforeRun()
        {
            Console.WriteLine("Cosmos booted successfully. Type a line of text to get it echoed back.");
        }

        protected override void Run()
        {
            Console.Write("Input: ");
            File newFile = new File("hi.txt");
            var input = Console.ReadLine();
            parseCommand(input);
        }
        private static double parseDouble(string s)
        {
            char[] number = s.ToCharArray(0,s.Length);
            bool isNegative;
            double accumulator = 0.0;
            int i = 0;

            // skip lead-in whitespace
            while (i < number.Length && number[i] == ' ')
            {
                ++i;
            }

            // parse the sign
            if (i >= number.Length) throw new FormatException();
            switch (number[i])
            {
                case '+': isNegative = false; ++i; break;
                case '-': isNegative = true; ++i; break;
                default: isNegative = false; break;
            }

            // parse the integer portion
            if (i >= number.Length) throw new FormatException();
            bool hasIntegerDigits = false;
            while (i < number.Length)
            {
                hasIntegerDigits = true;
                accumulator *= 10.0;

                switch (number[i])
                {
                    case '0': accumulator += 0; break;
                    case '1': accumulator += 1; break;
                    case '2': accumulator += 2; break;
                    case '3': accumulator += 3; break;
                    case '4': accumulator += 4; break;
                    case '5': accumulator += 5; break;
                    case '6': accumulator += 6; break;
                    case '7': accumulator += 7; break;
                    case '8': accumulator += 8; break;
                    case '9': accumulator += 9; break;
                }
                ++i;
            }
            if (!hasIntegerDigits) throw new FormatException();

            // parse the fraction, if there is one
            if (i < number.Length && number[i] == '.')
            { // got a decimal point
                ++i; //gobble the decimal point
                double factor = 1.0;
                while (i < number.Length )
                {
                    factor *= 10.0;

                    switch (number[i])
                    {
                        case '0': accumulator += 0 / factor; break;
                        case '1': accumulator += 1 / factor; break;
                        case '2': accumulator += 2 / factor; break;
                        case '3': accumulator += 3 / factor; break;
                        case '4': accumulator += 4 / factor; break;
                        case '5': accumulator += 5 / factor; break;
                        case '6': accumulator += 6 / factor; break;
                        case '7': accumulator += 7 / factor; break;
                        case '8': accumulator += 8 / factor; break;
                        case '9': accumulator += 9 / factor; break;
                    }
                    
                    ++i;
                }
            }

            // set the sign
            if (isNegative) accumulator = -accumulator;

            return accumulator;
        }

        private void parseCommand(string input)
        {
            string[] input1 = input.Split(' ');
            String com = input1[0];
            int command = com.ToCharArray().Length;

            if (command == 3)
            {
                if (input.Substring(0, 3) == "dir")
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        Console.WriteLine(files[i].getFileName() + files[i].getExtension());
                    }
                }
                else if (input.Substring(0, 3) == "SET")
                {
                    setVar(input);
                }
                else if (input.Substring(0, 3) == "ADD")
                {
                    char op = '+';
                    operation(input, op);
                }
                else if (input.Substring(0, 3) == "SUB")
                {
                    char op = '-';
                    operation(input, op);
                }
                else if (input.Substring(0, 3) == "MUL")
                {
                    char op = '*';
                    operation(input, op);
                }
                else if (input.Substring(0, 3) == "DIV")
                {
                    char op = '/';
                    operation(input, op);
                }
                else if (input.Substring(0, 3) == "RUN")
                {
                    run(input);
                }
                else Console.WriteLine("invalid command");
            }

            if (command == 4)
            {
                if (input.Substring(0, 4) == "echo")
                {
                    echo(input);
                }
                else Console.WriteLine("invalid command");
            }

            if (command == 5)
            {
                if (input.Substring(0, 5) == "print")
                {
                    Console.WriteLine("Variables: ");
                    for (int i = 0; i < lValue.Count; i++)
                    {
                        Console.WriteLine(lValue[i] + " = " + rValue[i]);
                    }
                    Console.WriteLine();
                    Console.WriteLine("Files: ");
                    for (int i = 0; i < files.Count; i++)
                    {
                        Console.WriteLine();
                        Console.WriteLine("File: " + files[i].getFileName() + files[i].getExtension());
                        Console.WriteLine("FileID: " + files[i].getFileId());
                        Console.WriteLine("FileSize: " + files[i].getFileSize() + " bytes");
                        Console.WriteLine("FileData: ");
                        List<string> fileText = files[i].getFileData();
                        for (int j = 0; j < fileText.Count; j++)
                        {
                            Console.WriteLine(fileText[j]);
                        }
                        Console.WriteLine();
                    }
                }
                else Console.WriteLine("invalid command");
            }

            if (command == 6)
            {
                if (input.Substring(0, 6) == "create")
                {
                    create(input);
                }
                else if (input.Substring(0, 6) == "RUNALL")
                {
                    runAll(input1);
                }
                else Console.WriteLine("invalid command");
            }
        }

        private void echo(string input)
        {
            bool printed = false;
            for (int i = 0; i < lValue.Count; i++)
            {
                if (input.Substring(5) == lValue[i])
                {
                    Console.WriteLine(rValue[i]);
                    printed = true;
                }
            }
            if (!printed)
            {
                Console.WriteLine(input.Substring(5));
            }
        }

        private void create(string input)
        {
            int index = input.IndexOf('.');
            string fileName = input.Substring(7, index - 7);
            string extension = input.Substring(index);
            File newFile = new File(fileName);
            List<string> fileContent = new List<string>();
            string text = Console.ReadLine();
            while (text != "save")
            {
                fileContent.Add(text);
                text = Console.ReadLine();
            }
            newFile.setFileData(fileContent);
            newFile.setExtension(extension);
            files.Add(newFile);
            for (int i = 0; i < fileContent.Count; i++)
            {
                // Console.WriteLine(fileContent[i]);
            }
        }

        private void setVar(string input)
        {
            string[] words = input.Split(' ');
            bool set = false;
            for (int i = 0; i < lValue.Count; i++)
            {
                if (lValue[i] == words[1])
                {
                    for (int j = 0; j < lValue.Count; j++)
                    {
                        if (lValue[j] == words[2])
                        {
                            rValue[i] = rValue[j];
                            set = true;
                        }
                    }
                    if (!set)
                    {
                        rValue[i] = Int32.Parse(words[2]);
                        //rValue[i] = parseDouble(words[2]);
                        set = true;
                    }
                }
            }

            if (!set)
            {
                lValue.Add(words[1]);
                rValue.Add(Int32.Parse(words[2]));
                //rValue.Add(parseDouble(words[2]));
            }
        }

        private void operation(string input, char op)
        {
            string[] words = input.Split(' ');
            int temp1 = int.MaxValue, temp2 = int.MaxValue;
            //double temp1 = int.MaxValue, temp2 = int.MaxValue;
            for (int i = 0; i < lValue.Count; i++)
            {
                if (words[1] == lValue[i])
                {
                    temp1 = rValue[i];
                }
                else if (words[2] == lValue[i])
                {
                    temp2 = rValue[i];
                }
            }

            if (temp1 == int.MaxValue)
            {
                temp1 = Int32.Parse(words[1]);
                //temp1 = parseDouble(words[1]);
            }
            if (temp2 == int.MaxValue)
            {
                temp2 = Int32.Parse(words[2]);
                //temp2 = parseDouble(words[2]);
            }

            bool set = false;
            for (int i = 0; i < lValue.Count; i++)
            {
                if (words[3] == lValue[i])
                {
                    switch (op)
                    {
                        case '+':
                            rValue[i] = temp1 + temp2;
                            set = true;
                            break;
                        case '-':
                            rValue[i] = temp1 - temp2;
                            set = true;
                            break;
                        case '*':
                            rValue[i] = temp1 * temp2;
                            set = true;
                            break;
                        case '/':
                            rValue[i] = temp1 / temp2;
                            set = true;
                            break;
                        default:
                            break;
                    }
                }
            }
            if (!set)
            {
                switch (op)
                {
                    case '+':
                        lValue.Add(words[3]);
                        rValue.Add(temp1 + temp2);
                        set = true;
                        break;
                    case '-':
                        lValue.Add(words[3]);
                        rValue.Add(temp1 - temp2);
                        set = true;
                        break;
                    case '*':
                        lValue.Add(words[3]);
                        rValue.Add(temp1 * temp2);
                        set = true;
                        break;
                    case '/':
                        lValue.Add(words[3]);
                        rValue.Add(temp1 / temp2);
                        set = true;
                        break;
                    default:
                        break;
                }
            }
        }

        private void run(string input)
        {
            int index = input.IndexOf('.');
            string fileName = input.Substring(4, index - 4);
            string extension = input.Substring(index);
            for (int i = 0; i < files.Count; i++)
            {
                if (files[i].getFileName() == fileName && files[i].getExtension() == (extension))
                {
                    Console.WriteLine("Running .Bat file");
                    List<string> batFile = files[i].getFileData();
                    for (int j = 0; j < batFile.Count; j++)
                    {
                        Console.WriteLine("Executing: " + batFile[j]);
                        parseCommand(batFile[j]);
                    }
                }
            }
        }

        private void runAll(string[] input)
        {
            List<String> filesToRun = new List<String>(); // Batch Job Queue
            int lineInBatchToRun = 0;
            int maxLines = 0;//max lines in a batch file
            for (int i = 1; i < input.Length; i++)
            {
                filesToRun.Add(input[i]);
                Console.WriteLine(input[i]);
            }

            for (int i = 0; i < filesToRun.Count; i++) //gets the max number of lines a batch file has
            {
                string batchFile = filesToRun[i];
                int index = batchFile.IndexOf('.');
                string fileName = batchFile.Substring(0, index);
                string extension = batchFile.Substring(index);
                for (int j = 0; j < files.Count; j++)
                {
                    if (files[j].getFileName() == fileName && files[j].getExtension() == (extension))
                    {
                        List<string> batFile = files[j].getFileData();
                        if (maxLines < batFile.Count)
                        {
                            maxLines = batFile.Count;
                        }
                    }
                }
            }


            for (int i = 0; i < maxLines; i++)//Executes All commands in the batch files kinda in parallel 
            {
                for (int j = 0; j < filesToRun.Count; j++)
                {
                    runLineInBatch(lineInBatchToRun, filesToRun[j]);
                }
                lineInBatchToRun++;
            }
        }

        public void runLineInBatch(int line, string batchFile)
        {
            int index = batchFile.IndexOf('.');
            string fileName = batchFile.Substring(0, index);
            string extension = batchFile.Substring(index);
            for (int i = 0; i < files.Count; i++)
            {
                if (files[i].getFileName() == fileName && files[i].getExtension() == (extension))
                {
                    Console.WriteLine("Running File: " + files[i].getFileName());
                    List<string> batFile = files[i].getFileData();
                    if (line < batFile.Count)
                    {
                        Console.WriteLine("Executing: " + batFile[line]);
                        parseCommand(batFile[line]);
                    }
                }
            }
        }


    }
    //public class Kernel : Sys.Kernel
    //{
    //    List<string> lValue = new List<string>();
    //    List<int> rValue = new List<int>();
    //    List<File> files = new List<File>();
    //    public static int baseFileID = 0;

    //    protected override void BeforeRun()
    //    {
    //        Console.WriteLine("Cosmos booted successfully. Type a line of text to get it echoed back.");
    //    }

    //    protected override void Run()
    //    {
    //        Console.Write("Input: ");
    //        var input = Console.ReadLine();

    //        parseCommand(input);

    //    }

    //    private void parseCommand(string input)
    //    {
    //        if (input.Substring(0, 4) == "echo")
    //        {
    //            echo(input);
    //        }
    //        else if (input.Substring(0, 6) == "create")
    //        {
    //            create(input);
    //        }
    //        else if (input.Substring(0, 3) == "dir")
    //        {
    //            for (int i = 0; i < files.Count; i++)
    //            {
    //                Console.WriteLine(files[i].getFileId() + files[i].getExtension());
    //            }
    //        }
    //        else if (input.Substring(0, 3) == "SET")
    //        {
    //            setVar(input);

    //        }
    //        else if (input.Substring(0, 3) == "ADD")
    //        {
    //            char op = '+';
    //            operation(input, op);
    //        }
    //        else if (input.Substring(0, 3) == "SUB")
    //        {
    //            char op = '-';
    //            operation(input, op);
    //        }
    //        else if (input.Substring(0, 3) == "MUL")
    //        {
    //            char op = '*';
    //            operation(input, op);
    //        }
    //        else if (input.Substring(0, 3) == "DIV")
    //        {
    //            char op = '/';
    //            operation(input, op);
    //        }
    //        else if (input.Substring(0, 3) == "RUN")
    //        {
    //            int index = input.IndexOf('.');
    //            string fileName = input.Substring(4, index - 4);
    //            string extension = input.Substring(index);
    //            for (int i = 0; i < files.Count; i++)
    //            {
    //                if (files[i].getFileId() == fileName && files[i].getExtension() == extension)
    //                {
    //                    List<string> batFile = files[i].getFileData();
    //                    for (int j = 0; j < batFile.Count; j++)
    //                    {
    //                        parseCommand(batFile[j]);
    //                    }
    //                }
    //            }
    //        }
    //        else if (input.Substring(0, 5) == "print")
    //        {
    //            for (int i = 0; i < lValue.Count; i++)
    //            {
    //                Console.WriteLine(lValue[i] + " = " + rValue[i]);
    //            }

    //            //for (int i = 0; i < fileContent.Count; i++)
    //            //{

    //            //}
    //        }
    //        else Console.WriteLine("invalid command");
    //    }

    //    private void echo(string input)
    //    {
    //        bool printed = false;
    //        for (int i = 0; i < lValue.Count; i++)
    //        {
    //            if (input.Substring(5) == lValue[i])
    //            {
    //                Console.WriteLine(rValue[i]);
    //                printed = true;
    //            }
    //        }
    //        if (!printed)
    //        {
    //            Console.WriteLine(input.Substring(5));
    //        }
    //    }

    //    private void create(string input)
    //    {
    //        int index = input.IndexOf('.');
    //        string fileName = input.Substring(7, index - 7);
    //        string extension = input.Substring(index);
    //        Console.WriteLine("File name: " + fileName);
    //        File newFile = new File(fileName);
    //        List<string> fileContent = new List<string>();
    //        string text = Console.ReadLine();
    //        while (text != "save")
    //        {
    //            fileContent.Add(text);
    //            text = Console.ReadLine();
    //        }
    //        newFile.setFileData(fileContent);
    //        newFile.setExtension(extension);
    //        files.Add(newFile);
    //        for (int i = 0; i < fileContent.Count; i++)
    //        {
    //            Console.WriteLine(fileContent[i]);
    //        }
    //    }

    //    private void setVar(string input)
    //    {
    //        string[] words = input.Split(' ');
    //        bool set = false;
    //        for (int i = 0; i < lValue.Count; i++)
    //        {
    //            if (lValue[i] == words[1])
    //            {
    //                for (int j = 0; j < lValue.Count; j++)
    //                {
    //                    if (lValue[j] == words[2])
    //                    {
    //                        rValue[i] = rValue[j];
    //                        set = true;
    //                    }
    //                }
    //                if (!set)
    //                {
    //                    rValue[i] = Int32.Parse(words[2]);
    //                    set = true;
    //                }
    //            }
    //        }

    //        if (!set)
    //        {
    //            lValue.Add(words[1]);
    //            rValue.Add(Int32.Parse(words[2]));

    //        }
    //    }

    //    private void operation(string input, char op)
    //    {
    //        string[] words = input.Split(' ');
    //        int temp1 = int.MaxValue, temp2 = int.MaxValue;
    //        for (int i = 0; i < lValue.Count; i++)
    //        {
    //            if (words[1] == lValue[i])
    //            {
    //                temp1 = rValue[i];
    //            }
    //            else if (words[2] == lValue[i])
    //            {
    //                temp2 = rValue[i];
    //            }
    //        }

    //        if (temp1 == int.MaxValue)
    //        {
    //            temp1 = Int32.Parse(words[1]);
    //        }
    //        if (temp2 == int.MaxValue)
    //        {
    //            temp2 = Int32.Parse(words[2]);
    //        }

    //        bool set = false;
    //        for (int i = 0; i < lValue.Count; i++)
    //        {
    //            if (words[3] == lValue[i])
    //            {
    //                switch (op)
    //                {
    //                    case '+':
    //                        rValue[i] = temp1 + temp2;
    //                        set = true;
    //                        break;
    //                    case '-':
    //                        rValue[i] = temp1 - temp2;
    //                        set = true;
    //                        break;
    //                    case '*':
    //                        rValue[i] = temp1 * temp2;
    //                        set = true;
    //                        break;
    //                    case '/':
    //                        rValue[i] = temp1 / temp2;
    //                        set = true;
    //                        break;
    //                    default:
    //                        break;
    //                }
    //            }
    //        }
    //        if (!set) { 
    //            switch (op)
    //            {
    //                case '+':
    //                    lValue.Add(words[3]);
    //                    rValue.Add(temp1 + temp2);
    //                    set = true;
    //                    break;
    //                case '-':
    //                    lValue.Add(words[3]);
    //                    rValue.Add(temp1 - temp2);
    //                    set = true;
    //                    break;
    //                case '*':
    //                    lValue.Add(words[3]);
    //                    rValue.Add(temp1 * temp2);
    //                    set = true;
    //                    break;
    //                case '/':
    //                    lValue.Add(words[3]);
    //                    rValue.Add(temp1 / temp2);
    //                    set = true;
    //                    break;
    //                default:
    //                    break;
    //            }
    //        }
    //    }
    //}
}
