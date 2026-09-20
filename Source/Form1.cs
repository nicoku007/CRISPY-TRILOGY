using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace Crispytrilogy
{
    public partial class Form1 : Form
    {
        private const string MasterServerUrl = "https://master.chocolate-doom.org/";

        private System.Drawing.Image imagen1;
        private System.Drawing.Image imagen2;
        private System.Drawing.Image imagen3;
        private System.Drawing.Image imagen4;

        private int _lastFormSize;

        private System.Windows.Forms.Timer resizeTimer;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
               
                cp.ExStyle |= 0x02000000; 
                return cp;
            }
        }


        public Form1()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            InitializeComponent();
            ConfigurarRichTextBox();
            this.DoubleBuffered = true;

            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);


            listBox1.SelectionMode = SelectionMode.MultiExtended;

            listBox1.DisplayMember = "Name";
            listBox1.ValueMember = "FullName";


            listBox2.SelectionMode = SelectionMode.MultiExtended;

            listBox2.DisplayMember = "Name";
            listBox2.ValueMember = "FullName";


            listBox3.SelectionMode = SelectionMode.MultiExtended;

            listBox3.DisplayMember = "Name";
            listBox3.ValueMember = "FullName";

            this.Resize += new EventHandler(Form1_Resize);
            _lastFormSize = GetFormArea(this.Size);
            resizeTimer = new System.Windows.Forms.Timer();
            resizeTimer.Interval = 100;
            resizeTimer.Tick += ResizeTimer_Tick;

            imagen1 = Properties.Resources.heretic;
            imagen2 = Properties.Resources.hexen1;
            imagen3 = Properties.Resources.TRILOGY2;
            imagen4 = Properties.Resources.strife;

            button1.MouseEnter += button1_MouseEnter;
            button1.MouseLeave += button1_MouseLeave;
            button2.MouseEnter += button2_MouseEnter;
            button2.MouseLeave += button2_MouseLeave;
        
        
        
        }
        
        public class Item
        {
            public string Name { get; set; }
            public string Value { get; set; }

            public Item(string name, string value)
            {
                Name = name;
                Value = value;
            }

            public override string ToString()
            {
                return Name;
            }
        }

        private void ConfigurarRichTextBox()
        {

            richTextBox1.Font = new Font("Consolas", 12, FontStyle.Regular);
            richTextBox1.WordWrap = false;
            richTextBox1.ScrollBars = RichTextBoxScrollBars.Both;
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }
        
        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {

            string assemblyName = new AssemblyName(args.Name).Name + ".dll";


            string resourceName = $"{typeof(Form1).Namespace}.{assemblyName}";

            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                if (stream != null)
                {
                    byte[] assemblyData = new byte[stream.Length];
                    stream.Read(assemblyData, 0, assemblyData.Length);
                    return Assembly.Load(assemblyData);
                }
            }
            return null;
        }

        private void stringParamAppend(ref string baseString, string param)
        {
            baseString += (string.IsNullOrEmpty(baseString) ? "" : " ") + param;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            wizardControl1.SelectedIndex = 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            wizardControl1.SelectedIndex = 2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            wizardControl1.SelectedIndex = 3;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            wizardControl1.SelectedIndex = 0;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            wizardControl1.SelectedIndex = 4;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            wizardControl1.SelectedIndex = 0;
        }
        
        private void button6_Click(object sender, EventArgs e)
        {
            wizardControl1.SelectedIndex = 0;
        }
       
        private void button15_Click(object sender, EventArgs e)
        {
            wizardControl1.SelectedIndex = 0;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox6.Items.Add(new Item(" -iwad heretic.wad", "Heretic: Shadow of the Serpent Riders"));
            comboBox6.Items.Add(new Item(" -iwad heretic.wad -merge heretic_fr.wad", "Heretic: Faith Renewed"));
            comboBox22.Items.Add(new Item("-skill 1", "Thou needeth a wet-nurse"));
            comboBox22.Items.Add(new Item("-skill 2", "Yellowbellies-r-us"));
            comboBox22.Items.Add(new Item("-skill 3", "Bringest them oneth"));
            comboBox22.Items.Add(new Item("-skill 4", "Thou art a smite-meister"));
            comboBox22.Items.Add(new Item("-skill 5", "Black plague possesses thee"));
            comboBox23.Items.Add(new Item("-hhever 1.0", "1.0"));
            comboBox23.Items.Add(new Item("-hhever 1.2", "1.2"));
            comboBox23.Items.Add(new Item("-hhever 1.3", "1.3"));

            comboBox26.Items.Add(new Item(" -nodes 1", "1"));
            comboBox26.Items.Add(new Item(" -nodes 2", "2"));
            comboBox26.Items.Add(new Item(" -nodes 3", "3"));
            comboBox26.Items.Add(new Item(" -nodes 4", "4"));
            comboBox24.Items.Add(new Item(" -server -coop", "Cooperative"));
            comboBox24.Items.Add(new Item(" -server -deathmatch", "Deathmatch"));
          
            comboBox27.Items.Add(new Item(" -iwad HEXEN.WAD", "Hexen: Beyond Heretic"));
            comboBox27.Items.Add(new Item(" -iwad HEXEN.WAD -merge HEXDD.WAD", "Deathkings of the Dark Citadel"));
            comboBox28.Items.Add(new Item(" -class 0", "Fighter"));
            comboBox28.Items.Add(new Item(" -class 1", "Cleric"));
            comboBox28.Items.Add(new Item(" -class 2", "Mage"));
            comboBox31.Items.Add(new Item(" -server -coop", "Cooperative"));
            comboBox31.Items.Add(new Item(" -server -deathmatch", "Deathmatch"));
          
            comboBox33.Items.Add(new Item(" -nodes 1", "1"));
            comboBox33.Items.Add(new Item(" -nodes 2", "2"));
            comboBox33.Items.Add(new Item(" -nodes 3", "3"));
            comboBox33.Items.Add(new Item(" -nodes 4", "4"));
            comboBox8.Items.Add(new Item(" -nodes 1", "1"));
            comboBox8.Items.Add(new Item(" -nodes 2", "2"));
            comboBox8.Items.Add(new Item(" -nodes 3", "3"));
            comboBox8.Items.Add(new Item(" -nodes 4", "4"));
            comboBox1.Items.Add(new Item(" -wad STRIFE1.WAD", "Strife: Quest for the Sigil"));
            comboBox2.Items.Add(new Item(" -warp 01", "MAP01: Sanctuary"));
            comboBox2.Items.Add(new Item(" -warp 02", "MAP02: Town"));
            comboBox2.Items.Add(new Item(" -warp 03", "MAP03: Front Base"));
            comboBox2.Items.Add(new Item(" -warp 04", "MAP04: Power Station"));
            comboBox2.Items.Add(new Item(" -warp 05", "MAP05: Prison"));
            comboBox2.Items.Add(new Item(" -warp 06", "MAP06: Sewer"));
            comboBox2.Items.Add(new Item(" -warp 07", "MAP07: Castle"));
            comboBox2.Items.Add(new Item(" -warp 08", "MAP08: Audience Chamber"));
            comboBox2.Items.Add(new Item(" -warp 09", "MAP09: Castle: Programmer's Keep"));
            comboBox2.Items.Add(new Item(" -warp 10", "MAP10: New Front Base"));
            comboBox2.Items.Add(new Item(" -warp 11", "MAP11: Borderlands"));
            comboBox2.Items.Add(new Item(" -warp 12", "MAP12: The Temple of the Oracle"));
            comboBox2.Items.Add(new Item(" -warp 13", "MAP13: Catacombs"));
            comboBox2.Items.Add(new Item(" -warp 14", "MAP14: Mines"));
            comboBox2.Items.Add(new Item(" -warp 15", "MAP15: Fortress: Administration"));
            comboBox2.Items.Add(new Item(" -warp 16", "MAP16: Fortress: Bishop's Tower"));
            comboBox2.Items.Add(new Item(" -warp 17", "MAP17: Fortress: The Bailey"));
            comboBox2.Items.Add(new Item(" -warp 18", "MAP18: Fortress: Stores"));
            comboBox2.Items.Add(new Item(" -warp 19", "MAP19: Fortress: Security Complex"));
            comboBox2.Items.Add(new Item(" -warp 20", "MAP20: Factory: Receiving"));
            comboBox2.Items.Add(new Item(" -warp 21", "MAP21: Factory: Manufacturing"));
            comboBox2.Items.Add(new Item(" -warp 22", "MAP22: Factory: Forge"));
            comboBox2.Items.Add(new Item(" -warp 23", "MAP23: Order Commons"));
            comboBox2.Items.Add(new Item(" -warp 24", "MAP24: Factory: Conversion Chapel"));
            comboBox2.Items.Add(new Item(" -warp 25", "MAP25: Catacombs: Ruined Temple"));
            comboBox2.Items.Add(new Item(" -warp 26", "MAP26: Proving Grounds"));
            comboBox2.Items.Add(new Item(" -warp 27", "MAP27: The Lab"));
            comboBox2.Items.Add(new Item(" -warp 28", "MAP28: Alien Ship"));
            comboBox2.Items.Add(new Item(" -warp 29", "MAP29: Entity's Lair"));
            comboBox2.Items.Add(new Item(" -warp 30", "MAP30: Abandoned Front Base"));
            comboBox2.Items.Add(new Item(" -warp 31", "MAP31: Training Facility"));
            comboBox2.Items.Add(new Item(" -warp 32", "MAP32: Sanctuary (demo)"));
            comboBox2.Items.Add(new Item(" -warp 33", "MAP33: Town (demo)"));
            comboBox2.Items.Add(new Item(" -warp 34", "MAP34: Movement Base (demo)"));
            comboBox3.Items.Add(new Item(" -skill 1", "Training"));
            comboBox3.Items.Add(new Item(" -skill 2", "Rookie"));
            comboBox3.Items.Add(new Item(" -skill 3", "Veteran"));
            comboBox3.Items.Add(new Item(" -skill 4", "Elite"));
            comboBox3.Items.Add(new Item(" -skill 5", "Bloodbath"));
            comboBox5.Items.Add(new Item(" -server -deathmatch", "Normal Deathmatch"));
            comboBox5.Items.Add(new Item("-server -altdeath", "Items respawn"));
            comboBox4.Items.Add(new Item(" -turbo 10x%", "10"));
            comboBox4.Items.Add(new Item(" -turbo 20x%", "20"));
            comboBox4.Items.Add(new Item(" -turbo 30x%", "30"));
            comboBox4.Items.Add(new Item(" -turbo 40x%", "40"));
            comboBox4.Items.Add(new Item(" -turbo 50x%", "50"));
            comboBox4.Items.Add(new Item(" -turbo 60x%", "60"));
            comboBox4.Items.Add(new Item(" -turbo 70x%", "70"));
            comboBox4.Items.Add(new Item(" -turbo 80x%", "80"));
            comboBox4.Items.Add(new Item(" -turbo 90x%", "90"));
            comboBox4.Items.Add(new Item(" -turbo 100x%", "100"));
            comboBox4.Items.Add(new Item(" -turbo 110x%", "110"));
            comboBox4.Items.Add(new Item(" -turbo 120x%", "120"));
            comboBox4.Items.Add(new Item(" -turbo 130x%", "130"));
            comboBox4.Items.Add(new Item(" -turbo 140x%", "140"));
            comboBox4.Items.Add(new Item(" -turbo 150x%", "150"));
            comboBox4.Items.Add(new Item(" -turbo 160x%", "160"));
            comboBox4.Items.Add(new Item(" -turbo 170x%", "170"));
            comboBox4.Items.Add(new Item(" -turbo 180x%", "180"));
            comboBox4.Items.Add(new Item(" -turbo 190x%", "190"));
            comboBox4.Items.Add(new Item(" -turbo 200x%", "200"));
            comboBox4.Items.Add(new Item(" -turbo 210x%", "210"));
            comboBox4.Items.Add(new Item(" -turbo 220x%", "220"));
            comboBox4.Items.Add(new Item(" -turbo 230x%", "230"));
            comboBox4.Items.Add(new Item(" -turbo 240x%", "240"));
            comboBox4.Items.Add(new Item(" -turbo 250x%", "250"));
            comboBox4.Items.Add(new Item(" -turbo 260x%", "260"));
            comboBox4.Items.Add(new Item(" -turbo 270x%", "270"));
            comboBox4.Items.Add(new Item(" -turbo 280x%", "280"));
            comboBox4.Items.Add(new Item(" -turbo 290x%", "290"));
            comboBox4.Items.Add(new Item(" -turbo 300x%", "300"));
            comboBox4.Items.Add(new Item(" -turbo 310x%", "310"));
            comboBox4.Items.Add(new Item(" -turbo 320x%", "320"));
            comboBox4.Items.Add(new Item(" -turbo 330x%", "330"));
            comboBox4.Items.Add(new Item(" -turbo 340x%", "340"));
            comboBox4.Items.Add(new Item(" -turbo 350x%", "350"));
            comboBox4.Items.Add(new Item(" -turbo 360x%", "360"));
            comboBox4.Items.Add(new Item(" -turbo 370x%", "370"));
            comboBox4.Items.Add(new Item(" -turbo 380x%", "380"));
            comboBox4.Items.Add(new Item(" -turbo 390x%", "390"));
            comboBox4.Items.Add(new Item(" -turbo 400x%", "400"));

          

            for (int i = 1; i <= 60; i++)
            {
                comboBox25.Items.Add(new Item($" -timer {i}", i.ToString()));
            }

            for (int i = 1; i <= 60; i++)
            {
                comboBox32.Items.Add(new Item($" -timer {i}", i.ToString()));
            }

            for (int i = 1; i <= 60; i++)
            {
                comboBox7.Items.Add(new Item($" -timer {i}", i.ToString()));
            }


            comboBox1.DisplayMember = "value";
            comboBox2.DisplayMember = "value";
            comboBox3.DisplayMember = "value";
            comboBox4.DisplayMember = "value";
            comboBox5.DisplayMember = "value";
            comboBox6.DisplayMember = "value";
            comboBox7.DisplayMember = "value";
            comboBox8.DisplayMember = "value";
            comboBox22.DisplayMember = "value";
            comboBox23.DisplayMember = "value";
            comboBox24.DisplayMember = "value";
            comboBox25.DisplayMember = "value";
            comboBox26.DisplayMember = "value";
            comboBox27.DisplayMember = "value";
            comboBox28.DisplayMember = "value";
            comboBox31.DisplayMember = "value";
            comboBox32.DisplayMember = "value";
            comboBox33.DisplayMember = "value";


            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 2;
            comboBox6.SelectedIndex = 0;
           


           
            comboBox22.SelectedIndex = 2;
            comboBox27.SelectedIndex = 0;

            comboBox28.SelectedIndex = 0;


            this.listBox1.Visible = false;
            this.listBox2.Visible = false;

            this.comboBox4.Enabled = false;
            this.comboBox7.Visible = false;
            this.comboBox8.Visible = false;
            this.textBox16.Enabled = false;
            this.textBox17.Visible = false;
            this.textBox20.Visible = false;
            this.textBox21.Visible = false;
            this.textBox22.Visible = false;
            this.textBox23.Visible = false;



            this.textBox42.Visible = false;
            this.textBox43.Visible = false;
            this.textBox44.Visible = false;
            this.textBox45.Visible = false;
            this.textBox30.Visible = false;
            this.textBox13.Enabled = false;
            this.textBox50.Visible = false;
            this.textBox51.Visible = false;
            this.textBox52.Visible = false;
            this.textBox53.Visible = false;
            this.textBox54.Visible = false;
            this.listBox3.Visible = false;
            this.textBox55.Enabled = false;



          
            this.comboBox23.Enabled = false;
            this.comboBox24.Enabled = false;
            this.comboBox25.Visible = false;
            this.comboBox26.Visible = false;

            this.comboBox31.Enabled = false;
            this.comboBox32.Visible = false;
            this.comboBox33.Visible = false;
            this.button39.Enabled = false;
           


        }

        private int GetFormArea(Size size)
        {
            return size.Width;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
           
            if (resizeTimer != null)
            {
                resizeTimer.Stop();
                resizeTimer.Start();
            }
        }

        private void ResizeTimer_Tick(object sender, EventArgs e)
        {

            resizeTimer.Stop();

            float scaleFactor = (float)GetFormArea(this.Size) / (float)_lastFormSize;


            if (System.Math.Abs(scaleFactor - 1.0f) < 0.01f) return;


            ResizeFont(this.Controls, scaleFactor);

            _lastFormSize = GetFormArea(this.Size);


           
        }

        private void ResizeFont(Control.ControlCollection coll, float scaleFactor)
        {
            foreach (Control c in coll)
            {
                if (c.HasChildren)
                {
                    ResizeFont(c.Controls, scaleFactor);
                }
                else
                {
                    if (true)
                    {

                        c.Font = new Font(c.Font.FontFamily.Name, c.Font.Size * scaleFactor);
                    }
                }
            }
        }

  private string ConstruirLineaDeComandosRedYDemo2()
{
    string parametrosFinales = "";


    if (checkBox50.Checked) stringParamAppend(ref parametrosFinales, "-respawn");

    if (checkBox51.Checked) stringParamAppend(ref parametrosFinales, "-nomonsters");


    if (checkBox61.Checked) stringParamAppend(ref parametrosFinales, "-autojoin");
    if (checkBox62.Checked) stringParamAppend(ref parametrosFinales, "-privateserver");

    string ipServidor = textBox45.Text.Trim();
    if (checkBox63.Checked && !string.IsNullOrEmpty(ipServidor))
    {
        stringParamAppend(ref parametrosFinales, $"-connect {ipServidor}");
    }


    string demoNameClean = textBox13.Text.Trim().Replace(" ", "-");
    if (!string.IsNullOrEmpty(demoNameClean))
    {
        if (checkBox54.Checked) stringParamAppend(ref parametrosFinales, $"-record {demoNameClean}");
        if (checkBox55.Checked) stringParamAppend(ref parametrosFinales, $"-playdemo {demoNameClean}");
    }


    string serverName = textBox42.Text.Trim();
    if (checkBox58.Checked && !string.IsNullOrEmpty(serverName))
    {
        stringParamAppend(ref parametrosFinales, $"-servername \"{serverName}\"");
    }


    string puertoServidor = textBox44.Text.Trim();
    if (checkBox60.Checked && !string.IsNullOrEmpty(puertoServidor))
    {
        stringParamAppend(ref parametrosFinales, $"-port {puertoServidor}");
    }



    if (!string.IsNullOrWhiteSpace(textBox30.Text))
    {
        stringParamAppend(ref parametrosFinales, textBox30.Text.Trim());
    }


    return parametrosFinales.Trim();
}

        private void button32_Click(object sender, EventArgs e)
        {

            ActualizarNombreJugador2(textBox43.Text.Trim());

            string seccionMerge = "";
            string seccionDeh = "";


            if (checkBox64.Checked)
            {
                if (listBox2.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Please select at least one file from the list.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string argumentosWad = "";
                string argumentosDeh = "";

                foreach (FileInfo archivoSeleccionado in listBox2.SelectedItems)
                {
                    string extension = archivoSeleccionado.Extension.ToLower();

                    if (extension == ".wad")
                    {
                        argumentosWad += $" \"{archivoSeleccionado.FullName}\"";
                    }

                    else if (extension == ".hhe" || extension == ".deh" || extension == ".bex")
                    {
                        argumentosDeh += $" \"{archivoSeleccionado.FullName}\"";
                    }
                }

                if (!string.IsNullOrEmpty(argumentosWad)) seccionMerge = $"-merge {argumentosWad.Trim()}";
                if (!string.IsNullOrEmpty(argumentosDeh)) seccionDeh = $"-deh {argumentosDeh.Trim()}";
            }


            string param2 = comboBox22.SelectedItem?.ToString() ?? "";
            string param3 = comboBox6.SelectedItem?.ToString() ?? "";
            string param5 = comboBox21.SelectedItem?.ToString() ?? "";

            string param7 = comboBox24.SelectedItem?.ToString() ?? "";
            string param8 = comboBox23.SelectedItem?.ToString() ?? "";
            string param9 = comboBox25.SelectedItem?.ToString() ?? "";
            string param10 = comboBox26.SelectedItem?.ToString() ?? "";

            string argument2 = $"{param2} {param3} {param5} {param7} {param8} {param9} {param10}".Trim();


            string parametrosUnificados = ConstruirLineaDeComandosRedYDemo2();


            string argumentosFinales = $"{seccionMerge} {seccionDeh} {argument2} {parametrosUnificados}".Trim();


            try
            {

                string rutaCarpetaChocoDoom = System.IO.Path.Combine(Application.StartupPath, "crispy-heretic");
                string exeDoomUnificado = System.IO.Path.Combine(rutaCarpetaChocoDoom, "crispy-heretic.exe");

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = exeDoomUnificado,
                    Arguments = argumentosFinales,
                    UseShellExecute = false,
                    CreateNoWindow = true,


                    WorkingDirectory = rutaCarpetaChocoDoom
                };

                if (!checkBox64.Checked)
                {
                    psi.RedirectStandardOutput = true;
                }

                using (Process process = Process.Start(psi))
                {
                    if (!checkBox64.Checked && process != null)
                    {
                        string output = process.StandardOutput.ReadToEnd();
                        process.WaitForExit();
                    }
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Error starting crispy Doom: The system cannot find 'crispy-heretic.exe' inside the 'crispy-heretic' folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarNombreJugador2(string nuevoNombre)
{
    if (string.IsNullOrEmpty(nuevoNombre)) return;


    string rutaLocalConfig = Path.Combine(Application.StartupPath, "crispy-heretic", "crispy-heretic.cfg");


    if (File.Exists(rutaLocalConfig))
    {
        try
        {
            string[] lineas = File.ReadAllLines(rutaLocalConfig);

            for (int i = 0; i < lineas.Length; i++)
            {
                if (lineas[i].StartsWith("player_name"))
                {

                    lineas[i] = $"player_name  \"{nuevoNombre}\"";
                    break;
                }
            }


            File.WriteAllLines(rutaLocalConfig, lineas);
        }
        catch
        {

        }
    }
}

private void button36_Click(object sender, EventArgs e)
{
    using (OpenFileDialog ofd = new OpenFileDialog())
    {

        ofd.Filter = "Archivos Heretic (*.wad;*.hhe;*.deh)|*.wad;*.hhe;*.deh|Archivos WAD (*.wad)|*.wad|Parches Heretic (*.hhe;*.deh)|*.hhe;*.deh|Todos los archivos (*.*)|*.*";
        ofd.Multiselect = true;
        ofd.Title = "Select your Heretic WAD and HHE files";

        if (ofd.ShowDialog() == DialogResult.OK)
        {
            listBox2.Items.Clear();

            foreach (string rutaCompleta in ofd.FileNames)
            {
                FileInfo archivo = new FileInfo(rutaCompleta);
                listBox2.Items.Add(archivo);
            }
        }
    }
}

private void button35_Click(object sender, EventArgs e)
{

    openFileDialog1.Filter = "Archivos LMP|*.lmp";
    openFileDialog1.Title = "Select LMP file";
    openFileDialog1.FileName = "";


    if (openFileDialog1.ShowDialog() == DialogResult.OK)
    {

        string rutaCompleta = openFileDialog1.FileName;


        string nombreArchivo = Path.GetFileName(rutaCompleta);


        textBox13.Text = nombreArchivo;
    }
}

private void button34_Click(object sender, EventArgs e)
{

    string rutaCarpetaChocoDoom = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "crispy-heretic");
    string setupPath = System.IO.Path.Combine(rutaCarpetaChocoDoom, "crispy-heretic-setup.exe");


    if (System.IO.File.Exists(setupPath))
    {
        try
        {

            System.Diagnostics.ProcessStartInfo psiSetup = new System.Diagnostics.ProcessStartInfo
            {
                FileName = setupPath,
                UseShellExecute = false,
                CreateNoWindow = false,


                WorkingDirectory = rutaCarpetaChocoDoom
            };

            System.Diagnostics.Process.Start(psiSetup);
        }
        catch (System.Exception ex)
        {
            System.Windows.Forms.MessageBox.Show("Could not launch the setup executable: " + ex.Message, "Execution Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        }
    }
    else
    {

        System.Windows.Forms.MessageBox.Show("The file 'crispy-heretic-setup.exe' was not found inside the 'crispy-heretic' directory.", "File Missing", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
    }
}

private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
{
    int indiceSeleccionado = comboBox6.SelectedIndex;

    switch (indiceSeleccionado)
    {
        case 0:
            {



                List<Item> listaCityoftheDamned = new List<Item>
                    {
                        new Item("-iwad HERETIC.WAD -warp 1 1", "E1M1: The Docks"),
                        new Item("-iwad HERETIC.WAD -warp 1 2", "E1M2: The Dungeons"),
                        new Item("-iwad HERETIC.WAD -warp 1 3", "E1M3: The Gatehouse"),
                        new Item("-iwad HERETIC.WAD -warp 1 4", "E1M4: The Guard Tower"),
                        new Item("-iwad HERETIC.WAD -warp 1 5", "E1M5: The Citadel"),
                        new Item("-iwad HERETIC.WAD -warp 1 6", "E1M6: The Cathedral"),
                        new Item("-iwad HERETIC.WAD -warp 1 7", "E1M7: The Crypts"),
                        new Item("-iwad HERETIC.WAD -warp 1 8", "E1M8: Hell's Maw"),
                        new Item("-iwad HERETIC.WAD -warp 1 9", "E1M9: The Graveyard"),
                        new Item("-iwad HERETIC.WAD -warp 2 1", "E2M1: The Crater"),
                        new Item("-iwad HERETIC.WAD -warp 2 2", "E2M2: The Lava Pits"),
                        new Item("-iwad HERETIC.WAD -warp 2 3", "E2M3: The River of Fire"),
                        new Item("-iwad HERETIC.WAD -warp 2 4", "E2M4: The Ice Grotto"),
                        new Item("-iwad HERETIC.WAD -warp 2 5", "E2M5: The Catacombs"),
                        new Item("-iwad HERETIC.WAD -warp 2 6", "E2M6: The Labyrinth"),
                        new Item("-iwad HERETIC.WAD -warp 2 7", "E2M7: The Great Hall"),
                        new Item("-iwad HERETIC.WAD -warp 2 8", "E2M8: The Portals of Chaos"),
                        new Item("-iwad HERETIC.WAD -warp 2 9", "E2M9: The Glacier"),
                        new Item("-iwad HERETIC.WAD -warp 3 1", "E3M1: The Storehouse"),
                        new Item("-iwad HERETIC.WAD -warp 3 2", "E3M2: The Cesspool"),
                        new Item("-iwad HERETIC.WAD -warp 3 3", "E3M3: The Confluence"),
                        new Item("-iwad HERETIC.WAD -warp 3 4", "E3M4: The Azure Fortress"),
                        new Item("-iwad HERETIC.WAD -warp 3 5", "E3M5: The Ophidian Lair"),
                        new Item("-iwad HERETIC.WAD -warp 3 6", "E3M6: The Halls of Fear"),
                        new Item("-iwad HERETIC.WAD -warp 3 7", "E3M7: The Chasm"),
                        new Item("-iwad HERETIC.WAD -warp 3 8", "E3M8: D'Sparil's Keep"),
                        new Item("-iwad HERETIC.WAD -warp 3 9", "E3M9: The Aquifer"),
                        new Item("-iwad HERETIC.WAD -warp 4 1", "E4M1: Catafalque"),
                        new Item("-iwad HERETIC.WAD -warp 4 2", "E4M2: Blockhouse"),
                        new Item("-iwad HERETIC.WAD -warp 4 3", "E4M3: Ambulatory"),
                        new Item("-iwad HERETIC.WAD -warp 4 4", "E4M4: Sepulcher"),
                        new Item("-iwad HERETIC.WAD -warp 4 5", "E4M5: Great Stair"),
                        new Item("-iwad HERETIC.WAD -warp 4 6", "E4M6: Halls of the Apostate"),
                        new Item("-iwad HERETIC.WAD -warp 4 7", "E4M7: Ramparts of Perdition"),
                        new Item("-iwad HERETIC.WAD -warp 4 8", "E4M8: Shattered Bridge"),
                        new Item("-iwad HERETIC.WAD -warp 4 9", "E4M9: Mausoleum"),
                        new Item("-iwad HERETIC.WAD -warp 5 1", "E5M1: Ochre Cliffs"),
                        new Item("-iwad HERETIC.WAD -warp 5 2", "E5M2: Rapids"),
                        new Item("-iwad HERETIC.WAD -warp 5 3", "E5M3: Quay"),
                        new Item("-iwad HERETIC.WAD -warp 5 4", "E5M4: Courtyard"),
                        new Item("-iwad HERETIC.WAD -warp 5 5", "E5M5: Hydratyr"),
                        new Item("-iwad HERETIC.WAD -warp 5 6", "E5M6: Colonnade"),
                        new Item("-iwad HERETIC.WAD -warp 5 7", "E5M7: Foetid Manse"),
                        new Item("-iwad HERETIC.WAD -warp 5 8", "E5M8: Field of Judgement"),
                        new Item("-iwad HERETIC.WAD -warp 5 9", "E5M9: Skein of D'Sparil"),
                        new Item("-iwad HERETIC.WAD -warp 6 1", "E6M1: known as \"Raven's Lair\""),
                        new Item("-iwad HERETIC.WAD -warp 6 2", "E6M2: known as \"The Water Shrine\""),
                        new Item("-iwad HERETIC.WAD -warp 6 3", "E6M3: known as \"American's Legacy\""),

                    }
                    ;

                comboBox21.DataSource = null;
                comboBox21.DataSource = listaCityoftheDamned;
                comboBox21.DisplayMember = "Value";

                comboBox21.SelectedIndex = 0;


                break;
            }

        case 1:
            {


                List<Item> listaHellsMaw = new List<Item>
                    {
                        new Item("-iwad HERETIC.WAD -warp 1 1", "E1M1: The Plaza"),
                        new Item("-iwad HERETIC.WAD -warp 1 2", "E1M2: Staith Town"),
                        new Item("-iwad HERETIC.WAD -warp 1 3", "E1M3: Crimson Palace"),
                        new Item("-iwad HERETIC.WAD -warp 1 4", "E1M4: Watergate"),
                        new Item("-iwad HERETIC.WAD -warp 1 5", "E1M5: Correctional Temple"),
                        new Item("-iwad HERETIC.WAD -warp 1 6", "E1M6: Foundry"),
                        new Item("-iwad HERETIC.WAD -warp 1 7", "E1M7: Eye of D'Sparil"),
                        new Item("-iwad HERETIC.WAD -warp 1 8", "E1M8: The Grand Chapel"),
                        new Item("-iwad HERETIC.WAD -warp 1 9", "E1M9: Pocket Plane"),
                    };

                comboBox21.DataSource = null;
                comboBox21.DataSource = listaHellsMaw;
                comboBox21.DisplayMember = "value";

                comboBox21.SelectedIndex = 0;


                break;
            }
         }
      }

private void checkBox50_CheckedChanged(object sender, EventArgs e)
{
    if (checkBox50.Checked)
    {

        checkBox51.Checked = false;
        checkBox33.Checked = false;
        checkBox61.Checked = false;
        checkBox63.Checked = false;

    }
}

private void checkBox51_CheckedChanged(object sender, EventArgs e)
{
    if (checkBox51.Checked)
    {
        checkBox61.Checked = false;

        checkBox63.Checked = false;
       
        checkBox50.Checked = false;

        checkBox33.Checked = false;
        comboBox22.Enabled = false;
        comboBox22.SelectedIndex = -1;
    }
    else
    {
        comboBox22.Enabled = true;
        comboBox22.SelectedIndex = 2;
    }
}

private void checkBox52_CheckedChanged(object sender, EventArgs e)
{
    if (checkBox52.Checked)
    {
        comboBox23.Enabled = true;
        comboBox23.SelectedIndex = 0;
    }
    else
    {
        comboBox23.Enabled = false;
        comboBox23.SelectedIndex = -1;
    }
}



private void checkBox54_CheckedChanged(object sender, EventArgs e)
{
    if (checkBox54.Checked)
    {
        checkBox55.Checked = false;
        textBox13.Enabled = true;
    }
    else
    {
        textBox13.Enabled = false;
    }
}

private void checkBox55_CheckedChanged(object sender, EventArgs e)
{
    if (checkBox55.Checked)
    {
        checkBox50.Checked = false;
        checkBox51.Checked = false;
        checkBox52.Checked = false;

        checkBox57.Checked = false;
        checkBox58.Checked = false;
        checkBox60.Checked = false;
        checkBox61.Checked = false;
        checkBox62.Checked = false;
        checkBox63.Checked = false;
        checkBox54.Checked = false;


        checkBox50.Enabled = false;
        checkBox51.Enabled = false;
        checkBox52.Enabled = false;

        checkBox57.Enabled = false;
        checkBox58.Enabled = false;
        checkBox60.Enabled = false;
        checkBox61.Enabled = false;
        checkBox62.Enabled = false;
        checkBox63.Enabled = false;


       
        comboBox21.SelectedIndex = -1;


        textBox13.Enabled = true;
    }
       else
       {
        if (comboBox6.SelectedIndex == 0)
        {
          comboBox21.SelectedIndex = 0;
          comboBox21.Enabled = true;


        }
         if (comboBox6.SelectedIndex == 1)
         {
          comboBox21.SelectedIndex = 0;
          comboBox21.Enabled = true;


          }


        comboBox21.SelectedIndex = 0;

        checkBox50.Enabled = true;
        checkBox51.Enabled = true;
        checkBox52.Enabled = true;

        checkBox57.Enabled = true;
        checkBox58.Enabled = true;
        checkBox60.Enabled = true;
        checkBox61.Enabled = true;
        checkBox62.Enabled = true;
        checkBox63.Enabled = true;



        textBox13.Enabled = false;
    }
}

private void checkBox33_CheckedChanged(object sender, EventArgs e)
{
    if (checkBox33.Checked)
    {


        checkBox50.Checked = false;
        checkBox51.Checked = false;
        checkBox52.Checked = false;
        checkBox61.Checked = false;
        checkBox62.Checked = false;
        checkBox63.Checked = false;
        comboBox22.Enabled = false;
        comboBox22.SelectedIndex = -1;
       
       

        comboBox21.Enabled = false;
        comboBox21.SelectedIndex = -1;

    }
    else
    {
    if (comboBox6.SelectedIndex == 0)
    {
       comboBox21.SelectedIndex = 0;
       comboBox21.Enabled = true;


    }
     if (comboBox6.SelectedIndex == 1)
     {
         comboBox21.SelectedIndex = 0;
         comboBox21.Enabled = true;


     }


        comboBox22.Enabled = true;
        comboBox22.SelectedIndex = 2;
       

        comboBox21.Enabled = true;
        comboBox21.SelectedIndex = 0;
    }
}


private void checkBox57_CheckedChanged(object sender, EventArgs e)
{
    if (checkBox57.Checked)
    {
        comboBox24.Enabled = true;
        comboBox25.Visible = true;
        comboBox26.Visible = true;
        comboBox24.SelectedIndex = 0;

        checkBox61.Checked = false;

        checkBox63.Checked = false;
    }
    else
    {
        comboBox24.Enabled = false;
        comboBox25.Visible = false;
        comboBox26.Visible = false;
        comboBox24.SelectedIndex = -1;
        comboBox25.SelectedIndex = -1;
        comboBox26.SelectedIndex = -1;
    }
}

private void checkBox58_CheckedChanged(object sender, EventArgs e)
{
    if (checkBox58.Checked)
    {

        checkBox61.Checked = false;

        checkBox63.Checked = false;
        textBox42.Visible = true;
        textBox42.Text = "your server name";
    }
    else
    {

        textBox42.Visible = false;
        textBox42.Text = "";
    }
}

private void checkBox59_CheckedChanged(object sender, EventArgs e)
{
    if (checkBox59.Checked)
    {

        textBox43.Visible = true;
        textBox43.Text = "your player name";

    }
    else
    {
        textBox43.Visible = false;
        textBox43.Text = "";
    }
}

private void checkBox60_CheckedChanged(object sender, EventArgs e)
{
    if (checkBox60.Checked)
    {
        textBox44.Visible = true;
        textBox44.Text = "2342";
    }
    else
    {
        textBox44.Visible = false;
        textBox44.Text = "";

    }
}

private void checkBox61_CheckedChanged(object sender, EventArgs e)
{
    if (checkBox61.Checked)
    {
        checkBox51.Checked = false;
        checkBox33.Checked = false;
        checkBox57.Checked = false;
        checkBox58.Checked = false;
        checkBox62.Checked = false;
        checkBox63.Checked = false;
        checkBox56.Checked = false;
        comboBox22.Enabled = false;
        comboBox22.SelectedIndex = -1;
        

        comboBox21.Enabled = false;
        comboBox21.SelectedIndex = -1;

    }
    else
    {
    if (comboBox6.SelectedIndex == 0)
    {
       comboBox21.SelectedIndex = 0;
       comboBox21.Enabled = true;


    }
    if (comboBox6.SelectedIndex == 1)
    {
        comboBox21.SelectedIndex = 0;
        comboBox21.Enabled = true;


    }


        comboBox22.Enabled = true;
        comboBox22.SelectedIndex = 2;
      

        comboBox21.Enabled = true;
        comboBox21.SelectedIndex = 0;

    }
}

private void checkBox62_CheckedChanged(object sender, EventArgs e)
{
    if (checkBox62.Checked)
    {
        checkBox61.Checked = false;
        checkBox63.Checked = false;

    }
}

private void checkBox63_CheckedChanged(object sender, EventArgs e)
{
    if (checkBox63.Checked)
    {
        checkBox51.Checked = false;
        checkBox33.Checked = false;
        checkBox57.Checked = false;
        checkBox58.Checked = false;
        checkBox62.Checked = false;
        checkBox61.Checked = false;
        checkBox56.Checked = false;
        comboBox22.Enabled = false;
        comboBox22.SelectedIndex = -1;
       

        comboBox21.Enabled = false;
        comboBox21.SelectedIndex = -1;

        textBox45.Visible = true;


    }
     else
     {
     if (comboBox6.SelectedIndex == 0)
     {
        comboBox21.SelectedIndex = 0;
        comboBox21.Enabled = true;


     }
     if (comboBox6.SelectedIndex == 1)
     {
        comboBox21.SelectedIndex = 0;
        comboBox21.Enabled = true;


     }


        comboBox22.Enabled = true;
        comboBox22.SelectedIndex = 2;
       

        comboBox21.Enabled = true;
        comboBox21.SelectedIndex = 0;
        textBox45.Visible = false;
        textBox45.Text = "";


    }
}

private void checkBox64_CheckedChanged(object sender, EventArgs e)
{
    if (checkBox64.Checked)
    {
        button36.Enabled = true;
        button36.Text = "Browse";
        listBox2.Visible = true;
    }


    else
    {
        button36.Enabled = false;
        button36.Text = "";
        listBox2.Visible = false;

    }
}

private void checkBox56_CheckedChanged(object sender, EventArgs e)
{
    if (checkBox56.Checked)
    {
        checkBox61.Checked = false;
        checkBox63.Checked = false;

        textBox30.Visible = true;
    }
    else
    {
        textBox30.Visible = false;
        textBox30.Text = "";
    }
}


       



        private void button33_Click(object sender, EventArgs e)
{
    System.Windows.Forms.Application.Exit();
}


private string ConstruirLineaDeComandosRedYDemo3()
{
    string parametrosFinales = "";


    if (checkBox66.Checked) stringParamAppend(ref parametrosFinales, "-respawn");

    if (checkBox65.Checked) stringParamAppend(ref parametrosFinales, "-nomonsters");


    if (checkBox77.Checked) stringParamAppend(ref parametrosFinales, "-autojoin");
    if (checkBox78.Checked) stringParamAppend(ref parametrosFinales, "-privateserver");

    string ipServidor = textBox54.Text.Trim();
    if (checkBox79.Checked && !string.IsNullOrEmpty(ipServidor))
    {
        stringParamAppend(ref parametrosFinales, $"-connect {ipServidor}");
    }


    string demoNameClean = textBox55.Text.Trim().Replace(" ", "-");
    if (!string.IsNullOrEmpty(demoNameClean))
    {
        if (checkBox67.Checked) stringParamAppend(ref parametrosFinales, $"-record {demoNameClean}");
        if (checkBox69.Checked) stringParamAppend(ref parametrosFinales, $"-playdemo {demoNameClean}");
    }


    string serverName = textBox51.Text.Trim();
    if (checkBox74.Checked && !string.IsNullOrEmpty(serverName))
    {
        stringParamAppend(ref parametrosFinales, $"-servername \"{serverName}\"");
    }


    string puertoServidor = textBox53.Text.Trim();
    if (checkBox76.Checked && !string.IsNullOrEmpty(puertoServidor))
    {
        stringParamAppend(ref parametrosFinales, $"-port {puertoServidor}");
    }



    if (!string.IsNullOrWhiteSpace(textBox50.Text))
    {
        stringParamAppend(ref parametrosFinales, textBox50.Text.Trim());
    }


    return parametrosFinales.Trim();
}

private void button38_Click(object sender, EventArgs e)
{

    ActualizarNombreJugador3(textBox52.Text.Trim());

    string seccionMerge = "";
    string seccionDeh = "";


    if (checkBox72.Checked)
    {
        if (listBox3.SelectedItems.Count == 0)
        {
            MessageBox.Show("Please select at least one file from the list.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        string argumentosWad = "";
        string argumentosDeh = "";

        foreach (FileInfo archivoSeleccionado in listBox3.SelectedItems)
        {
            string extension = archivoSeleccionado.Extension.ToLower();

            if (extension == ".wad")
            {
                argumentosWad += $" \"{archivoSeleccionado.FullName}\"";
            }

            else if (extension == ".hex" || extension == ".deh" || extension == ".bex")
            {
                argumentosDeh += $" \"{archivoSeleccionado.FullName}\"";
            }
        }

        if (!string.IsNullOrEmpty(argumentosWad)) seccionMerge = $"-merge {argumentosWad.Trim()}";
        if (!string.IsNullOrEmpty(argumentosDeh)) seccionDeh = $"-deh {argumentosDeh.Trim()}";
    }


    string param2 = comboBox27.SelectedItem?.ToString() ?? "";
    string param3 = comboBox29.SelectedItem?.ToString() ?? "";
    string param5 = comboBox30.SelectedItem?.ToString() ?? "";

    string param7 = comboBox28.SelectedItem?.ToString() ?? "";
    string param8 = comboBox31.SelectedItem?.ToString() ?? "";
    string param9 = comboBox32.SelectedItem?.ToString() ?? "";
    string param10 = comboBox33.SelectedItem?.ToString() ?? "";

    string argument2 = $"{param2} {param3} {param5} {param7} {param8} {param9} {param10}".Trim();


    string parametrosUnificados = ConstruirLineaDeComandosRedYDemo3();


    string argumentosFinales = $"{seccionMerge} {seccionDeh} {argument2} {parametrosUnificados}".Trim();


    try
    {

        string rutaCarpetaChocoDoom = System.IO.Path.Combine(Application.StartupPath, "crispy-hexen");
        string exeDoomUnificado = System.IO.Path.Combine(rutaCarpetaChocoDoom, "crispy-hexen.exe");

        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = exeDoomUnificado,
            Arguments = argumentosFinales,
            UseShellExecute = false,
            CreateNoWindow = true,


            WorkingDirectory = rutaCarpetaChocoDoom
        };

        if (!checkBox72.Checked)
        {
            psi.RedirectStandardOutput = true;
        }

        using (Process process = Process.Start(psi))
        {
            if (!checkBox72.Checked && process != null)
            {
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
            }
        }
    }
    catch (Exception)
    {

        MessageBox.Show("Error starting crispy hexen: The system cannot find 'crispy-hexen.exe' inside the 'crispy-hexen' folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}


private void ActualizarNombreJugador3(string nuevoNombre)
{
    if (string.IsNullOrEmpty(nuevoNombre)) return;


    string rutaLocalConfig = Path.Combine(Application.StartupPath, "crispy-hexen", "crispy-hexen.cfg");


    if (File.Exists(rutaLocalConfig))
    {
        try
        {
            string[] lineas = File.ReadAllLines(rutaLocalConfig);

            for (int i = 0; i < lineas.Length; i++)
            {
                if (lineas[i].StartsWith("player_name"))
                {

                    lineas[i] = $"player_name  \"{nuevoNombre}\"";
                    break;
                }
            }


            File.WriteAllLines(rutaLocalConfig, lineas);
        }
        catch
        {

        }
    }
}

private void comboBox27_SelectedIndexChanged(object sender, EventArgs e)
{
    int indiceSeleccionado = comboBox27.SelectedIndex;

    switch (indiceSeleccionado)
    {
        case 0:
            {



                List<Item> listahexen1 = new List<Item>
                    {
                        new Item("-warp 01", "MAP01: Winnowing Hall"),
                        new Item("-warp 02", "MAP02: Seven Portals"),
                        new Item(" -warp 03", "MAP03: Guardian of Ice"),
                        new Item(" -warp 04", "MAP04: Guardian of Fire"),
                        new Item(" -warp 05", "MAP05: Guardian of Steel"),
                        new Item(" -warp 06", "MAP06: Bright Crucible"),
                        new Item(" -warp 07", "MAP07: Shadow Wood"),
                        new Item(" -warp 08", "MAP08: Darkmere"),
                        new Item(" -warp 09", "MAP09: Caves of Circe"),
                        new Item(" -warp 10", "MAP010: Wastelands"),
                        new Item(" -warp 11", "MAP011: Sacred Grove"),
                        new Item(" -warp 12", "MAP012: Hypostyle"),
                        new Item(" -warp 13", "MAP013: Heresiarch's Seminary"),
                        new Item(" -warp 14", "MAP014: Dragon Chapel"),
                        new Item(" -warp 15", "MAP015: Griffin Chapel"),
                        new Item(" -warp 16", "MAP016: Deathwind Chapel"),
                        new Item(" -warp 17", "MAP017: Orchard of Lamentations"),
                        new Item(" -warp 18", "MAP018: Silent Refectory"),
                        new Item(" -warp 19", "MAP019: Wolf Chapel"),
                        new Item(" -warp 20", "MAP020: Forsaken Outpost"),
                        new Item(" -warp 21", "MAP021: Castle of Grief"),
                        new Item(" -warp 22", "MAP022: Gibbet"),
                        new Item(" -warp 23", "MAP023: Effluvium"),
                        new Item(" -warp 24", "MAP024: Dungeons"),
                        new Item(" -warp 25", "MAP025: Desolate Garden"),
                        new Item(" -warp 26", "MAP026: Necropolis"),
                        new Item(" -warp 27", "MAP027: Zedek's Tomb"),
                        new Item(" -warp 28", "MAP028: Menelkir's Tomb"),
                        new Item(" -warp 29", "MAP029: Traductus' Tomb"),
                        new Item(" -warp 30", "MAP030: Vivarium"),
                        new Item(" -warp 31", "MAP031: Dark Crucible"),



                        };

                comboBox29.DataSource = null;
                comboBox29.DataSource = listahexen1;
                comboBox29.DisplayMember = "Value";

                comboBox29.SelectedIndex = 0;


                break;
            }

        case 1:
            {


                List<Item> listahexen2 = new List<Item>
                    {
                        new Item("-iwad HEXEN.WAD -merge HEXDD.WAD -warp 01", "MAP01: Ruined Village"),
                        new Item("-iwad HEXEN.WAD -merge HEXDD.WAD -warp 02", "MAP02: Blight"),
                        new Item("-iwad HEXEN.WAD -merge HEXDD.WAD -warp 03", "MAP03: Sump"),
                        new Item("-iwad HEXEN.WAD -merge HEXDD.WAD -warp 04", "MAP04: Catacomb"),
                        new Item("-iwad HEXEN.WAD -merge HEXDD.WAD -warp 05", "MAP05: Badlands"),
                        new Item("-iwad HEXEN.WAD -merge HEXDD.WAD -warp 06", "MAP06: Brackenwood"),
                        new Item("-iwad HEXEN.WAD -merge HEXDD.WAD -warp 07", "MAP07: Pyre"),
                        new Item("-iwad HEXEN.WAD -merge HEXDD.WAD -warp 08", "MAP08: Constable's Gate "),
                        new Item("-iwad HEXEN.WAD -merge HEXDD.WAD -warp 09", "MAP09: Treasury"),
                        new Item("-iwad HEXEN.WAD -merge HEXDD.WAD -warp 10", "MAP010: Market Place"),
                        new Item("-iwad HEXEN.WAD -merge HEXDD.WAD -warp 11", "MAP011: Locus Requiescat"),
                        new Item("-iwad HEXEN.WAD -merge HEXDD.WAD -warp 12", "MAP012: Ordeal"),
                        new Item("-iwad HEXEN.WAD -merge HEXDD.WAD -warp 13", "MAP013: Armory"),
                        new Item("-iwad HEXEN.WAD -merge HEXDD.WAD -warp 14", "MAP014: Nave"),
                        new Item("-iwad HEXEN.WAD -merge HEXDD.WAD -warp 15", "MAP015: Chantry"),
                        new Item("-iwad HEXEN.WAD -merge HEXDD.WAD -warp 16", "MAP016: Abattoir"),
                        new Item("-iwad HEXEN.WAD -merge HEXDD.WAD -warp 17", "MAP017: Dark Watch"),
                        new Item("-iwad HEXEN.WAD -merge HEXDD.WAD -warp 18", "MAP018: Cloaca"),
                        new Item("-iwad HEXEN.WAD -merge HEXDD.WAD -warp 19", "MAP019: Ice Hold"),
                        new Item("-iwad HEXEN.WAD -merge HEXDD.WAD -warp 20", "MAP020: Dark Citadel "),
                        new Item("-iwad HEXEN.WAD -merge HEXDD.WAD -warp 33", "MAP021: Transit"),
                        new Item("-iwad HEXEN.WAD -merge HEXDD.WAD -warp 34", "MAP022: Over N Under"),
                        new Item("-iwad HEXEN.WAD -merge HEXDD.WAD -warp 35", "MAP023: Deathfog"),
                        new Item("-iwad HEXEN.WAD -merge HEXDD.WAD -warp 36", "MAP024: Castle of Pain"),
                        new Item("-iwad HEXEN.WAD -merge HEXDD.WAD -warp 37", "MAP025: Sewer Pit"),
                        new Item("-iwad HEXEN.WAD -merge HEXDD.WAD -warp 38", "MAP026: The Rose"),

                    };

                comboBox29.DataSource = null;
                comboBox29.DataSource = listahexen2;
                comboBox29.DisplayMember = "value";

                comboBox29.SelectedIndex = 0;


                break;
            }
    }
}

private void comboBox28_SelectedIndexChanged(object sender, EventArgs e)
{
    int indiceSeleccionado = comboBox28.SelectedIndex;

    switch (indiceSeleccionado)
    {
        case 0:
            {



                List<Item> listahexen1 = new List<Item>
                    {
                        new Item("-skill 1", "Squire"),
                        new Item("-skill 2", "Knight"),
                        new Item("-skill 3", "Warrior"),
                        new Item("-skill 4", "Berserker"),
                        new Item("-skill 5", "Titan"),




                        };

                comboBox30.DataSource = null;
                comboBox30.DataSource = listahexen1;
                comboBox30.DisplayMember = "Value";

                comboBox30.SelectedIndex = 2;


                break;
            }

        case 1:
            {


                List<Item> listahexen2 = new List<Item>
                    {
                        new Item("-skill 1", "Altar Boy"),
                        new Item("-skill 2", "Acolyte"),
                        new Item("-skill 3", "Priest"),
                        new Item("-skill 4", "Cardinal"),
                        new Item("-skill 5", "Pope"),

                    };

                comboBox30.DataSource = null;
                comboBox30.DataSource = listahexen2;
                comboBox30.DisplayMember = "value";

                comboBox30.SelectedIndex = 2;


                break;
            }
        case 2:
            {


                List<Item> listahexen3 = new List<Item>
                    {
                        new Item("-skill 1", "Apprentice"),
                        new Item("-skill 2", "Enchanter"),
                        new Item("-skill 3", "Sorcerer"),
                        new Item("-skill 4", "Warlock"),
                        new Item("-skill 5", "Archmage"),

                    };

                comboBox30.DataSource = null;
                comboBox30.DataSource = listahexen3;
                comboBox30.DisplayMember = "value";

                comboBox30.SelectedIndex = 2;


                break;
            }
    }
}

private void button39_Click(object sender, EventArgs e)
{
    using (OpenFileDialog ofd = new OpenFileDialog())
    {

        ofd.Filter = "Archivos Hexen (*.wad;*.hex)|*.wad;*.hex|Archivos WAD (*.wad)|*.wad|Parches Hexen (*.hex)|*.hex|Todos los archivos (*.*)|*.*";
        ofd.Multiselect = true;
        ofd.Title = "Select your Hexen WAD and HEX files";

        if (ofd.ShowDialog() == DialogResult.OK)
        {

            listBox3.Items.Clear();

            foreach (string rutaCompleta in ofd.FileNames)
            {
                FileInfo archivo = new FileInfo(rutaCompleta);
                listBox3.Items.Add(archivo);
            }
        }
    }
}

private void button40_Click(object sender, EventArgs e)
{

    openFileDialog1.Filter = "Archivos LMP|*.lmp";
    openFileDialog1.Title = "Select LMP file";
    openFileDialog1.FileName = "";


    if (openFileDialog1.ShowDialog() == DialogResult.OK)
    {

        string rutaCompleta = openFileDialog1.FileName;


        string nombreArchivo = Path.GetFileName(rutaCompleta);


        textBox55.Text = nombreArchivo;
    }
}

private void checkBox65_CheckedChanged(object sender, EventArgs e)
{
    if (checkBox65.Checked)
    {
        checkBox66.Checked = false;

        checkBox80.Checked = false;
        checkBox77.Checked = false;
        checkBox79.Checked = false;

        checkBox33.Checked = false;
        comboBox30.Enabled = false;
        comboBox30.SelectedIndex = -1;
    }
    else
    {
        if (comboBox28.SelectedIndex == 0)
        {
            comboBox30.SelectedIndex = 2;
            comboBox30.Enabled = true;


        }
        if (comboBox28.SelectedIndex == 1)
        {
            comboBox30.SelectedIndex = 2;
            comboBox30.Enabled = true;


        }



        if (comboBox28.SelectedIndex == 2)
        {
            comboBox30.SelectedIndex = 2;
            comboBox30.Enabled = true;


        }
    }
}

private void checkBox66_CheckedChanged(object sender, EventArgs e)
{
    if (checkBox66.Checked)
    {

        checkBox65.Checked = false;
        checkBox77.Checked = false;
        checkBox79.Checked = false;

    }
}

private void checkBox80_CheckedChanged(object sender, EventArgs e)
{

    if (checkBox80.Checked)
    {


        checkBox66.Checked = false;
        checkBox65.Checked = false;

        checkBox77.Checked = false;
        checkBox78.Checked = false;
        checkBox79.Checked = false;
        comboBox29.Enabled = false;
        comboBox29.SelectedIndex = -1;

        comboBox28.Enabled = false;
        comboBox28.SelectedIndex = -1;
        comboBox30.Enabled = false;
        comboBox30.SelectedIndex = -1;

    }
    else
    {
        if (comboBox27.SelectedIndex == 0)
        {
            comboBox29.SelectedIndex = 0;
            comboBox29.Enabled = true;


        }
        if (comboBox27.SelectedIndex == 1)
        {
            comboBox29.SelectedIndex = 0;
            comboBox29.Enabled = true;


        }




        comboBox28.Enabled = true;
        comboBox28.SelectedIndex = 0;
        comboBox30.Enabled = true;




    }

}

private void checkBox67_CheckedChanged(object sender, EventArgs e)
{
    if (checkBox67.Checked)
    {
        checkBox69.Checked = false;
        textBox55.Enabled = true;
    }
    else
    {
        textBox55.Enabled = false;
    }
}

private void checkBox69_CheckedChanged(object sender, EventArgs e)
{
    if (checkBox69.Checked)
    {
        checkBox67.Checked = false;
        checkBox65.Checked = false;
        checkBox66.Checked = false;
        checkBox71.Checked = false;
        checkBox73.Checked = false;
        checkBox74.Checked = false;
        checkBox75.Checked = false;
        checkBox76.Checked = false;
        checkBox77.Checked = false;
        checkBox78.Checked = false;
        checkBox79.Checked = false;
        checkBox80.Checked = false;

        checkBox67.Enabled = false;
        checkBox65.Enabled = false;
        checkBox66.Enabled = false;
        checkBox71.Enabled = false;
        checkBox73.Enabled = false;
        checkBox74.Enabled = false;
        checkBox75.Enabled = false;
        checkBox76.Enabled = false;
        checkBox77.Enabled = false;
        checkBox78.Enabled = false;
        checkBox79.Enabled = false;
        checkBox80.Enabled = false;
        comboBox29.Enabled = false;
        comboBox28.Enabled = false;
        comboBox30.Enabled = false;

        comboBox29.SelectedIndex = -1;
        comboBox28.SelectedIndex = -1;
        comboBox30.SelectedIndex = -1;
        textBox55.Enabled = true;
    }
    else
    {
        if (comboBox27.SelectedIndex == 0)
        {
            comboBox29.SelectedIndex = 0;
            comboBox29.Enabled = true;


        }
        if (comboBox27.SelectedIndex == 1)
        {
            comboBox29.SelectedIndex = 0;
            comboBox29.Enabled = true;


        }



        checkBox67.Enabled = true;
        checkBox65.Enabled = true;
        checkBox66.Enabled = true;
        checkBox71.Enabled = true;
        checkBox73.Enabled = true;
        checkBox74.Enabled = true;
        checkBox75.Enabled = true;
        checkBox76.Enabled = true;
        checkBox77.Enabled = true;
        checkBox78.Enabled = true;
        checkBox79.Enabled = true;
        checkBox80.Enabled = true;
        comboBox28.SelectedIndex = 0;

        comboBox28.Enabled = true;
        comboBox30.Enabled = true;
        textBox55.Enabled = false;
    }


}

private void checkBox71_CheckedChanged(object sender, EventArgs e)
{
    if (checkBox71.Checked)
    {
        checkBox77.Checked = false;
        checkBox79.Checked = false;

        textBox50.Visible = true;
    }
    else
    {
        textBox50.Visible = false;
        textBox50.Text = "";
    }
}

private void checkBox73_CheckedChanged(object sender, EventArgs e)
{
    if (checkBox73.Checked)
    {
        comboBox31.Enabled = true;
        comboBox32.Visible = true;
        comboBox33.Visible = true;
        comboBox31.SelectedIndex = 0;

        checkBox77.Checked = false;

        checkBox79.Checked = false;
    }
    else
    {
        comboBox31.Enabled = false;
        comboBox32.Visible = false;
        comboBox33.Visible = false;
        comboBox31.SelectedIndex = -1;
        comboBox32.SelectedIndex = -1;
        comboBox33.SelectedIndex = -1;
    }


}

private void checkBox74_CheckedChanged(object sender, EventArgs e)
{
    if (checkBox74.Checked)
    {

        checkBox77.Checked = false;

        checkBox79.Checked = false;
        textBox51.Visible = true;
        textBox51.Text = "your server name";
    }
    else
    {

        textBox51.Visible = false;
        textBox51.Text = "";
    }


}

private void checkBox75_CheckedChanged(object sender, EventArgs e)
{
    if (checkBox75.Checked)
    {

        checkBox77.Checked = false;

        checkBox79.Checked = false;
        textBox52.Visible = true;
        textBox52.Text = "your player name";
    }
    else
    {

        textBox52.Visible = false;
        textBox52.Text = "";
    }


}

private void checkBox76_CheckedChanged(object sender, EventArgs e)
{

    if (checkBox76.Checked)
    {
        textBox53.Visible = true;
        textBox53.Text = "2342";
    }
    else
    {
        textBox53.Visible = false;
        textBox53.Text = "";

    }

}

private void checkBox77_CheckedChanged(object sender, EventArgs e)
{
    if (checkBox77.Checked)
    {
        checkBox67.Checked = false;
        checkBox65.Checked = false;
        checkBox66.Checked = false;
        checkBox71.Checked = false;
        checkBox73.Checked = false;
        checkBox74.Checked = false;
        checkBox75.Checked = false;
        checkBox76.Checked = false;
        checkBox69.Checked = false;
        checkBox78.Checked = false;
        checkBox79.Checked = false;
        checkBox80.Checked = false;



        comboBox29.SelectedIndex = -1;
        comboBox28.SelectedIndex = -1;
        comboBox30.SelectedIndex = -1;

    }
    else
    {
        if (comboBox27.SelectedIndex == 0)
        {
            comboBox29.SelectedIndex = 0;
            comboBox29.Enabled = true;


        }
        if (comboBox27.SelectedIndex == 1)
        {
            comboBox29.SelectedIndex = 0;
            comboBox29.Enabled = true;


        }




        comboBox28.SelectedIndex = 0;

        comboBox28.Enabled = true;
        comboBox30.Enabled = true;

    }


}

private void checkBox79_CheckedChanged(object sender, EventArgs e)
{
    if (checkBox79.Checked)
    {
        checkBox67.Checked = false;
        checkBox65.Checked = false;
        checkBox66.Checked = false;
        checkBox71.Checked = false;
        checkBox73.Checked = false;
        checkBox74.Checked = false;
        checkBox75.Checked = false;
        checkBox76.Checked = false;
        checkBox69.Checked = false;
        checkBox78.Checked = false;
        checkBox77.Checked = false;
        checkBox80.Checked = false;



        comboBox29.SelectedIndex = -1;
        comboBox28.SelectedIndex = -1;
        comboBox30.SelectedIndex = -1;
        textBox54.Visible = true;
    }
    else
    {
        if (comboBox27.SelectedIndex == 0)
        {
            comboBox29.SelectedIndex = 0;
            comboBox29.Enabled = true;


        }
        if (comboBox27.SelectedIndex == 1)
        {
            comboBox29.SelectedIndex = 0;
            comboBox29.Enabled = true;


        }




        comboBox28.SelectedIndex = 0;

        comboBox28.Enabled = true;
        comboBox30.Enabled = true;
        textBox54.Visible = false;
    }


}

private void checkBox78_CheckedChanged(object sender, EventArgs e)
{
    if (checkBox78.Checked)
    {
        checkBox77.Checked = false;
        checkBox79.Checked = false;

    }

}

private void checkBox72_CheckedChanged(object sender, EventArgs e)
{
    if (checkBox72.Checked)
    {
        button39.Enabled = true;
        button39.Text = "Browse";
        listBox3.Visible = true;
    }


    else
    {
        button39.Enabled = false;
        button39.Text = "";
        listBox3.Visible = false;

    }
}

private void button41_Click(object sender, EventArgs e)
{

    string rutaCarpetaChocoDoom = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "crispy-hexen");
    string setupPath = System.IO.Path.Combine(rutaCarpetaChocoDoom, "crispy-hexen-setup.exe");


    if (System.IO.File.Exists(setupPath))
    {
        try
        {

            System.Diagnostics.ProcessStartInfo psiSetup = new System.Diagnostics.ProcessStartInfo
            {
                FileName = setupPath,
                UseShellExecute = false,
                CreateNoWindow = false,


                WorkingDirectory = rutaCarpetaChocoDoom
            };

            System.Diagnostics.Process.Start(psiSetup);
        }
        catch (System.Exception ex)
        {
            System.Windows.Forms.MessageBox.Show("Could not launch the setup executable: " + ex.Message, "Execution Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        }
    }
    else
    {

        System.Windows.Forms.MessageBox.Show("The file 'crispy-hexen-setup.exe' was not found inside the 'crispy-hexen' directory.", "File Missing", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
    }
}

private void button9_Click(object sender, EventArgs e)
{
    System.Windows.Forms.Application.Exit();
}

private void comboBox29_SelectedIndexChanged(object sender, EventArgs e)
{

    if (checkBox80.Checked && comboBox29.SelectedIndex != -1)
    {

        comboBox29.SelectedIndex = -1;
    }
    if (checkBox77.Checked && comboBox29.SelectedIndex != -1)
    {

        comboBox29.SelectedIndex = -1;
    }

    if (checkBox79.Checked && comboBox29.SelectedIndex != -1)
    {

        comboBox29.SelectedIndex = -1;
    }
    if (checkBox69.Checked && comboBox29.SelectedIndex != -1)
    {

        comboBox29.SelectedIndex = -1;
    }

}

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Image = imagen1;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Image = imagen3;
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Image = imagen2;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Image = imagen3;
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Image = imagen4;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Image = imagen3;
        }





        private async void button13_Click(object sender, EventArgs e)
        {
            
            string rutaCarpetaChocoDoom = Path.Combine(Application.StartupPath, "crispy-strife");
            string rutacrispyDoom = Path.Combine(rutaCarpetaChocoDoom, "crispy-strife.exe");

            
            if (!File.Exists(rutacrispyDoom))
            {
                richTextBox1.AppendText("Error: crispy-strife.exe was not found inside 'crispy-strife' folder.\n");
                return;
            }

           
            button13.Enabled = false;
            richTextBox1.Clear();
            richTextBox1.AppendText("Starting search for local servers inside crispy-strife...\n");

            
            await Task.Run(() => EjecutarBusquedaLocalDoom(rutacrispyDoom, rutaCarpetaChocoDoom));

            
            button13.Enabled = true;
            richTextBox1.AppendText("\nSearch finished.\n");
        }

        private void EjecutarBusquedaLocalDoom(string rutaExe, string workingDir)
        {
            
            string puertoServidorlocal = textBox24.Text.Trim();
            string argumentosFinales = "-localsearch";

            if (!string.IsNullOrEmpty(puertoServidorlocal))
            {
                argumentosFinales = $"-localsearch -port {puertoServidorlocal}";
            }

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = rutaExe,
                Arguments = argumentosFinales,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                StandardOutputEncoding = Encoding.UTF8,

                
                WorkingDirectory = workingDir
            };

            using (Process proceso = new Process { StartInfo = psi })
            {
               
                proceso.OutputDataReceived += (sender, e) => MostrarTextoEnRichTextBox(e.Data);
                proceso.ErrorDataReceived += (sender, e) => MostrarTextoEnRichTextBox(e.Data);

                proceso.Start();
                proceso.BeginOutputReadLine();
                proceso.BeginErrorReadLine();

                proceso.WaitForExit();
            }
        }

        private void MostrarTextoEnRichTextBox(string texto)
        {
            if (texto != null)
            {
                
                if (richTextBox1.InvokeRequired)
                {
                    richTextBox1.Invoke(new Action(() => MostrarTextoEnRichTextBox(texto)));
                }
                else
                {
                    
                    string textoLimpio = texto.Replace("_", " ");
                    richTextBox1.AppendText(textoLimpio + Environment.NewLine);

                    
                    richTextBox1.SelectionStart = richTextBox1.Text.Length;
                    richTextBox1.ScrollToCaret();
                }
            }
        }

       

        private async void button14_Click(object sender, EventArgs e)
        {
            await CargarServidoresEnTabla();
        }

        private async Task CargarServidoresEnTabla()
        {
            richTextBox1.Text = "Loading server list from master.chocolate-doom.org...";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    
                    string html = await client.GetStringAsync(MasterServerUrl);

                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(html);

                   
                    var tableRowNodes = doc.DocumentNode.SelectNodes("//table/tr");

                    if (tableRowNodes == null || tableRowNodes.Count <= 1)
                    {
                        richTextBox1.Text = "No active servers were found at this time.";
                        return;
                    }

                    StringBuilder sb = new StringBuilder();

                   
                    int anchoIp = 26;
                    int anchoNombre = 35;
                    int anchoVersion = 22;
                    int anchoJugadores = 15;

                   
                    sb.AppendLine(new string('=', anchoIp + anchoNombre + anchoVersion + anchoJugadores));
                    sb.AppendLine(
                        "Server Address".PadRight(anchoIp) +
                        "Server Name".PadRight(anchoNombre) +
                        "Version".PadRight(anchoVersion) +
                        "Max Players".PadRight(anchoJugadores)
                    );
                    sb.AppendLine(new string('-', anchoIp + anchoNombre + anchoVersion + anchoJugadores));

                    
                    for (int i = 1; i < tableRowNodes.Count; i++)
                    {
                        var cells = tableRowNodes[i].SelectNodes("td");
                        if (cells != null && cells.Count >= 4)
                        {
                            
                            string ip = cells[0].InnerText.Trim();
                            string nombre = cells[1].InnerText.Trim();
                            string version = cells[2].InnerText.Trim();
                            string jugadores = cells[3].InnerText.Trim();

                            
                            if (nombre.Length > anchoNombre - 3) nombre = nombre.Substring(0, anchoNombre - 4) + "...";
                            if (version.Length > anchoVersion - 3) version = version.Substring(0, anchoVersion - 4) + "...";

                           
                            sb.AppendLine(
                                ip.PadRight(anchoIp) +
                                nombre.PadRight(anchoNombre) +
                                version.PadRight(anchoVersion) +
                                jugadores.PadRight(anchoJugadores)
                            );
                        }
                    }

                    sb.AppendLine(new string('=', anchoIp + anchoNombre + anchoVersion + anchoJugadores));

                   
                    richTextBox1.Text = sb.ToString();
                }
            }
            catch (Exception)
            {
                richTextBox1.Text = $"Error retrieving data: the process cannot continue.";
            }

        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionLength > 0)
            {
                richTextBox1.Copy();
            }
        }
      


        private string ConstruirLineaDeComandosRedYDemo4()
        {
            string parametrosFinales = "";


            if (checkBox2.Checked) stringParamAppend(ref parametrosFinales, "-respawn");
            if (checkBox1.Checked) stringParamAppend(ref parametrosFinales, "-fast");
            if (checkBox4.Checked) stringParamAppend(ref parametrosFinales, "-nomonsters");


            if (checkBox12.Checked) stringParamAppend(ref parametrosFinales, "-autojoin");
            if (checkBox13.Checked) stringParamAppend(ref parametrosFinales, "-privateserver");

            string ipServidor = textBox23.Text.Trim();
            if (checkBox14.Checked && !string.IsNullOrEmpty(ipServidor))
            {
                stringParamAppend(ref parametrosFinales, $"-connect {ipServidor}");
            }


            string demoNameClean = textBox16.Text.Trim().Replace(" ", "-");
            if (!string.IsNullOrEmpty(demoNameClean))
            {
                if (checkBox5.Checked) stringParamAppend(ref parametrosFinales, $"-record {demoNameClean}");
                if (checkBox6.Checked) stringParamAppend(ref parametrosFinales, $"-playdemo {demoNameClean}");
            }


            string serverName = textBox20.Text.Trim();
            if (checkBox9.Checked && !string.IsNullOrEmpty(serverName))
            {
                stringParamAppend(ref parametrosFinales, $"-servername \"{serverName}\"");
            }


            string puertoServidor = textBox22.Text.Trim();
            if (checkBox11.Checked && !string.IsNullOrEmpty(puertoServidor))
            {
                stringParamAppend(ref parametrosFinales, $"-port {puertoServidor}");
            }



            if (!string.IsNullOrWhiteSpace(textBox17.Text))
            {
                stringParamAppend(ref parametrosFinales, textBox17.Text.Trim());
            }


            return parametrosFinales.Trim();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            
            ActualizarNombreJugador4(textBox21.Text.Trim());

            string seccionMerge = "";
            string seccionDeh = "";

            if (checkBox16.Checked)
            {
                if (listBox1.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Please select at least one file from the list.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string argumentosWad = "";
                string argumentosDeh = "";

                foreach (FileInfo archivoSeleccionado in listBox1.SelectedItems)
                {
                    string extension = archivoSeleccionado.Extension.ToLower();

                    if (extension == ".wad")
                    {
                        argumentosWad += $" \"{archivoSeleccionado.FullName}\"";
                    }

                    else if (extension == ".seh" || extension == ".deh" || extension == ".bex")
                    {
                        argumentosDeh += $" \"{archivoSeleccionado.FullName}\"";
                    }
                }

                if (!string.IsNullOrEmpty(argumentosWad)) seccionMerge = $"-merge {argumentosWad.Trim()}";
                if (!string.IsNullOrEmpty(argumentosDeh)) seccionDeh = $"-deh {argumentosDeh.Trim()}";
            }


            string param1 = comboBox1.SelectedItem?.ToString() ?? "";
            string param2 = comboBox2.SelectedItem?.ToString() ?? "";
            string param3 = comboBox3.SelectedItem?.ToString() ?? "";

            string param7 = comboBox4.SelectedItem?.ToString() ?? "";
            string param8 = comboBox5.SelectedItem?.ToString() ?? "";
            string param9 = comboBox7.SelectedItem?.ToString() ?? "";
            string param10 = comboBox8.SelectedItem?.ToString() ?? "";

           
            string argument2 = $"{param1} {param2} {param3} {param7} {param8} {param9} {param10}".Trim();

            
            string parametrosUnificados = ConstruirLineaDeComandosRedYDemo4();

            
            string argumentosFinales = $"{seccionMerge} {seccionDeh} {argument2} {parametrosUnificados}".Trim();

          
            try
            {
                string rutaCarpetaChocoStrife = System.IO.Path.Combine(Application.StartupPath, "crispy-strife");
                string exeStrifeUnificado = System.IO.Path.Combine(rutaCarpetaChocoStrife, "crispy-strife.exe");

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = exeStrifeUnificado,
                    Arguments = argumentosFinales,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WorkingDirectory = rutaCarpetaChocoStrife 
                };

                if (!checkBox16.Checked)
                {
                    psi.RedirectStandardOutput = true;
                }

                using (Process process = Process.Start(psi))
                {
                    if (!checkBox16.Checked && process != null)
                    {
                        string output = process.StandardOutput.ReadToEnd();
                        process.WaitForExit();
                    }
                }
            }
            catch (Exception)
            {
               
                MessageBox.Show("Error starting crispy Strife: The system cannot find 'crispy-strife.exe' inside the 'crispy-strife' folder.", "Execution Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarNombreJugador4(string nuevoNombre)
        {
            if (string.IsNullOrEmpty(nuevoNombre)) return;


            string rutaLocalConfig = Path.Combine(Application.StartupPath, "crispy-strife", "crispy-strife.cfg");


            if (File.Exists(rutaLocalConfig))
            {
                try
                {
                    string[] lineas = File.ReadAllLines(rutaLocalConfig);

                    for (int i = 0; i < lineas.Length; i++)
                    {
                        if (lineas[i].StartsWith("player_name"))
                        {

                            lineas[i] = $"player_name  \"{nuevoNombre}\"";
                            break;
                        }
                    }


                    File.WriteAllLines(rutaLocalConfig, lineas);
                }
                catch
                {

                }
            }
        }

        private void checkBox15_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox15.Checked)
            {


                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;
                checkBox6.Checked = false;
                checkBox7.Checked = false;
                checkBox8.Checked = false;
                checkBox9.Checked = false;
                checkBox10.Checked = false;
                checkBox11.Checked = false;
                checkBox12.Checked = false;
                checkBox13.Checked = false;
                checkBox14.Checked = false;

                comboBox2.Enabled = false;
                comboBox2.SelectedIndex = -1;
               

                comboBox3.Enabled = false;
                comboBox3.SelectedIndex = -1;

            }
            else
            {


                comboBox3.Enabled = true;
                comboBox3.SelectedIndex = 2;
               

                comboBox2.Enabled = true;
                comboBox2.SelectedIndex = 0;
            }
        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox12.Checked)
            {


                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;
                checkBox6.Checked = false;
                checkBox7.Checked = false;
                checkBox8.Checked = false;
                checkBox9.Checked = false;
                checkBox10.Checked = false;
                checkBox15.Checked = false;
                checkBox13.Checked = false;
                checkBox14.Checked = false;

                comboBox2.Enabled = false;
                comboBox2.SelectedIndex = -1;


                comboBox3.Enabled = false;
                comboBox3.SelectedIndex = -1;

            }
            else
            {


                comboBox3.Enabled = true;
                comboBox3.SelectedIndex = 2;


                comboBox2.Enabled = true;
                comboBox2.SelectedIndex = 0;
            }
        }

        private void checkBox14_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox14.Checked)
            {


                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;
                checkBox6.Checked = false;
                checkBox7.Checked = false;
                checkBox8.Checked = false;
                checkBox9.Checked = false;
                checkBox10.Checked = false;
                checkBox15.Checked = false;
                checkBox13.Checked = false;
                checkBox12.Checked = false;
               

                comboBox2.Enabled = false;
                comboBox2.SelectedIndex = -1;


                comboBox3.Enabled = false;
                comboBox3.SelectedIndex = -1;
                textBox23.Visible = true;

            }
            else
            {


                comboBox3.Enabled = true;
                comboBox3.SelectedIndex = 2;


                comboBox2.Enabled = true;
                comboBox2.SelectedIndex = 0;
                textBox23.Visible = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {

                checkBox15.Checked = false;
                checkBox12.Checked = false;
                checkBox14.Checked = false;
               

            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {

                checkBox15.Checked = false;
                checkBox12.Checked = false;
                checkBox14.Checked = false;


            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {

                checkBox15.Checked = false;
                checkBox12.Checked = false;
                checkBox14.Checked = false;
                comboBox4.Enabled = true;
                comboBox4.SelectedIndex = 0;

            }
            else
            {
                comboBox4.Enabled = false;
                comboBox4.SelectedIndex = -1;

            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {

                checkBox15.Checked = false;
                checkBox12.Checked = false;
                checkBox14.Checked = false;


            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                checkBox6.Checked = false;
                textBox16.Enabled = true;
            }
            else
            {
                textBox16.Enabled = false;
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;
                checkBox7.Checked = false;
                checkBox8.Checked = false;
                checkBox9.Checked = false;
                checkBox10.Checked = false;
                checkBox15.Checked = false;
                checkBox13.Checked = false;
                checkBox12.Checked = false;
                checkBox11.Checked = false;

                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
                checkBox3.Enabled = false;
                checkBox4.Enabled = false;
                checkBox5.Enabled = false;
                checkBox7.Enabled = false;
                checkBox8.Enabled = false;
                checkBox9.Enabled = false;
                checkBox10.Enabled = false;
                checkBox15.Enabled = false;
                checkBox13.Enabled = false;
                checkBox12.Enabled = false;
                checkBox11.Enabled = false;

                comboBox2.SelectedIndex = -1;
                comboBox3.SelectedIndex = -1;


                textBox16.Enabled = true;
            }
            else
            {

                comboBox2.SelectedIndex = 0;
                comboBox3.SelectedIndex = 0;

                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
                checkBox3.Enabled = true;
                checkBox4.Enabled = true;
                checkBox5.Enabled = true;
                checkBox7.Enabled = true;
                checkBox8.Enabled = true;
                checkBox9.Enabled = true;
                checkBox10.Enabled = true;
                checkBox15.Enabled = true;
                checkBox13.Enabled = true;
                checkBox12.Enabled = true;
                checkBox11.Enabled = true;


                textBox16.Enabled = false;
            }
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked)
            {
                checkBox15.Checked = false;
                checkBox12.Checked = false;
                checkBox14.Checked = false;

                textBox17.Visible = true;
            }
            else
            {
                textBox17.Visible = false;
                textBox17.Text = "";
            }
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked)
            {
                comboBox5.Enabled = true;
                comboBox7.Visible = true;
                comboBox8.Visible = true;
                comboBox5.SelectedIndex = 0;

                checkBox15.Checked = false;
                checkBox12.Checked = false;
                checkBox14.Checked = false;
            }
            else
            {
                comboBox5.Enabled = false;
                comboBox7.Visible = false;
                comboBox8.Visible = false;
                comboBox5.SelectedIndex = -1;
                comboBox7.SelectedIndex = -1;
                comboBox8.SelectedIndex = -1;
            }
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox9.Checked)
            {

                checkBox15.Checked = false;
                checkBox12.Checked = false;
                checkBox14.Checked = false;
                textBox20.Visible = true;
                textBox20.Text = "your server name";
            }
            else
            {

                textBox20.Visible = false;
                textBox20.Text = "";
            }
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox10.Checked)
            {

                checkBox15.Checked = false;
                checkBox12.Checked = false;
                checkBox14.Checked = false;
                textBox21.Visible = true;
                textBox21.Text = "your player name";
            }
            else
            {

                textBox21.Visible = false;
                textBox21.Text = "";
            }
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox11.Checked)
            {

                checkBox15.Checked = false;
                textBox22.Visible = true;
                textBox22.Text = "2342";
            }
            else
            {

                textBox22.Visible = false;
                textBox22.Text = "";
            }
        }

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox13.Checked)
            {

                checkBox15.Checked = false;
                checkBox12.Checked = false;
                checkBox14.Checked = false;


            }
        }

        private void checkBox16_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox16.Checked)
            {
                button12.Enabled = true;
                button12.Text = "Browse";
                listBox1.Visible = true;
            }


            else
            {
                button12.Enabled = false;
                button12.Text = "";
                listBox1.Visible = false;

            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {

                ofd.Filter = "Archivos Strife (*.wad;*.seh;*.deh)|*.wad;*.seh;*.deh|Archivos WAD (*.wad)|*.wad|Parches strife (*.seh;*.deh)|*.seh;*.deh|Todos los archivos (*.*)|*.*";
                ofd.Multiselect = true;
                ofd.Title = "Select your Strife WAD and SEH files";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    listBox1.Items.Clear();

                    foreach (string rutaCompleta in ofd.FileNames)
                    {
                        FileInfo archivo = new FileInfo(rutaCompleta);
                        listBox1.Items.Add(archivo);
                    }
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

            string rutaCarpetaChocoDoom = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "crispy-strife");
            string setupPath = System.IO.Path.Combine(rutaCarpetaChocoDoom, "crispy-strife-setup.exe");


            if (System.IO.File.Exists(setupPath))
            {
                try
                {

                    System.Diagnostics.ProcessStartInfo psiSetup = new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = setupPath,
                        UseShellExecute = false,
                        CreateNoWindow = false,


                        WorkingDirectory = rutaCarpetaChocoDoom
                    };

                    System.Diagnostics.Process.Start(psiSetup);
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Could not launch the setup executable: " + ex.Message, "Execution Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            else
            {

                System.Windows.Forms.MessageBox.Show("The file 'crispy-strife-setup.exe' was not found inside the 'crispy-strife' directory.", "File Missing", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            openFileDialog1.Filter = "Archivos LMP|*.lmp";
            openFileDialog1.Title = "Select LMP file";
            openFileDialog1.FileName = "";


            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                string rutaCompleta = openFileDialog1.FileName;


                string nombreArchivo = Path.GetFileName(rutaCompleta);


                textBox13.Text = nombreArchivo;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F1)
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
            }


            if (e.KeyCode == Keys.Escape)
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void comboBox21_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (checkBox33.Checked && comboBox21.SelectedIndex != -1)
            {

                comboBox21.SelectedIndex = -1;
            }
            if (checkBox61.Checked && comboBox21.SelectedIndex != -1)
            {

                comboBox21.SelectedIndex = -1;
            }

            if (checkBox63.Checked && comboBox21.SelectedIndex != -1)
            {

                comboBox21.SelectedIndex = -1;
            }
            if (checkBox55.Checked && comboBox21.SelectedIndex != -1)
            {

                comboBox21.SelectedIndex = -1;
            }
        }
     }
}  