using System.Collections.Generic;

namespace TableCalculator.Data
{
    public class Graph
    {
        /// список вершин, у які веде ребро з заданої
        private readonly Dictionary<string, List<string>> _forwardEdges = new();

        /// список вершин, з яких веде ребро в задану
        private readonly Dictionary<string, HashSet<string>> _backwardEdges = new();

        /// список циклічних вершин (які знаходяться в циклі або з них веде ребро у циклічну)
        private readonly HashSet<string> _cycle = new();

        /// <summary>
        /// чи є вершина циклічною (знаходиться в циклі або з неї веде ребро у циклічну)
        /// </summary>
        /// <param name="node">назва вершини</param>
        /// <returns>чи є вершина циклічною</returns>
        public bool IsCycle(string node)
            => _cycle.Contains(node);

        /// <summary>
        /// чи є хоча б одне ребро, що входить у цю вершину
        /// </summary>
        /// <param name="node">назва вершини</param>
        /// <returns>чи є хоча б одне ребро, що входить у цю вершину</returns>
        public bool HasDependent(string node)
            => _backwardEdges.ContainsKey(node) && _backwardEdges[node].Count > 0;

        /// <summary>
        /// топологічно сортує вершини, з яких є шлях у задану
        /// </summary>
        /// <param name="node">назва вершини</param>
        /// <param name="used">використовується лише всередині функції, множина відвіданих вершин</param>
        /// <param name="result">використовується лише всередині функції, результат</param>
        /// <returns>список топологічно відсортованих вершин: спочатку найвіддаленіші вершини, в кінці сама ця вершина</returns>
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

        /// <summary>
        /// змінює ребра, що виходять із вершини
        /// </summary>
        /// <param name="node">назва вершини</param>
        /// <param name="edges">список вершин, у які веде ребро з заданої</param>
        /// <returns>список змінених вершин: спочатку сама ця вершина, в кінці найвіддаленіші вершини</returns>
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
