using System.Diagnostics.CodeAnalysis;
using LLama;
using LLamaSharp.KernelMemory;
using LLamaSharp.SemanticKernel.ChatCompletion;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.KernelMemory;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using PalmHill.Llama.Models;


namespace PalmHill.Llama
{
    public class LlamaKernel
    {

        public Kernel Kernel { get; }
        public IServiceProvider Services { get; }

        [Experimental("SKEXP0001")]
        public LlamaKernel(IServiceProvider serviceProvider)
        {
            Services = serviceProvider.CreateScope().ServiceProvider;
            var kernelBuilder = Kernel.CreateBuilder();
            
            kernelBuilder.Services.AddSingleton<IAutoFunctionInvocationFilter, AutoInvocationFilter>();
            // var timePlugin = new KernelPlugin();
            kernelBuilder.Plugins.AddFromObject(Services.GetRequiredService<IKernelPlugin>());
            // var timePlugin = new Microsoft.SemanticKernel.Plugins.Core.TimePlugin();
            // kernelBuilder.Plugins.AddFromObject(timePlugin);
            kernelBuilder.Services.AddSingleton<IChatCompletionService>(sp =>
            {
                // var chatExecutor = new StatelessExecutor(injectedModel.Model, injectedModel.ModelParams);
                // var llamaSharpChatCompletion = new LLamaSharpChatCompletion(chatExecutor);
                var modelId = "gpt-4.1-nano";
                var apiKey= "sk-****************";
                OpenAIChatCompletionService chatCompletionService = new (
                        modelId: modelId,
                        apiKey: apiKey
                    );
                return chatCompletionService;
            });

            // kernelBuilder.Services.AddSingleton<Microsoft.KernelMemory.AI.ITextGenerator>(sp =>
            // {
            //     var ctx = injectedModel.Model.CreateContext(injectedModel.ModelParams);
            //     var textGenerator = new LlamaSharpTextGenerator(injectedModel.Model, ctx);
            //     return textGenerator;
            // });

            // kernelBuilder.Services.AddKernelMemory(km =>
            // {
            //     var embedding = new LLamaEmbedder(injectedModel.Model, injectedModel.EmbeddingParameters);
            //     var textEmbeddingGeneration = new LLamaSharpTextEmbeddingGenerator(embedding);
            //     km.WithLLamaSharpTextEmbeddingGeneration(textEmbeddingGeneration);
            //     km.Build<MemoryServerless>();
            // })
            // .AddSingleton<ServerlessLlmMemory>();


            Kernel = kernelBuilder.Build();

        }
    }
}
