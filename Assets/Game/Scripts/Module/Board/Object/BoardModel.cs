using Agate.MVC.Base;

namespace Jaddwal.Board
{
    public class BoardModel : BaseModel, IBoardModel
    {
        public int SizeX { get; set; }
        public int SizeY { get; set; }
    }

    public interface IBoardModel : IBaseModel
    {
        int SizeX { get; }
        int SizeY { get; }
    }
}
