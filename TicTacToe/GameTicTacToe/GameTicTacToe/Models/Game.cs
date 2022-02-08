namespace GameTicTacToe.Models
{
	public class Game
	{
		public static GameState CalculateGameStatus(char[] squares)
		{
			var winningCombinations = new int[8, 3]
			{
				{ 0, 1, 2 },
				{ 3, 4, 5 },
				{ 6, 7, 8 },
				{ 0, 3, 6 },
				{ 1, 4, 7 },
				{ 2, 5, 8 },
				{ 0, 4, 8 },
				{ 2, 4, 6 },
			};

			for (int i = 0; i < 8; i++)
			{
				if (squares[winningCombinations[i, 0]] != ' '
					&& squares[winningCombinations[i, 0]] == squares[winningCombinations[i, 1]]
					&& squares[winningCombinations[i, 0]] == squares[winningCombinations[i, 2]])
				{
					return squares[winningCombinations[i, 0]] == 'X' ? GameState.XWins : GameState.OWins;
				}
			}

			bool boardFull = true;

			for (int i = 0; i < squares.Length; i++)
			{
				if (squares[i] == ' ')
				{
					boardFull = false;
					break;
				}
			}

			return boardFull ? GameState.Draw : GameState.StillPlaying;
		}
	}
}

