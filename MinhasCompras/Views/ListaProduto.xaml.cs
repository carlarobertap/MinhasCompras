using System.Collections.ObjectModel;
using MinhasCompras.Models;

namespace MinhasCompras.Views
{


    public partial class ListaProduto : ContentPage
    {
        ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

        public string SelectedIndexChanged { get; private set; }

        public ListaProduto()
        {
            InitializeComponent();

            lst_produtos.ItemsSource = lista;
        }

        protected async override void OnAppearing()

        {
            try
            {
                lista.Clear();

                List<Produto> tmp = await App.Db.GetAll();

                tmp.ForEach(i => lista.Add(i));
            }

            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "Ok");
            }
        }
        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new Views.NovoProduto());

            }
            catch (Exception ex)
            {
                DisplayAlert("Ops", ex.Message, "Ok");
            }

        }

        private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {

                string q = e.NewTextValue;

                lst_produtos.IsRefreshing = true;

                lista.Clear();


                List<Produto> tmp = await App.Db.Search(q);

                tmp.ForEach(i => lista.Add(i));

            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "Ok");
            }
            finally
            {
                lst_produtos.IsRefreshing = false;
            }

        }

        private void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            double soma = lista.Sum(i => i.Total);

            string msg = $"O Total � {soma:C}";

            DisplayAlert("Total dos Produtos", msg, "OK");
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            try
            {

                MenuItem selecionado = sender as MenuItem;

                Produto p = selecionado.BindingContext as Produto;

                bool confirm = await DisplayAlert(
                    "Tem Certeza?", $"Remover {p.Descricao}?", "SIM", "N�O");

                if (confirm)
                {
                    await App.Db.Delete(p.Id);
                    lista.Remove(p);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "Ok");
            }
        }

        private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                Produto? p = e.SelectedItem as Produto;

                Navigation.PushAsync(new Views.EditarProduto
                {
                    BindingContext = p,
                });

            }
            catch (Exception ex)
            {
                DisplayAlert("Ops", ex.Message, "Ok");
            }
        }

        private async void lst_produtos_Refreshing(object sender, EventArgs e)
        {
            try
            {
                lista.Clear();

                List<Produto> tmp = await App.Db.GetAll();

                tmp.ForEach(i => lista.Add(i));
            }

            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "Ok");

            }
            finally
            {
                lst_produtos.IsRefreshing = false;
            }
        }

        private async void picker_filtro_categoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string categoria = picker_filtro_categoria.SelectedItem.ToString();

                lista.Clear();
                List<Produto> tmp;

                if (categoria == "Todos")
                    tmp = await App.Db.GetAll();
                else
                    tmp = await App.Db.GetByCategoria(categoria); // esse m�todo criei no banco de dados

                tmp.ForEach(i => lista.Add(i));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }
    }
}


