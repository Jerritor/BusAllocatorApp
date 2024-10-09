using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.OrTools.LinearSolver;
using System.Data;

namespace BusAllocatorApp
{
    public class BusAllocator
    {
        Vars vars;
        public int DemandMode { get; set; }

        #region Model Data
        // Define sets.
        /**
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
        **/
        // List of Routes
        public List<string>? solo_routes { get; set; }
        public List<Tuple<string, string>>? hybrid_routes { get; set; }
        //public List<(string, string)>? hybrid_routes { get; set; }

        // List of Department Names
        public List<string>? deptNames { get; set; }

        // First Day and Second Day
        public DateTime? firstDay { get; set; }
        public DateTime? secondDay { get; set; }

        // List of Time Sets
        public List<TimeSet>? timeSets { get; set; }
        // public List<Dictionary<string, object>>? timeSets { get; set; }

        // Dictionary of Rates
        public Dictionary<string, double>? costSmallBus { get; set; }
        public Dictionary<string, double>? costLargeBus { get; set; }
        public Dictionary<(string, string), double>? costSmallHybridRoute { get; set; }
        public Dictionary<(string, string), double>? costLargeHybridRoute { get; set; }
        //public Dictionary<Tuple<string, string>, double>? costSmallHybridRoute { get; set; }
        //public Dictionary<Tuple<string, string>, double>? costLargeHybridRoute { get; set; }
        //public Dictionary<(string, string), double>? costSmallHybridRoute { get; set; }
        //public Dictionary<(string, string), double>? costLargeHybridRoute { get; set; }


        // Dictionary of Buffer Capacities
        public Dictionary<object, int>? bufferCurrentSmall { get; set; }
        public Dictionary<object, int>? bufferCurrentLarge { get; set; }

        // Bus Max Capacities
        public int capacitySmallBus { get; set; }
        public int capacityLargeBus { get; set; }

        //////////
        // Dictionary to hold demand per route
        public Dictionary<string, int> demand { get; set; }

        // Decision Variables
        private Dictionary<string, Variable>? x_small;
        private Dictionary<string, Variable>? x_large;
        private Dictionary<(string, string), Variable>? y_small;
        private Dictionary<(string, string), Variable>? y_large;
        //private Dictionary<Tuple<string, string>, Variable>? y_small;
        //private Dictionary<Tuple<string, string>, Variable>? y_large;
        //private Dictionary<(string, string), Variable>? y_small;
        //private Dictionary<(string, string), Variable>? y_large;

        // Solver Instance
        private Solver? solver;

        #endregion

        public BusAllocator(Vars vars, int demandMode)
        {
            this.vars = vars;
            this.DemandMode = demandMode;

            IntegrateVars();
            //set to true if you want to try with test values
            //InstantiateVars();
            
            IncomingModel();
        }

        #region Model variables instantiation
        void InstantiateVars(bool isTemplate = false)
        {
            /**
            if (isTemplate)
            {
                InstantiateTemplateVars();
            }
            else
            {

            }
            **/
            IntegrateVars();
        }

