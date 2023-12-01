namespace Calculator.MVVM.Model
{
    class CalculatorManager
    {
        internal static double? Calculate(double num1, double num2, string symb)
        {
            switch (symb)
            {
                case "+": return num1 + num2;
                case "-": return num1 - num2;
                case "*": return num1 * num2;
                case "/": return num1 / num2;
                default: return null;
            }
        }

        internal static long? Factorial(int num)
        {
            if(num < 0)
                return null;

            long result = 1;
            for (int i = 1; i != num + 1; i++)
            {
                result *= i;
            }

            return result;
        }
    }
}
