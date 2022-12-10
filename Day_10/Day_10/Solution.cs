using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_10 {
    public class Instruction {
        public bool NopType;
        public int Cycle;
        public int AddValue;
    }
    internal class Solution {
        List<Instruction> instr_list;
        public int X_Register = 1;
        public string[,] CRT_monitor = new string[6, 40];
        public Solution() {
            string lineOfText;
            string ConfigPath = AppDomain.CurrentDomain.BaseDirectory + "input.txt";
            uint tmp = 0;
            FileStream filestream = new FileStream(ConfigPath,
                                            System.IO.FileMode.Open,
                                            System.IO.FileAccess.Read,
                                            System.IO.FileShare.ReadWrite);
            var reader = new System.IO.StreamReader(filestream, System.Text.Encoding.UTF8, true, 128);
            instr_list = new List<Instruction>();
            int rowIndex = 0;
            while ((lineOfText = reader.ReadLine()) != null) {
                if(lineOfText == "noop") {
                    Instruction new_Instr = new Instruction();
                    new_Instr.NopType = true;
                    new_Instr.Cycle = 1;
                    instr_list.Add(new_Instr); 
                } else if (lineOfText.Split(' ')[0] == "addx") {
                    Instruction new_Instr = new Instruction();
                    new_Instr.NopType = false;
                    new_Instr.Cycle = 2;
                    new_Instr.AddValue = Convert.ToInt32(lineOfText.Split(' ')[1]);
                    instr_list.Add(new_Instr);
                } else {
                    //debug
                    ;
                }

            } 
        }

        public void Part_One() {
            Execute_program();
        }
        public void Part_Two() {
            string Line = "";
            for(int row = 0; row < 6; row++) {
                Line = "";
                for (int col = 0; col < 40; col++) {
                    Line += CRT_monitor[row, col];
                }
                Console.WriteLine(Line);
            }
        
        }
        private void Execute_program() {
            X_Register = 1;
            int CycleCounter = 1;
            int Result = 0;
            foreach(Instruction instr in instr_list) {
                if (instr.NopType) {
                    Result += IncrementCycleCounter(ref CycleCounter, instr);
                } else {
                    //first cycle:
                    Result += IncrementCycleCounter(ref CycleCounter, instr);
                    //second cylce:
                    Result += IncrementCycleCounter(ref CycleCounter, instr);
                    X_Register += instr.AddValue;
                }
            }
            Console.WriteLine("Part one solution: " + Result.ToString());
        }

        private int IncrementCycleCounter(ref int _cycleCnt, Instruction _debug ) {
            int retVal = 0;

            int CurrentSignalStr = _cycleCnt * X_Register;
            if(_cycleCnt == 20) {
                Console.WriteLine("Current cycle: " + _cycleCnt.ToString() + " X register value: " + X_Register.ToString() + " Signal strength: " + CurrentSignalStr.ToString());
                retVal = CurrentSignalStr;
            } else if ((_cycleCnt-20)  %40 == 0) {
                Console.WriteLine("Current cycle: " + _cycleCnt.ToString() + " X register value: " + X_Register.ToString() + " Signal strength: " + CurrentSignalStr.ToString());
                retVal = CurrentSignalStr;
            }

            //Calc Crt Pixel
            DrawPixel(_cycleCnt, _debug);
            _cycleCnt += 1;
            return retVal;
        }


        private void DrawPixel(int _currentCycle, Instruction _debug) {
            string result = "";
            int PixelRow = 0;
            int PixelCol = _currentCycle % 40;
            if (PixelCol == 0) {
                PixelCol = 40;
            }
            PixelRow = (_currentCycle - 1) / 40;
            PixelCol -= 1;
            if (PixelCol == (X_Register - 1)) {
                result = "#";
            } else if (PixelCol == (X_Register)) {
                result = "#";
            } else if (PixelCol == (X_Register + 1)) {
                result = "#";
            } else {
                result = ".";
            }


           
            CRT_monitor[PixelRow, PixelCol] = result;
        }
    }
}
