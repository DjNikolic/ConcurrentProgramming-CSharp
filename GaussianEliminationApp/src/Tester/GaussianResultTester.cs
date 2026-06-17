namespace GaussianElimination.Tester
{
    public class GaussianResultTester
    {
        public void TestFirstRow(double[] row, double result, double[] variables)
        {
            double test = 0;
            for (int col = 0; col < row.Length; col++)
                test += variables[col] * row[col];
            if (test < (result + 0.0002) && test > (result - 0.0002))
                Console.WriteLine($"Resenje je tacno {test}, {result}");
            else
                Console.WriteLine($"Resenje je netacno {test}, {result}");
        }
    }
}