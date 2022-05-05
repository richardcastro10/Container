using Container.Shared.DTOs;
using Container.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Container.Presentation.Models
{
    public class MovimentacaoModel
    {
        public Guid id { get; set; }
        public TipoModel type { get; set; }
        public DateTime start { get; set; }
        public DateTime? end { get; set; }
        public ContainerModel container { get; set; }

        public static MovimentacaoModel Create(Movimentacao dto)
        {
            return dto == null ? null : new MovimentacaoModel()
            {
                id = dto.Id,
                start = DateTime.SpecifyKind(dto.Inicio, DateTimeKind.Utc),
                end = dto.Fim.HasValue ? DateTime.SpecifyKind(dto.Fim.Value, DateTimeKind.Utc) : (DateTime?)null,
                type = new TipoModel() { description = ((EnumTipoMovimentacao)dto.Tipo).GetStringValue(), id = dto.Tipo },
                container = ContainerModel.Create(dto.Container)
            };
        }

        public static string GetPropertyName(string property)
        {
            switch (property)
            {
                case nameof(MovimentacaoModel.type):
                    return nameof(Movimentacao.Tipo);          
                case nameof(MovimentacaoModel.end):
                    return nameof(Movimentacao.Fim);
                case nameof(MovimentacaoModel.start):
                default:
                    return nameof(Movimentacao.Inicio);
            }
        }
    }

    public class TipoModel
    {
        public Int32 id { get; set; }
        public string description { get; set; }
    }


    public static class MovimentacaoModelExt
    {
        public static Movimentacao Convert(this MovimentacaoModel model)
        {
            return model == null ? null : new Movimentacao()
            {
               Id= model.id != Guid.Empty ? model.id : Guid.NewGuid(),
               Inicio= model.start,
               Fim= model.end,
               Tipo= model.type.id,
               ContainerId = model.container.id
            };
        }
    }
}