using System;
using System.Collections.Generic;
using System.Linq;
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
    public partial class NovoProduto : Window
    {
        private ProdutoModel _pModel = new ProdutoModel();
        private Produto _produto;

        public NovoProduto()
        {
            InitializeComponent();

            InitializeComboBoxes();
        }

        public NovoProduto(Produto produto) : this()
        {
            _produto = produto;

            if (_produto != null)
            {
                PopulateFieldsFromProduto();
            }
        }

        private void InitializeComboBoxes()
        {
            boxGrupo.ItemsSource = Enum.GetValues(typeof(ProdutoGrupo)).Cast<ProdutoGrupo>();
            boxUnMedida.ItemsSource = Enum.GetValues(typeof(UnidadeMedida)).Cast<UnidadeMedida>();
        }

        private void PopulateFieldsFromProduto()
        {
            boxDescricao.Text = _produto.Descricao;
            boxUnMedida.SelectedItem = _produto.UnidadeDeMedida;
            boxCodBarras.Text = _produto.CodBarras;
            boxPrecoCusto.Text = _produto.PrecoCusto.ToString();
            boxPrecoVenda.Text = _produto.PrecoVenda.ToString();
            boxAtivo.IsEnabled = _produto.Ativo;
            boxGrupo.SelectedItem = _produto.ProdutoGrupo;
        }

        private void btnConfirmarProduto(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_produto == null)
                {
                    AddNewProduto();
                    MessageBox.Show("Producto Agregado!");
                }
                else
                {
                    EditExistingProduto();
                    MessageBox.Show("Producto Actualizado!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void AddNewProduto()
        {
            _pModel.AdicionarProduto(
                boxDescricao.Text,
                Enum.Parse<UnidadeMedida>(boxUnMedida.Text),
                boxCodBarras.Text,
                decimal.Parse(boxPrecoCusto.Text),
                decimal.Parse(boxPrecoVenda.Text),
                boxAtivo.IsEnabled,
                Enum.Parse<ProdutoGrupo>(boxGrupo.Text)
            );
        }

        private void EditExistingProduto()
        {
            _pModel.EditarProduto(
                _produto.Id,
                boxDescricao.Text,
                Enum.Parse<UnidadeMedida>(boxUnMedida.Text),
                boxCodBarras.Text,
                decimal.Parse(boxPrecoCusto.Text),
                decimal.Parse(boxPrecoVenda.Text),
                boxAtivo.IsEnabled,
                Enum.Parse<ProdutoGrupo>(boxGrupo.Text)
            );
        }
    }
}
