using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_4 {
    public class CleaningRange {
        public int LowerBorder;
        public int UpperBorder;
    }
    public class ElfPair {
        public CleaningRange FirstElf;
        public CleaningRange SecondElf;
    }
    internal class Input {
        List<ElfPair> ListOfPairs;
        public Input() {
            string lineOfText;
            string ConfigPath = AppDomain.CurrentDomain.BaseDirectory + "puzzle_input.txt";
            int lineIndex = 0;
            FileStream filestream = new FileStream(ConfigPath,
                                            System.IO.FileMode.Open,
                                            System.IO.FileAccess.Read,
                                            System.IO.FileShare.ReadWrite);
            var reader = new System.IO.StreamReader(filestream, System.Text.Encoding.UTF8, true, 128);
            ListOfPairs = new List<ElfPair>();

            string[] PairArray;
            string[] RangeArray;
            while ((lineOfText = reader.ReadLine()) != null) {
                ElfPair newElfPair = new ElfPair();
                PairArray = lineOfText.Split(',');
                //first elf
                newElfPair.FirstElf = new CleaningRange();
                RangeArray = PairArray[0].Split('-');
                newElfPair.FirstElf.LowerBorder = Convert.ToInt32(RangeArray[0]);
                newElfPair.FirstElf.UpperBorder = Convert.ToInt32(RangeArray[1]);


                //second elf
                newElfPair.SecondElf = new CleaningRange();
                RangeArray = PairArray[1].Split('-');
                newElfPair.SecondElf.LowerBorder = Convert.ToInt32(RangeArray[0]);
                newElfPair.SecondElf.UpperBorder = Convert.ToInt32(RangeArray[1]);

                ListOfPairs.Add(newElfPair);
            }
        }

        public int CalculateOverlappings() {
            int result = 0;
            foreach(ElfPair pair in ListOfPairs) {
                //first overlap
                if(pair.FirstElf.LowerBorder>= pair.SecondElf.LowerBorder && pair.FirstElf.UpperBorder <= pair.SecondElf.UpperBorder){
                    //overlap true
                    result += 1;
                    continue;
                }
                //second overlap
                if (pair.FirstElf.LowerBorder <= pair.SecondElf.LowerBorder && pair.FirstElf.UpperBorder >= pair.SecondElf.UpperBorder){
                    //overlap true
                    result += 1;
                }

            }


            return result;
        }
        public int CalculatePartialOverlappings() {
            int result = 0;
            foreach (ElfPair pair in ListOfPairs) {
                //full overlaps 
                //first overlap
                if (pair.FirstElf.LowerBorder >= pair.SecondElf.LowerBorder && pair.FirstElf.UpperBorder <= pair.SecondElf.UpperBorder) {
                    //overlap true
                    result += 1;
                    continue;
                }
                //second overlap
                if (pair.FirstElf.LowerBorder <= pair.SecondElf.LowerBorder && pair.FirstElf.UpperBorder >= pair.SecondElf.UpperBorder) {
                    //overlap true
                    result += 1;
                    continue;
                }

                //partial overlaps
                if (pair.SecondElf.UpperBorder >= pair.FirstElf.LowerBorder && pair.SecondElf.UpperBorder <= pair.FirstElf.UpperBorder) {
                    //overlap true
                    result += 1;
                    continue;
                }
                if (pair.SecondElf.LowerBorder >= pair.FirstElf.LowerBorder && pair.SecondElf.LowerBorder <= pair.FirstElf.UpperBorder) {
                    //overlap true
                    result += 1;
                    continue;
                }
            }


            return result;
        }
    }
}
