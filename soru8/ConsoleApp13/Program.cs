using System;
using System.Collections.Generic;

class SifreCozucu
{
    // Fibonacci sayıları
    static List<int> fibonacciSayilari = new List<int> { 1, 1, 2, 3, 5, 8, 13, 21, 34, 55 };

    // Asal sayıyı kontrol eden fonksiyon
    static bool AsalMi(int sayi)
    {
        if (sayi < 2) return false;
        for (int i = 2; i <= Math.Sqrt(sayi); i++)
        {
            if (sayi % i == 0) return false;
        }
        return true;
    }

    // Orijinal mesajı çözme fonksiyonu
    static string MesajCoz(string sifreliMesaj)
    {
        char[] cozulenMesaj = new char[sifreliMesaj.Length];

        for (int i = 0; i < sifreliMesaj.Length; i++)
        {
            int pozisyon = i + 1;
            int sifreliKarakter = (int)sifreliMesaj[i];
            int fibonacci = fibonacciSayilari[i % fibonacciSayilari.Count];

            int orijinalAscii;

            if (AsalMi(pozisyon))
            {
                // Asal pozisyonlarda mod 100
                orijinalAscii = (sifreliKarakter + 100 * fibonacci) / fibonacci;
            }
            else
            {
                // Asal olmayan pozisyonlarda mod 256
                orijinalAscii = (sifreliKarakter + 256 * fibonacci) / fibonacci;
            }

            cozulenMesaj[i] = (char)orijinalAscii;
        }

        return new string(cozulenMesaj);
    }

    static void Main()
    {
        Console.Write("Şifreli mesajı girin: ");
        string sifreliMesaj = Console.ReadLine();
        string cozulenMesaj = MesajCoz(sifreliMesaj);

        Console.WriteLine("Çözülen mesaj: " + cozulenMesaj);

        // Programın kapanmaması için
        Console.WriteLine("Devam etmek için bir tuşa basın...");
        Console.ReadKey();
    }
}
