using UnityEngine;
using System.Collections.Generic;

namespace Case_1
{
    public class GridController : MonoBehaviour
    {
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private GameObject _cellPrefab;
        [SerializeField] private Transform _parent;

        [SerializeField] private int _connectionLimit = 2;
        [SerializeField] private int _nSize;
        [SerializeField] private float _cellSize = 1f; 

        private GameObject[,] grid;
        public int NSize => _nSize;
        public float CellSize => _cellSize;

        public void Init()
        {
            CreateGrid();
            _uiManager.OnBuild += RebuildGrid;
        }

        public bool IsOverlap(Vector2Int gridPos)
        {
            if (gridPos.x >= 0 && gridPos.x < _nSize && gridPos.y >= 0 && gridPos.y < _nSize)
                return true;
            else
                return false;
        }

        public void ActivateCell(Vector2Int gridPos)
        {
            GetCell(gridPos).GetComponent<Cell>().Fill();
            CheckAndDeactivateCells(gridPos);
        }

        private void CheckAndDeactivateCells(Vector2Int lastActivatedGridPos)
        {
            List<Vector2Int> connectedCells = new List<Vector2Int>
            {
                lastActivatedGridPos
            };

            while (true)
            {
                var flag = connectedCells.Count;
                List<Vector2Int> newOnes = new List<Vector2Int>();

                foreach (var connected in connectedCells)
                {
                    foreach (var cells in GetNeighbors(connected))
                    {
                        if (!connectedCells.Contains(cells) && !newOnes.Contains(cells))
                        {
                            newOnes.Add(cells);
                        }
                    }
                }

                connectedCells.AddRange(newOnes);

                if (flag == connectedCells.Count)
                    break;
            }

            if (connectedCells.Count > _connectionLimit)
            {
                foreach (var item in connectedCells)
                {
                    GetCell(item).ResetCell();
                }
            }
        }

        private List<Vector2Int> GetNeighbors(Vector2Int gridPos)
        {
            List<Vector2Int> cells = new List<Vector2Int>();

            if (gridPos.y + 1 < _nSize)
            {
                var cell = GetCell(gridPos + new Vector2Int(0, 1));
                if (cell.IsFull)cells.Add(cell.GetPosition());
            }

            if (gridPos.y - 1 >= 0)
            {
                var cell = GetCell(gridPos + new Vector2Int(0, -1));
                if (cell.IsFull)cells.Add(cell.GetPosition());
            }

            if (gridPos.x + 1 < _nSize)
            {
                var cell = GetCell(gridPos + new Vector2Int(1, 0));
                if (cell.IsFull)cells.Add(cell.GetPosition());
            }

            if (gridPos.x - 1 >= 0)
            {
                var cell = GetCell(gridPos + new Vector2Int(-1, 0));
                if (cell.IsFull)cells.Add(cell.GetPosition());
            }

            return cells;
        }

        public void RebuildGrid(int size)
        {
            DeleteGrid();
            _nSize = size;
            CreateGrid();
        }

        public void CreateGrid()
        {
            grid = new GameObject[_nSize, _nSize];

            for (int y = 0; y < _nSize; y++)
            {
                for (int x = 0; x < _nSize; x++)
                {
                    grid[x, y] = Instantiate(_cellPrefab, new Vector3((x * _cellSize) + (_cellSize * 0.5f), 0.01f, (y * _cellSize) + (_cellSize * 0.5f)), Quaternion.identity);
                    grid[x, y].GetComponent<Cell>().SetPosition(x, y);
                    grid[x, y].transform.parent = _parent;
                }
            }
        }

        private void DeleteGrid()
        {
            for (int y = 0; y < _nSize; y++)
            {
                for (int x = 0; x < _nSize; x++)
                {
                    Destroy(grid[x, y]);
                }
            }
        }

        public Cell GetCell(Vector2Int gridPos)
        {
            return grid[gridPos.x, gridPos.y].GetComponent<Cell>();
        }
    }
}
