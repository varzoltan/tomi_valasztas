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
            double szazalek = összes*100.0/osszszavazo;
            Console.WriteLine("A választáson {0} állampolgár, a jogosultak {1}%-a vett részt.",összes,szazalek);
            Console.ReadKey();
        }
    }
}
