using SQLite;

namespace MinhasCompras.Models
{
    public class Produto
    {
        [PrimaryKey, AutoIncrement]
        public string Id { get; set; }
        public string Descricao { get; set; }
        public double Quantidade { get; set; }
        public double Preco {  get; set; }
    }
}
