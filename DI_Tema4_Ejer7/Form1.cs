using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DI_Tema4_Ejer7
{
    public delegate void Metodos();
    public partial class Form1 : Form
    {
        int palabras=0, frases=0,caracteres = 0;
        bool hayPalabra = false;
        bool hayFrase = false;
        ToolTip tt = new ToolTip();
        public Form1()
        {
            InitializeComponent();
            Metodos me;
            me = textBox1.Undo;
            deshacerToolStripMenuItem.Tag = me;
            me = textBox1.Copy;
            copiarToolStripMenuItem.Tag = me;
            me = textBox1.Paste;
            pegarToolStripMenuItem.Tag = me;
            me = textBox1.Cut;
            cortarToolStripMenuItem.Tag = me;
            me = textBox1.SelectAll;
            seleccionarTodoToolStripMenuItem.Tag = me;

            acercaDeToolStripMenuItem.Click += (object sender, EventArgs e) =>
            {
                MessageBox.Show("EL autor del programa ha sufrido innumerables brotes psicóticos durante la codificación del mismo, por favor, si lo ven avisen a las autoridades y ponganse a salvo, puede ser peligroso.", "Acerca de...");
            };

            this.KeyPreview = true;

            tt.SetToolTip(this.textBox1, "c: " + caracteres +", p: " + palabras + ", f: " + frases);
        }

        public bool SeBorra()
        {
            bool seBorra = true;
            DialogResult res = DialogResult.OK;
            if (textBox1.Modified)
            {
                res = MessageBox.Show("¿Estás seguro de que quieres cerrar sin guardar los cambios?", "Quieeeto miura", MessageBoxButtons.YesNo);
            }
            if (res == DialogResult.No)
            {
                seBorra = false;
            }
            return seBorra;
        }

        private void nuevoDocumentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (SeBorra())
            {
                textBox1.Text = "";
            }
        }

        private void guardarArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if(sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter f = new StreamWriter(sfd.FileName))
                    {
                        f.Write(textBox1.Text);
                        textBox1.Modified = false;
                    }
                }
                catch (IOException)
                {
                    MessageBox.Show("No sé que has hecho... pero tú sí...", "¿Ande Vas?",MessageBoxButtons.OK);
                }
            }
        }

        private void abrirArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SeBorra()) {
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (StreamReader sr = new StreamReader(ofd.FileName))
                        {
                            string aux;
                            while(( aux = sr.ReadToEnd()) != "")
                            textBox1.Text += aux;
                        }
                    }
                    catch (IOException)
                    {
                        MessageBox.Show("No sé que has hecho... pero tú sí...", "¿Ande Vas?", MessageBoxButtons.OK);
                    }
                    catch (OutOfMemoryException)
                    {
                        MessageBox.Show("No sé que has hecho... pero tú sí...", "¿Ande Vas?", MessageBoxButtons.OK);
                    }
                }
            }

        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SeBorra())
            {
                Close();
            }
        }

        private void deshacerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Metodos me;
            
            me = (Metodos)((ToolStripMenuItem)sender).Tag;
            me();
        }

        private void ajusteDeLíneaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ajusteDeLíneaToolStripMenuItem.Checked = !ajusteDeLíneaToolStripMenuItem.Checked;
            textBox1.WordWrap = ajusteDeLíneaToolStripMenuItem.Checked;
        }

        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripItemCollection tsic = selecciónDeEscrituraToolStripMenuItem.DropDownItems;
            foreach(ToolStripMenuItem tsi in tsic)
            {
                if (tsi.Checked)
                {
                    tsi.Checked = false;
                }
            }
            ((ToolStripMenuItem)sender).Checked = true;

            switch (((ToolStripMenuItem)sender).Text)
            {
                case "Mayúsculas":
                    textBox1.CharacterCasing = CharacterCasing.Upper;
                    break;
                case "Minúsculas":
                    textBox1.CharacterCasing = CharacterCasing.Lower;
                    break;
                case "Normal":
                    textBox1.CharacterCasing = CharacterCasing.Normal;
                    break;

            }
            
        }

        private void fuenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            DialogResult res = new DialogResult();

            res = fd.ShowDialog();
            if(res == DialogResult.OK)
            {
                textBox1.Font = fd.Font;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            
            
            tt.SetToolTip(this.textBox1, "c: " + caracteres + ", p: " + palabras + ", f: " + frases);
        }

        private void colorDeFondoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();

            DialogResult res = new DialogResult();
            res = cd.ShowDialog();
            if(res == DialogResult.OK)
            {
                if(((ToolStripMenuItem)sender) == colorDeFondoToolStripMenuItem)
                {
                    textBox1.BackColor = cd.Color;
                }
                else
                {
                    textBox1.ForeColor = cd.Color;
                }
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}
