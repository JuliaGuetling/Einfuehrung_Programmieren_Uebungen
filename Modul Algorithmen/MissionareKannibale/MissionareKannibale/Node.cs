namespace MissionareKannibale
{
    public class Node<T>
    {
        public MCState State { get; set; }
        public Node<T>? Parent { get; set; }

        public Node(MCState state, Node<T>? parent)
        {
            State = state;
            Parent = parent;
        }
    }
}
