using System;

class AsteroidMadencisi
{
    // Enerji maliyetlerini içeren matrisi alarak en az enerji ile hedefe ulaşma fonksiyonu
    static int MinimumEnerjiMaliyeti(int[,] enerjiMatrisi, int N)
    {
        // Minimum maliyetleri saklayacağımız DP tablosu
        int[,] dp = new int[N, N];

        // Başlangıç noktasındaki enerji maliyeti
        dp[0, 0] = enerjiMatrisi[0, 0];

        // İlk satırı doldurma (yalnızca sağa hareket edebiliriz)
        for (int i = 1; i < N; i++)
            dp[0, i] = dp[0, i - 1] + enerjiMatrisi[0, i];

        // İlk sütunu doldurma (yalnızca aşağıya hareket edebiliriz)
        for (int i = 1; i < N; i++)
            dp[i, 0] = dp[i - 1, 0] + enerjiMatrisi[i, 0];

        // Geri kalan hücrelerin maliyetlerini hesaplama
        for (int i = 1; i < N; i++)
        {
            for (int j = 1; j < N; j++)
            {
                // Sağ, aşağı ve sağ çapraz hareketlerin minimumunu alıyoruz
                int yukaridanGelen = dp[i - 1, j];
                int soldanGelen = dp[i, j - 1];
                int caprazdanGelen = (i > 0 && j > 0) ? dp[i - 1, j - 1] : int.MaxValue;

                // Geçerli hücreye ulaşmak için en az enerjiyi seçiyoruz
                dp[i, j] = Math.Min(Math.Min(yukaridanGelen, soldanGelen), caprazdanGelen) + enerjiMatrisi[i, j];
            }
        }

        // Son hücredeki minimum maliyet (N-1, N-1) noktasına ulaşma maliyetidir
        return dp[N - 1, N - 1];
    }

    static void Main()
    {
        int[,] enerjiMatrisi = {
            { 4, 3, 2, 1 },
            { 6, 5, 2, 4 },
            { 1, 1, 1, 1 },
            { 9, 3, 2, 2 }
        };

        int N = enerjiMatrisi.GetLength(0); // NxN boyut
        int sonuc = MinimumEnerjiMaliyeti(enerjiMatrisi, N);

        Console.WriteLine("En az enerji harcayarak hedefe ulaşma maliyeti: " + sonuc);

        // Programın kapanmaması için
        Console.WriteLine("Devam etmek için bir tuşa basın...");
        Console.ReadKey(); // Burada herhangi bir tuşa basmayı bekler
    }
}

