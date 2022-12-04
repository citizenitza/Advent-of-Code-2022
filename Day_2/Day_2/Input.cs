using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day_2 {
    public enum Shape {
        Rock = 1,
        Paper = 2,
        Scissor = 3,
    }
    public enum Result {
        Lost = 0,
        Draw = 3,
        Won = 6,
    }
    public class Round {
        public string RawText;
        public Shape MyChoice;
        public Shape EnemyChoice;
        public int ScoreOfRound() {
            int result = 0;

            if (MyChoice == Shape.Rock) {
                if (EnemyChoice == Shape.Rock) {
                    //draw
                    result = 3;
                } else if (EnemyChoice == Shape.Paper) {
                    //lost
                    result = 0;
                } else if (EnemyChoice == Shape.Scissor) {
                    //win
                    result = 6;
                }
            } else if (MyChoice == Shape.Paper) {
                if (EnemyChoice == Shape.Rock) {
                    //won
                    result = 6;
                } else if (EnemyChoice == Shape.Paper) {
                    //draw
                    result = 3;
                } else if (EnemyChoice == Shape.Scissor) {
                    //lost
                    result = 0;
                }
            } else if (MyChoice == Shape.Scissor) {
                if (EnemyChoice == Shape.Rock) {
                    //lost
                    result = 0;
                } else if (EnemyChoice == Shape.Paper) {
                    //won
                    result = 6;
                } else if (EnemyChoice == Shape.Scissor) {
                    //draw
                    result = 3;
                }
            }
            //add score for shape
            result += (int)MyChoice;
            return result;


        }
    }
    public class Input {

        List<Round> ListOfRounds = new List<Round>();
        List<Round> ListOfRounds_Correct = new List<Round>();
        public Input() {
            string lineOfText;
            string ConfigPath = AppDomain.CurrentDomain.BaseDirectory + "puzzle_input.txt";
            int lineIndex = 0;
            FileStream filestream = new FileStream(ConfigPath,
                                            System.IO.FileMode.Open,
                                            System.IO.FileAccess.Read,
                                            System.IO.FileShare.ReadWrite);
            var reader = new System.IO.StreamReader(filestream, System.Text.Encoding.UTF8, true, 128);
            ListOfRounds = new List<Round>();
            int ElfIndex = 0;
            string[] lineArray;
            while ((lineOfText = reader.ReadLine()) != null) {
                Round newRound = new Round();
                lineArray = new string[2];
                lineArray = lineOfText.Split(' ');
                string myChoice = lineArray[1];
                string EnemyChoice = lineArray[0];
                string Result = lineArray[1];
                newRound.RawText = lineOfText;
                newRound.MyChoice = GetMyChoice(myChoice);
                newRound.EnemyChoice = GetEnemyChoice(EnemyChoice);
                ListOfRounds.Add(newRound);

                Round newRound_Correct = new Round();
                newRound_Correct.RawText = lineOfText;
                newRound_Correct.EnemyChoice = GetEnemyChoice(EnemyChoice);
                newRound_Correct.MyChoice = GetMyCorrectChoice(newRound_Correct.EnemyChoice, Result);
                ListOfRounds_Correct.Add(newRound_Correct);
            }
        }
        public int GetScore() {
            int result = 0;
            foreach (Round rd in ListOfRounds) {
                result += rd.ScoreOfRound();
            }
            return result;
        }

        public int GetScore_2() {
            int result = 0;
            foreach (Round rd in ListOfRounds_Correct) {
                result += rd.ScoreOfRound();
            }
            return result;
        }
        private Shape GetMyCorrectChoice(Shape _enemy, string _result) {
            Shape result = new Shape();
            if (_result == "X") {
                //Need to loose
                if (_enemy == Shape.Rock) {
                    result = Shape.Scissor;
                } else if (_enemy == Shape.Paper) {
                    result = Shape.Rock;
                } else if (_enemy == Shape.Scissor) {
                    result = Shape.Paper;
                }
            } else if (_result == "Y") {
                //need to draw
                if (_enemy == Shape.Rock) {
                    result = Shape.Rock;
                } else if (_enemy == Shape.Paper) {
                    result = Shape.Paper;
                } else if (_enemy == Shape.Scissor) {
                    result = Shape.Scissor;
                }
            } else if (_result == "Z") {
                //need to win
                if (_enemy == Shape.Rock) {
                    result = Shape.Paper;
                } else if (_enemy == Shape.Paper) {
                    result = Shape.Scissor;
                } else if (_enemy == Shape.Scissor) {
                    result = Shape.Rock;
                }
            } else {
                //debug
                ;
            }


            return result;
        }
        private Shape GetEnemyChoice(string _input) {
            Shape result = new Shape();
            if (_input == "A") {
                result = Shape.Rock;
            } else if (_input == "B") {
                result = Shape.Paper;
            } else if (_input == "C") {
                result = Shape.Scissor;
            } else {
                //debug
                ;
            }
            return result;
        }
        private Shape GetMyChoice(string _input) {
            Shape result = new Shape();
            if (_input == "X") {
                result = Shape.Rock;
            } else if (_input == "Y") {
                result = Shape.Paper;
            } else if (_input == "Z") {
                result = Shape.Scissor;
            } else {
                //debug
                ;
            }
            return result;
        }
    }

}