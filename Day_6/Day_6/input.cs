using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_6 {
    public class FixedSizedQueue<T> {
        public ConcurrentQueue<T> q = new ConcurrentQueue<T>();
        private object lockObject = new object();

        public int Limit { get; set; }
        public void Enqueue(T obj) {
            q.Enqueue(obj);
            lock (lockObject) {
                T overflow;
                while (q.Count > Limit && q.TryDequeue(out overflow)) ;
            }
        }
    }
    internal class input {
        public string InputString;
        public input() {
            string lineOfText;
            string ConfigPath = AppDomain.CurrentDomain.BaseDirectory + "input.txt";
            int lineIndex = 0;
            FileStream filestream = new FileStream(ConfigPath,
                                            System.IO.FileMode.Open,
                                            System.IO.FileAccess.Read,
                                            System.IO.FileShare.ReadWrite);
            var reader = new System.IO.StreamReader(filestream, System.Text.Encoding.UTF8, true, 128);
            string[] lineArray;
            while ((lineOfText = reader.ReadLine()) != null) {
                InputString = lineOfText;
                ;
            }
        }
        public int PartOne() {
            int result = FindSOF(InputString);
            return result;
        }
        public int PartTwo() {
            int result = FindSOF_part2(InputString);
            return result;
        }
        private int FindSOF(string input) {
            int result = 0;
            FixedSizedQueue<char> Buffer =new FixedSizedQueue<char>();
            Buffer.Limit = 4;
            int index = 0;
            foreach(char ch in input) {
                Buffer.Enqueue(ch);
                index++;
                //check
                if (Buffer.q.Select(x => x).Distinct().ToList().Count() == 4) {
                    //4 distinct char
                    break;
                }
                
            }


            return index;
        }

        private int FindSOF_part2(string input) {
            int result = 0;
            FixedSizedQueue<char> Buffer = new FixedSizedQueue<char>();
            Buffer.Limit = 14;
            int index = 0;
            foreach (char ch in input) {
                Buffer.Enqueue(ch);
                index++;
                //check
                if (Buffer.q.Select(x => x).Distinct().ToList().Count() == 14) {
                    //4 distinct char
                    break;
                }

            }


            return index;
        }

    }
}
