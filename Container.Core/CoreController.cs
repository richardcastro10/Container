using Container.DataAccess.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Container.Core
{
    public class CoreController : IDisposable
    {
        public CoreController() { }
        public ClienteCore ClienteCore { get { return new ClienteCore(); } }
        public ContainerCore ContainerCore { get { return new ContainerCore(); } }
        public MovimentacaoCore MovimentacaoCore { get { return new MovimentacaoCore(); } }

        public void Dispose()
        {
        }
    }
}
