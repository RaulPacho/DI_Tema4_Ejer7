using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DI_Tema4_Ejer7
{
    public delegate void Metodos();
    public partial class Form1 : Form
    {
        bool hayPalabra = false;
        bool hayFrase = false;
        ToolTip tt = new ToolTip();
        int contLetra, contFrase, contPalabra;
        string ultimoDir = "";
        List<string> recientes = new List<string>();
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
            if (File.Exists("Conf.cfg"))
            {

                BinaryFormatter BF = new BinaryFormatter();
                FileStream Archivo = File.Open("Conf.cfg", FileMode.Open);
                Configuracion c = (Configuracion)BF.Deserialize(Archivo);
                Archivo.Close();

                textBox1.BackColor = c.bc;
                textBox1.ForeColor = c.fc;
                textBox1.CharacterCasing = c.cc;
                textBox1.Font = c.f;
                ajusteDeLíneaToolStripMenuItem.Checked = c.checkeado;
                textBox1.WordWrap = c.checkeado;
                ultimoDir = c.ultimo;
                recientes = c.recientes;

            }
                tt.SetToolTip(this.textBox1, "caracteres: " + 0 + ", palabras: " + 0 + ", frases: " + 0);
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
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter f = new StreamWriter(sfd.FileName))
                    {
                        ultimoDir = sfd.FileName;
                        rellenaRecientes();
                        f.Write(textBox1.Text);
                        textBox1.Modified = false;
                    }
                }
                catch (IOException)
                {
                    MessageBox.Show("No sé que has hecho... pero tú sí...", "¿Ande Vas?", MessageBoxButtons.OK);
                }
            }
        }

        public void rellenaRecientes()
        {
            if (recientes.Count() == 5)
            {
                recientes.RemoveAt(0);
            }
            recientes.Add(ultimoDir);

            ToolStripItemCollection items = recientesToolStripMenuItem.DropDownItems;
            for (int i = 0; i < recientes.Count;i++) {
                items[i].Text = recientes[i];
            }

        }

        private void abrirArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SeBorra())
            {
                
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = "";
                    try
                    {
                        using (StreamReader sr = new StreamReader(ofd.FileName))
                        {
                            ultimoDir = ofd.FileName;
                            rellenaRecientes();
                            string aux;
                            while ((aux = sr.ReadToEnd()) != "")
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
                Configuracion c = new Configuracion();
                c.bc = textBox1.BackColor;
                c.fc = textBox1.ForeColor;
                c.cc = textBox1.CharacterCasing;
                c.f = textBox1.Font;
                c.checkeado = ajusteDeLíneaToolStripMenuItem.Checked;
                c.ultimo = ultimoDir;
                c.recientes = recientes;


                BinaryFormatter bf = new BinaryFormatter();

                FileStream archivo = File.OpenWrite("Conf.cfg");

                bf.Serialize(archivo, c);
                archivo.Close();
                Console.ReadLine();
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
            foreach (ToolStripMenuItem tsi in tsic)
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
            if (res == DialogResult.OK)
            {
                textBox1.Font = fd.Font;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            contLetra = 0;
            contPalabra = 0;
            contFrase = 0;
}

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void colorDeFondoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();

            DialogResult res = new DialogResult();
            res = cd.ShowDialog();
            if (res == DialogResult.OK)
            {
                if (((ToolStripMenuItem)sender) == colorDeFondoToolStripMenuItem)
                {
                    textBox1.BackColor = cd.Color;
                }
                else
                {
                    textBox1.ForeColor = cd.Color;
                }
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Return)
            {

            }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {

            contLetra = textBox1.Text.Length;
            String[] aux = textBox1.Text.Split(' ');
            for (int i = 0; i < aux.Length; i++)
            {
                if (!aux[i].Trim().Equals(",") && !aux[i].Trim().Equals(".") && !(aux[i].Trim().Length == 0))
                {
                    contPalabra++;
                }
            }
            aux = textBox1.Text.Split('.');
            for (int i = 0; i < aux.Length; i++)
            {
                if (!aux[i].Trim().Equals(",") && !aux[i].Trim().Equals(".") && !(aux[i].Trim().Length == 0))
                {
                    contFrase++;
                }
            }

            tt.SetToolTip(this.textBox1, "caracteres: " + contLetra + ", palabras: " + contPalabra + ", frases: " + contFrase);

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = !SeBorra();
            base.OnFormClosing(e);
      
        }
    }
}
