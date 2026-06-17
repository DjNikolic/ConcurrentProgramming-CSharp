namespace GaussianElimination.Solver
{
    public class SequentialGaussianSolver : GaussianEliminationBase
    {
        protected override void ForwardElimination(double[][] matrix, double[] results)
        {
            for (int curRow = 0; curRow < matrix.Length; curRow++)
            {
                int pivotRow = LocatePivot(matrix, curRow);
                SwapRows(matrix, results, curRow, pivotRow);
                CalculateEliminationFactor(matrix, results, curRow);
                for (int curCol = curRow + 1; curCol < matrix.Length; curCol++)
                    UpdateRow(matrix, results, curCol, curRow);
                // ovde moze ispis za svaki 1000 red - mozda kasnije
            }
        }
    }
}