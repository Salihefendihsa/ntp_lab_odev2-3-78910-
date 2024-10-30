using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Kullanıcıdan sayıları alıyoruz
        Console.WriteLine("Sayıları girin (virgülle ayırarak, örneğin: 5,3,8): ");
        string[] inputNumbers = Console.ReadLine().Split(',');
        List<int> numbers = new List<int>();

        foreach (var num in inputNumbers)
        {
            numbers.Add(int.Parse(num.Trim()));
        }

        List<char> operators = new List<char>();
        bool validOperators = false;

        // Geçerli operatörler girilene kadar döngü
        while (!validOperators)
        {
            // Kullanıcıdan operatörleri alıyoruz
            Console.WriteLine("Operatörleri girin (boşlukla ayırarak, örneğin: + - * /): ");
            string[] inputOperators = Console.ReadLine().Split(' ');

            // Operatör listesi doldurulmadan önce temizleniyor
            operators.Clear();

            foreach (var op in inputOperators)
            {
                if (op.Trim().Length == 1 && "+-*/".Contains(op.Trim()))
                {
                    operators.Add(op.Trim()[0]);
                }
            }

            // Operatörler geçerli mi kontrol ediyoruz
            if (operators.Count > 0)
            {
                validOperators = true;
            }
            else
            {
                Console.WriteLine("Geçerli operatörler giriniz! (Boşlukla ayrılmış +, -, *, / gibi operatörler olmalı)");
            }
        }

        // Operatörlerin doğru sıralamasını bulmak için metodu çağırıyoruz
        FindCombination(numbers, operators);
    }

    static void FindCombination(List<int> numbers, List<char> operators)
    {
        int result = numbers[0];  // İlk sayıyı başlangıç noktası olarak belirliyoruz.

        // Her sayı ve operatör kombinasyonunu deniyoruz
        for (int i = 1; i < numbers.Count; i++)
        {
            foreach (var oper in operators)
            {
                result = ApplyOperation(result, numbers[i], oper);

                // Şartlara uyuyor mu kontrol edelim:
                if (result <= 0)
                {
                    Console.WriteLine("Hatalı kombinasyon, sonuç sıfırdan küçük oldu.");
                    break;
                }
                else if (i == numbers.Count - 1)
                {
                    Console.WriteLine("Geçerli bir kombinasyon bulundu: {0} = {1}", oper, result);
                }
            }
        }
    }

    static int ApplyOperation(int a, int b, char oper)
    {
        // Operatörleri sayılar arasında uyguluyoruz
        switch (oper)
        {
            case '+':
                return a + b;
            case '-':
                return a - b;
            case '*':
                return a * b;
            case '/':
                return b != 0 ? a / b : 0;  // Sıfıra bölünme hatasını önlüyoruz
            default:
                throw new ArgumentException("Geçersiz operatör");
        }
    }
}
