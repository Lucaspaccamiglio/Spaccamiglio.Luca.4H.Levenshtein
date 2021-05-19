using System;

namespace Spaccamiglio.Luca._4H.Levenshtein
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Prima parola: ");
            string Primaparola = Console.ReadLine();
            Console.WriteLine("Seconda parola: ");
            string Secondaparola = Console.ReadLine();

            int distanza = (Calcolatore.Frasi(Primaparola, Secondaparola));

            Console.WriteLine("distanza di Levenshtein: " + distanza);
        }

        public static class Calcolatore
        {
            static char c = '*';
            public static int Frasi
            (
                string prima,
                string seconda
            )
            {
                
                if (prima.Length == 0)
                {
                    return seconda.Length;
                }

                if (seconda.Length == 0)
                {
                    return prima.Length;
                }

               
                if (seconda[0].Equals(c) && !prima[0].Equals(c) && seconda.Length <= prima.Length)
                {
                    prima = prima.Substring((prima.Length - seconda.Length) + 1);
                }

                if (prima[0].Equals(c) && !seconda[0].Equals(c) && prima.Length <= seconda.Length)
                {
                    seconda = seconda.Substring((seconda.Length - prima.Length) + 1);
                }

                prima = (prima[0].Equals(c)) ? prima.Trim(c) : prima;
                seconda = (seconda[0].Equals(c)) ? seconda.Trim(c) : seconda;

               
                var d = new int[prima.Length + 1, seconda.Length + 1];
                for (var i = 0; i <= prima.Length; i++)
                {
                    d[i, 0] = i;
                }

                for (var j = 0; j <= seconda.Length; j++)
                {
                    d[0, j] = j;
                }

                for (var i = 1; i <= prima.Length; i++)
                {
                    for (var j = 1; j <= seconda.Length; j++)
                    {
                        var cost = (seconda[j - 1] == prima[i - 1]) ? 0 : 1;
                        d[i, j] = Min(
                         d[i - 1, j] + 1, 
                         d[i, j - 1] + 1, 
                         d[i - 1, j - 1] + cost 
                    );
                    }
                }
                return d[prima.Length, seconda.Length];
            }

            private static int Min(int e1, int e2, int e3) =>
                Math.Min(Math.Min(e1, e2), e3);
        }
    }
}