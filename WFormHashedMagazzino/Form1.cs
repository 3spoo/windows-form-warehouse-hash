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
        const int BYTE_PER_CARATTERE_UNICODE = 2;
        const int DIM_RECORD = 4 + DIM_NOME * BYTE_PER_CARATTERE_UNICODE + 16 + 4 + 1;

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
            using (FileStream fs = new FileStream(
                FILE_ARCHIVIO, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                if (fs.Length != M * DIM_RECORD)
                {
                    fs.SetLength(M * DIM_RECORD);
                }

                int id_max = 0;

                using (BinaryReader br = new BinaryReader(fs, Encoding.Unicode, true))
                {
                    // SCORRE TUTTE LE POSIZIONI DELL'ARCHIVIO
                    for (int posizione = 0; posizione < M; posizione++)
                    {
                        fs.Seek(posizione * DIM_RECORD, SeekOrigin.Begin);

                        int id_tmp = br.ReadInt32();

                        if (id_tmp > id_max)
                            id_max = id_tmp;
                    }
                }

                // RECUPERA IL CODICE MASSIMO UTILIZZATO
                ID_COUNTER = id_max;
            }
        }

        public void AggiornaDati()
        {
            dataGridView1.Rows.Clear();

            using (FileStream fs = new FileStream(
                FILE_ARCHIVIO, FileMode.Open, FileAccess.Read))
            {
                // SCORRE TUTTE LE POSIZIONI DELL'ARCHIVIO
                for (int posizione = 0; posizione < M; posizione++)
                {
                    Prodotto p = GetProdotto(fs, posizione);

                    if (p.Codice != 0 && p.IsActive)
                        dataGridView1.Rows.Add(
                            p.Codice, p.Nome, p.Prezzo, p.Qty);
                }
            }
        }

        public Prodotto GetProdotto(FileStream fs, int posizione)
        {
            fs.Seek(posizione * DIM_RECORD, SeekOrigin.Begin);

            using (BinaryReader br = new BinaryReader(fs, Encoding.Unicode, true))
            {
                // LEGGE I CAMPI DEL RECORD NELLO STESSO ORDINE DI SCRITTURA
                int codice = br.ReadInt32();
                string nome = ReadStringa(br, DIM_NOME);
                decimal prezzo = br.ReadDecimal();
                int qty = br.ReadInt32();
                bool isActive = br.ReadBoolean();

                return new Prodotto(codice, nome, prezzo, qty, isActive);
            }
        }

        public void WriteProdotto(FileStream fs, Prodotto prodotto, int posizione)
        {
            fs.Seek(posizione * DIM_RECORD, SeekOrigin.Begin);

            using (BinaryWriter bw = new BinaryWriter(fs, Encoding.Unicode, true))
            {
                // SCRIVE I CAMPI DEL RECORD NELLO STESSO ORDINE DI LETTURA
                bw.Write(prodotto.Codice);
                WriteStringa(bw, prodotto.Nome, DIM_NOME);
                bw.Write(prodotto.Prezzo);
                bw.Write(prodotto.Qty);
                bw.Write(prodotto.IsActive);
            }
        }

        // SCRIVE UNA STRINGA UTF-16 A DIMENSIONE FISSA
        public void WriteStringa(BinaryWriter bw, string stringa, int dimensione)
        {
            byte[] dati = Encoding.Unicode.GetBytes(stringa);
            int dimensioneByte = dimensione * BYTE_PER_CARATTERE_UNICODE;

            if (dati.Length > dimensioneByte)
                throw new ArgumentException("STRINGA TROPPO LUNGA.");

            bw.Write(dati);
            bw.Write(new byte[dimensioneByte - dati.Length]);
        }

        // LEGGE UNA STRINGA UTF-16 A DIMENSIONE FISSA
        public string ReadStringa(BinaryReader br, int dimensione)
        {
            int dimensioneByte = dimensione * BYTE_PER_CARATTERE_UNICODE;
            byte[] dati = br.ReadBytes(dimensioneByte);

            if (dati.Length != dimensioneByte)
                throw new EndOfStreamException("RECORD INCOMPLETO.");

            return Encoding.Unicode.GetString(dati).TrimEnd('\0');
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
            int qty = (int)nudQty.Value;

            Prodotto p = new Prodotto(nome, prezzo, qty);
            int posizione = Prodotto.Hash(p.Codice);

            using (FileStream fs = new FileStream(FILE_ARCHIVIO, FileMode.Open, FileAccess.Write))
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
                int posizione = Prodotto.Hash(codice);

                Prodotto p;

                using (FileStream fs = new FileStream(FILE_ARCHIVIO, FileMode.Open, FileAccess.Read))
                {
                    p = GetProdotto(fs, posizione);
                }

                if (p.Codice == codice && p.IsActive)
                {
                    txtCodice.Text = Convert.ToString(p.Codice);
                    txtNome.Text = p.Nome;
                    nudPrezzo.Value = p.Prezzo;
                    nudQty.Value = p.Qty;
                }
                else
                {
                    MessageBox.Show("PRODOTTO NON TROVATO", "ERRORE");
                }
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
                int posizione = Prodotto.Hash(codice);

                Prodotto p;

                using (FileStream fs = new FileStream(FILE_ARCHIVIO, FileMode.Open, FileAccess.Read))
                {
                    p = GetProdotto(fs, posizione);
                }

                if (p.Codice != codice || !p.IsActive)
                {
                    MessageBox.Show("PRODOTTO NON TROVATO", "ERRORE");
                    return;
                }

                string nome = txtNome.Text;

                if (nome.Trim().Length == 0)
                {
                    MessageBox.Show("NOME NON VALIDO", "ERRORE");
                    return;
                }

                p.Nome = nome;
                p.Prezzo = nudPrezzo.Value;
                p.Qty = (int)nudQty.Value;

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
                int posizione = Prodotto.Hash(codice);

                Prodotto p;

                using (FileStream fs = new FileStream(FILE_ARCHIVIO, FileMode.Open, FileAccess.Read))
                {
                    p = GetProdotto(fs, posizione);
                }

                if (p.Codice != codice || !p.IsActive)
                {
                    MessageBox.Show("PRODOTTO NON TROVATO", "ERRORE");
                    return;
                }

                p.IsActive = false;

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
