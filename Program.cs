
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conversii
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Introduceti numarul:");
            string number = Console.ReadLine();
            Console.WriteLine("Introduceti baza numarului de intrare:");
            int b1 = int.Parse(Console.ReadLine());
            Console.WriteLine("Introduceti baza tinta:");
            int b2 = int.Parse(Console.ReadLine());

            string rezultat = ConvertfromB1toB2(number, b1, b2);
            Console.WriteLine($"Rezultatul conversiei este {rezultat}");
        }
        static double ConvertToBase10(string number, int sbase)
        {
            double rezultat = 0;

            int integerPartEndIndex = number.IndexOf('.');
            if (integerPartEndIndex == -1)
            {
                integerPartEndIndex = number.Length;
            }
            //parte intreaga
            for (int i = 0; i < integerPartEndIndex; i++)
            {
                char digit = number[i];
                int DigitV = CharToDigitV(digit);
                rezultat = rezultat * sbase + DigitV;
            }
            //parte fractionara 
            if (integerPartEndIndex < number.Length - 1)
            {
                double f = 1.0 / sbase;

                for (int i = integerPartEndIndex + 1; i < number.Length; i++)
                {
                    char digit = number[i];
                    int DigitV = CharToDigitV(digit);
                    rezultat += DigitV * f;
                    f /= sbase;
                }
            }
                    return rezultat;    
        }
        private static string ConvertFrom10(double decimalV, int tbase)
        {
            string rezultat = "";
            //extragem partea intreaga si partea fractionara
            int ParteaIntreaga = (int)decimalV;
            double ParteFractionara = decimalV - ParteaIntreaga;

            //conversia partii intregi
            rezultat = ConvertParteIntreaga(ParteaIntreaga, tbase);

            if (ParteFractionara > 0)//daca exista
            {
                rezultat += ".";
                rezultat += ConvertFractionalPart(ParteFractionara, tbase);
            }
            return rezultat == "" ? "0" : rezultat;

        }
        static string ConvertParteIntreaga(int ParteIntreaga, int tbase)
        {
            if (ParteIntreaga == 0)
            { return "0"; }
            string rezultat = "";
            while (ParteIntreaga > 0)
            {
                int remainder = ParteIntreaga % tbase;
                char digit = DigitVToChar(remainder);
                rezultat = digit + rezultat;
                ParteIntreaga /= tbase;
            }
            return rezultat;
        }
             static string ConvertFractionalPart(double ParteFractionara, int tbase)
        {
            const int maxFractionalDigits = 8;
            string rezultat = "";
            for (int i = 0; i < maxFractionalDigits; i++)
            {
                ParteFractionara *= tbase;
                int digit = (int)ParteFractionara;
                ParteFractionara -= digit;
                rezultat += DigitVToChar(digit);
            }
            return rezultat;
            }
            static int CharToDigitV(char digit)
        {
            if (Char.IsDigit(digit))
                return int.Parse(digit.ToString());
            else
            {
                //pt A-F 
                return char.ToUpper(digit) - 'A' + 10;
            }
        }
        static char DigitVToChar(int digitV)
        {
            if (digitV < 10)
                return digitV.ToString()[0];
            else
            {
                //pt valori mai mari decat 9
                return (char)('A' + digitV - 10);
            }
        }
        //cazul 3
        static string ConvertfromB1toB2(string number, int sbase, int tbase)
        {
            //verif nr negativ
            bool negativ = false;
            if (number[0] == '-')
            {
                negativ = true;
                number = number.Substring(1);
            }
            //conversia din baza b1 in baza 10
            double decimalv = ConvertToBase10(number, sbase);

            //conversia din baza 10 in baza b2
            string rezultat = ConvertFrom10(decimalv, tbase);

            if (negativ)
            {
                rezultat = "-" + rezultat;
            }
            return rezultat;
        }
        }
    }

