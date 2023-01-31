namespace GameHub.Utilities
{
    internal static class Utilities
    {
        public readonly static string Line = @"            

- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        ";


        public static void PressAnyButton()
        {
            Console.CursorVisible = false;

            Console.WriteLine(Line + "\nPressione qualquer tecla para continuar");
            Console.ReadKey();
            Console.Clear();

            Console.CursorVisible = true;
        }
    }
}
