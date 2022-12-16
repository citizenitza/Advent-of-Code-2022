using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_9 {
    public enum Direction {
        Up = 0,
        Down = 1,
        Left = 2,
        Right = 3,
    }
    public class Point {
        public int X;
        public int Y;
    }
    public class Rope {
        public List<Point> RopeKnots = new List<Point>();
        public void MoveHead(int _newX, int _newY, Instruction debug) {
            //set new head
            RopeKnots[0].X = _newX;
            RopeKnots[0].Y = _newY;
            for (int i = 1; i < RopeKnots.Count; i++) {
                int diffX = RopeKnots[i - 1].X - RopeKnots[i].X;
                int diffY = RopeKnots[i - 1].Y - RopeKnots[i].Y;
                int absX = Math.Abs(diffX);
                int absY = Math.Abs(diffY);

                if (absX == 0 && absY == 0) {
                    //over no move
                } else if (absX == 0 && absY == 2) {
                    //vertical move
                    RopeKnots[i].Y += diffY/2;
                } else if (absX == 2 && absY == 0) {
                    //horizontal move
                    RopeKnots[i].X += diffX/2;
                } else if(absX != 0 && absY != 0) {
                    if (absX == 1 && absY == 1) {
                        //diagonal toucinh no move
                    }else if(absX == 2 && absY == 1) {
                        RopeKnots[i].X += (diffX / 2);
                        RopeKnots[i].Y += diffY;
                    } else if (absY == 2 && absX == 1) {
                        RopeKnots[i].X += diffX;
                        RopeKnots[i].Y += (diffY / 2);
                    } else if (absY == 2 && absX == 2) {
                        RopeKnots[i].X += (diffX / 2);
                        RopeKnots[i].Y += (diffY / 2);
                    } else {
                        //debug
                        Console.WriteLine("Debug");
                    }
                }
            }
        }
        public Point TailPos() {
            Point retVal = new Point();
            retVal.X = RopeKnots.Last().X;
            retVal.Y = RopeKnots.Last().Y;
            return retVal;
        }
    }
    public class Instruction {
        public Direction Direction;
        public int Count;
    }
    internal class Solution {
        List<Instruction> Instructions = new List<Instruction>();
        Rope Rope;
        public Solution() {
            string lineOfText;
            string ConfigPath = AppDomain.CurrentDomain.BaseDirectory + "input.txt";
            uint tmp = 0;
            FileStream filestream = new FileStream(ConfigPath,
                                            System.IO.FileMode.Open,
                                            System.IO.FileAccess.Read,
                                            System.IO.FileShare.ReadWrite);
            var reader = new System.IO.StreamReader(filestream, System.Text.Encoding.UTF8, true, 128);
            
            int rowIndex = 0;
            string[] lineArray;
            while ((lineOfText = reader.ReadLine()) != null) {
                lineArray = lineOfText.Split(' ');
                Instruction tmpInst = new Instruction();
                switch (lineArray[0]) {
                    case "U": {
                            tmpInst.Direction = Direction.Up;
                            break;
                        }
                    case "D": {
                            tmpInst.Direction = Direction.Down;
                            break;
                        }
                    case "L": {
                            tmpInst.Direction = Direction.Left;
                            break;
                        }
                    case "R": {
                            tmpInst.Direction = Direction.Right;
                            break;
                        }
                }
                tmpInst.Count = Convert.ToInt32(lineArray[1]);
                Instructions.Add(tmpInst);
            }
        }

        public void Part_One() {
            Rope Rope_One = new Rope();
            Point Head = new Point();
            Rope_One.RopeKnots.Add(Head);
            Point Tail = new Point();
            Rope_One.RopeKnots.Add(Tail);
            List<Point> TailTrace = new List<Point>();
            foreach (Instruction inst in Instructions) {
                for (int i = 0; i < inst.Count; i++) {
                    int NewX = Rope_One.RopeKnots[0].X;
                    int NewY = Rope_One.RopeKnots[0].Y;
                    switch (inst.Direction) {
                        case Direction.Up: {
                                NewY += 1;
                                break;
                            }
                        case Direction.Down: {
                                NewY -= 1;
                                break;
                            }
                        case Direction.Left: {
                                NewX -= 1;
                                break;
                            }
                        case Direction.Right: {
                                NewX += 1;
                                break;
                            }
                    }
                    Rope_One.MoveHead(NewX, NewY,inst);
                    TailTrace.Add(Rope_One.TailPos());
                }
            }


            List<Point> UniqueTail = TailTrace.GroupBy(m => new { m.X, m.Y }).Select(group => group.First()).ToList();
            Console.WriteLine("Part One solution: " + UniqueTail.Count());
        }

        public void Part_Two() {
            Rope Rope_One = new Rope();
            for(int knot = 0; knot < 10; knot++) {
                Point Tail = new Point();
                Rope_One.RopeKnots.Add(Tail);
            }
            List<Point> TailTrace = new List<Point>();
            foreach (Instruction inst in Instructions) {
                for (int i = 0; i < inst.Count; i++) {
                    int NewX = Rope_One.RopeKnots[0].X;
                    int NewY = Rope_One.RopeKnots[0].Y;
                    switch (inst.Direction) {
                        case Direction.Up: {
                                NewY += 1;
                                break;
                            }
                        case Direction.Down: {
                                NewY -= 1;
                                break;
                            }
                        case Direction.Left: {
                                NewX -= 1;
                                break;
                            }
                        case Direction.Right: {
                                NewX += 1;
                                break;
                            }
                    }
                    Rope_One.MoveHead(NewX, NewY,inst);
                    TailTrace.Add(Rope_One.TailPos());
                }
            }


            List<Point> UniqueTail = TailTrace.GroupBy(m => new { m.X, m.Y }).Select(group => group.First()).ToList();
            Console.WriteLine("Part Two solution: " + UniqueTail.Count());
        }

    }
}
