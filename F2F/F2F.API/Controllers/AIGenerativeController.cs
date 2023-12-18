using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LLama.Common;
using LLama;
using System.Text;

namespace F2F.API.Controllers
{
    public class AIGenerativeController : ApiController
    {
        [HttpPost("generate")]
        [AllowAnonymous]
        public async Task<IActionResult> GenerateResponse([FromBody] string request)
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
            using var context = model.CreateContext(parameters);

            InstructExecutor ex = new(context);
            // Initialize a chat session
            StringBuilder resultString = new();
            await foreach (
                var result in ex.InferAsync(
                    request,
                    new InferenceParams()
                    {
                        Temperature = 0.6f,
                        AntiPrompts = new List<string> { "User:" }
                    }
                )
            )
            {
                resultString.Append(result.ToString());
            }
            return Ok(resultString.ToString());
        }
    }
}
