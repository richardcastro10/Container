using Container.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Container.Presentation.Models
{
    public class ClienteModel
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string telephone { get; set; }
        public string document { get; set; }
        public string description { get; set; }

        public static ClienteModel Create(Cliente dto)
        {
            return dto == null ? null : new ClienteModel()
            {
                id = dto.Id,
                description = dto.Descricao,
                document = dto.Documento,
                name = dto.Nome,
                telephone = dto.Telefone
            };
        }
    }

    public static class ClienteModelExt
    {
        public static Cliente Convert(this ClienteModel model)
        {
            return model == null ? null : new Cliente()
            {
                Id = model.id != Guid.Empty ? model.id : Guid.NewGuid(),
                Descricao = model.description,
                Documento = model.document,
                Nome = model.name,
                Telefone = model.telephone
            };
        }
    }
}