        //Integrates data from the Vars instance into the BusAllocator class properties.
        private void IntegrateVars()
        {
            if (vars == null)
            {
                throw new ArgumentNullException(nameof(vars), "Vars instance cannot be null.");
            }

            // Assigning Solo Routes
            if (vars.solo_routes != null)
            {
                this.solo_routes = new List<string>(vars.solo_routes);
            }
            else
            {
                vars.mainForm.WriteLine("Solo Routes could not be integrated.");
                this.solo_routes = new List<string>();
            }

            // Assigning Hybrid Routes
            /**
            if (vars.hybrid_routes != null)
            {
                this.hybrid_routes = new List<(string, string)>(vars.hybrid_routes);
            }
            else
            {
                this.hybrid_routes = new List<(string, string)>();
            }**/
            if (vars.hybrid_routes != null)
            {
                this.hybrid_routes = new List<Tuple<string, string>>(vars.hybrid_routes);
            }
            else
            {
                vars.mainForm.WriteLine("Hybrid Routes could not be integrated.");
                this.hybrid_routes = new List<Tuple<string, string>>();
            }

            // Assigning Department Names
            if (vars.deptNames != null)
            {
                this.deptNames = new List<string>(vars.deptNames);
            }
            else
            {
                vars.mainForm.WriteLine("Department Names could not be integrated.");
                this.deptNames = new List<string>();
            }

            // Assigning First Day and Second Day
            this.firstDay = vars.firstDay;
            this.secondDay = vars.secondDay;

            // Assigning Time Sets
            if (vars.timeSets != null)
            {
                this.timeSets = new List<TimeSet>(vars.timeSets);
            }
            else
            {
                vars.mainForm.WriteLine("Time Sets could not be integrated.");
                this.timeSets = new List<TimeSet>();
            }

            // Assigning Cost Dictionaries
            this.costSmallBus = vars.costSmallBus != null
                ? new Dictionary<string, double>(vars.costSmallBus)
                : new Dictionary<string, double>();

            this.costLargeBus = vars.costLargeBus != null
                ? new Dictionary<string, double>(vars.costLargeBus)
                : new Dictionary<string, double>();

            this.costSmallHybridRoute = vars.costSmallHybridRoute != null
                ? new Dictionary<(string, string), double>(vars.costSmallHybridRoute)
                : new Dictionary<(string, string), double>();

            this.costLargeHybridRoute = vars.costLargeHybridRoute != null
                ? new Dictionary<(string, string), double>(vars.costLargeHybridRoute)
                : new Dictionary<(string, string), double>();
            /**
            this.costSmallHybridRoute = vars.costSmallHybridRoute != null
                ? new Dictionary<Tuple<string, string>, double>(vars.costSmallHybridRoute)
                : new Dictionary<Tuple<string, string>, double>();

            this.costLargeHybridRoute = vars.costLargeHybridRoute != null
                ? new Dictionary<Tuple<string, string>, double>(vars.costLargeHybridRoute)
                : new Dictionary<Tuple<string, string>, double>();

            this.costSmallHybridRoute = vars.costSmallHybridRoute != null
                ? new Dictionary<(string, string), double>(vars.costSmallHybridRoute)
                : new Dictionary<(string, string), double>();

            this.costLargeHybridRoute = vars.costLargeHybridRoute != null
                ? new Dictionary<(string, string), double>(vars.costLargeHybridRoute)
                : new Dictionary<(string, string), double>();
            **/




            // Assigning Buffer Capacities
            this.bufferCurrentSmall = vars.bufferCurrentSmall != null
                ? new Dictionary<object, int>(vars.bufferCurrentSmall)
                : new Dictionary<object, int>();

            this.bufferCurrentLarge = vars.bufferCurrentLarge != null
                ? new Dictionary<object, int>(vars.bufferCurrentLarge)
                : new Dictionary<object, int>();

            // Assigning Bus Capacities
            this.capacitySmallBus = vars.capacitySmallBus;
            this.capacityLargeBus = vars.capacityLargeBus;


            //Integrate Demand Data
            this.demand = vars.GetDemandsForAlloc(this.DemandMode);
        }
        /**
        private void InstantiateTemplateVars()
        {
            solo_routes = new List<string> { "ALABANG", "BINAN", "CARMONA", "BALIBAGO", "CABUYAO", "CALAMBA" };

            hybrid_routes = new List<(string, string)>
            {
                ("ALABANG", "CARMONA"),
                ("BINAN", "CARMONA"),
                ("CALAMBA", "CABUYAO")
            };

            // Parameters (to be replaced with actual data import code).
            cost_small_bus = new Dictionary<string, double>();
            cost_large_bus = new Dictionary<string, double>();
            cost_small_hybrid_route = new Dictionary<(string, string), double>();
            cost_large_hybrid_route = new Dictionary<(string, string), double>();
            
            capacity_small_bus = 18;
            capacity_large_bus = 56;
            
            buffer_current_small = new Dictionary<string, int>();
            buffer_current_large = new Dictionary<string, int>();
            buffer_current_small_hybrid = new Dictionary<(string, string), int>();
            buffer_current_large_hybrid = new Dictionary<(string, string), int>();
            
            demand = new Dictionary<string, int>();
        }**/
        #endregion

