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

    public partial class LoginDto
    {
        /// <summary>
        /// Initializes a new instance of the LoginDto class.
        /// </summary>
        public LoginDto() { }

        /// <summary>
        /// Initializes a new instance of the LoginDto class.
        /// </summary>
        public LoginDto(string usuario = default(string), string senha = default(string))
        {
            Usuario = usuario;
            Senha = senha;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "usuario")]
        public string Usuario { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "senha")]
        public string Senha { get; set; }

    }
}
