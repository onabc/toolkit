using Caliburn.Micro;

namespace Toolkit.Client.Models
{
    public class Tile : PropertyChangedBase
    {
        public bool IsMine { get; set; }

        public bool IsOpen { get; set; }

        public int MineCount { get; set; }

        public TileState State { get; set; }

        public Tile(TileState state)
        {
            State = state;
        }

        public Tile(bool isMine, TileState state)
            : this(state)
        {
            IsMine = isMine;

        }
    }
}
