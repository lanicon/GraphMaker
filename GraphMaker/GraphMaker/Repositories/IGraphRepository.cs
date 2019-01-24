using GraphMaker.Model;

namespace GraphMaker.Repositories
{
    public interface IGraphRepository
    {
        IGraph GetGraph();

        void SaveOrUpdate(IGraph graph);
    }
}
