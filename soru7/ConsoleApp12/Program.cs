using System;
using System.Collections.Generic;

class Labirent
{
    static int M = 5; // Labirent genişliği, örneğin 5x5 bir labirent
    static int N = 5; // Labirent yüksekliği
    static bool[,] ziyaretEdildi = new bool[M, N]; // Ziyaret edilen hücreleri takip için

    // Asal sayıları kontrol eden bir fonksiyon
    static bool AsalMi(int sayi)
    {
        if (sayi < 2) return false;
        for (int i = 2; i <= Math.Sqrt(sayi); i++)
        {
            if (sayi % i == 0) return false;
        }
        return true;
    }

    // Belirli bir hücreye geçiş yapılabilir mi kontrol ediyoruz
    static bool KapıAçıkMi(int x, int y)
    {
        // Koordinatların her iki basamağı da asal sayı mı kontrol et
        bool xAsalBasamak = AsalMi(x / 10) && AsalMi(x % 10);
        bool yAsalBasamak = AsalMi(y / 10) && AsalMi(y % 10);

        // Eğer her iki koordinat asal basamak ise devam et
        if (xAsalBasamak && yAsalBasamak)
        {
            int toplam = x + y;
            int carpim = x * y;

            // Toplam çarpıma bölünebiliyorsa kapı açıktır
            if (carpim != 0 && toplam % carpim == 0)
                return true;
        }
        return false;
    }

    // Hedefe ulaşmak için DFS (Derinlik Öncelikli Arama) algoritması
    static bool ŞehreUlaşabilirMiyim(int x, int y, List<Tuple<int, int>> yol)
    {
        // Eğer hedef noktaya geldiysek
        if (x == M - 1 && y == N - 1)
        {
            yol.Add(Tuple.Create(x, y));
            return true;
        }

        // Geçerli hücre ziyaret edildi olarak işaretlenir
        ziyaretEdildi[x, y] = true;
        yol.Add(Tuple.Create(x, y));

        // Hareket yönleri (sağa, aşağı, sola, yukarı)
        int[] dx = { 1, 0, -1, 0 };
        int[] dy = { 0, 1, 0, -1 };

        // Komşu hücreleri gez
        for (int i = 0; i < 4; i++)
        {
            int yeniX = x + dx[i];
            int yeniY = y + dy[i];

            // Yeni koordinat labirentin sınırları içinde mi?
            if (yeniX >= 0 && yeniX < M && yeniY >= 0 && yeniY < N)
            {
                // Kapı açık mı ve bu hücreye daha önce gitmedik mi?
                if (!ziyaretEdildi[yeniX, yeniY] && KapıAçıkMi(yeniX, yeniY))
                {
                    // Eğer hedefe ulaşabiliyorsak true döner
                    if (ŞehreUlaşabilirMiyim(yeniX, yeniY, yol))
                        return true;
                }
            }
        }

        // Eğer buraya geldiysek geri dönmemiz gerekiyor, yolu düzenliyoruz
        yol.RemoveAt(yol.Count - 1);
        return false;
    }

    static void Main()
    {
        List<Tuple<int, int>> yol = new List<Tuple<int, int>>();

        // Başlangıç noktasından hedefe ulaşmaya çalışıyoruz
        if (ŞehreUlaşabilirMiyim(0, 0, yol))
        {
            Console.WriteLine("Şehre ulaşılabilir! Yol:");
            foreach (var adim in yol)
                Console.WriteLine($"({adim.Item1}, {adim.Item2})");
        }
        else
        {
            Console.WriteLine("Şehir kayboldu!");
        }
        Console.ReadLine();
    }
}
