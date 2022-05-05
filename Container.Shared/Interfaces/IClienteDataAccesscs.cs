using Container.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container.Shared
{
    public interface IClienteDataAccesscs
    {
        List<Cliente> List(Int32 page, bool sortAsc, string search);
    }
}
