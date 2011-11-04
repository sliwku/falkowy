using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace AnalizatorFalkowy
{
    public abstract class Plik
    {
        protected byte[] dane8bit;
        private string nazwa;

        protected Int16[] dane16bit;       
        protected Int32[] dane32bit;

        protected int maksymalnaWartosc;
        protected int minimalnaWartosc;

        public byte[] Dane8bit
        {
            get { return dane8bit; }
        }
        public Int16[] Dane16bit
        {
            get { return dane16bit; }            
        }
        public Int32[] Dane32bit
        {
            get { return dane32bit; }            
        }        

        public int MinimalnaWartosc
        {
            get { return minimalnaWartosc; }
        }
        public int MaksymalnaWartosc
        {
            get { return maksymalnaWartosc; }
        }


        public Plik(string nazwa)
        {
            this.nazwa = nazwa;
        }

        /// <summary>
        /// Otwiera plik  i zpisuje jego zawartosc do tablicy bajtow Dane
        /// </summary>
        /// <param name="nazwa">sciezka do pliku</param>
        public virtual bool OtworzPlik()
        {
            try
            {                
                using (FileStream fs = File.OpenRead(nazwa))
                {
                    dane8bit = new byte[(int)fs.Length];

                    while (fs.Read(dane8bit, 0, dane8bit.Length) > 0)
                        ;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Zapisuje do pliku zawartosc tablicy bajtow Dane
        /// </summary>
        /// <param name="nazwa">sciezka do pliku</param>
        public virtual void ZapiszPlik()
        {
            // Usun plik jesli istnieje
            if (File.Exists(nazwa))
            {
                try
                {
                    File.Delete(nazwa);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return;
                }
            }

            try
            {
                // Utworz plik
                using (FileStream fs = File.Create(nazwa))
                {
                    for (int i = 0; i < dane8bit.Length; i++)
                        fs.WriteByte(dane8bit[i]);                    
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
