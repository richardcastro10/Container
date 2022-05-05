using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Container.Shared.Enums
{
    public enum EnumTipoMovimentacao
    {
        [Description("Embarque")] Embarque = 0,
        [Description("Descarga")] Descarga = 1,
        [Description("Gate In")] GateIn = 2,
        [Description("Gate Out")] GateOut = 3,
        [Description("Reposicionamento")] Reposicionamento = 4,
        [Description("Pesagem")] Pesagem = 5,
        [Description("Scanner")] Scanner = 6,
    }
}
