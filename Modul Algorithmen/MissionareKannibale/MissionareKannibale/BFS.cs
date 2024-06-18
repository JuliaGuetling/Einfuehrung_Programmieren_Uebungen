namespace MissionareKannibale
{
    public class BFS
    {
        public Node<MCState>? Bfs(MCState inital, Func<MCState, bool> goalTest, Func<MCState, List<MCState>> successors)
        {
            Queue<Node<MCState>> frontier = new();
            frontier.Enqueue(new Node<MCState>(inital, null) );

            HashSet<MCState> explored = [inital];  

            while ( frontier.Count > 0 )
            {
                var currentNode = frontier.Dequeue();
                var currentState = currentNode.State;

                if (currentState.GoalTest())
                {
                    return currentNode;
                }

                foreach (var child in currentState.Successors())
                {
                    if (explored.Contains(child))
                    {
                        continue;
                    }
                    explored.Add(child);
                    frontier.Enqueue(new Node<MCState>(child, currentNode));
                }
            }
            return null;
        }
    }
}
