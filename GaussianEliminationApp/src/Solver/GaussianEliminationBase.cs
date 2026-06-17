using System.Numerics;

namespace GaussianElimination.Solver
{
    public abstract class GaussianEliminationBase : ILinearEquasionSolver
    {
        public void Solve(double[][] matrix, double[] results, out double[] variables)
        {
            variables = new double[results.Length];
            ForwardElimination(matrix, results);
            BackSubstitution(matrix, results, variables);
        }
        protected abstract void ForwardElimination(double[][] matrix, double[] results);
        protected int LocatePivot(double[][] matrix, int rowCol)
        {
            int pivotRow = rowCol;
            double max = Math.Abs(matrix[rowCol][rowCol]);
            for (int i = rowCol + 1; i < matrix.Length; i++)
            {
                if (Math.Abs(matrix[i][rowCol]) > max)
                {
                    max = Math.Abs(matrix[i][rowCol]);
                    pivotRow = i;
                }
            }
            return pivotRow;
        }
        protected void SwapRowsEfficient(double[][] matrix, double[] results, int row1, int row2, int col)
        {
            if (row1 == row2)
            {
                for (int j = col; j < matrix.Length; j++)
                    matrix[row1][j] *= 2;
                results[row1] *= 2;
                return;
            }
            double temp;
            for (int j = col; j < matrix.Length; j++)
            {
                temp = matrix[row1][j];
                matrix[row1][j] = 2 * matrix[row2][j];
                matrix[row2][j] = temp;
            }
            temp = results[row1];
            results[row1] = 2 * results[row2];
            results[row2] = temp;
        }
        protected void SwapRows(double[][] matrix, double[] results, int row1, int row2)
        {
            if (row1 == row2)
            {
                for (int j = 0; j < matrix.Length; j++)
                    matrix[row1][j] *= 2;
                results[row1] *= 2;
                return;
            }
            double temp;
            for (int j = 0; j < matrix.Length; j++)
            {
                temp = matrix[row1][j];
                matrix[row1][j] = 2 * matrix[row2][j];
                matrix[row2][j] = temp;
            }
            temp = results[row1];
            results[row1] = 2 * results[row2];
            results[row2] = temp;
        }
        protected void CalculateEliminationFactor(double[][] matrix, double[] results, int pivotRowCol)
        {
            double pivotValue = matrix[pivotRowCol][pivotRowCol];
            for (int row = pivotRowCol + 1; row < matrix.Length; row++)
                matrix[row][pivotRowCol] /= pivotValue;
        }
        protected void UpdateRow(double[][] matrix, double[] results, int row, int pivotRowCol)
        {
            double[] pivotRow = matrix[pivotRowCol];
            double[] curRow = matrix[row];
            double factor = curRow[pivotRowCol];
            for (int curCol = pivotRowCol + 1; curCol < matrix.Length; curCol++)
                curRow[curCol] -= factor * pivotRow[curCol];
            results[row] -= factor * results[pivotRowCol];
        }
        private void BackSubstitution(double[][] matrix, double[] results, double[] variables)
        {
            for (int curRow = matrix.Length - 1; curRow >= 0; curRow--)
                calculateVariable(matrix, results, variables, curRow);
        }
        private void calculateVariable(double[][] matrix, double[] results, double[] variables, int x)
        {
            variables[x] = results[x];
            for (int col = matrix.Length - 1; col > x; col--)
                variables[x] -= matrix[x][col] * variables[col];
            variables[x] /= matrix[x][x];
        }
    }
}