        /**
        void IncomingModel()
        {
            // Create the solver.
            Solver solver = Solver.CreateSolver("CBC_MIXED_INTEGER_PROGRAMMING");

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
        **/

        public void IncomingModel()
        {
            // Initialize the solver
            solver = Solver.CreateSolver("CBC_MIXED_INTEGER_PROGRAMMING");
            if (solver == null)
            {
                throw new Exception("Failed to create solver.");
            }

            // Initialize Decision Variables
            InitializeDecisionVariables();

            // Define Objective Function
            DefineObjectiveFunction();

            // Define Constraints
            DefineConstraints();

            // Solve the problem
            Solver.ResultStatus resultStatus = solver.Solve();

            // Handle Solution
            HandleSolution(resultStatus);
        }

        /// Initializes decision variables for the optimization model.
        /// </summary>
        private void InitializeDecisionVariables()
        {
            // Initialize Decision Variables
            x_small = new Dictionary<string, Variable>();
            x_large = new Dictionary<string, Variable>();
            y_small = new Dictionary<(string, string), Variable>();
            y_large = new Dictionary<(string, string), Variable>();
            //y_small = new Dictionary<Tuple<string, string>, Variable>();
            //y_large = new Dictionary<Tuple<string, string>, Variable>();
            //y_small = new Dictionary<(string, string), Variable>();
            //y_large = new Dictionary<(string, string), Variable>();

            // Create variables for solo routes.
            foreach (var route in solo_routes)
            {
                x_small[route] = solver!.MakeIntVar(0.0, double.PositiveInfinity, $"x_small_{route}");
                x_large[route] = solver.MakeIntVar(0.0, double.PositiveInfinity, $"x_large_{route}");
            }

            // Create variables for hybrid routes.
            foreach (var hybrid in hybrid_routes)
            {
                var key = (hybrid.Item1, hybrid.Item2); // Convert Tuple to value tuple
                y_small[key] = solver.MakeIntVar(0.0, double.PositiveInfinity, $"y_small_{hybrid.Item1}_{hybrid.Item2}");
                y_large[key] = solver.MakeIntVar(0.0, double.PositiveInfinity, $"y_large_{hybrid.Item1}_{hybrid.Item2}");
            }
            /**foreach (var hybrid in hybrid_routes)
            {
                y_small[hybrid] = solver.MakeIntVar(0.0, double.PositiveInfinity, $"y_small_{hybrid.Item1}_{hybrid.Item2}");
                y_large[hybrid] = solver.MakeIntVar(0.0, double.PositiveInfinity, $"y_large_{hybrid.Item1}_{hybrid.Item2}");
            }**/
        }

        /// <summary>
        /// Defines the objective function to minimize total costs.
        /// </summary>
        private void DefineObjectiveFunction()
        {
            Objective objective = solver!.Objective();

            // Solo Routes Costs
            foreach (var route in solo_routes)
            {
                if (costSmallBus.ContainsKey(route))
                {
                    objective.SetCoefficient(x_small[route], costSmallBus[route]);
                }
                if (costLargeBus.ContainsKey(route))
                {
                    objective.SetCoefficient(x_large[route], costLargeBus[route]);
                }
            }

            // Hybrid Routes Costs
            
            /**
            foreach (var hybrid in hybrid_routes)
            {
                if (costSmallHybridRoute.ContainsKey(hybrid))
                {
                    objective.SetCoefficient(y_small[hybrid], costSmallHybridRoute[hybrid]);
                }
                if (costLargeHybridRoute.ContainsKey(hybrid))
                {
                    objective.SetCoefficient(y_large[hybrid], costLargeHybridRoute[hybrid]);
                }
            }**/

            objective.SetMinimization();
        }

