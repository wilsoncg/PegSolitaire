using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PegSolitaire;
using QuickGraph;
using QuickGraph.Graphviz.Dot;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting peg solver...");
            PegBoard initialState = CreateInitialState();
            PegBoard targetState = CreateTargetState();
            Console.WriteLine(string.Format("Initial state: {0}", initialState));
            Console.WriteLine(string.Format("Target state: {0}", targetState));
            var solver = new PegSolitaireSolver(initialState, targetState);
            //var path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            Console.WriteLine("Starting graph generation...");
            var graph = solver.GenerateGraph();
            //var dfs = solver.DepthFirstSearch(graph);
            //var edges = graph.Edges.Select(x => x.Source + "->" + x.Target);
            //foreach (var state in dfs.Keys)
            //{
            //    Console.WriteLine(state);
            //}
            //Console.WriteLine();
            Console.WriteLine(string.Format("Contains solution: {0}", solver.ContainsSolution(graph)));
            var shortestPath = solver.FindShortestPath(graph);
            PrintPath(shortestPath);
            //var graphviz = new QuickGraph.Graphviz.GraphvizAlgorithm<PegBoard, Edge<PegBoard>>(graph, path+"\\graph.dot", GraphvizImageType.Png);
            //graphviz.Generate();
            Console.WriteLine(graph.Vertices.Count());
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press a key to exit.");
            Console.WriteLine();
            Console.ReadKey(true);
            Console.WriteLine();
            Console.WriteLine("Exiting...");
            Environment.Exit(1);

        }

        private static PegBoard CreateTargetState()
        {
            return new PegBoard()
                       {
                           Pegs =
                               {
                                   new Peg(PegState.Blue), new Peg(PegState.Blue), new Peg(PegState.Blue), new Peg(PegState.Blue), 
                                   new Peg(PegState.Empty), new Peg(PegState.Empty),
                                   new Peg(PegState.Red), new Peg(PegState.Red), new Peg(PegState.Red), new Peg(PegState.Red)
                               }
                       };
        }

        private static PegBoard CreateInitialState()
        {
            return new PegBoard()
                       {
                           Pegs =
                               {
                                   new Peg(PegState.Red), new Peg(PegState.Red), new Peg(PegState.Red), new Peg(PegState.Red), 
                                   new Peg(PegState.Empty), new Peg(PegState.Empty),
                                   new Peg(PegState.Blue), new Peg(PegState.Blue), new Peg(PegState.Blue), new Peg(PegState.Blue)
                               }
                       };
        }

        private static void PrintPath(List<Edge<PegBoard>> path)
        {
            foreach (var edge in path)
            {
                Console.WriteLine("[{0}->{1}]", edge.Source, edge.Target);
            }
        }
    }
}
