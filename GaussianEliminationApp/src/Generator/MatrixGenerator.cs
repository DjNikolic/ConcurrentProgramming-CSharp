namespace GaussianElimination.Generator
{
    public class MatrixGenerator
    {
        private readonly Random _random = new Random();
        public double[][] GenerateMatrix(int n)
        {
            double[][] matrix = new double[n][];
            for (int i = 0; i < n; i++)
            {
                matrix[i] = new double[n];
                for (int j = 0; j < n; j++)
                {
                    double value;
                    do
                    {
                        value = _random.NextDouble() * 20000.0 - 10000.0;
                    } while (Math.Abs(value) < 50);
                    matrix[i][j] = value;
                }
            }
            return matrix;
        }
        public double[] GenerateVector(int n)
        {
            double[] vector = new double[n];
            for (int i = 0; i < n; i++)
            {
                double value;
                do
                {
                    value = _random.NextDouble() * 20000.0 - 10000.0;
                } while (Math.Abs(value) < 50);
                vector[i] = value;

            }
            return vector;
        }
        public static double[][] CopyMatrix(double[][] old)
        {
            if (old == null)
                return null;
            double[][] ret = new double[old.Length][];
            for (int i = 0; i < old.Length; i++)
            {
                ret[i] = new double[old[i].Length];
                Array.Copy(old[i], ret[i], old[i].Length);
            }
            return ret;
        }
        public static double[] CopyVector(double[] old)
        {
            if (old == null)
                return null;
            double[] ret = new double[old.Length];
            Array.Copy(old, ret, old.Length);
            return ret;
        }
    }
}