using PushStart_Console.Models;
using PushStart_Console.Repository;
using System;
using System.Collections.Generic;

namespace PushStart_Console.Application
{
    public class GameApplication
    {
        public static List<int> impossibleList;
        public List<PieceModel> addedPieces;

        public static bool sucess = false;
        public void Start()
        {
            var dialogRepository = new DialogRepository();
            var pieceRepository = new PieceRepository();
            dialogRepository.WelcomeDialog();
            Console.ReadLine();
            Process(pieceRepository.StandardValues());
        }
        public void Process(GameModel model)
        {
            bool hasImpossibilities = true;
            var dialogRepository = new DialogRepository();
            var pieceRepository = new PieceRepository();
            int[,] matrix = new int[model.Lines, model.Columns];
            var loops = 0;
            impossibleList = new List<int>();
            addedPieces = new List<PieceModel>();

            if (pieceRepository.BasicValidator(model))
            {
                dialogRepository.SizeErrorMessage();
                Console.ReadLine();
                Start();
            }
            dialogRepository.PiecesInformation(model);

            foreach (var piece in model.Pieces)
            {
                if (piece.Quantity > 0)
                {
                    for (int x = 0; x < piece.Quantity; x++)
                    {
                        addedPieces.Add(piece);
                    }
                }
            }
            while (hasImpossibilities)
            {
                foreach (var piece in addedPieces)
                {
                    pieceRepository.AddPiece(matrix, model.Lines, model.Columns, piece);
                }
                if (impossibleList.Count <= 0)
                {
                    hasImpossibilities = false;
                }
                if (hasImpossibilities)
                {
                    matrix = new int[model.Lines, model.Columns];
                    impossibleList.Clear();
                }
                if (loops >= 1000)
                {
                    dialogRepository.TooManyTriesMessage();
                    var answer = Console.ReadLine();
                    if (answer.ToLower() == "reiniciar")
                    {
                        loops = 0;
                        Console.Clear();
                        Start();
                    }
                }
                loops++;
            }

            dialogRepository.Result(matrix, model.Lines, model.Columns);
            var afirmations = pieceRepository.Challange(matrix, model.Lines, model.Columns);
            foreach (var a in afirmations)
            {
                Console.WriteLine(a);
            }
            dialogRepository.SucessMessage();
            Console.ReadLine();
            Console.Clear();
            Start();

        }

    }
}
