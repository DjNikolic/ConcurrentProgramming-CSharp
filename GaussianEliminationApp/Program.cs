using System.Numerics;
using GaussianElimination.Generator;
using GaussianElimination.Solver;
using GaussianElimination.Tester;

namespace GaussianElimination
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the number of equations: ");
            int n = int.Parse(Console.ReadLine());

            MatrixGenerator generator = new MatrixGenerator();
            double[][] matrixSequential = generator.GenerateMatrix(n);
            double[] resultsSequential = generator.GenerateVector(n);
            double[][] matrixParallel = MatrixGenerator.CopyMatrix(matrixSequential);
            double[] resultsParallel = MatrixGenerator.CopyVector(resultsSequential);
            double[] variables;

            ILinearEquasionSolver sequentialSolver = new SequentialGaussianSolver();
            ILinearEquasionSolver parallelSolver = new ParallelGaussianSolver();
            GaussianResultTester tester = new GaussianResultTester();
            SolverBenchmark benchmark = new SolverBenchmark();

            Console.WriteLine($"Number of processors: {Environment.ProcessorCount}");
            Console.WriteLine("");
            Console.WriteLine("Warm up...");
            Console.WriteLine("");

            sequentialSolver.Solve(matrixSequential, resultsSequential, out variables);
            tester.TestFirstRow(matrixSequential[0], resultsSequential[0], variables);
            parallelSolver.Solve(matrixParallel, resultsParallel, out variables);
            tester.TestFirstRow(matrixParallel[0], resultsParallel[0], variables);

            Console.WriteLine("");
            Console.WriteLine("Benchmark started...");
            Console.WriteLine("");

            int repetition = 5;
            double sequentialTotal = 0;
            double parallelTotal = 0;
            double time;

            for (int i = 0; i < repetition; i++)
            {
                matrixSequential = generator.GenerateMatrix(n);
                resultsSequential = generator.GenerateVector(n);
                matrixParallel = MatrixGenerator.CopyMatrix(matrixSequential);
                resultsParallel = MatrixGenerator.CopyVector(resultsSequential);

                time = benchmark.Measure(sequentialSolver, matrixSequential, resultsSequential, out variables);
                Console.WriteLine("Sequential time " + i + $": {time} s");
                Console.WriteLine("");
                tester.TestFirstRow(matrixSequential[0], resultsSequential[0], variables);
                Console.WriteLine("");
                sequentialTotal += time;

                time = benchmark.Measure(parallelSolver, matrixParallel, resultsParallel, out variables);
                Console.WriteLine("Parallel time " + i + $": {time} s");
                Console.WriteLine("");
                tester.TestFirstRow(matrixParallel[0], resultsParallel[0], variables);
                parallelTotal += time;
                Console.WriteLine("");
            }

            double avgSequential = sequentialTotal / repetition;
            double avgParallel = parallelTotal / repetition;
            Console.WriteLine($"Sequential average: {avgSequential:F4} s");
            Console.WriteLine($"Parallel average: {avgParallel:F4} s");
            Console.WriteLine($"Speedup: {(avgSequential - avgParallel) / avgSequential * 100:F4} %");
        }
    }
}