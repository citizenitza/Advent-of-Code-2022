using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_14 {
    public enum Element {
        Air = 0,
        Rock = 1,
        Sand = 2,
    }
    public class GridItem {
        public Element Type;
        public GridItem() {
            Type = new Element();
            Type = Element.Air;
        }
    }
    internal class Solution {
        GridItem[,] Grid;
        int cols = 0;
        int rows = 0;
        int SandSourceCol = 0;
        int SandSourceRow= 0;
        List<int> ValuesX = new List<int>();
        List<int> ValuesY = new List<int>();
        public Solution() {
            string lineOfText;
            string ConfigPath = AppDomain.CurrentDomain.BaseDirectory + "input.txt";
            FileStream filestream = new FileStream(ConfigPath,
                                            System.IO.FileMode.Open,
                                            System.IO.FileAccess.Read,
                                            System.IO.FileShare.ReadWrite);
            var reader = new System.IO.StreamReader(filestream, System.Text.Encoding.UTF8, true, 128);
            string[] lineArray;
            
            int rowIndex = 0;

            while ((lineOfText = reader.ReadLine()) != null) {
                lineArray = lineOfText.Split("->");
                foreach (string coordinate in lineArray) {
                    ValuesY.Add(Convert.ToInt32(coordinate.Trim().Split(',')[0]));
                    ValuesX.Add(Convert.ToInt32(coordinate.Trim().Split(',')[1]));
                }

            }


        }
        

        private void InitGrid(bool _PartOne) {
            int PartTwoCorrection = 0;
            if (_PartOne) {
                rows = (ValuesX.Max()) + 1;
                cols = (ValuesY.Max() - ValuesY.Min()) + 1;
            } else {
                rows = (ValuesX.Max()) + 3; //+2
                PartTwoCorrection = rows * 2;
                cols = (ValuesY.Max() - ValuesY.Min()) + 1 + PartTwoCorrection + 1;
            }
            
            int ColCorrection = ValuesY.Min() - ((PartTwoCorrection / 2));
            Grid = new GridItem[rows, cols];
            SandSourceCol = 500 - ColCorrection;
            SandSourceRow = 0;

            //init
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < cols; j++) {
                    Grid[i, j] = new GridItem();
                }       
            }

            //read
            string lineOfText;
            string ConfigPath = AppDomain.CurrentDomain.BaseDirectory + "input.txt";
            string[] lineArray;
            FileStream filestream2 = new FileStream(ConfigPath,
                    System.IO.FileMode.Open,
                    System.IO.FileAccess.Read,
                    System.IO.FileShare.ReadWrite);
            var reader2 = new System.IO.StreamReader(filestream2, System.Text.Encoding.UTF8, true, 128);

            while ((lineOfText = reader2.ReadLine()) != null) {
                lineArray = lineOfText.Split("->");
                int StartCol = Convert.ToInt32(lineArray[0].Trim().Split(',')[0]) - ColCorrection;
                int StartRow = Convert.ToInt32(lineArray[0].Trim().Split(',')[1]);
                for (int i = 1; i < lineArray.Count(); i++) {
                    int EndCol = Convert.ToInt32(lineArray[i].Trim().Split(',')[0]) - ColCorrection;
                    int EndRow = Convert.ToInt32(lineArray[i].Trim().Split(',')[1]);
                    if (StartCol == EndCol) {
                        if (EndRow > StartRow) {
                            for (int j = StartRow; j <= EndRow; j++) {
                                Grid[j, StartCol].Type = Element.Rock;
                            }
                        } else {
                            for (int j = StartRow; j >= EndRow; j--) {
                                Grid[j, StartCol].Type = Element.Rock;
                            }
                        }
                    } else if (StartRow == EndRow) {
                        if (EndCol > StartCol) {
                            for (int j = StartCol; j <= EndCol; j++) {
                                Grid[StartRow, j].Type = Element.Rock;
                            }
                        } else {
                            for (int j = StartCol; j >= EndCol; j--) {
                                Grid[StartRow, j].Type = Element.Rock;
                            }
                        }
                    } else {
                        //debug
                        ;
                    }
                    StartCol = EndCol;
                    StartRow = EndRow;
                }
            }

            if (!_PartOne) {
                //draw floor
                for (int j = 0; j < cols; j++) {
                    Grid[rows-1, j].Type = Element.Rock;
                }
            }
            //DrawGrid(_PartOne);


        }

        private void DrawGrid(bool _PartOne) {
            for(int i = 0; i < rows; i++) {
                string line = "";
                for (int j = 0; j < cols; j++) {
                    //if(!_PartOne && i == rows - 1) {
                    //    line += "#";
                    //    continue;
                    //}
                    switch (Grid[i, j].Type) {
                        case Element.Rock: {
                                line += "#";
                                break;
                            }
                        case Element.Air: {
                                line += ".";
                                break;
                            }
                        case Element.Sand: {
                                line += "o";
                                break;
                            }
                    }
                }
                Console.WriteLine(line);
            }
        }


        public void Part_One() {
            bool AbyssFall = false;
            int SandCount = 0;
            InitGrid(true);

            while (!AbyssFall) {
                //simulate next Sand unit
                if (!SimulateSand()) {
                    break;
                }
                SandCount++;
                //debug
                //Console.Clear();
                //DrawGrid();
                //Thread.Sleep(50);
            }
            Console.WriteLine("Part one solution: " + SandCount.ToString());
            DrawGrid(true);
        }


        private bool SimulateSand() {
            bool SandStable = false;
            int NextCol = SandSourceCol;
            int NextRow = SandSourceRow;
            while (!SandStable) {
                NextRow += 1;
                if(NextRow == rows) {
                    //abyss
                    break;
                }
                if (Grid[NextRow, NextCol].Type == Element.Air) {
                    //next vertical is clear
                    continue;
                } else {
                    //check left
                    NextCol -= 1;
                    try {
                        if (Grid[NextRow, NextCol].Type == Element.Air) {
                            //diagonal left clear
                            continue;
                        } else {
                            NextCol += 2;
                            if (Grid[NextRow, NextCol].Type == Element.Air) {
                                //diagonal right clear
                                continue;
                            } else {
                                //stable
                                int newSandRow = NextRow - 1;
                                int newSandCol = NextCol - 1;
                                Grid[newSandRow, newSandCol].Type = Element.Sand;
                                return true;
                            }
                        }
                    }catch (Exception ex) when (ex is System.IndexOutOfRangeException) {
                        break;
                    }
                }
            }
                return false;
        }


        public void Part_Two() {
            bool AbyssFall = false;
            int SandCount = 0;
            InitGrid(false);
            while (!AbyssFall) {
                //simulate next Sand unit

                //if (!SimulateSand()) {
                //    break;
                //}
                if (!SimulateSand_PartTwo()) {
                    break;
                }
                if (SandCount < 0) {
                    break;
                }
                SandCount++;
                //debug
                //Console.Clear();
                //DrawGrid();
                //Thread.Sleep(50);
            }
            Console.WriteLine("Part Two solution: " + SandCount.ToString());
            DrawGrid(false);
        }


        private bool SimulateSand_PartTwo() {
            bool SandStable = false;
            int NextCol = SandSourceCol;
            int NextRow = SandSourceRow;
            //check if start full
            if(Grid[SandSourceRow, SandSourceCol].Type == Element.Sand) {
                return false;
            }
            while (!SandStable) {
                NextRow += 1;
                if (NextRow == rows) {
                    //abyss
                    break;
                }
                if (Grid[NextRow, NextCol].Type == Element.Air) {
                    //next vertical is clear
                    continue;
                } else {
                    //check left
                    NextCol -= 1;
                    try {
                        if (Grid[NextRow, NextCol].Type == Element.Air) {
                            //diagonal left clear
                            continue;
                        } else {
                            NextCol += 2;
                            if (Grid[NextRow, NextCol].Type == Element.Air) {
                                //diagonal right clear
                                continue;
                            } else {
                                //stable
                                int newSandRow = NextRow - 1;
                                int newSandCol = NextCol - 1;
                                Grid[newSandRow, newSandCol].Type = Element.Sand;
                                return true;
                            }
                        }
                    } catch (Exception ex) when (ex is System.IndexOutOfRangeException) {
                        break;
                    }
                }
            }
            return false;
        }
    }
}
