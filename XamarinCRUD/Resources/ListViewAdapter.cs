using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XamarinCRUD.Resources.Model;

namespace XamarinCRUD.Resources
{
    public class ViewHolder : Java.Lang.Object
    {
        public TextView txtFirstName { get; set; }
        public TextView txtLastName { get; set; }
        public TextView txtAge { get; set; }
    }
    public class ListViewAdapter:BaseAdapter
    {
        private Activity activity;
        private List<Model.Person> lstPerson;
        public ListViewAdapter(Activity activity, List<Model.Person> lstPerson)
        {
            this.activity = activity;
            this.lstPerson = lstPerson;
        }

        public override int Count {
            get
            {
                return lstPerson.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return lstPerson[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.listview_datatemplate, parent, false);
            var txtFName = view.FindViewById<TextView>(Resource.Id.textView1);
            var txtLName = view.FindViewById<TextView>(Resource.Id.textView2);
            var txtAge = view.FindViewById<TextView>(Resource.Id.textView3);
            var txtId = view.FindViewById<TextView>(Resource.Id.textView4);

            txtFName.Text = lstPerson[position].FirstName;
            txtLName.Text = lstPerson[position].LastName;
            txtAge.Text = lstPerson[position].Age.ToString();
            txtId.Text = lstPerson[position].Id.ToString();
            return view; 
        }
    }
}