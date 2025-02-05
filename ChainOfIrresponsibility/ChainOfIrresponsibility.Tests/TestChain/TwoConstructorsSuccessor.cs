using System;

namespace ChainOfIrresponsibility.Tests.TestChain;

public class TwoConstructorsSuccessor : IChain
{
    public TwoConstructorsSuccessor()
    {
        
    }

    public TwoConstructorsSuccessor(int randomParameter)
    {
        
    }
    public Task HandleAsync(TestRequest request, CancellationToken token)
    {
        Console.WriteLine("Successor with two constructors");
        return Task.CompletedTask;
    }
}
