namespace GaussianElimination.Solver
{
    public interface ILinearEquasionSolver
    {
        void Solve(double[][] matrix, double[] results, out double[] variables);
    }
}