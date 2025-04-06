using System.Globalization;
using SQLite;


namespace MinhasCompras.Models
{
    public class Produto
    {
        string _descricao;
       
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Descricao {
            get => _descricao;
            set
            {
                if (value == null)
                {
                    throw new Exception("Por favor, preencha a descrição");
                }

                _descricao = value;

            }
        
        }
        double _quantidade;
        public double Quantidade 
        {
            get => _quantidade;
            set
            {
                if (value <= 0)
                    throw new Exception("Por favor, preencha quantidade com um número maior que zero");

                _quantidade = value;
            }
        }
        double _preco;
        public double Preco {
            get => _preco;
            set
            {
                if (value == 0)
                    throw new Exception("Por favor, preencha o preço com um valor maior que zero");

                _preco = value;
            }
        }

        public double Total { get => Quantidade * Preco; }

        public string Categoria { get; set; }   
    }
}