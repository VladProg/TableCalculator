using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using TableCalculator.Data;

namespace UnitTests
{
    [TestClass]
    public class GraphTest
    {
        private readonly Graph _graph = new();
        private readonly Dictionary<string, List<string>> _edges = new();

        private void Check(string node, List<string> edges,
                           HashSet<string> notCycle, HashSet<string> cycle,
                           HashSet<string> hasntDependent, HashSet<string> hasDependend)
        {
            _edges[node] = edges;
            List<string> changed = _graph.ChangeNode(node, edges);
            HashSet<string> expected = new(notCycle.Union(cycle));
            HashSet<string> processed = new();
            foreach (string cur in changed)
            {
                foreach (string to in _edges[cur])
                    Assert.IsTrue(processed.Contains(to) || !notCycle.Contains(to),
                                  "Graph.ChangeNode: {0} is dependent from {1}, but {1} is not processed before {0}", cur, to);
                processed.Add(cur);
            }
            Assert.IsTrue(processed.SetEquals(expected),
                          "Graph.ChangeNode: returned set \"{0}\" is not equal to expected \"{1}\"",
                          string.Join(", ", processed), string.Join(", ", expected));
            foreach (string cur in notCycle)
                Assert.IsFalse(_graph.IsCycle(cur), "Graph.IsCycle(\"{0}\") is true, but should be false", cur);
            foreach (string cur in cycle)
                Assert.IsTrue(_graph.IsCycle(cur), "Graph.IsCycle(\"{0}\") is false, but should be true", cur);
            foreach (string cur in hasntDependent)
                Assert.IsFalse(_graph.HasDependent(cur), "Graph.HasDependent(\"{0}\") is true, but should be false", cur);
            foreach (string cur in hasDependend)
                Assert.IsTrue(_graph.HasDependent(cur), "Graph.HasDependent(\"{0}\") is false, but should be true", cur);
        }

        [TestMethod]
        public void Test()
        {
            Check(node: "A1",
                  edges: new() { "A2", "A3" },
                  // A1->A2
                  //   ->A3
                  notCycle: new() { "A1" },
                  cycle: new() { },
                  hasntDependent: new() { "A1" },
                  hasDependend: new() { "A2", "A3" });

            Check(node: "A2",
                  edges: new() { "A3" },
                  // A1->A2->A3
                  //   ----->
                  notCycle: new() { "A1", "A2" },
                  cycle: new() { },
                  hasntDependent: new() { "A1" },
                  hasDependend: new() { "A2", "A3" });

            Check(node: "A3",
                  edges: new() { "A3" },
                  // A1->A2->A3<]
                  //   ----->
                  notCycle: new() { },
                  cycle: new() { "A1", "A2", "A3" },
                  hasntDependent: new() { "A1" },
                  hasDependend: new() { "A2", "A3" });

            Check(node: "A3",
                  edges: new() { "A2" },
                  // A1->A2<->A3
                  //   ------>
                  notCycle: new() { },
                  cycle: new() { "A1", "A2", "A3" },
                  hasntDependent: new() { "A1" },
                  hasDependend: new() { "A2", "A3" });

            Check(node: "A2",
                  edges: new() { },
                  // A1->A2<-A3
                  //   ----->
                  notCycle: new() { "A1", "A2", "A3" },
                  cycle: new() { },
                  hasntDependent: new() { "A1" },
                  hasDependend: new() { "A2", "A3" });

            Check(node: "A1",
                  edges: new() { "A1" },
                  // A1<] A2<-A3
                  notCycle: new() { },
                  cycle: new() { "A1" },
                  hasntDependent: new() { "A3" },
                  hasDependend: new() { "A1", "A2" });

            Check(node: "A1",
                  edges: new() { "A3" },
                  // A1 A2<-A3
                  //  ----->
                  notCycle: new() { "A1" },
                  cycle: new() { },
                  hasntDependent: new() { "A1" },
                  hasDependend: new() { "A2", "A3" });

            Check(node: "A2",
                  edges: new() { "A1" },
                  // A1<-A2<-A3
                  //  ------>
                  notCycle: new() { },
                  cycle: new() { "A1", "A2", "A3" },
                  hasntDependent: new() { },
                  hasDependend: new() { "A1", "A2", "A3" });
        }
    }
}
