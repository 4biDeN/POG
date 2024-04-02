using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trabalho_T2.Classes;

namespace trabalho_T2.Classes
{
    public enum Header
    {
        Codigo = 0,
        Descricao = 1,
        Categoria = 2,
        Preco = 3,
        Estoque = 4,
        QtdVendida = 5
    }
    public class ProdutoParser
    {
        public static List<Produto> ConverterLista(string arquivo)
        {
            List<Produto> produtos = new List<Produto>();

            var linhas = arquivo.Split('\n').Select(line => line.Trim()).ToList();

            linhas.RemoveAt(0);

            foreach (var linha in linhas)
            {
                var campos = linha.Split(';');

                if (campos.Length >= Enum.GetNames(typeof(Header)).Length)
                {
                    Produto produto = new Produto
                    {
                        Codigo = Convert.ToInt32(campos[(int)Header.Codigo]),
                        Descricao = campos[(int)Header.Descricao],
                        Categoria = campos[(int)Header.Categoria],
                        Preco = Convert.ToDouble(campos[(int)Header.Preco]),
                        Estoque = Convert.ToInt32(campos[(int)Header.Estoque]),
                        QtdVendida = Convert.ToInt32(campos[(int)Header.QtdVendida])
                    };
                    produtos.Add(produto);
                }
                else
                {
                    Console.WriteLine("Erro ao converter linha: " + linha);
                }
            }
            return produtos;
        }
    }
}
