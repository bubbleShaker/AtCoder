using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Player
{
    static void Main(string[] args)
    {
        string[] inputs;
        inputs = Console.ReadLine().Split(' ');
        int N = int.Parse(inputs[0]); // the total number of nodes in the level, including the gateways
        int L = int.Parse(inputs[1]); // the number of links
        int E = int.Parse(inputs[2]); // the number of exit gateways
        Dictionary<int, HashSet<int>> graph = new Dictionary<int, HashSet<int>>();
        HashSet<int> EISet = new HashSet<int>();

        for (int i = 0; i < N; i++)
        {
            graph[i] = new HashSet<int>();
        }

        for (int i = 0; i < L; i++)
        {
            inputs = Console.ReadLine().Split(' ');
            int N1 = int.Parse(inputs[0]); // N1 and N2 defines a link between these nodes
            int N2 = int.Parse(inputs[1]);
            graph[N1].Add(N2);
            graph[N2].Add(N1);
        }
        for (int i = 0; i < E; i++)
        {
            int EI = int.Parse(Console.ReadLine()); // the index of a gateway node
            EISet.Add(EI);
        }

        // game loop
        while (true)
        {
            int SI = int.Parse(Console.ReadLine()); // The index of the node on which the Bobnet agent is positioned this turn

            Solver solver = new Solver(SI, EISet, graph);
            solver.Debuger();
            var (ansNode1, ansNode2) = solver.ReturnAnsNodes();
            solver.DeleteEdge(ansNode1, ansNode2);

            Console.WriteLine($"{ansNode1} {ansNode2}");
        }
    }
}

class Solver
{
    private int start { get; set; }
    private HashSet<int> gateways { get; set; }
    private Dictionary<int, HashSet<int>> graph { get; set; }
    private HashSet<int> hasChecked { get; set; }
    private int minCount { get; set; }

    public Solver(int s, HashSet<int> gate, Dictionary<int, HashSet<int>> g)
    {
        start = s;
        gateways = gate;
        graph = g;
        hasChecked = new HashSet<int>();
        minCount = 100100100;
    }

    private int ansNode { get; set; }
    private int ansGateway { get; set; }

    public (int, int) ReturnAnsNodes()
    {
        hasChecked.Add(start);
        Dfs(start, 0);
        return (ansNode, ansGateway);
    }

    public void DeleteEdge(int node1, int node2)
    {
        graph[node1].Remove(node2);
        graph[node2].Remove(node1);
    }

    public void Dfs(int u, int count)
    {
        count++;
        Console.Error.WriteLine($"u is {u}, count is {count}");
        foreach (int v in graph[u])
        {
            if (hasChecked.Contains(v)) continue;
            Console.Error.WriteLine($"u is {u}, v is {v}");
            if (gateways.Contains(v) == false)
            {
                hasChecked.Add(v);
                Dfs(v, count);
                continue;
            }
            if (count > minCount) continue;
            minCount = count;
            ansNode = u;
            ansGateway = v;
            Console.Error.WriteLine($"minCount is {minCount}, ansNode is {ansNode}, ansGateway is {ansGateway}");
        }
    }

    public void Debuger()
    {
    }
}