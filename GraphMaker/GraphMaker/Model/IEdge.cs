namespace GraphMaker.Model
{
    public interface IEdge
    {
        INode First { get; }

        INode Second { get; }

        int Length { get; set; }

        INode OtherNode(INode node);

        bool IsIncident(INode node);
    }
}
