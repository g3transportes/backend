using System;
namespace G3Transportes.WebApi.Models
{
    public class Conciliacao
    {
        public Conciliacao()
        {
        }

        public int Id { get; set; }
        public int IdConta { get; set; }
        public DateTime Data { get; set; }
        public double Saldo { get; set; }
        public string Anexo { get; set; }

        public virtual ContaBancaria Conta { get; set; }
    }
}
