using System;
using UnityEngine;

namespace TicTacToe.Gameplay
{
    public enum PlayerState
    {
        None,
        X,
        O
    }
    
    public class Cell 
    {
        public PlayerState Value { get; private set; }
        
        public bool IsEmpty => Value == PlayerState.None;

        public void SetValue(PlayerState player)
        {
            if (!IsEmpty) return;
            Value = player;
        }
    }

    // GameLogic
    public class GameManager
    {
        public PlayerState CurrentPlayer { get; private set; }

        public void SwitchTurn()
        {
            CurrentPlayer =  CurrentPlayer == PlayerState.X ? PlayerState.O : PlayerState.X;
        }
    }

    public class GameEvents
    {
        public event Action<int> OnCellPlayed;

        public void CellPlayed(int index)
        {
            OnCellPlayed?.Invoke(index);
        }
    }

    public class GameController
    {
        private GameManager gameManager;
        private Cell[] cells;

        public GameController()
        {
            gameManager = new GameManager();
            cells = new Cell[9];

            for (int i = 0; i < 9; i++)
            {
                cells[i] = new Cell();
            }
        }

        public void Play(int index)
        {
            if (!cells[index].IsEmpty) return;
            
            cells[index].SetValue(gameManager.CurrentPlayer);
            gameManager.SwitchTurn();
        }
    }
}

