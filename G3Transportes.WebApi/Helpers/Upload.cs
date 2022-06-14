using System;
namespace G3Transportes.WebApi.Helpers
{
    public class Upload
    {
        public Upload()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string NomeArquivo { get; set; }
        public string Caminho { get; set; }
        public string CaminhoThumbnail { get; set; }
        public string Url { get; set; }
        public string UrlThumbnail { get; set; }
        public long Tamanho { get; set; }
    }
}
