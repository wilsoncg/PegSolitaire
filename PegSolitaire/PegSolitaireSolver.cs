using System;
using System.Collections.Generic;
using System.Linq;
using QuickGraph;
using QuickGraph.Algorithms;
using QuickGraph.Algorithms.Observers;
using QuickGraph.Algorithms.Search;
using QuickGraph.Algorithms.ShortestPath;

namespace PegSolitaire
{
    public class PegSolitaireSolver
    {
        private PegBoard _initialState;
        private PegBoard _targetState;

        public PegSolitaireSolver(PegBoard initialState, PegBoard targetState)
        {
            _initialState = initialState;
            _targetState = targetState;
        }

        public BidirectionalGraph<PegBoard, Edge<PegBoard>> GenerateGraph()
        {
            var directedGraph = new BidirectionalGraph<PegBoard, Edge<PegBoard>>();
            var pegBoardStates = new Queue<PegBoard>();
            pegBoardStates.Enqueue(_initialState);
            directedGraph.AddVertex(_initialState);

            var canMakeMove = true;
            while (canMakeMove)
            {
                if (pegBoardStates.Count > 0)
                {
                    var peekState = pegBoardStates.Peek();
                    if (!peekState.Equals(_targetState))
                    {
                        var state = pegBoardStates.Dequeue();
                        var moves = state.GenerateMoves();
                        foreach (var move in moves)
                        {
                            if (!pegBoardStates.Contains(move))
                            {
                                if (!move.Equals(_targetState))
                                {
                                    pegBoardStates.Enqueue(move);
                                }
                            }
                            directedGraph.AddVertex(move);
                            var edge = new Edge<PegBoard>(state, move);
                            directedGraph.AddEdge(edge);
                        }
                    }
                }
                else
                {
                    canMakeMove = false;
                }
            }

            return directedGraph;
        }

        public BidirectionalGraph<PegBoard, Edge<PegBoard>> GraphFromList(List<Edge<PegBoard>> list)
        {
            var graph = new BidirectionalGraph<PegBoard, Edge<PegBoard>>();

            foreach(var edge in list)
            {
                graph.AddVerticesAndEdge(edge);
            }
            return graph;
        }

        public bool ContainsSolution(BidirectionalGraph<PegBoard, Edge<PegBoard>> graph)
        {
            return graph.Vertices.Contains(_targetState);
        }

        public IDictionary<PegBoard, Edge<PegBoard>> DepthFirstSearch(BidirectionalGraph<PegBoard, Edge<PegBoard>> graph)
        {
            var dfs = new DepthFirstSearchAlgorithm<PegBoard, Edge<PegBoard>>(graph);
            var observer = new VertexPredecessorRecorderObserver<PegBoard, Edge<PegBoard>>();

            using (observer.Attach(dfs))
            {
                dfs.Compute();
            }
            return observer.VertexPredecessors;
        }

        public List<Edge<PegBoard>> FindShortestPath(BidirectionalGraph<PegBoard, Edge<PegBoard>> graph)
        {
            Func<Edge<PegBoard>, double> edgeCost = (e => 1);

            var tryGetPaths = graph.ShortestPathsDijkstra(edgeCost, _initialState);

            // creating the algorithm instance
            var dijkstra =
                new DijkstraShortestPathAlgorithm<PegBoard, Edge<PegBoard>>(graph, edgeCost);

            // creating the observer
            var vis = new VertexPredecessorRecorderObserver<PegBoard, Edge<PegBoard>>();

            // compute and record shortest paths
            using (vis.Attach(dijkstra))
                dijkstra.Compute(_initialState);

            // vis can create all the shortest path in the graph
            IEnumerable<Edge<PegBoard>> path;
            if (tryGetPaths(_targetState, out path))
            {
                return path.ToList();
            }
            return new List<Edge<PegBoard>>();
        }

        public List<Edge<PegBoard>> FindAllPaths(BidirectionalGraph<PegBoard, Edge<PegBoard>> graph)
        {
            return new List<Edge<PegBoard>>();
        }
    }
}
