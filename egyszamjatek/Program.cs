using System;
using System.Runtime.CompilerServices;
using System.Xml.Schema;

namespace egyszamjatek
{
    internal class Program
    {
        static List<Jatekos> fordulok = new();
        static int forduloSorszama;

        static void Main(string[] args)
        {
            Feladat02();
            Feladat03();
            Feladat04();
            Feladat05();
            Feladat06();
            Feladat07();
            Feladat08();
            Feladat09();
            Feladat10();
        }

        static List<Jatekos> egyszamjatek = new();
        private static void Feladat02()
        {           
            using StreamReader sr = new(path:@"..\..\..\src\egyszamjatek.txt");
            while (!sr.EndOfStream)
            {
                string? sor = sr.ReadLine();
                string[] s = sor.Split(' ');
                Jatekos jatekosTippek = new Jatekos();
                jatekosTippek.ForduloList = new List<int>();
                jatekosTippek.Nev = s[s.Length - 1];
                for (int i = 0; i < s.Length-2; i++)
                {
                    jatekosTippek.ForduloList.Add(int.Parse(s[i]));
                }
                egyszamjatek.Add(jatekosTippek);
            }

        }
        private static void Feladat03()
        {
            Console.WriteLine("3. feladat: Játékosok száma: "+egyszamjatek.Count);
        }
        private static void Feladat04()
        {
            int fordulokSzama = egyszamjatek[0].ForduloList.Count;
            Console.WriteLine("4. feladat: Fordulók száma: "+fordulokSzama);
        }
        private static void Feladat05()
        {
            int db=0;
            for (int i = 0; i < egyszamjatek.Count; i++)
            {
                if (egyszamjatek[i].ForduloList[0] == 1)
                {
                    db++; 
                }
            }
            Console.WriteLine("5. feladat: "+ (db!=0 ? "Az első fordulóban volt egyes tipp!" : "Az első fordulóban nem volt egyes tipp!"));
        }
        private static void Feladat06()
        {            
            int max = int.MinValue;
            for (int i = 0; i < egyszamjatek.Count; i++)
            {                
                for (int j = 0; j < egyszamjatek[i].ForduloList.Count; j++)
                {
                    if (egyszamjatek[i].ForduloList[j]>max)
                    {
                        max = egyszamjatek[i].ForduloList[j];
                    }
                }
            }
            Console.WriteLine("6. feladat: A legnagyobb tipp a fordulók során: "+max);
        }
        private static int Feladat07()
        {
            Console.Write("Kérem a forduló sorszámát [1-9]: ");
            forduloSorszama = int.Parse(Console.ReadLine());
            if (forduloSorszama < 1 || forduloSorszama > 9)
            {
                forduloSorszama = 1;
            }            
            return forduloSorszama;
        }
        private static void Feladat08()
        {
            int minSzam = MinTippKeres();            
            Console.WriteLine(minSzam != 0 ? "8. feladat: A nyertes tipp a megadott fordulóban: " + minSzam : "8. feladat: Nem volt egyedi tipp a megadott fordulóban!");
        }

        private static int MinTippKeres()
        {
            Dictionary<int, int> tippek = new Dictionary<int, int>();
            for (int i = 0; i < egyszamjatek.Count; i++)
            {
                if (tippek.ContainsKey(egyszamjatek[i].ForduloList[forduloSorszama - 1]))
                {
                    tippek[egyszamjatek[i].ForduloList[forduloSorszama - 1]]++;
                }
                else
                {
                    tippek[egyszamjatek[i].ForduloList[forduloSorszama - 1]] = 1;
                }
            }
            int minSzam = 0;
            try
            {
                var csakEgy = tippek.Where(t => t.Value == 1);
                minSzam = csakEgy.MinBy(t => t.Key).Key;
            }
            catch (InvalidOperationException)
            {
                minSzam = 0;
            }                                   
            return minSzam;
        }

        private static void Feladat09()
        {
            string nyertesNev = nyertesNevKeres();
            Console.WriteLine(nyertesNev != "" ? "9. feladat: A megadott forduló nyertese: " + nyertesNev : "9. feladat: Nem volt nyertes a megadott fordulóban!");
        }

        private static string nyertesNevKeres()
        {
            int minSzam = MinTippKeres();
            string nyertesNev = "";
            for (int i = 0; i < egyszamjatek.Count; i++)
            {


                if (egyszamjatek[i].ForduloList[forduloSorszama - 1] == minSzam)
                {
                    nyertesNev = egyszamjatek[i].Nev;                    
                    return nyertesNev;
                }

            }
            return nyertesNev;
        }

        private static void Feladat10()
        {
            using StreamWriter sw = new(path:@"..\..\..\bin\Debug\nyertes.txt");            
            int minSzam = MinTippKeres();
            string nyertesNev = nyertesNevKeres();
            sw.WriteLine("Forduló száma: " + forduloSorszama + ".");
            sw.WriteLine("Nyertes tipp: " + minSzam);
            sw.WriteLine("Nyertes játékos: " + nyertesNev);
        }
    }
}