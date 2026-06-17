namespace GaussianElimination.Solver
{
    public class ParallelGaussianSolver : GaussianEliminationBase
    {
        private const int SIZE = 500;
        protected override void ForwardElimination(double[][] matrix, double[] results)
        {
            for (int curRow = 0; curRow < matrix.Length; curRow++)
            {
                int pivotRow = LocatePivot(matrix, curRow);
                SwapRows(matrix, results, curRow, pivotRow);
                CalculateEliminationFactor(matrix, results, curRow);

                int start = curRow + 1;
                int end = matrix.Length;
                if (end - start < SIZE)
                {
                    for (int row = start; row < end; row++)
                    {
                        UpdateRow(matrix, results, row, curRow);
                    }
                }
                else
                {
                    Parallel.For(start, end, new ParallelOptions
                    {
                        MaxDegreeOfParallelism = Environment.ProcessorCount
                    }, row =>
                    {
                        UpdateRow(matrix, results, row, curRow);
                    });
                }
            }
        }
    }
}