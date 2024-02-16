using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ZarzadzanieUbraniami
{
    class Program
    {
        static void Main(string[] args)
        {
            var bazaDanych = new BazaDanychUbran("ubrania.txt");
            var zarzadca = new ZarzadcaUbran(bazaDanych);

            bool running = true;
            while (running)
            {
                Console.WriteLine("Witaj w aplikacji zarządzania ubraniami!");
                Console.WriteLine("1. Dodaj ubranie");
                Console.WriteLine("2. Usuń ubranie");
                Console.WriteLine("3. Wyświetl listę aktualnych ubrań");
                Console.WriteLine("4. Wyjście z aplikacji");
                Console.WriteLine("Wybierz opcję:");

                string wybor = Console.ReadLine();

                if (wybor.All(char.IsDigit))
                {
                    int opcja = int.Parse(wybor);
                    switch (opcja)
                    {
                        case 1:
                            zarzadca.DodajUbranie();
                            break;
                        case 2:
                            zarzadca.UsunUbranie();
                            break;
                        case 3:
                            zarzadca.WyswietlListeUbran();
                            break;
                        case 4:
                            running = false;
                            break;
                        default:
                            Console.WriteLine("Niepoprawny wybór. Wybierz opcję od 1 do 4.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Niepoprawny wybór. Wprowadź tylko cyfry od 1 do 4.");
                }
            }
        }
    }

    abstract class Ubranie
    {
        public string Marka { get; set; }
        public string Rozmiar { get; set; }
        public string Kolor { get; set; }
        public decimal Cena { get; set; }
        public abstract string Kategoria { get; }

        protected Ubranie(string marka, string rozmiar, string kolor, decimal cena)
        {
            Marka = marka;
            Rozmiar = rozmiar;
            Kolor = kolor;
            Cena = cena;
        }

        public override string ToString()
        {
            return $"Marka: {Marka}, Kategoria: {Kategoria}, Rozmiar: {Rozmiar}, Kolor: {Kolor}, Cena: {Cena}";
        }
    }

    class Sweter : Ubranie
    {
        public string RodzajSwetra { get; set; }

        public override string Kategoria { get { return "Sweter"; } }

        public Sweter(string marka, string rozmiar, string kolor, decimal cena, string rodzajSwetra)
            : base(marka, rozmiar, kolor, cena)
        {
            RodzajSwetra = rodzajSwetra;
        }

        public override string ToString()
        {
            return base.ToString() + $", Rodzaj swetra: {RodzajSwetra}";
        }
    }

    class Spodnie : Ubranie
    {
        public string TypSpodni { get; set; }

        public override string Kategoria { get { return "Spodnie"; } }

        public Spodnie(string marka, string rozmiar, string kolor, decimal cena, string typSpodni)
            : base(marka, rozmiar, kolor, cena)
        {
            TypSpodni = typSpodni;
        }

        public override string ToString()
        {
            return base.ToString() + $", Typ spodni: {TypSpodni}";
        }
    }

    class Koszulka : Ubranie
    {
        public string RodzajKoszulki { get; set; }

        public override string Kategoria { get { return "Koszulka"; } }

        public Koszulka(string marka, string rozmiar, string kolor, decimal cena, string rodzajKoszulki)
            : base(marka, rozmiar, kolor, cena)
        {
            RodzajKoszulki = rodzajKoszulki;
        }

        public override string ToString()
        {
            return base.ToString() + $", Rodzaj koszulki: {RodzajKoszulki}";
        }
    }

    class Buty : Ubranie
    {
        public string TypButow { get; set; }

        public override string Kategoria { get { return "Buty"; } }

        public Buty(string marka, string rozmiar, string kolor, decimal cena, string typButow)
            : base(marka, rozmiar, kolor, cena)
        {
            TypButow = typButow;
        }

        public override string ToString()
        {
            return base.ToString() + $", Typ butów: {TypButow}";
        }
    }

    interface IBazaDanychUbran
    {
        void ZapiszUbranie(Ubranie ubranie);
        void UsunUbranie();
        List<Ubranie> PobierzUbrania();
        void ZapiszUbraniaDoPliku(List<Ubranie> ubrania);
    }

    class ZarzadcaUbran
    {
        private IBazaDanychUbran _bazaDanych;

        public ZarzadcaUbran(IBazaDanychUbran bazaDanych)
        {
            _bazaDanych = bazaDanych;
        }

        public void DodajUbranie()
        {
            Console.WriteLine("Dodawanie nowego ubrania:");
            Console.Write("Marka: ");
            string marka = Console.ReadLine();
            Console.Write("Kategoria (Sweter/Spodnie/Koszulka/Buty): ");
            string kategoria = Console.ReadLine();
            Console.Write("Rozmiar: ");
            string rozmiar = Console.ReadLine();
            Console.Write("Kolor: ");
            string kolor = Console.ReadLine();
            Console.Write("Cena: ");
            decimal cena;
            while (!decimal.TryParse(Console.ReadLine(), out cena))
            {
                Console.Write("Niepoprawna cena. Wprowadź poprawną cenę: ");
            }

            Ubranie noweUbranie = null;

            switch (kategoria.ToLower())
            {
                case "sweter":
                    Console.Write("Rodzaj swetra: ");
                    string rodzajSwetra = Console.ReadLine();
                    noweUbranie = new Sweter(marka, rozmiar, kolor, cena, rodzajSwetra);
                    break;
                case "spodnie":
                    Console.Write("Typ spodni: ");
                    string typSpodni = Console.ReadLine();
                    noweUbranie = new Spodnie(marka, rozmiar, kolor, cena, typSpodni);
                    break;
                case "koszulka":
                    Console.Write("Rodzaj koszulki: ");
                    string rodzajKoszulki = Console.ReadLine();
                    noweUbranie = new Koszulka(marka, rozmiar, kolor, cena, rodzajKoszulki);
                    break;
                case "buty":
                    Console.Write("Typ butów: ");
                    string typButow = Console.ReadLine();
                    noweUbranie = new Buty(marka, rozmiar, kolor, cena, typButow);
                    break;
                default:
                    Console.WriteLine("Niepoprawna kategoria.");
                    return;
            }

            _bazaDanych.ZapiszUbranie(noweUbranie);

            Console.WriteLine("Ubranie zostało dodane!");
        }

        public void UsunUbranie()
        {
            _bazaDanych.UsunUbranie();
        }

        public void WyswietlListeUbran()
        {
            List<Ubranie> listaUbran = _bazaDanych.PobierzUbrania();

            Console.WriteLine("Lista aktualnych ubrań:");
            if (listaUbran.Count == 0)
            {
                Console.WriteLine("Brak ubrań w bazie danych.");
            }
            else
            {
                foreach (var ubranie in listaUbran)
                {
                    Console.WriteLine(ubranie);
                }
            }
        }
    }

    class BazaDanychUbran : IBazaDanychUbran
    {
        private string _sciezkaDoPliku;
        private List<Ubranie> _listaUbran = new List<Ubranie>();

        public BazaDanychUbran(string sciezkaDoPliku)
        {
            _sciezkaDoPliku = sciezkaDoPliku;
            if (File.Exists(sciezkaDoPliku))
            {
                Console.WriteLine("Plik istnieje. Rozpoczęcie odczytu danych...");
                using (StreamReader reader = new StreamReader(sciezkaDoPliku))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Console.WriteLine($"Odczytano linię: {line}");
                        string[] elements = line.Split(',');
                        if (elements.Length >= 5)
                        {
                            string kategoria = elements[1];
                            switch (kategoria.ToLower())
                            {
                                case "sweter":
                                    if (elements.Length >= 6)
                                    {
                                        _listaUbran.Add(new Sweter(elements[0], elements[2], elements[3], decimal.Parse(elements[4]), elements[5]));
                                    }
                                    break;
                                case "spodnie":
                                    if (elements.Length >= 6)
                                    {
                                        _listaUbran.Add(new Spodnie(elements[0], elements[2], elements[3], decimal.Parse(elements[4]), elements[5]));
                                    }
                                    break;
                                case "koszulka":
                                    if (elements.Length >= 6)
                                    {
                                        _listaUbran.Add(new Koszulka(elements[0], elements[2], elements[3], decimal.Parse(elements[4]), elements[5]));
                                    }
                                    break;
                                case "buty":
                                    if (elements.Length >= 6)
                                    {
                                        _listaUbran.Add(new Buty(elements[0], elements[2], elements[3], decimal.Parse(elements[4]), elements[5]));
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Błąd: Niepoprawny format danych w pliku.");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Plik nie istnieje.");
            }
        }

        public void ZapiszUbranie(Ubranie ubranie)
        {
            _listaUbran.Add(ubranie);
            ZapiszUbraniaDoPliku(_listaUbran);
        }

        public void UsunUbranie()
        {
            if (_listaUbran.Count == 0)
            {
                Console.WriteLine("Brak ubrań do usunięcia.");
                return;
            }

            Console.WriteLine("Wybierz numer ubrania do usunięcia:");
            for (int i = 0; i < _listaUbran.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_listaUbran[i]}");
            }

            int numerUbrania;
            while (!int.TryParse(Console.ReadLine(), out numerUbrania) || numerUbrania < 1 || numerUbrania > _listaUbran.Count)
            {
                Console.Write("Niepoprawny numer. Wybierz ponownie: ");
            }

            _listaUbran.RemoveAt(numerUbrania - 1);

            ZapiszUbraniaDoPliku(_listaUbran);

            Console.WriteLine("Ubranie zostało usunięte.");
        }

        public List<Ubranie> PobierzUbrania()
        {
            return _listaUbran;
        }

        public void ZapiszUbraniaDoPliku(List<Ubranie> ubrania)
        {
            using (StreamWriter writer = new StreamWriter(_sciezkaDoPliku))
            {
                foreach (var ubranie in ubrania)
                {
                    writer.WriteLine($"{ubranie.Marka},{ubranie.Kategoria},{ubranie.Rozmiar},{ubranie.Kolor},{ubranie.Cena}");
                }
            }
        }
    }
}