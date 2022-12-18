using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_8 {
    class Tree {
        public int Height;
        public bool Visible;
    }
    internal class Input {
        Tree[,] Grid;
        int rows = 0;
        int cols = 0;

        public Input() {
            string lineOfText;
            string ConfigPath = AppDomain.CurrentDomain.BaseDirectory + "input.txt";
            uint tmp = 0;
            FileStream filestream = new FileStream(ConfigPath,
                                            System.IO.FileMode.Open,
                                            System.IO.FileAccess.Read,
                                            System.IO.FileShare.ReadWrite);
            var reader = new System.IO.StreamReader(filestream, System.Text.Encoding.UTF8, true, 128);
            lineOfText = reader.ReadLine();
            var rowcount = System.IO.File.ReadAllLines(ConfigPath).Length;
            rows = rowcount;
            cols = lineOfText.ToCharArray().Count();
            Grid = new Tree[cols, rows];
            ;
            filestream.Position = 0;
            reader.DiscardBufferedData();
            char[] lineArray;
            int rowIndex = 0;
            while ((lineOfText = reader.ReadLine()) != null) {
            //   lineArray  = lineOfText.ToCharArray();
                int colIndex = 0;
                foreach (char c in lineOfText) {
                    //new tree
                    Grid[rowIndex, colIndex] = new Tree();
                    Grid[rowIndex, colIndex].Height = Convert.ToInt32(c.ToString());
                    Grid[rowIndex, colIndex].Visible = false;
                    colIndex++;
                }

                rowIndex++;
            } 
        }
        public void PartOne() {
            VisibilityCheckHorizontal(true);
            VisibilityCheckHorizontal(false);
            VisibilityCheckVertical(true);
            VisibilityCheckVertical(false);
            VisibilityCount();
        }
        List<Tree> VisibleTrees = new List<Tree>();
        private void VisibilityCount() {
            
            int result = 0;
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < cols; j++) {
                    if (Grid[i, j].Visible) {
                        result++;
                        VisibleTrees.Add(Grid[i, j]);
                    }

                }
            }
            ;
            Console.WriteLine("Part One: " + result.ToString());
        }
        public void VisibilityCheckHorizontal(bool Direction) {//false - to down, true to up,

            if (Direction) {
                Console.WriteLine("Horizontal up");
                int[] HighestInRow = new int[cols];
                for (int i = rows-1; i >=0; i--) {
                    for (int j = 0; j < cols; j++) {
                        if (i == 0 || j == 0 || i == (rows - 1) || j == (cols - 1)) {
                            HighestInRow[j] = Grid[i, j].Height;
                            if(!Grid[i, j].Visible) {
                                //edge tree
                                Grid[i, j].Visible = true;
                                Console.WriteLine("debug visible row: " + i.ToString()+  " col: " + j.ToString());
                            }


                        } else {
                            if (Grid[i, j].Height > HighestInRow[j]) {
                                HighestInRow[j] = Grid[i, j].Height;
                                if (!Grid[i, j].Visible) {
                                    Grid[i, j].Visible = true;
                                      Console.WriteLine("debug visible row: " + i.ToString() + " col: " + j.ToString());
                                }
                            }

                        }

                    }
                }

            } else {
                Console.WriteLine("Horizontal down");
                int[] HighestInRow = new int[cols];
                for (int i = 0; i < rows; i++) {
                    for (int j = 0; j < cols; j++) {
                        if (i == 0 || j == 0 || i == (rows - 1) || j == (cols - 1)) {
                            //edge tree
                            HighestInRow[j] = Grid[i, j].Height;
                            if (!Grid[i, j].Visible) {
                                Grid[i, j].Visible = true;
                                Console.WriteLine("debug visible row: " + i.ToString() + " col: " + j.ToString());
                            }
                        } else {
                            if (Grid[i, j].Height > HighestInRow[j]) {
                                HighestInRow[j] = Grid[i, j].Height;
                                if (!Grid[i, j].Visible) {
                                    Grid[i, j].Visible = true;
                                    Console.WriteLine("debug visible row: " + i.ToString() + " col: " + j.ToString());
                                }
                            }
                        }

                    }
                }
            }


        }
        public void VisibilityCheckVertical(bool Direction) {//true left to right

            

            if (Direction) {
                Console.WriteLine("Vertical to right");
                int[] HighestInCol = new int[rows];
                for (int i = 0; i < cols; i++) {
                    for (int j = 0; j < rows; j++) {
                        if (i == 0 || j == 0 || i == (cols - 1) || j == (rows - 1)) {
                            //edge tree
                            HighestInCol[j] = Grid[j, i].Height;
                            if (!Grid[j, i].Visible) {
                                Grid[j, i].Visible = true;
                                Console.WriteLine("debug visible row: " + j.ToString() + " col: " + i.ToString());
                            }
                        } else {
                            if (Grid[j, i].Height > HighestInCol[j]) {
                                HighestInCol[j] = Grid[j, i].Height;
                                if (!Grid[j, i].Visible) {
                                    Grid[j, i].Visible = true;
                                    Console.WriteLine("debug visible row: " + j.ToString() + " col: " + i.ToString());
                                }
                            }
                        }

                    }
                }
            } else {
                Console.WriteLine("Vertical to left");
                int[] HighestInCol = new int[rows];
                for (int i = cols-1; i>=0; i--) {
                    for (int j = 0; j < rows; j++) {
                        if (i == 0 || j == 0 || i == (cols-1) || j == (rows-1)) {
                            //edge tree
                            HighestInCol[j] = Grid[j, i].Height;
                            if (!Grid[j, i].Visible) {
                                Grid[j, i].Visible = true;
                                Console.WriteLine("debug visible row: " + j.ToString() + " col: " + i.ToString());
                            }
                        } else {
                            if (Grid[j, i].Height > HighestInCol[j]) {
                                HighestInCol[j] = Grid[j, i].Height;
                                if (!Grid[j, i].Visible) {
                                    Grid[j, i].Visible = true;
                                    Console.WriteLine("debug visible row: " + j.ToString() + " col: " + i.ToString());
                                }
                            }
                        }
                    }
                }
            }
        }


        public void PartTwo() {
            List<int> ScenicScores = new List<int>();
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < cols; j++) {
                    if (Grid[i, j].Visible) {
                        ScenicScores.Add(CalcScenicScore(i, j));
                    }

                }
            }
            ScenicScores = ScenicScores.OrderBy(x => x).ToList();
            Console.WriteLine("Part Two solution: " + ScenicScores.Last().ToString());
        }
        private int CalcScenicScore(int _startRow, int _startCol) {
            Tree CurrentTree = Grid[_startRow, _startCol];
            int top = 0;
            int down= 0;
            int left= 0;
            int right = 0;
            int result = 0;
            //top
            for (int i = _startRow-1; i >=0 ; i--) {
                if(Grid[i, _startCol].Height>= CurrentTree.Height) {
                    top++;
                    break;
                } else {
                    top++;
                }
            }
            //down
            for(int i= _startRow + 1; i< rows; i++) {
                if (Grid[i,_startCol].Height >= CurrentTree.Height) {
                    down++;
                    break;
                } else {
                    down++;
                }
            }
            //left
            for (int i = _startCol - 1; i >= 0; i--) {
                if (Grid[_startRow, i].Height >= CurrentTree.Height) {
                    left++;
                    break;
                } else {
                    left++;
                }
            }
            //right
            for (int i = _startCol + 1; i < rows; i++) {
                if (Grid[_startRow, i].Height >= CurrentTree.Height) {
                    right++;
                    break;
                } else {
                    right++;
                }
            }

            result = top * down * left * right;
            Console.WriteLine("Row: " + _startRow.ToString() + " Col: " + _startCol + " Scenic score: " + result.ToString());
            return result;
        }
    }
}
