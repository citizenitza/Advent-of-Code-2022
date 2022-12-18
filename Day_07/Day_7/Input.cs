using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;
namespace Day_7 {
    
    class ElfFile {
        public string Name; 
        public long Size; // 0 if Dir
        public long DirSize; // 0 if File
        public List<ElfFile> Files; // Empty if its a file
        public ElfFile Parent; //null if file
    }

    class DirSize {
        public string Name;
        public long Size;
        public ElfFile Reference;
    }

    internal class Input {


        ElfFile Root = new ElfFile();
        List<DirSize> DeletableDirs = new List<DirSize>();
        List<DirSize> AllDirs = new List<DirSize>();
        public Input() {
            string lineOfText;
            string ConfigPath = AppDomain.CurrentDomain.BaseDirectory + "input.txt";
            uint tmp = 0;
            FileStream filestream = new FileStream(ConfigPath,
                                            System.IO.FileMode.Open,
                                            System.IO.FileAccess.Read,
                                            System.IO.FileShare.ReadWrite);
            var reader = new System.IO.StreamReader(filestream, System.Text.Encoding.UTF8, true, 128);
            Root.Size = 0;
            Root.Name = "/";
            Root.Files = new List<ElfFile>();
            Root.Parent = null;
            ElfFile CurrentPath;
            //set root
            CurrentPath = Root;
            string[] lineArray;

            while ((lineOfText = reader.ReadLine()) != null) {
                //InputString = lineOfText
                if (lineOfText[0] == '$') {
                    //command
                    lineArray = lineOfText.Split(' ');
                    if (lineArray[1] == "ls") {
                        //ignore
                    } else if (lineArray[1] == "cd") {
                        //change dir
                        if(lineArray[2] == "..") {
                            // up 
                            CurrentPath = CurrentPath.Parent;
                        }else if(lineArray[2] != String.Empty ) {
                            //down
                            SetCurrentDir(lineOfText, ref CurrentPath);
                        } else {
                            //debug
                            ;
                        }
                    }
                } else if(lineOfText.Split(' ')[0] == "dir") {
                    //add dir
                    AddObject(ref CurrentPath, lineOfText, true);
                } else if(UInt32.TryParse(lineOfText.Split(' ')[0],out tmp)) {
                    //add file
                    AddObject(ref CurrentPath, lineOfText, false);
                } else {
                    //debug
                    ;
                }
                
            }
        }

        private void SetCurrentDir(string _line, ref ElfFile _currentPath) {
            foreach (ElfFile newCurrentDir in _currentPath.Files) {
                if(newCurrentDir.Size == 0) {
                    //its a dirű
                    if (newCurrentDir.Name == _line.Split(' ')[2]) {
                        _currentPath = newCurrentDir;
                        return;
                    }
                }

            }
        }
        private void AddObject(ref ElfFile _dir, string _line,bool _addDir) {
            //add dirs and files on ls command
            //_addDir true = DIR, false = FILE
            if (_addDir) {
                ElfFile newDir = new ElfFile();
                newDir.Name = _line.Split(' ')[1];
                newDir.Parent = _dir;
                newDir.Files = new List<ElfFile>();
                _dir.Files.Add(newDir);
            } else {
                //add file
                ElfFile newFile = new ElfFile();
                newFile.Name = _line.Split(' ')[1];
                newFile.Size = Convert.ToInt64(_line.Split(' ')[0]);
                newFile.Parent = null;
                _dir.Files.Add(newFile);
            }
        }
        public void Part_two() {
            long DiskSpace = 70000000;
            long UpdateSpace = 30000000;
            long Unused = DiskSpace - Root.DirSize;
            Console.WriteLine("Unused space: " + Unused.ToString());
            List<DirSize> LargeEnough = new List<DirSize>();
            foreach (DirSize dir in AllDirs) {
                if ((Unused + dir.Size) > UpdateSpace) {
                    Console.WriteLine("Dir large enough: " + dir.Size);
                    LargeEnough.Add(dir);
                }
            }
            LargeEnough = LargeEnough.OrderBy(y => y.Size).ToList();
            Console.WriteLine("Part Two solution: " + LargeEnough[0].Size);
        }
        public void Part_one() {
            long directionSize = 0;
            DeletableDirs = new List<DirSize>();
            ListFolderSizes(Root, ref directionSize);

            WriteSum();

        }

        public void WriteSum() {
            long sum = 0;
            foreach(DirSize _dir in DeletableDirs) {
                sum += _dir.Size;
            }
            Console.WriteLine("Sum: " + sum.ToString());
        }
        public void ListFolderSizes(ElfFile _elfFile,ref long parentsize) {
            long CurrentSize = 0;
            foreach(ElfFile fl in _elfFile.Files) {
                if (fl.Size == 0) {
                    //dir
                    ListFolderSizes(fl, ref CurrentSize);
                   
                } else {
                    //file
                    //parentsize += fl.Size;
                    CurrentSize += fl.Size;
                }

            }
            parentsize += CurrentSize;
            //current dir done

            DirSize tmp = new DirSize();
            tmp.Name = _elfFile.Name;
            tmp.Size = CurrentSize;
            tmp.Reference = _elfFile;
            AllDirs.Add(tmp);

            if (CurrentSize <= 100000) {//part one
                DeletableDirs.Add(tmp);
            }
            _elfFile.DirSize = CurrentSize;
            Console.WriteLine(_elfFile.Name + " size: " + CurrentSize.ToString());
        }

    }
}
