using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using XamarinCRUD.Resources;
using XamarinCRUD.Resources.DataHelper;
using XamarinCRUD.Resources.Model;
using System;
using Android.Util;

namespace XamarinCRUD
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        TextView textMessage;
        Database db;
        ListView lstData;
        List<XamarinCRUD.Resources.Model.Person> lstSource = new List<XamarinCRUD.Resources.Model.Person>(); 

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            //Create Database
            db = new Database();
            db.CreateDataBase();

            textMessage = FindViewById<TextView>(Resource.Id.message);
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);

            var edtFName = FindViewById<EditText>(Resource.Id.fname);
            var edtLName = FindViewById<EditText>(Resource.Id.lname);
            var edtAge = FindViewById<EditText>(Resource.Id.age);
            var edtId = FindViewById<TextView>(Resource.Id.idTxt);
            lstData = FindViewById<ListView>(Resource.Id.personList);
            var btnAdd = FindViewById<Button>(Resource.Id.addbtn);
            var btnDelete = FindViewById<Button>(Resource.Id.deletebtn);
            var btnEdit = FindViewById<Button>(Resource.Id.editbtn);
            var upBtn = FindViewById<Button>(Resource.Id.uploadBtn);

            LoadData();

            //event
            btnAdd.Click += delegate
            {
                XamarinCRUD.Resources.Model.Person pers = new XamarinCRUD.Resources.Model.Person()
                {
                    FirstName = edtFName.Text,
                    LastName = edtLName.Text,
                    Age = int.Parse(edtAge.Text)
                };
                if (db.InsertPerson(pers))
                {
                    Toast.MakeText(this, "Successfully Added Person", ToastLength.Short).Show();
                    LoadData();
                    edtFName.Text = "";
                    edtLName.Text = "";
                    edtAge.Text = "";
                    edtId.Text = "0";
                }
            };
            btnEdit.Click += delegate
            {
                XamarinCRUD.Resources.Model.Person pers = new XamarinCRUD.Resources.Model.Person()
                {
                    FirstName = edtFName.Text,
                    LastName = edtLName.Text,
                    Age = int.Parse(edtAge.Text),
                    Id = int.Parse(edtId.Text)
                };
                if (db.EditPerson(pers))
                {
                    Toast.MakeText(this, "Successfully Edited Person", ToastLength.Short).Show();
                    LoadData();
                    edtFName.Text = "";
                    edtLName.Text = "";
                    edtAge.Text = "";
                    edtId.Text = "0";
                }
            };
            btnDelete.Click += delegate
            {
                XamarinCRUD.Resources.Model.Person pers = new XamarinCRUD.Resources.Model.Person()
                {
                    FirstName = edtFName.Text,
                    LastName = edtLName.Text,
                    Age = int.Parse(edtAge.Text),
                    Id = int.Parse(edtId.Text)
    };
                if (db.DeletePerson(pers))
                {
                    Toast.MakeText(this, "Successfully Deleted Person", ToastLength.Short).Show();
                    LoadData();
                    edtFName.Text = "";
                    edtLName.Text = "";
                    edtAge.Text = "";
                    edtId.Text = "0";
                }
            };

            lstData.ItemClick += (s, e) => {
                //set color on clicked item
                for (int i = 0; i < lstData.Count; i++)
                {
                    if (e.Position == i)
                        lstData.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.Green);
                    else
                        lstData.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.White);
                }

                //bind data
                var txtfname1 = e.View.FindViewById<TextView>(Resource.Id.textView1);
                var txtlname1 = e.View.FindViewById<TextView>(Resource.Id.textView2);
                var txtage1 = e.View.FindViewById<TextView>(Resource.Id.textView3);
                var txtid = e.View.FindViewById<TextView>(Resource.Id.textView4);

                edtFName.Text = txtfname1.Text;
                edtLName.Text = txtlname1.Text;
                edtAge.Text = txtage1.Text;
                edtId.Text = txtid.Text;
            };

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.navigation_home:
                    textMessage.SetText(Resource.String.title_home);
                    return true;
                case Resource.Id.navigation_dashboard:
                    textMessage.SetText(Resource.String.title_dashboard);
                    return true;
                case Resource.Id.navigation_notifications:
                    textMessage.SetText(Resource.String.title_notifications);
                    return true;
            }
            return false;
        }
        public void LoadData()
        {
            lstSource = db.personTableList();
            var adapter = new ListViewAdapter(this, lstSource);
            lstData.Adapter = adapter;
        }
    }
}

