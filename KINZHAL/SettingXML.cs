using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KINZHAL
{
    public class SettingXML
    {
        private string coeffKrutHor;

        public string CoeffKrutHor
        {
            get { return coeffKrutHor; }
            set { coeffKrutHor = value; }
        }
        private string coefKrutVert;

        public string CoefKrutVert
        {
            get { return coefKrutVert; }
            set { coefKrutVert = value; }
        }
        private string coefSensHor;

        public string CoefSensHor
        {
            get { return coefSensHor; }
            set { coefSensHor = value; }
        }
        private string coefSensVert;

        public string CoefSensVert
        {
            get { return coefSensVert; }
            set { coefSensVert = value; }
        }
    }
}
