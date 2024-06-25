using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Chain
{
    public interface IChain
    {
        public Task ExecuteAsync(RandomRequest request, CancellationToken token);
    }
}
