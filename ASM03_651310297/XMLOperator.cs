using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ASM03_651310297 {
    public class XMLOperator {
        String pathToPlayersXML = "Players.xml";
        XElement players;
        public XMLOperator() {
              players = XElement.Load(pathToPlayersXML);
        }
    }
}