using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using trabalho_T2.Classes;

class Program
{
    static void Main(string[] args)
    {
        string arquivo = File.ReadAllText("..\\..\\..\\Dataset.csv");
        List<Produto> produtos = ProdutoParser.ConverterLista(arquivo);

        string menu =
                "1 - Produtos mais vendidos\n" +
                "2 - Produtos com mais estoque\n" +
                "3 - Categoria mais vendida\n" +
                "4 - Produtos menos vendidos\n" +
                "5 - Estoque de segurança\n" +
                "6 - Excesso de estoque\n" +
                "7 - Média de preço por categoria\n" +
                "0 - SAIR\n";

            int option = -1;
            
            while (option != 0)
            {
                Console.WriteLine(menu);
                Console.Write("Escolha uma opção: ");
                try
                {
                    option = int.Parse(Console.ReadLine());

                    switch (option)
                    {
                        case 1:
                            ProdutosMaisVendidos(produtos);
                            Console.WriteLine("\nPressione Enter para continuar...");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        case 2:
                            ProdutosMaisEstoque(produtos);
                            Console.WriteLine("\nPressione Enter para continuar...");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        case 3:
                            CategoriaVendida(produtos);
                            Console.WriteLine("\nPressione Enter para continuar...");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        case 4:
                            ProdutosMenosVendidos(produtos);
                            Console.WriteLine("\nPressione Enter para continuar...");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        case 5:
                            EstoqueSeguranca(produtos);
                            Console.WriteLine("\nPressione Enter para continuar...");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        case 6:
                            EstoqueExcesso(produtos);
                            Console.WriteLine("\nPressione Enter para continuar...");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        case 7:
                            MediaCategoria(produtos);
                            Console.WriteLine("\nPressione Enter para continuar...");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        case 0:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Opção inválida. Por favor, escolha uma opção válida.");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Opção inválida. Por favor, escolha uma opção válida.");
                }
            }
        }
        static void ProdutosMaisVendidos(List<Produto> produtos)
        {
            Console.Clear();
            Console.WriteLine("Top 5 produtos mais vendidos:\n");

            var pMaisVendidos = produtos.OrderByDescending(p => p.QtdVendida).Take(5);

            foreach (var produto in pMaisVendidos)
            {
                Console.WriteLine($"Código: {produto.Codigo}, Descrição: {produto.Descricao}");
            }
        }
        static void ProdutosMaisEstoque(List<Produto> produtos)
        {
            Console.Clear();
            Console.WriteLine("Top 3 produtos com mais Estoque\n");

            var pMaisEstoque = produtos.OrderByDescending(p => p.Estoque).Take(3);

            foreach (var produto in pMaisEstoque)
            {
                Console.WriteLine($"Código: {produto.Codigo}, Descrição: {produto.Descricao}, Estoque: {produto.Estoque}  ");
            }
        }
        static void CategoriaVendida(List<Produto> produtos)
        {
            Dictionary<string, int> qtdeCategoria = new Dictionary<string, int>();

            foreach (var produto in produtos)
            {
                if (qtdeCategoria.ContainsKey(produto.Categoria))
                {
                    qtdeCategoria[produto.Categoria] += produto.QtdVendida;
                }
                else
                {
                    qtdeCategoria[produto.Categoria] = produto.QtdVendida;
                }
            }

            string cVendida = qtdeCategoria.OrderByDescending(p => p.Value).FirstOrDefault().Key;

            Console.Clear();
            Console.WriteLine($"Categoria mais vendida é: {cVendida}");

        }
        static void ProdutosMenosVendidos(List<Produto> produtos)
        {
            Console.Clear();
            Console.WriteLine("Top 5 produtos menos vendidos:\n");

            var pMenosVendidos = produtos.OrderBy(p => p.QtdVendida).Take(5);

            foreach (var produto in pMenosVendidos)
            {
                Console.WriteLine($"Código: {produto.Codigo}, Descrição: {produto.Descricao}, Quantidade Vendida: {produto.QtdVendida}");
            }
        }
        static void EstoqueSeguranca(List<Produto> produtos)
        {
            Console.Clear();
            Console.WriteLine("Produtos com estoque baixo\n");

            foreach (var produto in produtos)
            {
                if (produto.Estoque < (produto.QtdVendida * 0.33))
                {
                    Console.WriteLine($"Código: {produto.Codigo}, Descrição: {produto.Descricao}, Estoque: {produto.Estoque}  ");
                }
            }
        }
        static void EstoqueExcesso(List<Produto> produtos)
        {
            Console.Clear();
            Console.WriteLine("Produtos com estoque em Excesso\n");

            foreach (var produto in produtos)
            {
                if ((produto.QtdVendida * 3) <= produto.Estoque)
                {
                    Console.WriteLine($"Código: {produto.Codigo}, Descrição: {produto.Descricao}, Estoque: {produto.Estoque}  ");
                }
            }
        }
        static void MediaCategoria(List<Produto> produtos)
        {
            Dictionary<string, double> qtdeCategoria = new Dictionary<string, double>();
            Dictionary<string, double> qtdeProduto = new Dictionary<string, double>();

            foreach (var produto in produtos)
            {
                if (qtdeCategoria.ContainsKey(produto.Categoria))
                {
                    qtdeCategoria[produto.Categoria] += produto.Preco;
                    qtdeProduto[produto.Categoria]++;
                }
                else
                {
                    qtdeCategoria[produto.Categoria] = produto.Preco;
                    qtdeProduto[produto.Categoria] = 1;
                }
            }
            
            Console.Clear();
            Console.WriteLine("A Média de Preço por categoria é:\n");
            
            foreach (var kvp in qtdeCategoria)
            {
                string categoria = kvp.Key;
                double qtde = kvp.Value;
                
                if(qtdeProduto.ContainsKey(categoria) && qtdeProduto[categoria] > 0)
                {
                    double mCategoria = qtde / qtdeProduto[categoria];
                    Console.WriteLine($"{categoria}: R$ {Math.Round(mCategoria, 2)}");
                }
            }
        }
  }
