using Container.Shared.DTOs;
using Container.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Container.Presentation.Models
{
    public class ContainerModel
    {
        public Guid id { get; set; }
        public string type { get; set; }
        public string number { get; set; }
        public StatusModel status { get; set; }
        public CategoriaModel category { get; set; }
        public ClienteModel client { get; set; }

        public static ContainerModel Create(Shared.DTOs.Container dto)
        {
            return dto == null ? null : new ContainerModel()
            {
                id = dto.Id,
                type = dto.Tipo.ToString(),
                number = dto.Numero,
                category = new CategoriaModel() { description = ((EnumTipoCategoria)dto.Categoria).GetStringValue(), id = dto.Categoria },
                status = new StatusModel() { description = ((EnumTipoStatus)dto.Status).GetStringValue(), id = dto.Status },
                client = ClienteModel.Create(dto.Cliente)
            };
        }

        public static string GetPropertyName(string property)
        {
            switch (property)
            {
                case nameof(ContainerModel.status):
                    return nameof(Shared.DTOs.Container.Status);              
                case nameof(ContainerModel.type):
                    return nameof(Shared.DTOs.Container.Tipo);                
                case nameof(ContainerModel.category):
                    return nameof(Shared.DTOs.Container.Categoria);
                case nameof(ContainerModel.number):
                default:
                    return nameof(Shared.DTOs.Container.Numero);
            }
        }
    }

    public class StatusModel
    {
        public Int32 id { get; set; }
        public string description { get; set; }
    }
   public class CategoriaModel
    {
        public Int32 id { get; set; }
        public string description { get; set; }
    }

    public static class ContainerModelExt
    {
        public static Shared.DTOs.Container Convert(this ContainerModel model)
        {
            return model == null ? null : new Shared.DTOs.Container()
            {
                Id = model.id != Guid.Empty ? model.id : Guid.NewGuid(),
                Categoria = model.category.id,
                UsuarioId = model.client.id,
                Numero= model.number,
                Status= model.status.id,
                Tipo= Int32.Parse(model.type)
            };
        }
    }
}