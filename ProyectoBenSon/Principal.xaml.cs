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
    /// Interaction logic for Principal.xaml
    /// </summary>
    public partial class Principal : Window
    {
        public Principal(string usuarioAtual)
        {
            InitializeComponent();

            BoxUsuarioAtual.Text = usuarioAtual;
        }

        private void BtnCadastroProduto(object sender, RoutedEventArgs e)
        {
            NovoProduto novoProduto = new NovoProduto();

            novoProduto.ShowDialog();
        }

        private void btnEditarProduto(object sender, RoutedEventArgs e)
        {
            Produto produto = (Produto)gridProdutos.SelectedItem;

            if (produto != null)
            {
                NovoProduto novoProduto = new NovoProduto(produto);

                novoProduto.ShowDialog();
            }
            else
            {
                MessageBox.Show("Seleccione un item");
            }
        }

        private async void BtnConsultarProduto(object sender, RoutedEventArgs e)
        {
            ProdutoModel _pModel = new ProdutoModel();
            gridProdutos.ItemsSource = await _pModel.ListarProdutos();
        }

        private void BtnNovaVendaDialog(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseas incluir nombre y dni del cliente?", "Nombre y DNI del Cliente", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
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

        private async void BtnConsultarVenda(object sender, RoutedEventArgs e)
        {
            VendaModel _vModel = new VendaModel();

            gridVendas.ItemsSource = await _vModel.ListarVendas();
        }

        /*
        private void BtnCreatePDF_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {

                string filePath = @"D:\WPF\MyWPFApp\MyWPFApp\PDFResources\sRGB_CS_profile.icm";
                string fontFile = @"D:\WPF\MyWPFApp\MyWPFApp\PDFResources\FreeSans.ttf";
                string fileName = @"D:\WPF\MyWPFApp\MyWPFApp\files\sample" + DateTime.Now.ToString("ddMMMyyyyHHmmss") + ".pdf";

                PdfADocument pdf = new PdfADocument(
                    new PdfWriter(fileName),
                    PdfAConformanceLevel.PDF_A_1B,
                    new PdfOutputIntent("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1",
                    new FileStream(filePath, FileMode.Open, FileAccess.Read)));


                PdfFont font = PdfFontFactory.CreateFont(fontFile, PdfEncodings.WINANSI,
               PdfFontFactory.EmbeddingStrategy.FORCE_EMBEDDED);
                Document document = new Document(pdf);

                document.SetFont(font);


                Paragraph header = new Paragraph("BENSON").SetTextAlignment(TextAlignment.CENTER).SetFontSize(20);
                document.Add(header);

                Paragraph subheader = new Paragraph("INFORME DE VENTA").SetTextAlignment(TextAlignment.CENTER).SetFontSize(10);
                document.Add(subheader);

                LineSeparator ls = new LineSeparator(new SolidLine());
                document.Add(ls);


                Paragraph sellerHeader = new Paragraph("Cliente :").SetBold().SetTextAlignment(TextAlignment.LEFT);
                Paragraph sellerDetail = new Paragraph("").SetTextAlignment(TextAlignment.LEFT);
                Paragraph sellerAddress = new Paragraph("").SetTextAlignment(TextAlignment.LEFT);
                Paragraph sellerContact = new Paragraph("").SetTextAlignment(TextAlignment.LEFT);

                document.Add(sellerHeader);
                document.Add(sellerDetail);
                document.Add(sellerAddress);
                document.Add(sellerContact);


                Paragraph customerHeader = new Paragraph("Vendedor:").SetBold().SetTextAlignment(TextAlignment.RIGHT);
                Paragraph customerDetail = new Paragraph("BenSon Sl").SetTextAlignment(TextAlignment.RIGHT);
                Paragraph customerAddress1 = new Paragraph("Praza da Lexión, s/n, 32002 Ourense").SetTextAlignment(TextAlignment.RIGHT);

                Paragraph customerContact = new Paragraph("988 78 84 70").SetTextAlignment(TextAlignment.RIGHT);

                document.Add(customerHeader);
                document.Add(customerDetail);
                document.Add(customerAddress1);
                document.Add(customerContact);


                Paragraph orderNo = new Paragraph("Venta No:000000").SetBold().SetTextAlignment(TextAlignment.LEFT);
                Paragraph invoiceTimestamp = new Paragraph("Fecha: 16/06/2023 04:25:37 PM").SetTextAlignment(TextAlignment.LEFT);


                document.Add(orderNo);
                document.Add(invoiceTimestamp);

                Table table = new Table(5, true);

                table.SetFontSize(9);
                Cell headerProductId = new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph("Code"));
                Cell headerProduct = new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph("Product"));
                Cell headerProductPrice = new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph("Price"));
                Cell headerProductQty = new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph("Qty"));
                Cell headerTotal = new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph("Total"));

                table.AddCell(headerProductId);
                table.AddCell(headerProduct);
                table.AddCell(headerProductPrice);
                table.AddCell(headerProductQty);
                table.AddCell(headerTotal);

                double grandTotalVal = 0;
                foreach (Orders c in orders)
                {
                    Cell productid = new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph(c.productId.ToString()));
                    Cell product = new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph(c.product));
                    Cell price = new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph(c.price.ToString()));
                    Cell qty = new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph(c.qty.ToString()));
                    var value = c.price * c.qty;
                    Cell total = new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph(value.ToString()));

                    grandTotalVal += value;
                    table.AddCell(productid);
                    table.AddCell(product);
                    table.AddCell(price);
                    table.AddCell(qty);
                    table.AddCell(total);
                }


                Cell grandTotalHeader = new Cell(1, 4).SetTextAlignment(TextAlignment.RIGHT).Add(new Paragraph("Total: "));
                Cell grandTotal = new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph("  " + grandTotalVal.ToString()));

                table.AddCell(grandTotalHeader);
                table.AddCell(grandTotal);


                document.Add(table);
                table.Flush();
                table.Complete();
                document.Close();

                System.Diagnostics.Process.Start(fileName);
            }
            catch (Exception ex)
            {

            }

        }
        */


        private void BtnFecharSistema(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}