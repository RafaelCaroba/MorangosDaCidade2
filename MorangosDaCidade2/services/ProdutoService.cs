using MorangosDaCidade.Entities;
using MorangosDaCidade.Repository;
using MorangosDaCidade2.Entities;
using MorangosDaCidade2.repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MorangosDaCidade2.services
{
    internal class ProdutoService
    {
        public ProdutoRepository produtoRepository = new ProdutoRepository();

        public async Task<bool> SalvarProdutoAsync(Produto produto)
        {
            if (await produtoRepository.CadastrarProdutoAsync(produto) > 0) return true;
            else return false;
        }

        public async Task<List<Produto>> ListarProdutosAsync()
        {
            List<Produto> produtos = await produtoRepository.ListarFuncionariosAsync();
            return produtos;
        }

        public async Task<List<Produto>> ListarProdutosPorNomeAsync(string nome)
        {
            List<Produto> produtos = await produtoRepository.BuscarProdutoPorNomeAsync(nome);
            return produtos;
        }

        public async Task<Produto> BuscarProdutoPorIdAsync(int id)
        {
            Produto produto = await produtoRepository.BuscarProdutoPorIdAsync(id);
            return produto;
        }

        public async Task<bool> AtualizarProdutoAsync(Produto p)
        {
            if (await produtoRepository.AtualizarProdutoAsync(p) > 0) return true;
            else return false;
        }

        public async Task<bool> DeletarProdutoAsync(int id)
        {
            if (await produtoRepository.DeletarProdutoAsync(id) > 0) return true;
            else return false;
        }
    }
}
