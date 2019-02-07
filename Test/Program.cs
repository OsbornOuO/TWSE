namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null)
            {
                throw new System.ArgumentNullException(nameof(args));
            }
        }
    }
}
