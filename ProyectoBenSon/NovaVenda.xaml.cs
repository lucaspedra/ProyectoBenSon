using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ProyectoBenSon.DAL;
using ProyectoBenSon.Model;

namespace ProyectoBenSon
{
    /// <summary>
    /// Interaction logic for NovaVenda.xaml
    /// </summary>
    public partial class NovaVenda : Window
    {
        VendaModel vModel = new VendaModel();
        List<Produto> produtos = new List<Produto>();

        public NovaVenda(string nomeCliente, string cpfCliente)
        {
            InitializeComponent();
            blockNomeCliente.Text = nomeCliente;
            blockCpfCliente.Text = cpfCliente;

        }

        private async void boxCodProduto_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var produto = await vModel.ProcurarProduto(int.Parse(boxCodProduto.Text));
                if (produto != null)
                {
                    blockNomeProduto.Text = produto.Descricao;
                }
                else
                {
                    MessageBox.Show("Producto no encontrado!");
                }
            }
        }

        private async void boxQuantidade_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                int codigoProduto = int.Parse(boxCodProduto.Text);
                Produto produto = await vModel.ProcurarProduto(codigoProduto);
                produtos.Add(produto);

                NovaVendaCollection vendas = new NovaVendaCollection(produto.Id, produto.Descricao, produto.UnidadeDeMedida, produto.PrecoVenda, int.Parse(boxQuantidade.Text));

                gridVendaProduto.Items.Add(vendas);

                blockTotal.Text = (decimal.Parse(blockTotal.Text) + vendas.Total).ToString();
            }
        }

        private async void btnConfirmarVenda(object sender, RoutedEventArgs e)
        {
            bool status = await vModel.NovaVenda(blockCpfCliente.Text, blockNomeCliente.Text, decimal.Parse(blockTotal.Text), boxObs.Text, produtos);

            if (status == true)
            {
                MessageBox.Show("Venta registrada!");
                Close();

                MessageBoxResult result = MessageBox.Show("Desea incluir nombre y dni del cliente?", "Nombre y DNI del Cliente", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        NomeCpf nomeCpf = new NomeCpf();
                        nomeCpf.ShowDialog();
                        break;

                    case MessageBoxResult.No:
                        NovaVenda novaVenda = new NovaVenda("No informado", "No informado");
                        novaVenda.ShowDialog();
                        break;
                }
            }
            else
            {
                MessageBox.Show("Error al registrar venta!", "ERRO", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    class NovaVendaCollection
    {
        public int ProdutoId { get; set; }
        public string ProdutoNome { get; set; }
        public UnidadeMedida UnidadeDeMedida { get; set; }
        public decimal PrecoVenda { get; set; }
        public int QuantidadeProduto { get; set; }
        public decimal Total { get; set; }

        public NovaVendaCollection(int produtoId, string produtoNome, UnidadeMedida unidadeDeMedida, decimal precoVenda, int quantidadeProduto)
        {
            ProdutoId = produtoId;
            ProdutoNome = produtoNome;
            UnidadeDeMedida = unidadeDeMedida;
            PrecoVenda = precoVenda;
            QuantidadeProduto = quantidadeProduto;
            Total = quantidadeProduto * precoVenda;
        }

    }
}