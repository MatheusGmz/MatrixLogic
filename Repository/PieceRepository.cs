using PushStart_Console.Application;
using PushStart_Console.Models;
using PushStart_Console.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PushStart_Console
{
    public class PieceRepository
    {

        public int[,] AddPiece(int[,] matrix, int maxLines, int maxColumns, PieceModel piece)
        {
            if (maxLines < piece.LineSize || maxColumns < piece.ColumnSize)
            {
                Console.WriteLine(string.Concat("Não há espaço para a peça ", piece.Name, "! \nAltere o tamanho da matriz ou a quantidade de peças."), Console.ForegroundColor = ConsoleColor.Red);
                Console.ForegroundColor = ConsoleColor.White;
                return matrix;
            }

            var possibilities = new List<PieceModel>();
            for (int line = 0; line < maxLines; line++)
            {
                for (int column = 0; column < maxColumns; column++)
                {
                    if (matrix[line, column] == 0 && maxColumns - column >= piece.ColumnSize && maxLines - line >= piece.LineSize)
                    {
                        possibilities.Add(new PieceModel() { LinePosition = line, ColumnPosition = column });
                    }
                }
            }
            var invalid = new List<PieceModel>();
            foreach (var p in possibilities)
            {
                for (int c = p.ColumnPosition; c < piece.ColumnSize + p.ColumnPosition; c++)
                {
                    for (int l = p.LinePosition; l < piece.LineSize + p.LinePosition; l++)
                    {
                        if (matrix[l, c] != 0)
                        {
                            invalid.Add(p);
                        }
                    }
                }
            }
            foreach (var i in invalid)
            {
                possibilities.Remove(i);
            }
            if (possibilities.Count <= 0)
            {
                GameApplication.impossibleList.Add(piece.Id);
                return matrix;
            }

            var rnd = new Random(DateTime.Now.Millisecond);
            var position = rnd.Next(0, possibilities.Count);

            for (int c = possibilities[position].ColumnPosition; c < piece.ColumnSize + possibilities[position].ColumnPosition; c++)
            {
                for (int l = possibilities[position].LinePosition; l < piece.LineSize + possibilities[position].LinePosition; l++)
                {
                    matrix[l, c] = piece.Id;
                }
            }

            return matrix;
        }
        public List<string> Challange(int[,] matrix, int maxLines, int maxColumns)
        {
            var afirmations = new List<string>();
            if (FindPurplePosition(matrix, maxLines, maxColumns))
            {
                afirmations.Add("A peça roxa (ID 4) está acima de uma peça vermelha (ID 2).");
            }
            if (FindGreenPosition(matrix, maxLines, maxColumns))
            {
                afirmations.Add("A peça verde (ID 5) está à direita de uma peça azul (ID 6).");
            }
            if (FindYellowPosition(matrix, maxLines, maxColumns))
            {
                afirmations.Add("A peça amarela (ID 3) está abaixo de uma peça preta (ID 1).");
            }

            return afirmations;
        }
        public bool BasicValidator(GameModel model)
        {
            var totalFields = 0;
            foreach (var piece in model.Pieces)
            {
                for (int x = 0; x < piece.Quantity; x++)
                {
                    totalFields += piece.LineSize * piece.ColumnSize;
                }
            }
            return totalFields != model.Columns * model.Lines;

        }
        public bool FindPurplePosition(int[,] matrix, int maxLines, int maxColumns)
        {
            var purplePositions = new List<PieceModel>();
            var redPositions = new List<PieceModel>();
            for (int l = 0; l < maxLines; l++)
            {
                for (int c = 0; c < maxColumns; c++)
                {
                    if (matrix[l, c] == 4)
                    {
                        purplePositions.Add(new PieceModel() { LinePosition = l, ColumnPosition = c });
                    }
                }
            }
            for (int l = 0; l < maxLines; l++)
            {
                for (int c = 0; c < maxColumns; c++)
                {
                    if (matrix[l, c] == 2)
                    {
                        redPositions.Add(new PieceModel() { LinePosition = l, ColumnPosition = c });
                    }
                }
            }
            foreach (var pp in purplePositions)
            {
                var search = redPositions.Where(rp => rp.LinePosition > pp.LinePosition).ToList();
                if (search.Count() > 0)
                {
                    return true;
                }
            }
            return false;

        }
        public bool FindYellowPosition(int[,] matrix, int maxLines, int maxColumns)
        {
            var yellowPosition = new List<PieceModel>();
            var blackPosition = new List<PieceModel>();
            for (int l = 0; l < maxLines; l++)
            {
                for (int c = 0; c < maxColumns; c++)
                {
                    if (matrix[l, c] == 3)
                    {
                        yellowPosition.Add(new PieceModel() { LinePosition = l, ColumnPosition = c });
                    }
                }
            }
            for (int l = 0; l < maxLines; l++)
            {
                for (int c = 0; c < maxColumns; c++)
                {
                    if (matrix[l, c] == 1)
                    {
                        blackPosition.Add(new PieceModel() { LinePosition = l, ColumnPosition = c });
                    }
                }
            }
            foreach (var yp in yellowPosition)
            {

                var search = blackPosition.Where(bp => bp.LinePosition < yp.LinePosition).ToList();
                if (search.Count() > 0)
                {
                    return true;
                }
            }
            return false;

        }
        public bool FindGreenPosition(int[,] matrix, int maxLines, int maxColumns)
        {
            var greenPosition = new List<PieceModel>();
            var bluePosition = new List<PieceModel>();
            for (int l = 0; l < maxLines; l++)
            {
                for (int c = 0; c < maxColumns; c++)
                {
                    if (matrix[l, c] == 5)
                    {
                        greenPosition.Add(new PieceModel() { LinePosition = l, ColumnPosition = c });
                    }
                }
            }
            for (int l = 0; l < maxLines; l++)
            {
                for (int c = 0; c < maxColumns; c++)
                {
                    if (matrix[l, c] == 6)
                    {
                        bluePosition.Add(new PieceModel() { LinePosition = l, ColumnPosition = c });
                    }
                }
            }
            foreach (var gp in greenPosition)
            {

                var search = bluePosition.Where(bp => bp.ColumnPosition < gp.LinePosition).ToList();
                if (search.Count() > 0)
                {
                    return true;
                }
            }
            return false;

        }
        public GameModel StandardValues()
        {
            var model = new GameModel()
            {
                Pieces = new List<PieceModel> { }
            };
            model.Pieces.Add(new PieceModel()
            {
                Id = 1,
                Name = "preta(s)",
                LineSize = 2,
                ColumnSize = 6,
                Quantity = 1,
                DialogColor = ConsoleColor.DarkGray
            });

            model.Pieces.Add(new PieceModel()
            {
                Id = 2,
                Name = "vermelha(s)",
                LineSize = 2,
                ColumnSize = 2,
                Quantity = 1,
                DialogColor = ConsoleColor.Red
            });
            model.Pieces.Add(new PieceModel()
            {
                Id = 3,
                Name = "amarela(s)",
                LineSize = 2,
                ColumnSize = 1,
                Quantity = 2,
                DialogColor = ConsoleColor.Yellow
            });
            model.Pieces.Add(new PieceModel()
            {
                Id = 4,
                Name = "roxa(s)",
                LineSize = 1,
                ColumnSize = 8,
                Quantity = 1,
                DialogColor = ConsoleColor.DarkMagenta
            });
            model.Pieces.Add(new PieceModel()
            {
                Id = 5,
                Name = "verde(s)",
                LineSize = 1,
                ColumnSize = 2,
                Quantity = 3,
                DialogColor = ConsoleColor.Green
            });
            model.Pieces.Add(new PieceModel()
            {
                Id = 6,
                Name = "azul(is)",
                LineSize = 1,
                ColumnSize = 1,
                Quantity = 6,
                DialogColor = ConsoleColor.Blue
            });
            model.Lines = 5;
            model.Columns = 8;
            return model;

        }

    }
}
