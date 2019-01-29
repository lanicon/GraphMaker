using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GraphMaker.Model;

namespace GraphMaker.Extensions
{
    public static partial class GraphExtensions
    {
        public static List<IEdge> GetMinimumSpanningTree(this IGraph graph)
        {
            throw new NotImplementedException();
        }

        public static int CompareByLength(IEdge edge1, IEdge edge2)
        {
            return edge1.Length.CompareTo(edge2.Length);
        }

        public static string PrinLength(List<IEdge> edges)
        {
            string temp="";
            for(int i=0; i < edges.Count;i++)
            {
                temp += edges[i].Length.ToString();
            }
            return temp;
        }

        public static void PrintEdges(List<IEdge> edges)
        {
            int sum = 0;
            foreach (var edge in edges)
            {
                sum += edge.Length;
            }
            string temp = "Минимальная длина остовного дерева = " + sum.ToString() + "\n";
            foreach (var edge in edges)
            {
                temp += edge.ToString() + " (" + edge.Length + ")\n";
            }
            MessageBox.Show(temp);
        }

        public static List<IEdge> MinTreePrim(this IGraph graph)
        {
            List<IEdge> primEdge = new List<IEdge>();
            List<IEdge> tempEdge = new List<IEdge>();
            List<INode> primNode = new List<INode>();
            int count = 1;
            if (graph.CCcountStackDFS() > 1)
            {
                MessageBox.Show("Поиск минимального остовного дерева невозможен, так как граф несвязный");
                return null;
            }
            if (graph.Nodes.Count == 0)
            {
                MessageBox.Show("Не был найден граф");
                return null;
            }
            if (graph.Edges.Count == 0)
            {
                MessageBox.Show("Не найдено ни одного ребра");
                return null;
            }
            //MessageBox.Show(PrinLength(graph.Nodes[0].IncidentEdges));
            
            //Добавить проверку на количество компонент связности            
                primNode.Add((Node)graph.Nodes[0]);
                tempEdge.AddRange(graph.Nodes[0].IncidentEdges);
                //graph.Nodes[0].IncidentEdges.Sort(CompareByLength);
                while(count<graph.Nodes.Count)
                {
                    tempEdge = tempEdge.Where(
                       x => (
                           (primNode.Where(
                               z => z.Number == x.First.Number).Count() != 0) ^
                           primNode.Where(
                               z => z.Number == x.Second.Number).Count() != 0))
                       .ToList();
                    tempEdge.Sort(CompareByLength);
                    primEdge.Add(tempEdge.First());

                    if (primNode.Contains(primEdge.Last().First))
                    {
                        primNode.Add(primEdge.Last().Second);                        
                    }
                    else
                    {
                        primNode.Add(primEdge.Last().First);
                    }
                                        
                    tempEdge.AddRange(graph.Nodes
                        .Where(
                            x => x.Number == primNode.Last().Number)
                        .First().IncidentEdges);
                    count++;
                }
            PrintEdges(primEdge);
            return primEdge;
        }
    }
}
