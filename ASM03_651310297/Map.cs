using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASM03_651310297 {
    public class Map {
        public readonly bool[,] adjacencyMatrix = new bool[,] { { false, true, true, false, false },
                                                { true, false, false, true, false },
                                                { true, false, false, true, false },
                                                { false, true, true, false, true },
                                                { false, false, false, true, false } };


    }
}