using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DI_Tema4_Ejer7
{
    [Serializable]
    class Configuracion
    {
       
        public Boolean checkeado;
        public CharacterCasing cc;
        public Color bc;
        public Color fc;
        public Font f;
        public string ultimo;
        public List <string> recientes;


    }
}
