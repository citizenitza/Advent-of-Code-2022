using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day_1_1 {
    public class Elf {
        public int number;
        public List<int> CalorieItems = new List<int>();
        public int CalorieSum() {
            int result = 0;
            result = CalorieItems.Sum();
            return result;
        }
    }


    internal class Input {
        public List<Elf> Elf_list = new List<Elf>();
        public Input() {
            string lineOfText;
            string ConfigPath = AppDomain.CurrentDomain.BaseDirectory + "input.txt";
            int lineIndex = 0;
            FileStream filestream = new FileStream(ConfigPath,
                                          System.IO.FileMode.Open,
                                          System.IO.FileAccess.Read,
                                          System.IO.FileShare.ReadWrite);
            var reader = new System.IO.StreamReader(filestream, System.Text.Encoding.UTF8, true, 128);
            Elf_list = new List<Elf>();
            List<int> tmpCalorie = new List<int>();
            int ElfIndex = 0;
            while ((lineOfText = reader.ReadLine()) != null) {
                if (lineOfText == "") {
                    // elf ready
                    Elf Elf_tmp = new Elf();
                    Elf_tmp.number = ElfIndex++;
                    Elf_tmp.CalorieItems = tmpCalorie;
                    Elf_list.Add(Elf_tmp);
                    tmpCalorie = new List<int>();
                } else {
                    tmpCalorie.Add(Convert.ToInt32(lineOfText));
                }
            }


        }

        public int MaxCalories() {
            int result = 0;
            foreach(Elf elf in Elf_list) {
                if (elf.CalorieSum() > result) {
                    result = elf.CalorieSum();
                }
            }

            return result;
        }
        public int MaxCaloriesTop3() {
            int result = 0;
            List<int> CalorieSum_List = new List<int>();
            foreach (Elf elf in Elf_list) {
                int tmp = elf.CalorieSum();
                CalorieSum_List.Add(tmp);
            }
            CalorieSum_List = CalorieSum_List.OrderByDescending(y => y).ToList();
            result = CalorieSum_List[0] + CalorieSum_List[1] + CalorieSum_List[2];
            return result;
        }
    }

}
