using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.OrTools.LinearSolver;
using System.Data;

namespace BusAllocatorApp
{
    internal class BusAllocator
    {
        static void Main()
        {
            // Create the solver.
            Solver solver = Solver.CreateSolver("CBC_MIXED_INTEGER_PROGRAMMING");

            // Define sets.
            List<string> solo_routes = new List<string> { "ALABANG", "BINAN", "CARMONA", "BALIBAGO", "CABUYAO", "CALAMBA" };

            List<(string, string)> hybrid_routes = new List<(string, string)>
            {
                ("ALABANG", "CARMONA"),
                ("BINAN", "CARMONA"),
                ("CALAMBA", "CABUYAO")
            };

            // Parameters (to be replaced with actual data import code).
            Dictionary<string, double> cost_small_bus = new Dictionary<string, double>();
            Dictionary<string, double> cost_large_bus = new Dictionary<string, double>();
            Dictionary<(string, string), double> cost_small_hybrid_route = new Dictionary<(string, string), double>();
            Dictionary<(string, string), double> cost_large_hybrid_route = new Dictionary<(string, string), double>();
            int capacity_small_bus = 18;
            int capacity_large_bus = 56;
            Dictionary<string, int> buffer_current_small = new Dictionary<string, int>();
            Dictionary<string, int> buffer_current_large = new Dictionary<string, int>();
            Dictionary<(string, string), int> buffer_current_small_hybrid = new Dictionary<(string, string), int>();
            Dictionary<(string, string), int> buffer_current_large_hybrid = new Dictionary<(string, string), int>();
            Dictionary<string, int> demand = new Dictionary<string, int>();

            // Load cost rates from the spreadsheet (pseudo-code; implement actual data loading).
            // For the example, we'll use dummy data.
            foreach (var route in solo_routes)
            {
                cost_small_bus[route] = 100; // Replace with actual cost.
                cost_large_bus[route] = 150; // Replace with actual cost.
                demand[route] = 50; // Replace with actual demand.
                buffer_current_small[route] = 0; // Flat buffer of 0 for small buses.
                buffer_current_large[route] = 3; // Flat buffer of 3 for large buses.
            }

            foreach (var route in hybrid_routes)
            {
                cost_small_hybrid_route[route] = 120; // Replace with actual cost.
                cost_large_hybrid_route[route] = 180; // Replace with actual cost.
                buffer_current_small_hybrid[route] = 0;
                buffer_current_large_hybrid[route] = 3;
            }

            // Decision Variables.
            var x_small = new Dictionary<string, Variable>();
            var x_large = new Dictionary<string, Variable>();
            var y_small = new Dictionary<(string, string), Variable>();
            var y_large = new Dictionary<(string, string), Variable>();

            // Create variables for solo routes.
            foreach (var i in solo_routes)
            {
                x_small[i] = solver.MakeIntVar(0.0, double.PositiveInfinity, $"x_small_{i}");
                x_large[i] = solver.MakeIntVar(0.0, double.PositiveInfinity, $"x_large_{i}");
            }

            // Create variables for hybrid routes.
            foreach (var (i, j) in hybrid_routes)
            {
                y_small[(i, j)] = solver.MakeIntVar(0.0, double.PositiveInfinity, $"y_small_{i}_{j}");
                y_large[(i, j)] = solver.MakeIntVar(0.0, double.PositiveInfinity, $"y_large_{i}_{j}");
            }

            // Objective Function.
            Objective objective = solver.Objective();

            foreach (var i in solo_routes)
            {
                if (cost_small_bus.ContainsKey(i))
                    objective.SetCoefficient(x_small[i], cost_small_bus[i]);
                if (cost_large_bus.ContainsKey(i))
                    objective.SetCoefficient(x_large[i], cost_large_bus[i]);
            }

            foreach (var (i, j) in hybrid_routes)
            {
                if (cost_small_hybrid_route.ContainsKey((i, j)))
                    objective.SetCoefficient(y_small[(i, j)], cost_small_hybrid_route[(i, j)]);
                if (cost_large_hybrid_route.ContainsKey((i, j)))
                    objective.SetCoefficient(y_large[(i, j)], cost_large_hybrid_route[(i, j)]);
            }

            objective.SetMinimization();

            // Constraints.
            foreach (var i in solo_routes)
            {
                LinearExpr lhs = new LinearExpr();

                if (cost_small_bus.ContainsKey(i))
                    lhs += capacity_small_bus * x_small[i];
                if (cost_large_bus.ContainsKey(i))
                    lhs += capacity_large_bus * x_large[i];

                // Add contributions from hybrid routes.
                foreach (var j in solo_routes)
                {
                    var ij = (i, j);
                    var ji = (j, i);

                    if (hybrid_routes.Contains(ij))
                    {
                        if (cost_small_hybrid_route.ContainsKey(ij))
                            lhs += capacity_small_bus * y_small[ij];
                        if (cost_large_hybrid_route.ContainsKey(ij))
                            lhs += capacity_large_bus * y_large[ij];
                    }
                    if (hybrid_routes.Contains(ji))
                    {
                        if (cost_small_hybrid_route.ContainsKey(ji))
                            lhs += capacity_small_bus * y_small[ji];
                        if (cost_large_hybrid_route.ContainsKey(ji))
                            lhs += capacity_large_bus * y_large[ji];
                    }
                }

                // Demand constraint.
                int routeDemand = demand.ContainsKey(i) ? demand[i] : 0;
                solver.Add(lhs >= routeDemand, $"Demand_Constraint_{i}");
            }

            // Solve the problem.
            Solver.ResultStatus resultStatus = solver.Solve();

            // Check if the problem has an optimal solution.
            if (resultStatus == Solver.ResultStatus.OPTIMAL)
            {
                Console.WriteLine($"Total Cost = {solver.Objective().Value()}");

                // Output variable values.
                foreach (var i in solo_routes)
                {
                    Console.WriteLine($"{x_small[i].Name()} = {x_small[i].SolutionValue()}");
                    Console.WriteLine($"{x_large[i].Name()} = {x_large[i].SolutionValue()}");
                }

                foreach (var (i, j) in hybrid_routes)
                {
                    Console.WriteLine($"{y_small[(i, j)].Name()} = {y_small[(i, j)].SolutionValue()}");
                    Console.WriteLine($"{y_large[(i, j)].Name()} = {y_large[(i, j)].SolutionValue()}");
                }
            }
            else
            {
                Console.WriteLine("The problem does not have an optimal solution.");
            }
        }
    }
}
