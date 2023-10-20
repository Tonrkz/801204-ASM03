using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ASM03_651310297 {
    public class XMLOperator {
        private static XMLOperator instance;
        public static XMLOperator Instance {
            get {
                if (instance == null) {
                    instance = new XMLOperator();
                }
                return instance;
            }
        }

        String pathToPlayersXML = "Players.xml";
        XElement players;
        public XMLOperator() {
              players = XElement.Load(pathToPlayersXML);
        }

        public void SavePlayer() {
            if (players.Elements("player").Any(item => item.Attribute("name").Value == Players.Instance.name)) {
                players.Elements("player").Where(item => item.Attribute("name").Value == Players.Instance.name).Remove();
            }
            players.Add(new XElement("player", new XAttribute("name", Players.Instance.name),
                                               new XElement("HP", Players.Instance.HP),
                                               new XElement("maxHP", Players.Instance.maxHP),
                                               new XElement("level", Players.Instance.level),
                                               new XElement("EXP", Players.Instance.EXP),
                                               new XElement("maxEXP", Players.Instance.maxEXP),
                                               new XElement("ATK", Players.Instance.ATK),
                                               new XElement("DEF", Players.Instance.DEF),
                                               new XElement("AGI", Players.Instance.AGI),
                                               new XElement("position", Players.Instance.position),
                                               new XElement("gold", Players.Instance.gold),
                                               new XElement("swordID", 31),
                                               new XElement("shieldID", 5)));
            players.Save(pathToPlayersXML);
        }

        public XElement LoadPlayer() {
            return players;
        }
    }
}