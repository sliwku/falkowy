using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AnalizatorFalkowy
{
    public class PlikWave : Plik
    {
        #region strukturyZagniezdzone
        public struct Riff
        {
            /// <summary>
            /// Tekst ASCII “RIFF” - okrela standard
            /// </summary>
            public Int32 ChunkID { get; set; }
            /// <summary>
            /// Rozmiar całego pliku w bajtach minus 8 bajtów
            ///(pola chunkID, chunkSize)
            /// </summary>
            public Int32 ChunkSize { get; set; }
            /// <summary>
            /// Tekst ASCII - “WAVE”
            /// </summary>
            public Int32 Format { get; set; }            
        }

        public struct Fmt
        {
            /// <summary>
            /// Tekst ASCII “_fmt ”
            /// </summary>
            public Int32 ChunkID { get; set; }
            /// <summary>
            /// Rozmiar tego bloku w bajtach minus 8 bajtów (pola chunkID, chunkSize)
            /// </summary>
            public Int32 ChunkSize { get; set; }
            /// <summary>
            /// 1 - zapis bez kompresji, 1> uzyto kompresji
            /// </summary>
            public Int16 AudioFormat { get; set; }            
            /// <summary>
            /// Liczba kanałów 1-mono, 2-stereo
            /// </summary>
            public Int16 NumChannels { get; set; }           
            /// <summary>
            /// Czestotliwosc próbkowania 800, 44100
            /// </summary>
            public Int32 SampleRate { get; set; }            
            /// <summary>
            /// Liczba bajtów na sekunde
            ///(wszystkie kanaly) = sampleRate*numChannels*bitsPerSample/8
            /// </summary>
            public Int32 ByteRate { get; set; }            
            /// <summary>
            /// Liczba bajtów na próbke (wszystkie kanaly) = numChannels*bitsPerSample/8
            /// </summary>
            public Int16 BlockAlign { get; set; }
            /// <summary>
            /// Liczba bitów na próbke (przypadajacych na jeden kanał) - 8 lub 16
            /// </summary>
            public Int16 BitsPerSample { get; set; }
        }

        public struct Data
        {
            /// <summary>
            /// Tekst ASCII “data”
            /// </summary>
            public Int32 ChunkID { get; set; }
            /// <summary>
            /// Rozmiar danych dzwiekowych w bajtach = numSamples*numChannels*bitsPerSample/8
            /// </summary>
            public Int32 ChunkSize { get; set; }
            /// <summary>
            /// Tablica probek 16-bitowych
            /// </summary>
            public Int16[] Data16 { get; set; }
            /// <summary>
            /// Tablica probek 8-bitowych
            /// </summary>
            public Byte[] Data8 { get; set; }
        }
        #endregion

        private const Int32 TEKST_RIFF = 0x46464952;
        private const Int32 TEKST_WAVE = 0x45564157;
        private const Int32 TEKST_DATA = 0x61746164;

        private BinaryReader binReader;        

        private Riff RIFF;
        private Fmt FMT;
        private Data DATA;        

        private Int16[] kanal0;
        private Int16[] kanal1;
        private Byte[] kanal0Bit8;
        private Byte[] kanal1Bit8;

        private string nazwa;

        public Int32 CzestotliwoscProbkowania
        {
            get { return FMT.SampleRate; }
        }

        public string Nazwa
        {
            get { return nazwa; }           
        }

        public Int16[] Kanal0Bit16
        {
            get { return kanal0; }  
        }
        public Int16[] Kanal1Bit16
        {
            get { return kanal1; }
        }
        public Byte[] Kanal0Bit8
        {
            get { return kanal0Bit8; }
        }
        public Byte[] Kanal1Bit8
        {
            get { return kanal1Bit8; }
        }

        public PlikWave(string nazwa) : base(nazwa)
        {
            this.nazwa = nazwa;
        }        

        public override bool OtworzPlik()
        {
            if (!File.Exists(nazwa))
                return false;

            try
            {
                binReader = new BinaryReader(File.Open(nazwa, FileMode.Open));
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
                return false;
            }

            try
            {
                //wczytujemy zgodnie ze specyfikacja pliku WAVE do bloku RIFF
                RIFF.ChunkID = binReader.ReadInt32();
                RIFF.ChunkSize = binReader.ReadInt32();
                RIFF.Format = binReader.ReadInt32();

                //sprawdzamy czy zgodnie ze specyfikcja chunkID to RIFF a format WAVE
                if (RIFF.ChunkID != TEKST_RIFF || RIFF.Format != TEKST_WAVE)
                {
                    System.Windows.Forms.MessageBox.Show("Błąd otwarcia pliku. Program obsługuje tylko pliki wave");
                    return false;
                }

                //wczytujemy do FMT
                FMT.ChunkID = binReader.ReadInt32();
                FMT.ChunkSize = binReader.ReadInt32();
                FMT.AudioFormat = binReader.ReadInt16();
                FMT.NumChannels = binReader.ReadInt16();
                FMT.SampleRate = binReader.ReadInt32();
                FMT.ByteRate = binReader.ReadInt32();
                FMT.BlockAlign = binReader.ReadInt16();
                FMT.BitsPerSample = binReader.ReadInt16();

                if (FMT.AudioFormat != 1)
                {
                    System.Windows.Forms.MessageBox.Show("Błąd otwarcia pliku. Program obsługuje tylko pliki wave bez kompresji");
                    return false;
                }

                //blok FMT moze miec rozna dlugosc okreslona w chunkSize
                binReader.BaseStream.Seek(FMT.ChunkSize - 16, SeekOrigin.Current);

                Int32 chunkID = 0, chunkSize = 0;

                //w tym miejscu moga byc opcjonalne bloki. Wykonujemy
                //petle dopoki nie dojdziemy do bloku DATA z chunkId "data"
                do
                {
                    chunkID = binReader.ReadInt32();
                    chunkSize = binReader.ReadInt32();
                    //61 74 61 64 -> atad -> data
                    if (chunkID == TEKST_DATA)
                        break;
                    binReader.BaseStream.Seek(chunkSize, SeekOrigin.Current);
                    //opcjonalnych blokow nigdzie nie zapisujemy; nie sa nam potrzebne                   
                } while (true);

                DATA.ChunkID = chunkID;
                DATA.ChunkSize = chunkSize;


                if (FMT.BitsPerSample == 16)
                {
                    DATA.Data16 = new Int16[DATA.ChunkSize / 2];
                    for (int i = 0; i < DATA.Data16.Length; i++)
                        DATA.Data16[i] = binReader.ReadInt16();
                }
                else if (FMT.BitsPerSample == 8)
                {
                    DATA.Data8 = new Byte[DATA.ChunkSize];
                    for (int i = 0; i < DATA.Data8.Length; i++)
                        DATA.Data8[i] = binReader.ReadByte();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Program obsługuje tylko pliki 8 i 16 bitowe. Nieobsługiwany rodzaj pliku.");
                    return false;
                }

                if (FMT.NumChannels == 1 && DATA.Data16 != null)
                    kanal0 = DATA.Data16;
                else if (FMT.NumChannels == 1)
                    kanal0Bit8 = DATA.Data8;
                else if (FMT.NumChannels == 2 && DATA.Data16 != null)
                {
                    kanal0 = new Int16[DATA.Data16.Length / 2];
                    kanal1 = new Int16[DATA.Data16.Length / 2];
                    for (int i = 0, j = 0; j < kanal0.Length; i += 2)
                        kanal0[j++] = DATA.Data16[i];
                    for (int i = 1, j = 0; j < kanal1.Length; i += 2)
                        kanal0[j++] = DATA.Data16[i];
                }
                else if (FMT.NumChannels == 2)
                {
                    kanal0Bit8 = new Byte[DATA.Data8.Length / 2];
                    kanal1Bit8 = new Byte[DATA.Data8.Length / 2];
                    for (int i = 0, j = 0; j < kanal0Bit8.Length; i += 2)
                        kanal0Bit8[j++] = DATA.Data8[i];
                    for (int i = 1, j = 0; j < kanal1Bit8.Length; i += 2)
                        kanal0Bit8[j++] = DATA.Data8[i];
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Plik ma " + FMT.NumChannels.ToString() + " kanalow. Program oblsuguje do 2 kanalow. Nieoblsugiwany rodzaj pliku.");
                    return false;
                }

                if (FMT.BitsPerSample == 16)
                {
                    minimalnaWartosc = Int16.MinValue;
                    maksymalnaWartosc = Int16.MaxValue;
                }
                else if (FMT.BitsPerSample == 8)
                {
                    minimalnaWartosc = Byte.MinValue;
                    maksymalnaWartosc = Byte.MaxValue;
                }

                dane8bit = kanal0Bit8;
                dane16bit = kanal0;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
                return false;
            }
            finally
            {
                binReader.Close();
            }
            return true;
        }      

        public override void ZapiszPlik()
        {
            if (FMT.ChunkSize != 16)
            {
                int diffrence = FMT.ChunkSize - 16;
                FMT.ChunkSize = 16;
                RIFF.ChunkSize -= diffrence;
            }

            try
            {
                using (BinaryWriter binWriter = new BinaryWriter(File.Open(nazwa, FileMode.Create)))
                {
                    binWriter.Write(RIFF.ChunkID);
                    binWriter.Write(RIFF.ChunkSize);
                    binWriter.Write(RIFF.Format);

                    binWriter.Write(FMT.ChunkID);
                    binWriter.Write(FMT.ChunkSize);
                    binWriter.Write(FMT.AudioFormat);
                    binWriter.Write(FMT.NumChannels);
                    binWriter.Write(FMT.SampleRate);
                    binWriter.Write(FMT.ByteRate);
                    binWriter.Write(FMT.BlockAlign);
                    binWriter.Write(FMT.BitsPerSample);

                    binWriter.Write(DATA.ChunkID);

                    if (FMT.NumChannels == 1 && FMT.BitsPerSample == 16)
                    {
                        if (kanal0 == null)
                        {
                            DATA.ChunkSize = 0;
                        }
                        else
                        {
                            DATA.ChunkSize = kanal0.Length * 2;
                            binWriter.Write(DATA.ChunkSize);

                            foreach (Int16 i in kanal0)
                                binWriter.Write(i);
                        }
                    }
                    else if (FMT.NumChannels == 1 && FMT.BitsPerSample == 8)
                    {
                        if (kanal0Bit8 == null)
                        {
                            DATA.ChunkSize = 0;
                        }
                        else
                        {
                            DATA.ChunkSize = kanal0Bit8.Length;
                            binWriter.Write(DATA.ChunkSize);

                            foreach (Byte b in kanal0Bit8)
                                binWriter.Write(b);
                        }
                    }
                    else if (FMT.NumChannels == 2 && FMT.BitsPerSample == 16)
                    {
                        DATA.ChunkSize = kanal0.Length * 4;
                        binWriter.Write(DATA.ChunkSize);

                        bool pierwszyKanal = false;
                        for (int i = 0; i < kanal0.Length; )
                        {
                            pierwszyKanal = !pierwszyKanal;
                            if (pierwszyKanal)
                                binWriter.Write(kanal0[i]);
                            else
                                binWriter.Write(kanal1[i++]);
                        }
                    }
                    else if (FMT.NumChannels == 2 && FMT.BitsPerSample == 8)
                    {
                        DATA.ChunkSize = kanal0Bit8.Length * 2;
                        binWriter.Write(DATA.ChunkSize);

                        bool pierwszyKanal = false;
                        for (int i = 0; i < kanal0Bit8.Length; )
                        {
                            pierwszyKanal = !pierwszyKanal;
                            if (pierwszyKanal)
                                binWriter.Write(kanal0Bit8[i]);
                            else
                                binWriter.Write(kanal1Bit8[i++]);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }   
        }

       
        
    }
}
