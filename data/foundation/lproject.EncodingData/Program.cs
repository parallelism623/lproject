using System.Text;

string text = "Hiếu";

PrintBytes("UTF-16 LE (Encoding.Unicode)", Encoding.Unicode.GetBytes(text));
PrintBytes("UTF-8", Encoding.UTF8.GetBytes(text));
PrintBytes("UTF-32 LE", Encoding.UTF32.GetBytes(text));


static void PrintBytes(string label, byte[] bytes)
{
    Console.Write($"{label}: ");
    foreach (var b in bytes)
    {
        Console.Write($"{b} ");  
    }
    Console.WriteLine();

}