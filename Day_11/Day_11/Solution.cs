using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Day_11 {
    public class Monkey {
        public string Serial_number;
        public Queue<ulong> Items = new Queue<ulong>();
        public ulong Test_Divisor;
        public ulong Inspection_counter = 0;
        public string Operation_item_1;
        public string Operation_item_2;
        public string Operator;
        public string Target_true;
        public string Target_false;
        public bool Monkey_Empty() {
            if(Items.Count() == 0) {
                return true;
            } else {
                return false;
            }
        }
        public string Process_Next(ref ulong result, bool PartOne,ulong gModule) {
            Inspection_counter++;
            string retVal = "";//target
            ulong CurrentItem = Items.Dequeue();
            CurrentItem = CalcWorryOperation(CurrentItem);
            if (PartOne) {
                CurrentItem = CurrentItem / 3;
            } else {
                CurrentItem= CurrentItem % gModule;
            }
            result = CurrentItem;
            //result = CurrentItem % Test_Divisor;
            if (CurrentItem % Test_Divisor == 0) {
                retVal = Target_true;
            } else {
                retVal = Target_false;
            }

            return retVal;
        }

         private ulong CalcWorryOperation(ulong _current) {
            ulong retVal = 0;
            if(Operation_item_2 == "old") { // selfoperation
                if(Operator == "*") {
                    retVal = _current * _current;
                } else if(Operator == "+") {
                    retVal = _current + _current;
                }
            } else {
                ulong num = Convert.ToUInt64(Operation_item_2);
                if (Operator == "*") {
                    retVal = _current * num;
                } else if (Operator == "+") {
                    retVal = _current + num;
                }
            }

            return retVal;
        }
        
    }
    internal class Solution {
        List<Monkey> Monkeys;
        public Solution() {
            string lineOfText;
            string ConfigPath = AppDomain.CurrentDomain.BaseDirectory + "input.txt";
            uint tmp = 0;
            FileStream filestream = new FileStream(ConfigPath,
                                            System.IO.FileMode.Open,
                                            System.IO.FileAccess.Read,
                                            System.IO.FileShare.ReadWrite);
            var reader = new System.IO.StreamReader(filestream, System.Text.Encoding.UTF8, true, 128);
            Monkeys = new List<Monkey>();
            int rowIndex = 0;
            while ((lineOfText = reader.ReadLine()) != null) {
                string[] lineArray = lineOfText.Split(' ');
                if (lineArray[0] == "Monkey") {

                    //Monkey 0:
                    Monkey monkey_tmp = new Monkey();
                    monkey_tmp.Serial_number = lineArray[1].Replace(":",String.Empty);

                    //Starting items: 54, 65, 75, 74
                    lineOfText = reader.ReadLine();
                    lineArray = lineOfText.Split(':')[1].Split(',');
                    for(int i = 0; i < lineArray.Count(); i++) {
                        monkey_tmp.Items.Enqueue(Convert.ToUInt64(lineArray[i].Trim()));
                    }

                    //  Operation: new = old + 6
                    lineOfText = reader.ReadLine();
                    lineArray = lineOfText.Split('=')[1].Trim().Split(' ');
                    monkey_tmp.Operation_item_1 = lineArray[0];
                    monkey_tmp.Operator = lineArray[1];
                    monkey_tmp.Operation_item_2 = lineArray[2];

                    //Test: divisible by 19
                    lineOfText = reader.ReadLine();
                    monkey_tmp.Test_Divisor = Convert.ToUInt64(lineOfText.Trim().Split(' ')[3]);

                    //  If true: throw to monkey 2
                    lineOfText = reader.ReadLine();
                    monkey_tmp.Target_true = lineOfText.Trim().Split(' ')[5];

                    //  If false: throw to monkey 0
                    lineOfText = reader.ReadLine();
                    monkey_tmp.Target_false = lineOfText.Trim().Split(' ')[5];
                    //add finished monkey
                    Monkeys.Add(monkey_tmp);
                }
            }
        }

        public void Part_Two() {

            ulong LeastCommonMultiplier = Monkeys.Select(x => x.Test_Divisor).Aggregate((x, y) => x * y);

            for (int i = 0; i < 10000; i++) {
                ProcessRound(false, LeastCommonMultiplier);
                //Console.WriteLine("After round " + (i+1).ToString());
                //for (int j = 0; j < Monkeys.Count(); j++) {
                //    string combinedString = string.Join(",", Monkeys[j].Items.ToArray());
                //    Console.WriteLine("Monkey " + Monkeys[j].Serial_number + ": " + combinedString);
                //}
                if(i == 0) {
                    Console.WriteLine("After round " + (i + 1).ToString());
                    for (int j = 0; j < Monkeys.Count(); j++) {
                        Console.WriteLine("Monkey " + Monkeys[j].Serial_number + " inspected items " + Monkeys[j].Inspection_counter + " times. ");
                    }
                }

                if((i+1) %1000 == 0) {
                    Console.WriteLine("After round " + (i+1).ToString());
                    for (int j = 0; j < Monkeys.Count(); j++) {
                        Console.WriteLine("Monkey " + Monkeys[j].Serial_number + " inspected items " + Monkeys[j].Inspection_counter + " times. ");
                    }
                }
            }


            //

            //calc result
            List<Monkey> ResultTmp = new List<Monkey>();
            ResultTmp = Monkeys.OrderByDescending(x => x.Inspection_counter).ToList();
            ulong result = ResultTmp[0].Inspection_counter * ResultTmp[1].Inspection_counter;
            Console.WriteLine("Monkey business: " + result.ToString());
        }
        public void Part_One() {
            for(int i = 0; i < 20; i++) {
                ProcessRound(true,0);
                Console.WriteLine("After round " + (i + 1).ToString());
                for (int j = 0; j < Monkeys.Count(); j++) {
                    string combinedString = string.Join(",", Monkeys[j].Items.ToArray());
                    Console.WriteLine("Monkey " + Monkeys[j].Serial_number + ": " + combinedString);
                }
            }

            for (int j = 0; j < Monkeys.Count(); j++) {
                Console.WriteLine("Monkey " + Monkeys[j].Serial_number + " inspected items " + Monkeys[j].Inspection_counter + " times. ");
            }
            //

            //calc result
            List<Monkey> ResultTmp = new List<Monkey>();
            ResultTmp = Monkeys.OrderByDescending(x => x.Inspection_counter).ToList();
            ulong result = ResultTmp[0].Inspection_counter * ResultTmp[1].Inspection_counter;
            Console.WriteLine("Monkey business: " + result.ToString());
        }

        private void ProcessRound(bool _Relief, ulong lcm) {

            for (int i = 0; i < Monkeys.Count(); i++) {
                while (!Monkeys[i].Monkey_Empty()) {
                    ulong result = 0;
                    string target = Monkeys[i].Process_Next(ref result, _Relief, lcm);
                    //Console.WriteLine(result.ToString() + "   " + target);
                    PassItemToMonkey(target, result);
                }
            }
        }
        private void PassItemToMonkey(string _target, ulong _newItem) {
            for (int i = 0; i < Monkeys.Count(); i++) {
                if (Monkeys[i].Serial_number == _target) {
                    Monkeys[i].Items.Enqueue(_newItem);
                }
            }
        }
    }
}
