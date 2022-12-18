using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Day_5 {
    public class Crate {
        public string Name;
    }
    public class Move {
        public int Cnt;
        public int From;
        public int To;
    }
    internal class Input {
        List<Crate>[] Stacks = new List<Crate>[9];
        List<Move> ListOfMoves;
        public Input() {
            InitStack();

            string lineOfText;
            string ConfigPath = AppDomain.CurrentDomain.BaseDirectory + "input_moves.txt";
            int lineIndex = 0;
            FileStream filestream = new FileStream(ConfigPath,
                                            System.IO.FileMode.Open,
                                            System.IO.FileAccess.Read,
                                            System.IO.FileShare.ReadWrite);
            var reader = new System.IO.StreamReader(filestream, System.Text.Encoding.UTF8, true, 128);
            ListOfMoves = new List<Move>();
            string[] lineArray; 
            while ((lineOfText = reader.ReadLine()) != null) {
                Move newMove = new Move();
                lineArray = lineOfText.Split(' ');
                newMove.Cnt   = Convert.ToInt32(lineArray[1]);
                newMove.From    = Convert.ToInt32(lineArray[3])-1;
                newMove.To      = Convert.ToInt32(lineArray[5])-1;
                ListOfMoves.Add(newMove);
            }
            //Part 1
            //  ArrangeStack();
            //Part 2
            ArrangeStacks_9001();
        }

        public string TopOfStacks() {
            string result = "";
            for (int i = 0; i < Stacks.Length; i++) {
                result += Stacks[i].Last().Name; 
            }


            return result;
        }
        private void ArrangeStack() {
            foreach(Move mv in ListOfMoves) {
                for (int i = 0; i < mv.Cnt; i++) {
                    //get element from stack
                    Crate tmp = Stacks[mv.From].Last();
                    //remove last
                    Stacks[mv.From].RemoveAt(Stacks[mv.From].Count() - 1);
                    //add to new
                    Stacks[mv.To].Add(tmp);
                }
            }
        }
        private void ArrangeStacks_9001() {
            foreach (Move mv in ListOfMoves) {
                //get last n element from stack
                List<Crate> tmp = Stacks[mv.From].Skip(Math.Max(0, Stacks[mv.From].Count() - mv.Cnt)).ToList();
                ;
                //remove last n
                for (int i = 0; i < mv.Cnt; i++) { 
                    Stacks[mv.From].RemoveAt(Stacks[mv.From].Count() - 1);
                }
                    //add to new
                    Stacks[mv.To].AddRange(tmp);
            }
        }
        private void InitStack() {
            for (int i = 0; i < Stacks.Length; i++) {
                Stacks[i] = new List<Crate>();
            }

            //no ragrets


            Crate tmp = new Crate();
            //stack 1
            tmp.Name = "N";
            Stacks[0].Add(tmp);
            tmp = new Crate();
            tmp.Name = "B";
            Stacks[0].Add(tmp);
            tmp = new Crate();
            tmp.Name = "D";
            Stacks[0].Add(tmp);
            tmp = new Crate();
            tmp.Name = "T";
            Stacks[0].Add(tmp);
            tmp = new Crate();
            tmp.Name = "V";
            Stacks[0].Add(tmp);
            tmp = new Crate();
            tmp.Name = "G";
            Stacks[0].Add(tmp);
            tmp = new Crate();
            tmp.Name = "Z";
            Stacks[0].Add(tmp);
            tmp = new Crate();
            tmp.Name = "J";
            Stacks[0].Add(tmp);

            //stack 2
            tmp = new Crate();
            tmp.Name = "S";
            Stacks[1].Add(tmp);
            tmp = new Crate();
            tmp.Name = "R";
            Stacks[1].Add(tmp);
            tmp = new Crate();
            tmp.Name = "M";
            Stacks[1].Add(tmp);
            tmp = new Crate();
            tmp.Name = "D";
            Stacks[1].Add(tmp);
            tmp = new Crate();
            tmp.Name = "W";
            Stacks[1].Add(tmp);
            tmp = new Crate();
            tmp.Name = "P";
            Stacks[1].Add(tmp);
            tmp = new Crate();
            tmp.Name = "F";
            Stacks[1].Add(tmp);

            //stack 3
            tmp = new Crate();
            tmp.Name = "V";
            Stacks[2].Add(tmp);
            tmp = new Crate();
            tmp.Name = "C";
            Stacks[2].Add(tmp); 
            tmp = new Crate();
            tmp.Name = "R";
            Stacks[2].Add(tmp); 
            tmp = new Crate();
            tmp.Name = "S";
            Stacks[2].Add(tmp); 
            tmp = new Crate();
            tmp.Name = "Z";
            Stacks[2].Add(tmp);

            //stack 4
            tmp = new Crate();
            tmp.Name = "R";
            Stacks[3].Add(tmp);
            tmp = new Crate();
            tmp.Name = "T";
            Stacks[3].Add(tmp);
            tmp = new Crate();
            tmp.Name = "J";
            Stacks[3].Add(tmp);
            tmp = new Crate();
            tmp.Name = "Z";
            Stacks[3].Add(tmp);
            tmp = new Crate();
            tmp.Name = "P";
            Stacks[3].Add(tmp);
            tmp = new Crate();
            tmp.Name = "H";
            Stacks[3].Add(tmp);
            tmp = new Crate();
            tmp.Name = "G";
            Stacks[3].Add(tmp);

            //stack 5
            tmp = new Crate();
            tmp.Name = "T";
            Stacks[4].Add(tmp);
            tmp = new Crate();
            tmp.Name = "C";
            Stacks[4].Add(tmp);
            tmp = new Crate();
            tmp.Name = "J";
            Stacks[4].Add(tmp);
            tmp = new Crate();
            tmp.Name = "N";
            Stacks[4].Add(tmp);
            tmp = new Crate();
            tmp.Name = "D";
            Stacks[4].Add(tmp);
            tmp = new Crate();
            tmp.Name = "Z";
            Stacks[4].Add(tmp);
            tmp = new Crate();
            tmp.Name = "Q";
            Stacks[4].Add(tmp);
            tmp = new Crate();
            tmp.Name = "F";
            Stacks[4].Add(tmp);


            //stack 6
            tmp = new Crate();
            tmp.Name = "N";
            Stacks[5].Add(tmp);
            tmp = new Crate();
            tmp.Name = "V";
            Stacks[5].Add(tmp);
            tmp = new Crate();
            tmp.Name = "P";
            Stacks[5].Add(tmp);
            tmp = new Crate();
            tmp.Name = "W";
            Stacks[5].Add(tmp);
            tmp = new Crate();
            tmp.Name = "G";
            Stacks[5].Add(tmp);
            tmp = new Crate();
            tmp.Name = "S";
            Stacks[5].Add(tmp);
            tmp = new Crate();
            tmp.Name = "F";
            Stacks[5].Add(tmp);
            tmp = new Crate();
            tmp.Name = "M";
            Stacks[5].Add(tmp);

            //stack 7
            tmp = new Crate();
            tmp.Name = "G";
            Stacks[6].Add(tmp);
            tmp = new Crate();
            tmp.Name = "C";
            Stacks[6].Add(tmp);
            tmp = new Crate();
            tmp.Name = "V";
            Stacks[6].Add(tmp);
            tmp = new Crate();
            tmp.Name = "B";
            Stacks[6].Add(tmp);
            tmp = new Crate();
            tmp.Name = "P";
            Stacks[6].Add(tmp);
            tmp = new Crate();
            tmp.Name = "Q";
            Stacks[6].Add(tmp);

            //stack 8
            tmp = new Crate();
            tmp.Name = "Z";
            Stacks[7].Add(tmp);
            tmp = new Crate();
            tmp.Name = "B";
            Stacks[7].Add(tmp);
            tmp = new Crate();
            tmp.Name = "P";
            Stacks[7].Add(tmp);
            tmp = new Crate();
            tmp.Name = "N";
            Stacks[7].Add(tmp);


            //stack 9
            tmp = new Crate();
            tmp.Name = "W";
            Stacks[8].Add(tmp);
            tmp = new Crate();
            tmp.Name = "P";
            Stacks[8].Add(tmp);
            tmp = new Crate();
            tmp.Name = "J";
            Stacks[8].Add(tmp);
        }
    }
}
