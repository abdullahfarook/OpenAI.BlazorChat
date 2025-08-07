using System.ComponentModel;
using Microsoft.SemanticKernel;

namespace PalmHill.Llama;
public interface IKernelPlugin
{
    DateTimeOffset Time();
}
public class KernelPlugin: IKernelPlugin
{
    [KernelFunction, Description("Get the current time")]
    public DateTimeOffset Time() => DateTimeOffset.Now;
}   