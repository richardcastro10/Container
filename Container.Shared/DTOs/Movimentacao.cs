//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Container.Shared.DTOs
{
    using System;
    using System.Collections.Generic;
    
    public partial class Movimentacao
    {
        public System.Guid Id { get; set; }
        public int Tipo { get; set; }
        public System.DateTime Inicio { get; set; }
        public Nullable<System.DateTime> Fim { get; set; }
        public System.Guid ContainerId { get; set; }
    
        public virtual Container Container { get; set; }
    }
}
