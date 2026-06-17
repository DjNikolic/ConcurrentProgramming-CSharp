using System.Diagnostics;

namespace GaussianElimination.Solver
{
    public class SolverBenchmark
    {
        public double Measure(ILinearEquasionSolver solver, double[][] matrix, double[] results, out double[] variables)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            solver.Solve(matrix, results, out variables);
            stopwatch.Stop();
            return stopwatch.Elapsed.TotalSeconds;
        }
    }
}