using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_13 {

    public interface Item {  }

    public class Type_int : Item {
        public int Integer;
    }

    public class Type_list : Item {
        public bool Divider = false;
        public List<Item> List;
    }
    public class Pair {
        public List<Item> First;
        public List<Item> Second;
    }
    internal class Solution {
        List<Pair> pairs;
        public Solution() {
            string lineOfText;
            string ConfigPath = AppDomain.CurrentDomain.BaseDirectory + "input.txt";
            FileStream filestream = new FileStream(ConfigPath,
                                            System.IO.FileMode.Open,
                                            System.IO.FileAccess.Read,
                                            System.IO.FileShare.ReadWrite);
            var reader = new System.IO.StreamReader(filestream, System.Text.Encoding.UTF8, true, 128);
            char[] lineArray;
            pairs = new List<Pair>();
            int PosTmp = 0;
            int rowIndex = 0;
            while ((lineOfText = reader.ReadLine()) != null) {
                if (lineOfText == String.Empty) {
                    continue;
                }
                Pair newPair = new Pair();
                //First Pair
                PosTmp = 0;
                newPair.First = ParseList2(lineOfText,false,0,ref PosTmp);

                //Second Pair
                lineOfText = reader.ReadLine();
                PosTmp = 0;
                newPair.Second= ParseList2(lineOfText,false,0, ref PosTmp);


                pairs.Add(newPair);
            } 


        }
        public void Part_Two() {
            //add extra packets:
            int PosTmp = 0;
            List<List<Item>> OrderedPackets = new List<List<Item>>();

            List<Item> divider1 = ParseList2("[[2]]", false, 0, ref PosTmp);
            (divider1[0] as Type_list).Divider = true;
            OrderedPackets.Add(divider1);
            PosTmp = 0;
            List<Item> divider2 = ParseList2("[[6]]", false, 0, ref PosTmp);
            (divider2[0] as Type_list).Divider = true;
            OrderedPackets.Add(divider2);
            ;
            //add existing packets
            foreach (Pair pair in pairs) {

                //add first pair
                int currentIndex = 0;
                bool insterted = false;
                for (int i = 0; i < OrderedPackets.Count; i++) {
                    if (CompareTwo(pair.First, OrderedPackets[i])) {
                        OrderedPackets.Insert(currentIndex, pair.First);
                        insterted = true;
                        break;
                    } else {
                        currentIndex++;
                        continue;
                    }
                }
                if (!insterted) {
                    //add as last
                    OrderedPackets.Add(pair.First);
                }

                //add second pair
                currentIndex = 0;
                insterted = false;
                for (int i = 0; i < OrderedPackets.Count; i++) {
                    if (CompareTwo(pair.Second, OrderedPackets[i])) {
                        OrderedPackets.Insert(currentIndex, pair.Second);
                        insterted = true;
                        break;
                    } else {
                        currentIndex++;
                        continue;
                    }
                }
                if (!insterted) {
                    //add as last
                    OrderedPackets.Add(pair.Second);
                }


            }

            //find dividers
            int first = 0;
            bool firstfound = false;
            int second = 0;

            for (int i = 0; i < OrderedPackets.Count; i++) {
                try {
                    if (((OrderedPackets[i] as List<Item>)[0] as Type_list).Divider == true) {
                        if (!firstfound) {
                            firstfound = true;
                            first = i + 1;
                        } else {
                            second = i + 1;
                            break;
                        }
                    }
                } catch (Exception ex) when (ex is System.ArgumentOutOfRangeException or System.NullReferenceException) {
                    ;
                }
            }

            Console.WriteLine("Part two solution: " + (first * second).ToString());
        }
        public void Part_One() {
            int pairIndex = 1;
            List<int> Indexes = new List<int>();
            foreach (Pair pair in pairs) {
                bool RightOrder = false;

                RightOrder = CompareTwo(pair.First, pair.Second);

                if (RightOrder) {
                    //store index for result
                    Indexes.Add(pairIndex);
                }
                pairIndex++;
            }

            int resultSum = Indexes.Sum();
            Console.WriteLine("Part one solution: " + resultSum.ToString());
        }
        public bool CompareTwo(List<Item> _Left, List<Item> _Right) {
            bool ResultFound = false;
            bool Result = false;
            for (int i = 0; i < _Left.Count(); i++) {
                if (i >= (_Right.Count())) {
                    // Right side ran out of items
                    ResultFound = true;
                    Result = false;
                    break;
                }
                Result = false;
                ResultFound = false;
                ResultFound = CheckOrder(_Left[i], _Right[i], ref Result);
                if (ResultFound) {
                    break;
                }
                ;
            }
            if (!ResultFound) {
                if (_Right.Count() > _Left.Count()) {
                    //Left side ran out of items
                    Result = true;
                    //  ResultFound = true;
                }
            }
            return Result;
        }
        public bool CheckOrder(Item _cLeft, Item _cRight, ref bool _RightOrder) { // return: result found
            if (_cLeft is Type_int) {
                if (_cRight is Type_int) {
                    //int - int
                    Type_int left = _cLeft as Type_int;
                    Type_int right = _cRight as Type_int;
                    if (left.Integer < right.Integer) {
                        //Left side is smaller
                        _RightOrder = true;
                        return true;//result found
                    } else if (left.Integer == right.Integer) {
                        //continue
                        return false;
                    } else {
                        // Right side is smaller
                        _RightOrder = false;
                        return true;//result found
                    }

                } else {
                    //int - list
                    Type_list left = new Type_list();
                    left.List = new List<Item>();
                    left.List.Add(_cLeft as Type_int);
                    bool localRightorder = false;
                    bool localResultFound = CheckOrder(left, _cRight, ref localRightorder);
                    _RightOrder = localRightorder;
                    return localResultFound;

                }
            } else {
                if (_cRight is Type_int) {
                    //list - int

                    Type_list right = new Type_list();
                    right.List = new List<Item>();
                    right.List.Add(_cRight as Type_int);
                    bool localRightorder = false;
                    bool localResultFound = CheckOrder(_cLeft, right, ref localRightorder);
                    _RightOrder = localRightorder;
                    return localResultFound;



                } else {
                    //list - list
                    Type_list left = _cLeft as Type_list;
                    Type_list right = _cRight as Type_list;
                    bool ResultFound = false;
                    bool RightOrder = false;
                    if(left.List.Count() == 0 && right.List.Count() != 0) {
                        //Left side ran out of items
                        RightOrder = true;
                        ResultFound = true;
                        _RightOrder = RightOrder;
                        return ResultFound;
                    }
                    if (left.List.Count() != 0 && right.List.Count() == 0) {
                        //Right side ran out of items
                        RightOrder = false;
                        ResultFound = true;
                        _RightOrder = RightOrder;
                        return ResultFound;
                    }
                    if (left.List.Count() == 0 && right.List.Count() == 0) {
                        //debug
                        ;
                    }
                    for (int i = 0; i < left.List.Count(); i++) {
                        if (i >= (right.List.Count())) {
                            // Right side ran out of items
                            RightOrder = false;
                            ResultFound = true;
                            break;
                        }
                        RightOrder = false;
                        ResultFound = false;
                        ResultFound = CheckOrder(left.List[i], right.List[i], ref RightOrder);
                        if (ResultFound) {
                            break;
                        }
                    }
                    if (!ResultFound) {
                        if (right.List.Count() > left.List.Count()) {
                            //Left side ran out of items
                            RightOrder = true;
                            ResultFound = true;
                        }
                    }
                    _RightOrder = RightOrder;
                    if (!ResultFound) {
                        ;
                    }
                    return ResultFound;
                }
            }

            return false;
        }


        private List<Item> ParseList2(string _line,bool recursive,int StartPos, ref int CharInc) {
            List<Item> retVal = new List<Item>();
            //remove brackets from ends:
            string sline = _line;
            //remove first [

            if (!recursive) {
                sline = _line.Remove(0, 1);
                }

        string tmpItem = "";
            int tmpInt = 0;
            for(int i = StartPos; i< sline.Length; i++,CharInc++) {
                if (sline[i] == '[') {
                    //new array 
                    int PosInc = 0;
                    Type_list newList = new Type_list();
                    newList.List = new List<Item>();
                    newList.List = ParseList2(sline, true, (i+1), ref PosInc);
                    retVal.Add(newList);
                    i += (PosInc+1);
                    CharInc += (PosInc + 1);
                    continue;
                }
                if (Int32.TryParse(sline[i].ToString(), out tmpInt)) {
                    //numeric
                    tmpItem += sline[i].ToString();
                }
                if (sline[i] == ',') {
                    if (tmpItem != String.Empty) {
                        Type_int newInt = new Type_int();
                        newInt.Integer = Convert.ToInt32(tmpItem);
                        retVal.Add(newInt);
                        tmpItem = "";
                    } else {
                        continue;
                    }
                }
                if(sline[i] == ']') {
                    if(tmpItem!= String.Empty) {
                        Type_int newInt = new Type_int();
                        newInt.Integer = Convert.ToInt32(tmpItem);
                        retVal.Add(newInt);
                        tmpItem = "";
                    }
                    //CharInc = i;
                    return retVal;
                }

            }

            return retVal;
        }
    }


}
