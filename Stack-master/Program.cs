using System.Collections;

namespace StackExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Stack stack = new Stack();
            stack.Push("A");
            stack.Push("B");
            stack.Push("C");

            Console.WriteLine(stack.Pop()); // C
            Console.WriteLine(stack.Pop()); // B
            Console.WriteLine(stack.Pop()); // A
        }
    }
}
