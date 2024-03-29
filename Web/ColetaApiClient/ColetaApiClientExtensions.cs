﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace ColetaApi
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Rest;
    using Models;

    /// <summary>
    /// Extension methods for ColetaApiClient.
    /// </summary>
    public static partial class ColetaApiClientExtensions
    {
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='login'>
            /// </param>
            public static TokenDto PostAutenticacao(this IColetaApiClient operations, LoginDto login = default(LoginDto))
            {
                return Task.Factory.StartNew(s => ((IColetaApiClient)s).PostAutenticacaoAsync(login), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='login'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<TokenDto> PostAutenticacaoAsync(this IColetaApiClient operations, LoginDto login = default(LoginDto), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.PostAutenticacaoWithHttpMessagesAsync(login, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            public static IList<ColetaRespostaDto> GetColetasResposta(this IColetaApiClient operations)
            {
                return Task.Factory.StartNew(s => ((IColetaApiClient)s).GetColetasRespostaAsync(), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<IList<ColetaRespostaDto>> GetColetasRespostaAsync(this IColetaApiClient operations, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetColetasRespostaWithHttpMessagesAsync(null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            public static IList<DetalhesRespostaDto> GetColetasRespostaDetalhes(this IColetaApiClient operations, int id)
            {
                return Task.Factory.StartNew(s => ((IColetaApiClient)s).GetColetasRespostaDetalhesAsync(id), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<IList<DetalhesRespostaDto>> GetColetasRespostaDetalhesAsync(this IColetaApiClient operations, int id, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetColetasRespostaDetalhesWithHttpMessagesAsync(id, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='listaSimplificada'>
            /// </param>
            public static IList<PerguntaDto> GetPerguntas(this IColetaApiClient operations, bool? listaSimplificada = false)
            {
                return Task.Factory.StartNew(s => ((IColetaApiClient)s).GetPerguntasAsync(listaSimplificada), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='listaSimplificada'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<IList<PerguntaDto>> GetPerguntasAsync(this IColetaApiClient operations, bool? listaSimplificada = false, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetPerguntasWithHttpMessagesAsync(listaSimplificada, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='dados'>
            /// </param>
            public static void PostPerguntas(this IColetaApiClient operations, PerguntaDto dados = default(PerguntaDto))
            {
                Task.Factory.StartNew(s => ((IColetaApiClient)s).PostPerguntasAsync(dados), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='dados'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task PostPerguntasAsync(this IColetaApiClient operations, PerguntaDto dados = default(PerguntaDto), CancellationToken cancellationToken = default(CancellationToken))
            {
                await operations.PostPerguntasWithHttpMessagesAsync(dados, null, cancellationToken).ConfigureAwait(false);
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            public static PerguntaDto GetPergunta(this IColetaApiClient operations, int id)
            {
                return Task.Factory.StartNew(s => ((IColetaApiClient)s).GetPerguntaAsync(id), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<PerguntaDto> GetPerguntaAsync(this IColetaApiClient operations, int id, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetPerguntaWithHttpMessagesAsync(id, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            public static void DeletePergunta(this IColetaApiClient operations, int id)
            {
                Task.Factory.StartNew(s => ((IColetaApiClient)s).DeletePerguntaAsync(id), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task DeletePerguntaAsync(this IColetaApiClient operations, int id, CancellationToken cancellationToken = default(CancellationToken))
            {
                await operations.DeletePerguntaWithHttpMessagesAsync(id, null, cancellationToken).ConfigureAwait(false);
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            public static IList<QuestionarioDto> GetQuestionarios(this IColetaApiClient operations)
            {
                return Task.Factory.StartNew(s => ((IColetaApiClient)s).GetQuestionariosAsync(), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<IList<QuestionarioDto>> GetQuestionariosAsync(this IColetaApiClient operations, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetQuestionariosWithHttpMessagesAsync(null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='dados'>
            /// </param>
            public static void PostQuestionario(this IColetaApiClient operations, QuestionarioPerguntaDto dados = default(QuestionarioPerguntaDto))
            {
                Task.Factory.StartNew(s => ((IColetaApiClient)s).PostQuestionarioAsync(dados), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='dados'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task PostQuestionarioAsync(this IColetaApiClient operations, QuestionarioPerguntaDto dados = default(QuestionarioPerguntaDto), CancellationToken cancellationToken = default(CancellationToken))
            {
                await operations.PostQuestionarioWithHttpMessagesAsync(dados, null, cancellationToken).ConfigureAwait(false);
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            public static QuestionarioDto GetQuestionario(this IColetaApiClient operations, int id)
            {
                return Task.Factory.StartNew(s => ((IColetaApiClient)s).GetQuestionarioAsync(id), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<QuestionarioDto> GetQuestionarioAsync(this IColetaApiClient operations, int id, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetQuestionarioWithHttpMessagesAsync(id, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            public static void DeleteQuestionario(this IColetaApiClient operations, int id)
            {
                Task.Factory.StartNew(s => ((IColetaApiClient)s).DeleteQuestionarioAsync(id), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task DeleteQuestionarioAsync(this IColetaApiClient operations, int id, CancellationToken cancellationToken = default(CancellationToken))
            {
                await operations.DeleteQuestionarioWithHttpMessagesAsync(id, null, cancellationToken).ConfigureAwait(false);
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            /// <param name='listaSimplificada'>
            /// </param>
            public static IList<PerguntaDto> GetPerguntasQuestionario(this IColetaApiClient operations, int id, bool? listaSimplificada = true)
            {
                return Task.Factory.StartNew(s => ((IColetaApiClient)s).GetPerguntasQuestionarioAsync(id, listaSimplificada), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            /// <param name='listaSimplificada'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<IList<PerguntaDto>> GetPerguntasQuestionarioAsync(this IColetaApiClient operations, int id, bool? listaSimplificada = true, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetPerguntasQuestionarioWithHttpMessagesAsync(id, listaSimplificada, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            /// <param name='dados'>
            /// </param>
            public static void PostRespostas(this IColetaApiClient operations, int id, ColetaDto dados = default(ColetaDto))
            {
                Task.Factory.StartNew(s => ((IColetaApiClient)s).PostRespostasAsync(id, dados), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            /// <param name='dados'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task PostRespostasAsync(this IColetaApiClient operations, int id, ColetaDto dados = default(ColetaDto), CancellationToken cancellationToken = default(CancellationToken))
            {
                await operations.PostRespostasWithHttpMessagesAsync(id, dados, null, cancellationToken).ConfigureAwait(false);
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            public static IList<TipoPerguntaDto> GetTipoPergunta(this IColetaApiClient operations)
            {
                return Task.Factory.StartNew(s => ((IColetaApiClient)s).GetTipoPerguntaAsync(), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<IList<TipoPerguntaDto>> GetTipoPerguntaAsync(this IColetaApiClient operations, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetTipoPerguntaWithHttpMessagesAsync(null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            public static IList<UsuarioDto> GetUsuarios(this IColetaApiClient operations)
            {
                return Task.Factory.StartNew(s => ((IColetaApiClient)s).GetUsuariosAsync(), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<IList<UsuarioDto>> GetUsuariosAsync(this IColetaApiClient operations, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetUsuariosWithHttpMessagesAsync(null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            public static IList<string> GetColetaApi(this IColetaApiClient operations)
            {
                return Task.Factory.StartNew(s => ((IColetaApiClient)s).GetColetaApiAsync(), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<IList<string>> GetColetaApiAsync(this IColetaApiClient operations, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetColetaApiWithHttpMessagesAsync(null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='value'>
            /// </param>
            public static void PostColetaApi(this IColetaApiClient operations, string value = default(string))
            {
                Task.Factory.StartNew(s => ((IColetaApiClient)s).PostColetaApiAsync(value), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='value'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task PostColetaApiAsync(this IColetaApiClient operations, string value = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                await operations.PostColetaApiWithHttpMessagesAsync(value, null, cancellationToken).ConfigureAwait(false);
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            public static string GetByIdColetaApi(this IColetaApiClient operations, int id)
            {
                return Task.Factory.StartNew(s => ((IColetaApiClient)s).GetByIdColetaApiAsync(id), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<string> GetByIdColetaApiAsync(this IColetaApiClient operations, int id, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetByIdColetaApiWithHttpMessagesAsync(id, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            /// <param name='value'>
            /// </param>
            public static void PutColetaApi(this IColetaApiClient operations, int id, string value = default(string))
            {
                Task.Factory.StartNew(s => ((IColetaApiClient)s).PutColetaApiAsync(id, value), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            /// <param name='value'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task PutColetaApiAsync(this IColetaApiClient operations, int id, string value = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                await operations.PutColetaApiWithHttpMessagesAsync(id, value, null, cancellationToken).ConfigureAwait(false);
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            public static void DeleteColetaApi(this IColetaApiClient operations, int id)
            {
                Task.Factory.StartNew(s => ((IColetaApiClient)s).DeleteColetaApiAsync(id), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task DeleteColetaApiAsync(this IColetaApiClient operations, int id, CancellationToken cancellationToken = default(CancellationToken))
            {
                await operations.DeleteColetaApiWithHttpMessagesAsync(id, null, cancellationToken).ConfigureAwait(false);
            }

    }
}
