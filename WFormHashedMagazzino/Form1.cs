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

// TEMPO_IMPIEGATO: 1H21
namespace WFormHashedMagazzino
{
    public partial class Form1 : Form
    {
        // ** VARS **
        string FILE_ARCHIVIO = "archivio.dat";

        const int M = 100;
        const int DIM_NOME = 16;
        const int DIM_RECORD = 4 + DIM_NOME + 16 + 4 + 1;

        static int ID_COUNTER;

        public Form1()
        {
            InitializeComponent();

            GeneraFile();
            AggiornaDati();
        }

        // ** CLASS **
        public class Prodotto
        {
            private int _codice;

            public int Codice
            {
                get { return _codice; }
            }

            private string _nome;

            public string Nome
            {
                get { return _nome; }
                set { _nome = value; }
            }

            private decimal _prezzo;

            public decimal Prezzo
            {
                get { return _prezzo; }
                set { _prezzo = value; }
            }

            private int _qty;

            public int Qty
            {
                get { return _qty; }
                set { _qty = value; }
            }

            private bool _isActive;

            public bool IsActive
            {
                get { return _isActive; }
                set { _isActive = value; }
            }

            public Prodotto(int c, string n, decimal p, int q, bool ia)
            {
                _codice = c;
                _nome = n;
                _prezzo = p;
                _qty = q;
                _isActive = ia;
            }

            public Prodotto(string n, decimal p, int q)
            {
                _codice = ++ID_COUNTER;
                _nome = n;
                _prezzo = p;
                _qty = q;
                _isActive = true;
            }

            public static int Hash(int id)
            {
                // IPOTIZZO UN M = 100
                return id.GetHashCode() % M; 
            }

        }

