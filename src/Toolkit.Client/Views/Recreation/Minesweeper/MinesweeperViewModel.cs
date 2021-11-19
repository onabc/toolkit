using Caliburn.Micro;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Toolkit.Client.Models;
using Toolkit.Library.Extensions;

namespace Toolkit.Client.Views
{
    public class MinesweeperViewModel : Screen, IRecreationModule
    {
        /// <summary>
        /// 最大行数
        /// </summary>
        private const int MAX_ROWS = 16;

        /// <summary>
        /// 最大列数
        /// </summary>
        private const int MAX_COLUMNS = 30;

        /// <summary>
        /// 序号
        /// </summary>
        public byte SortNumber { get; init; } = 0;

        /// <summary>
        /// 显示名称
        /// </summary>
        public override string DisplayName { get; set; } = "Minesweeper";

        /// <summary>
        /// 雷数
        /// </summary>
        public int MineCount { get; set; }

        /// <summary>
        /// 行数
        /// </summary>
        public int Rows { get; set; }

        /// <summary>
        /// 列数
        /// </summary>
        public int Columns { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public GameStatus GameStatus { get; set; }

        /// <summary>
        /// 方块列表
        /// </summary>
        public BindableCollection<Tile> TileItems { get; set; }

        protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            Configure();
            InitializeTiles();
            InitializeMines();
            await base.OnInitializeAsync(cancellationToken);
        }

        private void Configure()
        {
            Rows = 9;
            Columns = 9;
            MineCount = 10;
        }

        /// <summary>
        /// 初始化方块
        /// </summary>
        private void InitializeTiles()
        {
            List<Tile> tiles = new List<Tile>();

            for (int i = 0, tileCount = Rows * Columns; i < tileCount; i++)
            {
                Tile tile = new Tile(TileState.Masked);
                tiles.Add(tile);
            }

            TileItems = new BindableCollection<Tile>(tiles);
        }

        /// <summary>
        /// 布雷
        /// </summary>
        private void InitializeMines()
        {
            Hashtable hashtable = new Hashtable();
            Random rm = new Random();
            int maxRandom = Rows * Columns;

            for (int i = 0; hashtable.Count < MineCount; i++)
            {
                int nValue = rm.Next(maxRandom);
                if (hashtable.ContainsValue(nValue))
                    continue;
                Tile tile = TileItems[nValue];
                if (!tile.IsMine)
                {
                    tile.IsMine = true;
                    hashtable.Add(nValue, nValue);
                }
            }
        }

        /// <summary>
        /// 重新开始
        /// </summary>
        public void ReStart()
        {
            InitializeTiles();
            InitializeMines();
            GameStatus = GameStatus.Running;
        }

        /// <summary>
        /// 左键点击
        /// </summary>
        /// <param name="tile"></param>
        /// <param name="e"></param>
        public void MouseLeftButtonDown(Tile tile, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {

            }
            else if (e.ClickCount == 1)
            {
                tile.IsOpen = true;
                if (tile.IsMine)
                {
                    GameStatus = GameStatus.GameOver;
                }
                else
                {
                    tile.MineCount = CalculateMineCount(tile);
                }
            }
            CheckGameStatus();
        }

        /// <summary>
        /// 右键点击
        /// </summary>
        /// <param name="tile"></param>
        public void MouseRightButtonDown(Tile tile)
        {
            if (tile.IsOpen)
                return;

            tile.State++;
            if (tile.State > TileState.Doubt)
            {
                tile.State = TileState.Masked;
            }
        }

        /// <summary>
        /// 计算周围雷数
        /// </summary>
        /// <param name="tile"></param>
        /// <returns></returns>
        private int CalculateMineCount(Tile tile)
        {
            int aroundMineCount = 0;
            IEnumerable<Tile> tiles = GetArroundTile(tile);

            foreach (Tile t in tiles)
            {
                if (tile.IsMine)
                {
                    aroundMineCount++;
                }
            }
            return aroundMineCount;
        }

        private IEnumerable<Tile> GetArroundTile(Tile tile)
        {
            List<Tile> tiles = new List<Tile>();
            int index = TileItems.IndexOf(tile);
            if (index < 0) return tiles;

            List<int> aroundIndex = new List<int>();
            if (index / Columns > 0)
            {
                aroundIndex.Add(index - Columns);
            }
            if (index / Columns < Rows)
            {
                aroundIndex.Add(index + Columns);
            }
            if (index % Columns > 1)
            {
                aroundIndex.Add(index - Columns - 1);
                aroundIndex.Add(index - 1);
                aroundIndex.Add(index + Columns - 1);
            }
            if (index % Columns < Columns - 1)
            {
                aroundIndex.Add(index - Columns + 1);
                aroundIndex.Add(index + 1);
                aroundIndex.Add(index + Columns + 1);
            }

            return TileItems.GetList(aroundIndex);
        }

        private void CheckGameStatus()
        {
            Func<Tile, bool> predicate = x => (x.IsMine && x.State == TileState.Marked) || (!x.IsMine && x.IsOpen);
            if (GameStatus != GameStatus.GameOver && TileItems.All(predicate))
            {
                GameStatus = GameStatus.Bingo;
            }
        }
    }
}