using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using G3Transportes.WebApi.Helpers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace G3Transportes.WebApi.Controllers
{
    [ApiController]
    [Route("usuario")]
    public class Usuario : Controller
    {
        private IQueryable<Models.Usuario> Filtra(IQueryable<Models.Usuario> query, Filters.Usuario filtro)
        {
            //efetua verificacoes
            if (filtro.CodigoFilter)
                query = query.Where(a => filtro.CodigoValue.Contains(a.Id));

            if (filtro.NomeFilter)
                query = query.Where(a => a.Nome.Contains(filtro.NomeValue));

            if (filtro.EmailFilter)
                query = query.Where(a => a.Email.Contains(filtro.EmailValue));

            if (filtro.SenhaFilter)
                query = query.Where(a => a.Senha.Contains(filtro.SenhaValue));

            if (filtro.AtivoFilter)
                query = query.Where(a => a.Ativo == filtro.AtivoValue);

            //retorna a query
            return query;
        }

        [HttpPost("lista/{pagina}/{tamanho}")]
        public ListResult<Models.Usuario> Pega([FromBody] Filters.Usuario filtro, int pagina, int tamanho)
        {
            var result = new ListResult<Models.Usuario>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.Usuario
                                .OrderBy(a => a.Nome)
                                .AsQueryable();

                //efetua o filtro
                query = Filtra(query, filtro);

                //configura o resultado
                result.IsValid = true;
                result.CurrentPage = pagina;
                result.PageSize = tamanho;
                result.TotalItems = query.Count();
                result.TotalPages = Comum.CalculaTotalPages(result.TotalItems, result.PageSize);
                result.Items = query.Skip(result.PageSize * (result.CurrentPage - 1))
                                    .Take(result.PageSize)
                                    .ToList();
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.Errors.Add(ex.Message);
            }

            return result;
        }

        [HttpGet("item/{id}")]
        public ItemResult<Models.Usuario> Pega(int id)
        {
            var result = new ItemResult<Models.Usuario>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.Usuario.FirstOrDefault(a => a.Id == id);

                if (query != null)
                {
                    result.Item = query;
                }
                else
                {
                    result.IsValid = false;
                    result.Errors.Add("Nenhum registro encontrado");
                }
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.Errors.Add(ex.Message);
            }

            return result;
        }

        [HttpPost("login")]
        public ItemResult<Models.Usuario> Login([FromBody]Helpers.Login login)
        {
            var result = new ItemResult<Models.Usuario>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.Usuario.FirstOrDefault(a => a.Email == login.Email && a.Senha == login.Password);

                if (query != null)
                {
                    result.Item = query;
                }
                else
                {
                    result.IsValid = false;
                    result.Errors.Add("Nenhum registro encontrado");
                }
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.Errors.Add(ex.Message);
            }

            return result;
        }

        [HttpPost("recupera")]
        public ItemResult<Helpers.RecoverPassword> Recupera([FromBody]Helpers.RecoverPassword recover)
        {
            var result = new ItemResult<Helpers.RecoverPassword>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.Usuario.FirstOrDefault(a => a.Email == recover.Email);

                if (query != null)
                {
                    result.Item = recover;
                }
                else
                {
                    result.IsValid = false;
                    result.Errors.Add("Nenhum registro encontrado");
                }
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.Errors.Add(ex.Message);
            }

            return result;
        }

        [HttpPost("insere")]
        public ItemResult<Models.Usuario> Insere([FromBody]Models.Usuario item)
        {
            var result = new ItemResult<Models.Usuario>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                conn.Usuario.Add(item);
                conn.SaveChanges();

                //pega item incluido
                result.Item = item;
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.Errors.Add(ex.Message);
            }

            return result;
        }

        [HttpPut("atualiza")]
        public ItemResult<Models.Usuario> Atualiza([FromBody]Models.Usuario item)
        {
            var result = new ItemResult<Models.Usuario>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                conn.Usuario.Update(item);
                conn.SaveChanges();

                //pega item incluido
                result.Item = item;
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.Errors.Add(ex.Message);
            }

            return result;
        }

        [HttpPut("atualiza/senha")]
        public ItemResult<Models.Usuario> AtualizaSenha([FromBody]Helpers.ChangePassword item)
        {
            var result = new ItemResult<Models.Usuario>();

            try
            {
                using var conn = new Contexts.EFContext();

                var query = conn.Usuario.FirstOrDefault(a => a.Id == item.Id && a.Senha == item.CurrentPassword);

                if (query != null)
                {
                    //inicializa a query
                    conn.Usuario.Update(query);
                    conn.SaveChanges();

                    //pega item incluido
                    result.Item = query;
                }
                else
                {
                    result.IsValid = false;
                    result.Errors.Add("Nenhum registro encontrado");
                }
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.Errors.Add(ex.Message);
            }

            return result;
        }

        [HttpDelete("deleta/{id}")]
        public ItemResult<Models.Usuario> Deleta(int id)
        {
            var result = new ItemResult<Models.Usuario>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.Usuario.FirstOrDefault(a => a.Id == id);

                if (query != null)
                {
                    conn.Usuario.Remove(query);
                    conn.SaveChanges();

                    result.Item = query;
                }
                else
                {
                    result.IsValid = false;
                    result.Errors.Add("Nenhum registro encontrado");
                }
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.Errors.Add(ex.Message);
            }

            return result;
        }
    }
}
