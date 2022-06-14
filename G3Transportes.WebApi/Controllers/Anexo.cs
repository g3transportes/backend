using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using G3Transportes.WebApi.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace G3Transportes.WebApi.Controllers
{
    [ApiController]
    [Route("anexo")]
    public class Anexo : Controller
    {
        private IWebHostEnvironment _enviroment;

        public Anexo(IWebHostEnvironment environment)
        {
            _enviroment = environment;
        }



        private IQueryable<Models.Anexo> Filtra(IQueryable<Models.Anexo> query, Filters.Anexo filtro)
        {
            //efetua verificacoes
            if (filtro.CodigoFilter)
                query = query.Where(a => filtro.CodigoValue.Contains(a.Id));

            if (filtro.DescricaoFilter)
                query = query.Where(a => a.Nome.Contains(filtro.DescricaoValue));

            //retorna a query
            return query;
        }

        [HttpPost("lista/{pagina}/{tamanho}")]
        public ListResult<Models.Anexo> Pega([FromBody] Filters.Anexo filtro, int pagina, int tamanho)
        {
            var result = new ListResult<Models.Anexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.Anexo
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
        public ItemResult<Models.Anexo> Pega(int id)
        {
            var result = new ItemResult<Models.Anexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.Anexo.FirstOrDefault(a => a.Id == id);

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

        [HttpPost("insere")]
        public ItemResult<Models.Anexo> Insere([FromBody]Models.Anexo item)
        {
            var result = new ItemResult<Models.Anexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                conn.Anexo.Add(item);
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
        public ItemResult<Models.Anexo> Atualiza([FromBody]Models.Anexo item)
        {
            var result = new ItemResult<Models.Anexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                conn.Anexo.Update(item);
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

        [HttpDelete("deleta/{id}")]
        public ItemResult<Models.Anexo> Deleta(int id)
        {
            var result = new ItemResult<Models.Anexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.Anexo.FirstOrDefault(a => a.Id == id);

                if (query != null)
                {
                    conn.Anexo.Remove(query);
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

        [HttpPost("upload")]
        public ListResult<Models.Anexo> Upload()
        {
            var result = new ListResult<Models.Anexo>();
            var arquivos = Request.Form.Files;
            var prefix = "g3transportes";
            var url = "https://api-g3transportes.ecolinx.com.br";

            try
            {
                foreach (var arquivo in arquivos)
                {
                    //configura
                    bool isLinux = _enviroment.WebRootPath.IndexOf(@"\") == -1 ? true : false;
                    string filename = ContentDispositionHeaderValue.Parse(arquivo.ContentDisposition).FileName.Trim().ToString().Replace("\"", "");
                    string nome_arquivo = string.Format("{0}_{1}.{2}", prefix, Guid.NewGuid().ToString(), filename.Substring(filename.LastIndexOf('.') + 1));
                    string caminho = string.Format(@"{0}\anexos\{1}", _enviroment.WebRootPath, nome_arquivo);

                    if (isLinux)
                    {
                        caminho = caminho.Replace(@"\", "/");
                    }

                    //salva no disco
                    using (FileStream fs = System.IO.File.Create(caminho))
                    {
                        arquivo.CopyTo(fs);
                        fs.Flush();
                    }

                    //cria objeto
                    var anexo = new Models.Anexo();
                    anexo.Id = 0;
                    anexo.Nome = filename;
                    anexo.Arquivo = string.Format(@"{0}/anexos/{1}", url, nome_arquivo);

                    //adiciona no bd
                    using var conn = new Contexts.EFContext();
                    conn.Anexo.Add(anexo);
                    conn.SaveChanges();

                    //adiciona na lista
                    result.Items.Add(anexo);
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
