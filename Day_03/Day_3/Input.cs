using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics.Contracts;

namespace Day_3 {
    public class Rucksack {
        public string First_Compartment;
        public string Second_Compartment;
        public string CharInBoth;
        public bool Recurrence;
        public string BothCompartments() {
            return First_Compartment + Second_Compartment;
        }
        public int Priority() {
            int result = 0;
            result = GetPriority(CharInBoth);

            return result;
        }

        private int GetPriority(string _inputChar) {
            int result = 0;
            int asciiValue = Encoding.ASCII.GetBytes(_inputChar)[0];
            if (65 <= asciiValue && asciiValue <= 90) {//A-Z
                result = asciiValue - 38;
            } else if (97 <= asciiValue && asciiValue <= 122) {//a-z
                result = asciiValue - 96;
            } else {
                //not in range
                //debug
                ;
            }
            return result;

        }
    }
    public class Elf_Group {
        public string Badge;
        public int Priority() {
            int result = 0;
            result = GetPriority(Badge);

            return result;
        }

        private int GetPriority(string _inputChar) {
            int result = 0;
            int asciiValue = Encoding.ASCII.GetBytes(_inputChar)[0];
            if (65 <= asciiValue && asciiValue <= 90) {//A-Z
                result = asciiValue - 38;
            } else if (97 <= asciiValue && asciiValue <= 122) {//a-z
                result = asciiValue - 96;
            } else {
                //not in range
                //debug
                ;
            }
            return result;

        }
    }
        internal class Input {
        List<Rucksack> listOfRucksacks;
        List<Elf_Group> listOfGroups;
        public Input() {
            string lineOfText;
            string ConfigPath = AppDomain.CurrentDomain.BaseDirectory + "puzzle_input.txt";
            int lineIndex = 0;
            FileStream filestream = new FileStream(ConfigPath,
                                            System.IO.FileMode.Open,
                                            System.IO.FileAccess.Read,
                                            System.IO.FileShare.ReadWrite);
            var reader = new System.IO.StreamReader(filestream, System.Text.Encoding.UTF8, true, 128);

            listOfRucksacks = new List<Rucksack>();
            string[] lineArray;
            while ((lineOfText = reader.ReadLine()) != null) {
                Rucksack newRuckSack = new Rucksack();
                newRuckSack.First_Compartment = lineOfText.Substring(0, (int)(lineOfText.Length / 2));
                newRuckSack.Second_Compartment = lineOfText.Substring((int)(lineOfText.Length / 2), (int)(lineOfText.Length / 2));
                foreach (char a in newRuckSack.First_Compartment) {
                    if (newRuckSack.Second_Compartment.Any(x => x == a)) {
                        newRuckSack.Recurrence = true;
                        newRuckSack.CharInBoth = a.ToString();
                    }
                }
                listOfRucksacks.Add(newRuckSack);
            }
        }

        public int CalcPrioritySum() {
            int result = 0;
            foreach(Rucksack rS in listOfRucksacks) {
                result += rS.Priority();
            }

            return result;
        }

        public int CalcBadgePriority() {
            listOfGroups = new List<Elf_Group>();
            int result = 0;
            int NumberofGroups = listOfRucksacks.Count() / 3;
            //iterate through groups 
            for(int i = 0; i < NumberofGroups; i++) {
                Elf_Group newGroup = new Elf_Group();
                int iGroup = i * 3;
                foreach (char a in listOfRucksacks[iGroup].BothCompartments()) {
                    if (listOfRucksacks[iGroup+1].BothCompartments().Any(x => x == a)) {
                        if (listOfRucksacks[iGroup + 2].BothCompartments().Any(x => x == a)) {
                            //Badge found
                            newGroup.Badge = a.ToString();
                            listOfGroups.Add(newGroup);
                            break;
                        }
                    }
                }
                    
            }

            foreach (Elf_Group eG in listOfGroups) {
                result += eG.Priority();
            }
            return result;
        }

    }
}
