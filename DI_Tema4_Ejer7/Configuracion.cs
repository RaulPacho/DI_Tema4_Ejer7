using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DI_Tema4_Ejer7
{
    class Configuracion
    {
        /*
                   bw.Write(ajusteDeLíneaToolStripMenuItem.Checked);
                   bw.Write(""+textBox1.CharacterCasing);
                   bw.Write(textBox1.BackColor + "," +textBox1.ForeColor);
                   bw.Write(""+textBox1.Font);
                   bw.Write(ultimoDir);
                   String aux = "";
                   for(int i = 0; i < recientes.Count; i++)
                   {
                       aux += recientes[i] + ",";
                   }
                   bw.Write(aux);
                   */
        public Boolean checkeado;
        public CharacterCasing cc;
        public Color bc;
        public Color fc;
        public Font f;
        public string ultimo;
        public List <string> recientes;


    }
}
