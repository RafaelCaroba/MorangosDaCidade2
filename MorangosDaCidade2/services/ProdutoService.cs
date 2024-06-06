using MorangosDaCidade.Entities;
using MorangosDaCidade.Repository;
using MorangosDaCidade2.Entities;
using MorangosDaCidade2.repositories;
using System;
using System.Collections.Generic;


namespace MorangosDaCidade2.services
{
    internal class ProdutoService
    {
        public ProdutoRepository produtoRepository = new ProdutoRepository();

        public bool SalvarProduto(Produto produto)
        {
            if (produtoRepository.CadastrarProduto(produto) > 0) return true;
            else return false;
        }

        public List<Produto> ListarProdutos()
        {
            List<Produto> produtos = produtoRepository.ListarFuncionarios();
            return produtos;
        }

        public List<Produto> ListarProdutosPorNome(string nome)
        {
            List<Produto> produtos = produtoRepository.BuscarProdutoPorNome(nome);
            return produtos;
        }

        public Produto BuscarProdutoPorId(int id)
        {
            Produto produto = produtoRepository.BuscarProdutoPorId(id);
            return produto;
        }

        public bool AtualizarProduto(Produto p)
        {
            if (produtoRepository.AtualizarProduto(p) > 0) return true;
            else return false;
        }

        public bool DeletarProduto(int id)
        {
            if (produtoRepository.DeletarProduto(id) > 0) return true;
            else return false;
        }
    }
}
