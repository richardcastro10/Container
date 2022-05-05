using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Container.Shared.Enums
{
    public enum EnumTipoStatus
    {
        [Description("Vazio")] Vazio = 0,
        [Description("Cheio")] Cheio = 1
    }
}