        // ** FUNCTIONS **
        public void GeneraFile()
        {
            using(FileStream fs = new FileStream(FILE_ARCHIVIO, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                if (fs.Length != M * DIM_RECORD)
                    fs.SetLength(M * DIM_RECORD);

                else
                {
                    int id_max = -1;

                    using(BinaryReader br = new BinaryReader(fs))
                    {
                        int posizione = 0;
                        foreach (var item in FILE_ARCHIVIO)
                        {
                            fs.Seek(posizione * DIM_RECORD, SeekOrigin.Begin);
                            int id_tmp = br.ReadInt32();
                            id_max = id_tmp > id_max ? id_tmp : id_max;
                            posizione++;
                        }
                        
                        ID_COUNTER = id_max > 0 ? id_max : 0;
                    }
                }
            }
        }

        public void AggiornaDati()
        {
            dataGridView1.Rows.Clear();

            using(FileStream fs = new FileStream(FILE_ARCHIVIO, FileMode.Open, FileAccess.Read))
            {
                int posizione = 0;
                foreach (var item in FILE_ARCHIVIO)
                {
                    fs.Seek(posizione * DIM_RECORD, SeekOrigin.Begin);
                    Prodotto p = GetProdotto(fs, posizione);
                        
                    if (p != null && p.IsActive)
                        dataGridView1.Rows.Add(p.Codice, p.Nome, p.Prezzo, p.Qty);
                    posizione++;
                }
            }
        }

        public Prodotto GetProdotto(FileStream fs, int posizione)
        {
            try
            {
                fs.Seek(posizione * DIM_RECORD, SeekOrigin.Begin);
                using (BinaryReader br = new BinaryReader(fs, Encoding.UTF8, true)) // LEAVE OPEN = true
                {
                    // Encoding.UTF8.GetString(br.ReadBytes(DIM_NOME))
                    return new Prodotto(br.ReadInt32(), Encoding.UTF8.GetString(br.ReadBytes(DIM_NOME)), br.ReadDecimal(), br.ReadInt32(), br.ReadBoolean());
                }
            }
            catch (Exception)
            {
                MessageBox.Show("PRODOTTO NON TROVATO", "ERRORE");
                return null;
            }
        }

        public void WriteProdotto(FileStream fs, Prodotto prodotto, int posizione)
        {
            fs.Seek(posizione * DIM_RECORD, SeekOrigin.Begin);
            using (BinaryWriter bw = new BinaryWriter(fs, Encoding.UTF8, true))
            {
                bw.Write(prodotto.Codice);
                WriteStringa(bw, prodotto.Nome, DIM_NOME);
                bw.Write(prodotto.Prezzo);
                bw.Write(prodotto.Qty);
                bw.Write(prodotto.IsActive);
            }
        }

        // SCRITTURA STRINGA ATTRAVERSO BYTE_ARRAY
        public void WriteStringa(BinaryWriter bw, string stringa, int dimensione)
        {
            byte[] stringaByte = Encoding.UTF8.GetBytes(stringa);
            byte[] bufffer = new byte[dimensione];

            int byteToCopy = Math.Min(stringaByte.Length, bufffer.Length);
            Array.Copy(stringaByte, bufffer, byteToCopy);

            bw.Write(bufffer);
        }

        // ** BUTTONS **
        private void bInserisci_Click(object sender, EventArgs e)
        {
            string nome = txtNome.Text;

            if (nome.Trim().Length == 0)
            {
                MessageBox.Show("NOME NON VALIDO", "ERRORE");
                return;
            }

            decimal prezzo = nudPrezzo.Value;
            int qty = (int) nudQty.Value;

            Prodotto p = new Prodotto(nome, prezzo, qty);
            int posizione = Prodotto.Hash(p.Codice);

            using(FileStream fs = new FileStream(FILE_ARCHIVIO, FileMode.Open, FileAccess.Write))
            {
                WriteProdotto(fs, p, posizione);
            }

            AggiornaDati();
        }

        private void bCerca_Click(object sender, EventArgs e)
        {
            try
            {
                int codice = Convert.ToInt32(txtCodice.Text);

                Prodotto p;
                using (FileStream fs = new FileStream(FILE_ARCHIVIO, FileMode.Open, FileAccess.Read))
                {
                    p = GetProdotto(fs, codice);
                }

                if (p != null && p.IsActive)
                {
                    txtCodice.Text = Convert.ToString(p.Codice);
                    txtNome.Text = p.Nome;
                    nudPrezzo.Value = p.Prezzo;
                    nudQty.Value = p.Qty;
                }
                else
                    MessageBox.Show("PRODOTTO NON TROVATO", "ERRORE");
            }
            catch (Exception)
            {
                MessageBox.Show("CODICE NON VALIDO", "ERRORE");
            }
        }

        private void bModifica_Click(object sender, EventArgs e)
        {
            try
            {
                int codice = Convert.ToInt32(txtCodice.Text);

                Prodotto p;
                using (FileStream fs = new FileStream(FILE_ARCHIVIO, FileMode.Open, FileAccess.Read))
                {
                    p = GetProdotto(fs, codice);
                }

                string nome = txtNome.Text;

                if (nome.Trim().Length == 0)
                {
                    MessageBox.Show("NOME NON VALIDO", "ERRORE");
                    return;
                }

                decimal prezzo = nudPrezzo.Value;
                int qty = (int)nudQty.Value;

                p.Nome = nome;
                p.Prezzo = prezzo;
                p.Qty = qty;

                int posizione = Prodotto.Hash(p.Codice);

                using (FileStream fs = new FileStream(FILE_ARCHIVIO, FileMode.Open, FileAccess.Write))
                {
                    WriteProdotto(fs, p, posizione);
                }

                AggiornaDati();
            }
            catch (Exception)
            {
                MessageBox.Show("CODICE NON VALIDO", "ERRORE");
            }
        }

        private void bElimina_Click(object sender, EventArgs e)
        {
            try
            {
                int codice = Convert.ToInt32(txtCodice.Text);

                Prodotto p;
                using (FileStream fs = new FileStream(FILE_ARCHIVIO, FileMode.Open, FileAccess.Read))
                {
                    p = GetProdotto(fs, codice);
                }

                p.IsActive = false;

                int posizione = Prodotto.Hash(p.Codice);

                using (FileStream fs = new FileStream(FILE_ARCHIVIO, FileMode.Open, FileAccess.Write))
                {
                    WriteProdotto(fs, p, posizione);
                }

                AggiornaDati();
            }
            catch (Exception)
            {
                MessageBox.Show("CODICE NON VALIDO", "ERRORE");
            }
        }
    }
}
