using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AppSaudeFamilia.Util;

namespace AppSaudeFamilia.Adapters
{
    [Activity(Label = "itensMenu")]
    public class AdapterItensMenu : BaseAdapter<MenuDTO>
    {
        private List<MenuDTO> itensMenu;
        private Activity context;
        public ProgressDialog loadingListaAcads;

        public AdapterItensMenu(Activity _context, List<MenuDTO> itens)
        {
            this.context = _context;
            this.itensMenu = itens;
            loadingListaAcads = new ProgressDialog(context);
        }

        public IList<MenuDTO> List { get; set; }

        public override int Count {
            get {
                return itensMenu.Count;
            }
        }

        public override MenuDTO this[int position] {
            get {
                return itensMenu[position];
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = context.LayoutInflater.Inflate(Resource.Layout.AdapterItensMenu, parent, false);
            ImageView imgItens = view.FindViewById<ImageView>(Resource.Id.imgItemMenu);
            TextView txtItens = view.FindViewById<TextView>(Resource.Id.txtItemMenu);

            //MUDAR OS TEXTOS DOS ITENS DO ADAPTER
            imgItens.SetImageResource(itensMenu[position].IdIcone);
            txtItens.Text = itensMenu[position].TxtItem.ToString();

            return view;
        }
    }
}