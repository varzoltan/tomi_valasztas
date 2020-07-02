using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace tomi_valasztas
{
    class Program
    {
        struct Adat
        {
            public int sorszam;
            public int szavazat;
            public string nev;
            public string part;
        }

        static void Main(string[] args)
        {
            const int osszszavazo = 12345;
            Adat[] adatok = new Adat[100];
            StreamReader olvas = new StreamReader(@"C:\Users\Rendszergazda\Desktop\2013-majus\szavazatok.txt");
            int n = 0;
            while (!olvas.EndOfStream)
            {
                string sor = olvas.ReadLine();
                string[] db = sor.Split();
                adatok[n].sorszam = int.Parse(db[0]);
                adatok[n].szavazat = int.Parse(db[1]);
                adatok[n].nev = db[2] +" "+ db[3];
                adatok[n].part = db[4];
                n++;
            }
            olvas.Close();
            Console.WriteLine("1. Feladat: Beolvasás kész.");
            Console.WriteLine("\n2.Feladat:\nA helyhatósági választásokon {0} képviselőjelölt indult!",n);
            Console.WriteLine("\n3.Feladat:\nKérem adjon meg egy nevet: ");
            string nev = Console.ReadLine();
            int h = 0;
            for (int i =0;i<n;i++)
            {
                if (nev == adatok[i].nev)
                {
                    Console.WriteLine("{0} nevezetű jelölt {1} szavazatot kapott.",nev,adatok[i].szavazat);
                    h++;
                } 
            }
            if (h==0)
            {
                Console.WriteLine("Ilyen nevű képviselőjelölt nem szerepel a nyilvántartásban!");
            }
            int összes = 0;
            for (int i =0;i<n;i++)
            {
                összes  = összes + adatok[i].szavazat;
            }
            double szazalek = összes * 100.0 / osszszavazo;
            Console.WriteLine("A választáson {0} állampolgár, a jogosultak {1}%-a vett részt.",összes,Math.Round(szazalek,2));

            //5.feladat
            /*int gyep =  0;
            int zep = 0;
            int hep = 0;
            int tisz = 0;
            int fuggetlen = 0;
            for (int i =0;i<n;i ++)
            {
                if ("GYEP" ==adatok[i].part)
                {
                    gyep = gyep + adatok[i].szavazat;
                }
                if ("ZEP" == adatok[i].part)
                {
                    zep = zep + adatok[i].szavazat;
                }
                if ("HEP" == adatok[i].part)
                {
                    hep = hep + adatok[i].szavazat;
                }
                if ("TISZ" == adatok[i].part)
                {
                    tisz = tisz + adatok[i].szavazat;
                }
                if ("-" == adatok[i].part)
                {
                    fuggetlen = fuggetlen + adatok[i].szavazat;
                }
            }
            Console.WriteLine("Gyümölcsevők Pártja: {0}%",Math.Round(gyep*100.0/osszszavazo,2));
            Console.WriteLine("Zöldségevők Pártja: {0}%", Math.Round(zep * 100.0 / osszszavazo, 2));
            Console.WriteLine("Húsevők Pártja: {0}%", Math.Round(hep * 100.0 / osszszavazo, 2));
            Console.WriteLine("Tejivók Szövetsége: {0}%", Math.Round(tisz * 100.0 / osszszavazo, 2));
            Console.WriteLine("Független jelöltek: {0}%", Math.Round(fuggetlen * 100.0 / osszszavazo, 2));
            */
            string[] partok = { "GYEP", "HEP", "TISZ", "ZEP", "-" };  
            for(int i = 0; i < partok.Length; i++)
            {
                int osszegez = 0;
                for (int j = 0;j<n;j++)
                {
                    if (partok[i] == adatok[j].part)
                    {
                        osszegez += adatok[j].szavazat;
                    }                  
                }
                double szazaleki = Math.Round((double)osszegez / osszszavazo * 100,2);
                if (partok[i] == "-")
                {
                    Console.WriteLine($"Függetlenek Pártja= {szazaleki}%");
                }
                else
                {
                    Console.WriteLine($"{partok[i]} Pártja= {szazaleki}%");
                }              
            }

            //6.feladat
            int max = 0;
            for (int i =0;i<n;i++ )
            {
                if (max < adatok[i].szavazat)
                {
                    max = adatok[i].szavazat;
                }
            }
            for (int i = 0; i < n; i++)
            {
                if (max== adatok[i].szavazat)
                {
                    if (adatok[i].part == "-")
                    {
                        Console.WriteLine(adatok[i].nev + " Független");
                    }
                    else
                    {
                        Console.WriteLine(adatok[i].nev + " " + adatok[i].part);
                    }
                }
            }
            //7.feladat
            StreamWriter ir = new StreamWriter(@"C:\Users\Rendszergazda\Desktop\2013-majus\kepviselok.txt");
            for(int i = 1; i < n; i++)
            {
                int maximum = 0,z = 0;
                for(int j = 0; j < n; j++)
                {
                    if (adatok[j].sorszam == i)
                    {
                        if (adatok[j].szavazat > maximum)
                        {
                            maximum = adatok[j].szavazat;
                            z = j;
                        }
                    }
                }
                if (maximum != 0)
                {
                    if (adatok[z].part == "-")
                    {
                        ir.WriteLine($"{i}.választókerületben győztes: {adatok[z].nev} pártja: független.");
                    }
                    else
                    {
                        ir.WriteLine($"{i}.választókerületben győztes: {adatok[z].nev} pártja: {adatok[z].part}");
                    }           
                }
            }
            ir.Close();
            Console.ReadKey();
        }
    }
}
