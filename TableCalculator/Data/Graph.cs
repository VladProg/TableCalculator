using System.Collections.Generic;
using System.Diagnostics;

namespace TableCalculator.Data
{
    class Graph
    {
        private readonly Dictionary<string, List<string>> _forwardEdges = new();
        private readonly Dictionary<string, HashSet<string>> _backwardEdges = new();
        private readonly HashSet<string> _cycle = new();

        public bool IsCycle(string node)
            => _cycle.Contains(node);

        public bool IsDependent(string node)
            => _backwardEdges.ContainsKey(node) && _backwardEdges[node].Count > 0;

        private List<string> TopSort(string node, HashSet<string> used = null, List<string> result = null)
        {
            if (used is null)
            {
                used = new();
                result = new();
            }
            if (used.Contains(node))
                return result;
            used.Add(node);
            _cycle.Add(node);
            foreach (var from in _backwardEdges[node])
                TopSort(from, used, result);
            result.Add(node);
            return result;
        }

        public List<string> ChangeNode(string node, List<string> edges)
        {
            if (!_forwardEdges.ContainsKey(node))
                _forwardEdges[node] = new();
            if (!_backwardEdges.ContainsKey(node))
                _backwardEdges[node] = new();
            foreach (string to in _forwardEdges[node])
                _backwardEdges[to].Remove(node);
            _forwardEdges[node] = edges;
            foreach (string to in edges)
            {
                if (!_forwardEdges.ContainsKey(to))
                    _forwardEdges[to] = new();
                if (!_backwardEdges.ContainsKey(to))
                    _backwardEdges[to] = new();
                _backwardEdges[to].Add(node);
            }
            List<string> changed = TopSort(node);
            changed.Reverse();
            foreach(string cur in changed)
            {
                bool notCycle = true;
                foreach(string to in _forwardEdges[cur])
                    if (_cycle.Contains(to))
                    {
                        notCycle = false;
                        break;
                    }
                if (notCycle)
                    _cycle.Remove(cur);
            }
            return changed;
        }
    }
}