        /// <summary>
        /// Defines the constraints for the optimization model.
        /// </summary>
        private void DefineConstraints()
        {
            foreach (var route in solo_routes)
            {
                //LinearExpr lhs = 0;
                LinearExpr lhs = new LinearExpr();

                if (costSmallBus.ContainsKey(route))
                {
                    lhs += capacitySmallBus * x_small[route];
                }

                if (costLargeBus.ContainsKey(route))
                {
                    lhs += capacityLargeBus * x_large[route];
                }

                // Add contributions from hybrid routes where the current route is involved
                foreach (var hybrid in hybrid_routes)
                {
                    // Convert Tuple<string, string> to value tuple (string, string)
                    var key = (hybrid.Item1, hybrid.Item2);

                    if (hybrid.Item1 == route)
                    {
                        if (costSmallHybridRoute.ContainsKey(key))
                        {
                            lhs += capacitySmallBus * y_small[key];
                        }
                        if (costLargeHybridRoute.ContainsKey(key))
                        {
                            lhs += capacityLargeBus * y_large[key];
                        }
                    }

                    if (hybrid.Item2 == route)
                    {
                        if (costSmallHybridRoute.ContainsKey(key))
                        {
                            lhs += capacitySmallBus * y_small[key];
                        }
                        if (costLargeHybridRoute.ContainsKey(key))
                        {
                            lhs += capacityLargeBus * y_large[key];
                        }
                    }
                }
                /**
                // Add contributions from hybrid routes where current route is involved
                foreach (var hybrid in hybrid_routes)
                {
                    if (hybrid.Item1 == route)
                    {
                        if (costSmallHybridRoute.ContainsKey(hybrid))
                        {
                            lhs += capacitySmallBus * y_small[hybrid];
                        }
                        if (costLargeHybridRoute.ContainsKey(hybrid))
                        {
                            lhs += capacityLargeBus * y_large[hybrid];
                        }
                    }

                    if (hybrid.Item2 == route)
                    {
                        if (costSmallHybridRoute.ContainsKey(hybrid))
                        {
                            lhs += capacitySmallBus * y_small[hybrid];
                        }
                        if (costLargeHybridRoute.ContainsKey(hybrid))
                        {
                            lhs += capacityLargeBus * y_large[hybrid];
                        }
                    }
                }**/

                // Demand constraint
                int routeDemand = demand.ContainsKey(route) ? demand[route] : 0;
                // Create the constraint and set its name
                //Google.OrTools.LinearSolver.Constraint constraint
                solver.Add(lhs >= routeDemand); //constraint added without a name
                Console.WriteLine($"Added demand constraint for route: {route}");
                //constraint.SetName($"Demand_Constraint_{route}");
                //int routeDemand = demand.ContainsKey(route) ? demand[route] : 0;
                //solver.Add(lhs >= routeDemand, $"Demand_Constraint_{route}");
            }
        }

        /// <summary>
        /// Handles the solution after the solver has run.
        /// </summary>
        /// <param name="resultStatus">The result status from the solver.</param>
        private void HandleSolution(Solver.ResultStatus resultStatus)
        {
            if (resultStatus == Solver.ResultStatus.OPTIMAL)
            {
                vars.mainForm.WriteLine($"Total Cost = {solver!.Objective().Value()}");

                // Output variable values for solo routes
                foreach (var route in solo_routes)
                {
                    vars.mainForm.WriteLine($"{x_small[route].Name()} = {x_small[route].SolutionValue()}");
                    vars.mainForm.WriteLine($"{x_large[route].Name()} = {x_large[route].SolutionValue()}");
                }

                // Output variable values for hybrid routes
                foreach (var hybrid in hybrid_routes)
                {
                    var key = (hybrid.Item1, hybrid.Item2); // Convert Tuple to value tuple
                    vars.mainForm.WriteLine($"{y_small[key].Name()} = {y_small[key].SolutionValue()}");
                    vars.mainForm.WriteLine($"{y_large[key].Name()} = {y_large[key].SolutionValue()}");
                }
                /**foreach (var hybrid in hybrid_routes)
                {
                    vars.mainForm.WriteLine($"{y_small[hybrid].Name()} = {y_small[hybrid].SolutionValue()}");
                    vars.mainForm.WriteLine($"{y_large[hybrid].Name()} = {y_large[hybrid].SolutionValue()}");
                }**/
            }
            else
            {
                vars.mainForm.WriteLine("The problem does not have an optimal solution.");
            }
        }
    }
}
