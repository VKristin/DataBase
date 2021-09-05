using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Vhodnoy_DataBase
{
    class DataBase
    {
        public string A;
        public string B;
        public string C;
        public string D;
        public string E;
        public string F;
        public string G;
    }

    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\User\Desktop\Учёба\3 курс\БД\Входной\DataBase.tsv";
            StreamReader input;
            StreamWriter output;
            string header = "A \t B \t C \t D \t E \t F \t G";

            if (!File.Exists(path))
            {
                File.Create(path); // если не удалось найти файл по указанному пути - создаём новый файл
            }

            input = new StreamReader(path);
            
            if (input.ReadLine() == null) //заполним названия столбцов, если БД пуста
            {
                input.Close();
                output = new StreamWriter(path);
                output.WriteLine(header);
                output.Close();
            }
            input.Close();

            sbyte answer = -1;

            while (answer != 0)
            {
                Console.WriteLine("Что нужно сделать с БД?");
                Console.WriteLine("[1] - добавить запись");
                Console.WriteLine("[2] - удалить запись");
                Console.WriteLine("[3] - обновить запись");
                Console.WriteLine("[4] - считать запись/записи");
                Console.WriteLine("[0] - закончить работу");
                answer = Convert.ToSByte(Console.ReadLine());

                if (answer == 1) //добавление записи в конец
                {
                    input = new StreamReader(path);
                    int acc = 0;
                    string line;

                    while ((line = input.ReadLine()) != null)
                        acc++;
                    input.Close();

                    //output = new StreamWriter(path);
                    output = File.AppendText(path);
                    string[] data = new string[7]; //создадим строковый массив для данных
                    data[0] = Convert.ToString(acc);
                    char simv = 'B';

                    Console.WriteLine("Введите следующую информацию для добавления в БД:");

                    for (int i = 1; i < 7; i++)
                    {
                        Console.Write(simv + ": ");
                        data[i] = Console.ReadLine();
                        simv++;
                    }

                    output.WriteLine(data[0] + "\t" + data[1] + "\t" + data[2] + "\t" + data[3] + "\t" + data[4] + "\t" + data[5] + "\t" + data[6]);
                    output.Close();
                }

                if (answer == 4) //считывание данных из БД
                {
                    input = new StreamReader(path);
                    Console.WriteLine("Считать всю информацию [1] или считать отдельную запись [2]?");
                    sbyte ans1 = Convert.ToSByte(Console.ReadLine());
                    if (ans1 == 1)
                    {
                        input = new StreamReader(path);
                        string line;
                        while ((line = input.ReadLine()) != null)
                            Console.WriteLine(line);
                    }
                    if (ans1 == 2)
                    {
                        Console.WriteLine("Запись под каким номером Вы хотите получить?");
                        int ans2 = Convert.ToInt32(Console.ReadLine());
                        string line;
                        byte acc = 0;

                        while ((line = input.ReadLine()) != null)
                        {
                            string[] subs = line.Split('\t');
                            int help;
                            if (Int32.TryParse(subs[0], out help) && Convert.ToInt32(subs[0]) == ans2)
                            {
                                Console.WriteLine("Ваша запись:");
                                Console.WriteLine(header);
                                Console.WriteLine(line);
                                acc++;
                            }
                        }
                        if (acc == 0)
                            Console.WriteLine("Запись с таким номером не найдена");

                    }
                    input.Close();
                }
            }
        }
    }
}
