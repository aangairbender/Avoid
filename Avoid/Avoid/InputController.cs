using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Avoid
{
    class InputController
    {
        public String[] keyCodes;
        public int lastKeyIndex;

        public InputController()
        {
            keyCodes = new String[4];
            lastKeyIndex = 0;
            readKeysFromFile();
        }
        private void readKeysFromFile()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("config.xml");
            foreach(XmlNode xmlNode in xmlDoc.DocumentElement.ChildNodes)
            {
                if(xmlNode.Name == "keys")
                {
                    foreach(XmlNode keyNode in xmlNode.ChildNodes)
                    {
                        keyCodes[Convert.ToInt32(keyNode.Attributes.GetNamedItem("index").Value)] = keyNode.FirstChild.InnerText;
                    }

                }
            }
        }
        public void keyDown(KeyEventArgs e)
        {
            for (int i = 0; i < 4; ++i)
            {
                if ((Keys)Convert.ToInt32(keyCodes[i]) == e.KeyCode) lastKeyIndex = i;
            }
        }
        public void keyUp(KeyEventArgs e)
        {

        }

        public int getKeyIndex()
        {
            return lastKeyIndex;
        }

    }
}
