using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.Threading.Tasks;
using System;
using AvaloniaChessApp;
using AvaloniaChessApp.Pieces;
using AvaloniaChessApp.ViewModels;
using System.Text.Json;


namespace Agent
{
    // Data models for structured responses
    public class ResponseMove
    {
        public Position? OldPosition { get; set; }
        public Position? NewPosition { get; set; }
    }

    public class ChessAgent
    {
        private readonly Kernel _kernel;
        private readonly IChatCompletionService _chatService;
        private readonly ChatHistory _chatHistory;

        private readonly MainWindowViewModel viewModel;

        public ChessAgent(MainWindowViewModel viewModel, string apiKey, string model)
        {
            this.viewModel = viewModel;

            var builder = Kernel.CreateBuilder();
            builder.AddOpenAIChatCompletion(model, apiKey);
            _kernel = builder.Build();

            // Get chat completion service
            _chatService = _kernel.GetRequiredService<IChatCompletionService>();

            // Initialize chat history with system prompt
            _chatHistory = new ChatHistory();
            _chatHistory.AddSystemMessage(@"You are an expert chess player, you will be playing aganist an opponent with your black pieces.
You analyze chess positions represented as an 8x8 grid and make moves, dot indicates an empty space.
Always respond with the following JSON format:
{{
  ""oldPosition"": {
    ""row"": current position from top to bottom (0-7),
    ""column"": current position from left to right (0-7)
  },
  ""newPosition"": {
    ""row"": new position from top to bottom (0-7),
    ""column"": new position from left to right (0-7)
  },
}}

");
        }

        public async Task<ResponseMove?> DoNextMoveAsync()
        {
            string resultJson = await MakeNextMove();
            System.Console.WriteLine("AI Response JSON:");
            System.Console.WriteLine(resultJson);

            try
            {
                var result = JsonSerializer.Deserialize<ResponseMove>(
                    resultJson,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );

                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<string> MakeNextMove()
        {
            try
            {
                // Convert board to readable format
                string boardString = GridToString(CreateGrid());

                var userMessage = $@"Here is the current chess board:
{boardString}

Make your next move.";

                _chatHistory.AddUserMessage(userMessage);

                var settings = new OpenAIPromptExecutionSettings
                {
                    MaxTokens = 2000,
                    Temperature = 0.3,
                    ResponseFormat = "json_object"
                };

                var response = await _chatService.GetChatMessageContentAsync(
                    _chatHistory,
                    settings
                );

                _chatHistory.AddAssistantMessage(response.Content ?? "{}");

                return response.Content ?? "{}";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during MakeNextMove:");
                Console.WriteLine(ex.Message);
                return JsonSerializer.Serialize(new
                {
                    error = ex.Message,
                    evaluation = "AI Prompt failed"
                });
            }
        }

        string[,] CreateGrid()
        {
            var grid = new string[App.BoardSize, App.BoardSize];
            for (int row = 0; row < App.BoardSize; row++)
            {
                for (int col = 0; col < App.BoardSize; col++)
                {
                    Base piece = viewModel.GetPieceAtPosition(row, col);
                    if (piece == null)
                        grid[row, col] = ".";
                    else
                        grid[row, col] = piece.Icon();
                }
            }

            return grid;
        }

        static string GridToString(string[,] grid)
        {
            var rows = grid.GetLength(0);
            var cols = grid.GetLength(1);
            var result = new System.Text.StringBuilder();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result.Append(grid[i, j]);
                    if (j < cols - 1) result.Append(" ");
                }
                result.AppendLine();
            }

            return result.ToString();
        }
    }
}
