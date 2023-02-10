using System;

internal class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        using var game = new Ethereal.Client.Ethereal();
        game.Run();
    }
}