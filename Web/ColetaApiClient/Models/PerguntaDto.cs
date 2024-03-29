﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace ColetaApi.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;

    public partial class PerguntaDto
    {
        /// <summary>
        /// Initializes a new instance of the PerguntaDto class.
        /// </summary>
        public PerguntaDto() { }

        /// <summary>
        /// Initializes a new instance of the PerguntaDto class.
        /// </summary>
        public PerguntaDto(int id, string descricao, int idTipoPergunta, string tipoPergunta, IList<AlternativaDto> alternativas = default(IList<AlternativaDto>))
        {
            Id = id;
            Descricao = descricao;
            IdTipoPergunta = idTipoPergunta;
            TipoPergunta = tipoPergunta;
            Alternativas = alternativas;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "descricao")]
        public string Descricao { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "idTipoPergunta")]
        public int IdTipoPergunta { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "tipoPergunta")]
        public string TipoPergunta { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "alternativas")]
        public IList<AlternativaDto> Alternativas { get; set; }

        /// <summary>
        /// Validate the object. Throws ValidationException if validation fails.
        /// </summary>
        public virtual void Validate()
        {
            if (Descricao == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Descricao");
            }
            if (TipoPergunta == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "TipoPergunta");
            }
            if (this.Descricao != null)
            {
                if (this.Descricao.Length > 500)
                {
                    throw new ValidationException(ValidationRules.MaxLength, "Descricao", 500);
                }
                if (this.Descricao.Length < 0)
                {
                    throw new ValidationException(ValidationRules.MinLength, "Descricao", 0);
                }
            }
            if (this.Alternativas != null)
            {
                foreach (var element in this.Alternativas)
                {
                    if (element != null)
                    {
                        element.Validate();
                    }
                }
            }
        }
    }
}
