using PushStart_Console.Models;
using System;

namespace PushStart_Console
{
    public class DialogRepository
    {
        public void WelcomeDialog()
        {
            Console.Write("Olá! Bem vindo ao teste! \nPressione qualquer tecla para iniciar. ", Console.ForegroundColor = ConsoleColor.White);
            Console.WriteLine();
        }
        public void SucessMessage()
        {
            Console.WriteLine("Consegui!");
            Console.WriteLine("Digite qualquer coisa para reiniciar o teste.");
        }
        public void SizeErrorMessage()
        {
            Console.WriteLine("A quantidade de espaço que as peças ocupam é diferente da quantidade de espaço que matriz possui.\n" +
                              "Vai sobrar ou faltar espaço. Verifique!", Console.ForegroundColor = ConsoleColor.Red);
        }
        public void TooManyTriesMessage()
        {
            Console.WriteLine("Já tentei 1.000 vezes... Tem certeza que é possível encaixar todas?");
            Console.WriteLine("- Digite 'reiniciar' para tentarmos outro valor, ou qualquer outra coisa para continuar.");
        }

        public void PiecesInformation(GameModel model)
        {
            Console.WriteLine(string.Concat("Ok! A matriz tem: ", model.Lines, " linhas e ", model.Columns, " colunas.\n"));
            Console.WriteLine("Tentando encaixe com as seguintes peças:\n");
            foreach (var pieces in model.Pieces)
            {
                Console.Write(string.Concat(" -", pieces.Quantity, " peça(s) "), Console.ForegroundColor = ConsoleColor.White);
                Console.Write(string.Concat(pieces.Name, "(ID ", pieces.Id, ")\n"), Console.ForegroundColor = pieces.DialogColor);
            }
        }
        public void Result(int[,] matrix, int lines, int columns)
        {
            for (int x = 0; x < lines; x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    Console.Write(matrix[x, y] + "\t", Console.ForegroundColor = ConsoleColor.White);
                }
                Console.WriteLine();
            }
        }
        
    }
}
