using LLama.Common;
using LLama;
using Microsoft.AspNetCore.SignalR;

namespace F2F.API.SignalRWebpack.Hubs
{
    public class GenerativeAIHub : Hub, IDisposable
    {
        private LLamaContext _context;

        public GenerativeAIHub()
        {
            string lamaPath = @"D:\Projects\Diploma\F2F\llama-2-13b\llama-2-7b.Q4_K_M.gguf";

            // Load a model
            var parameters = new ModelParams(lamaPath)
            {
                ContextSize = 1024,
                Seed = 1337,
                GpuLayerCount = 5
            };

            using var model = LLamaWeights.LoadFromFile(parameters);
            _context = model.CreateContext(parameters);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task GenerateResponse(string connectionId, string request)
        {
            InstructExecutor ex = new(_context);
            var formattedRequest =
                $"Below is an instruction that describes a task. Write a response that appropriately completes the request.\r\n\r\n### Instruction:\r\n{request}\r\n\r\n### Response:";
            await foreach (
                var result in ex.InferAsync(
                    formattedRequest,
                    new InferenceParams() { Temperature = 0.6f }
                )
            )
            {
                await Clients
                    .Clients(connectionId)
                    .SendAsync("onGenerateResponse", result.ToString());
            }
        }
    }
}
