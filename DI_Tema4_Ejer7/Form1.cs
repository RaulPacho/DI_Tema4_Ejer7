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
    }
}
