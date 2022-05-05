using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Container.Shared.Enums
{
    public enum EnumTipoCategoria
    {
        [Description("Importação")] Importacao = 0,
        [Description("Exportação")] Exportacao = 1
    }
}
