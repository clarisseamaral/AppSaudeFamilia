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

    public partial class ColetaRespostaDto
    {
        /// <summary>
        /// Initializes a new instance of the ColetaRespostaDto class.
        /// </summary>
        public ColetaRespostaDto() { }

        /// <summary>
        /// Initializes a new instance of the ColetaRespostaDto class.
        /// </summary>
        public ColetaRespostaDto(int? idColeta = default(int?), string nomeUsuario = default(string), int? idUsuario = default(int?), DateTime? data = default(DateTime?))
        {
            IdColeta = idColeta;
            NomeUsuario = nomeUsuario;
            IdUsuario = idUsuario;
            Data = data;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "idColeta")]
        public int? IdColeta { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "nomeUsuario")]
        public string NomeUsuario { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "idUsuario")]
        public int? IdUsuario { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public DateTime? Data { get; set; }

    }
}