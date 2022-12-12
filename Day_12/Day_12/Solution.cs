using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_12 {
    public class Tile {
        public int Height;
        public int Distance;
 //       public bool Infinite_Distance;
        public bool Start;
        public bool End;
        public bool Visited;

    }
    public class StartPos {
        public int row = 0;
        public int col = 0;
        public int Distance = 0;
        public bool Visited = false;
    }
    internal class Solution {
        Tile[,] Map;
        int rows = 0;
        int cols = 0;
        int startPos_row_part1 = 0;
        int startPos_col_part1 = 0;
        int endPos_row = 0;
        int endPos_col = 0;
        public Solution() {
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
            Map = new Tile[rows, cols];
            ;
            filestream.Position = 0;
            reader.DiscardBufferedData();
            char[] lineArray;
            int rowIndex = 0;
            while ((lineOfText = reader.ReadLine()) != null) {
                int colIndex = 0;
                foreach (char c in lineOfText) {
                    //new tree
                    Map[rowIndex, colIndex] = new Tile();
                    if (c == 'S') {
                        //start node
                        Map[rowIndex, colIndex].Height = Convert.ToInt32('a') - 97;
                        //  Map[rowIndex, colIndex].Infinite_Distance = false;
                        Map[rowIndex, colIndex].Start = true;
                        Map[rowIndex, colIndex].Visited = true;
                        Map[rowIndex, colIndex].Distance = 0;
                        startPos_row_part1 = rowIndex;
                        startPos_col_part1 = colIndex;

                    } else if (c == 'E') {
                        Map[rowIndex, colIndex].Height = Convert.ToInt32('z') - 97;
                        //     Map[rowIndex, colIndex].Infinite_Distance = true;
                        Map[rowIndex, colIndex].End = true;
                        Map[rowIndex, colIndex].Distance = 999999999;
                        endPos_row = rowIndex;
                        endPos_col = colIndex;
                        ;
                    } else {
                        Map[rowIndex, colIndex].Height = Convert.ToInt32(c) - 97;
                        //    Map[rowIndex, colIndex].Infinite_Distance = true;
                        ;
                    }
                    colIndex++;
                }
                rowIndex++;
            }
        }

        public void Part_One() {
            //start with start position
            ProcessNeighbours(startPos_row_part1, startPos_col_part1);

            Console.WriteLine("Solution part one: " + Map[endPos_row, endPos_col].Distance.ToString());

        }
        public void Part_Debug() {
            //start with start position
            ResetMap(4, 0);
            ProcessNeighbours(4, 0);
            Console.WriteLine("Solution debug: " + Map[endPos_row, endPos_col].Distance.ToString());

        }

        public void Part_Two() {
            List<StartPos> start_position = new List<StartPos>();
            for (int row = 0; row < rows; row++) {
                for (int col = 0; col < cols; col++) {
                    if(Map[row, col].Height == 0) {
                        StartPos newStartPos = new StartPos();
                        newStartPos.row = row;
                        newStartPos.col = col;
                        start_position.Add(newStartPos);
                    }
                }
            }
            for(int i = 0; i < start_position.Count(); i++) {
                ResetMap(start_position[i].row, start_position[i].col);
                //Console.WriteLine("Iteration: " + i.ToString());
                //ResetMap();
                ProcessNeighbours(start_position[i].row, start_position[i].col);
                start_position[i].Distance = Map[endPos_row, endPos_col].Distance;
                start_position[i].Visited = Map[endPos_row, endPos_col].Visited;
                 //Console.WriteLine(i + " temp : " + Map[endPos_row, endPos_col].Distance.ToString());
            }
            start_position = start_position.Where(y=>y.Visited == true).ToList().OrderBy(x => x.Distance).ToList();

            Console.WriteLine("Part two: " + start_position[0].Distance.ToString());
        }

        private void ResetMap(int _startRow, int _startCol) {
            for (int row = 0; row < rows; row++) {
                for (int col = 0; col < cols; col++) {
                    //reset
                    Map[row, col].Start = false;
                    Map[row, col].Distance = 0;
                    Map[row, col].Visited = false;
                }
            }

            //set start
            Map[_startRow, _startCol].Start = true;
            Map[_startRow, _startCol].Visited = true;
            Map[_startRow, _startCol].Distance = 0;
        }
        private void ProcessNeighbours(int _row, int _col) {
            Tile current = Map[_row, _col];
           // DrawDebug(_row, _col);
            //top neighbour
            if (_row!=0) {
                int newrow = _row - 1;
                if (Map[newrow, _col].Height <= (current.Height + 1)) {
                    //accessible
                    int newDist = current.Distance + 1;
                    if (Map[newrow, _col].Visited) {
                        if (newDist < Map[newrow, _col].Distance) {
                            Map[newrow, _col].Distance = newDist;
                            ProcessNeighbours(newrow, _col);
                        }

                    } else {
                        Map[newrow, _col].Visited = true;
                        Map[newrow, _col].Distance = newDist;
                        //recurse
                        ProcessNeighbours(newrow, _col);
                    }

                }
            }
            //bot 
            if (_row != (rows-1)) {
                int newrow = _row + 1;
                if (Map[newrow, _col].Height <= (current.Height + 1)) {
                    //accessible
                    int newDist = current.Distance + 1;
                    if (Map[newrow, _col].Visited) {
                        if (newDist < Map[newrow, _col].Distance) {
                            Map[newrow, _col].Distance = newDist;
                            ProcessNeighbours(newrow, _col);
                        }

                    } else {
                        Map[newrow, _col].Visited = true;
                        Map[newrow, _col].Distance = newDist;
                        //recurse
                        ProcessNeighbours(newrow, _col);
                    }

                }
            }
            //left
            if (_col != 0) {
                int newcol = _col -1;
                if (Map[_row, newcol].Height <= (current.Height + 1)) {
                    //accessible
                    int newDist = current.Distance + 1;
                    if (Map[_row, newcol].Visited) {
                        if (newDist < Map[_row, newcol].Distance) {
                            Map[_row, newcol].Distance = newDist;
                            ProcessNeighbours(_row, newcol);
                        }

                    } else {
                        Map[_row, newcol].Visited = true;
                        Map[_row, newcol].Distance = newDist;
                        //recurse
                        ProcessNeighbours(_row, newcol);
                    }

                }
            }
            //right
            if (_col != (cols - 1)) {
                int newcol = _col + 1;
                if (Map[_row, newcol].Height <= (current.Height + 1)) {
                    //accessible
                    int newDist = current.Distance + 1;
                    if(Map[_row, newcol].Visited) {
                        if(newDist < Map[_row, newcol].Distance) {
                            Map[_row, newcol].Distance = newDist;
                            ProcessNeighbours(_row, newcol);
                        }

                    } else {
                        Map[_row, newcol].Visited = true;
                        Map[_row, newcol].Distance = newDist;
                        //recurse
                        ProcessNeighbours(_row, newcol);
                    }
                    
                }
            }
            
        }

        private void DrawDebug(int _row, int _col) {
            Console.WriteLine("------------------------------------");
            Console.WriteLine("Row: " + _row.ToString() +  " Col: " + _col.ToString());

            for (int row = 0; row < rows; row++) {
                string line = "";
                for (int col = 0; col < cols; col++) {

                    line += Map[row, col].Distance.ToString() + " ";
                
                }
                Console.WriteLine(line);
            }

        }

    }

}
