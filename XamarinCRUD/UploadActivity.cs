using Android;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XamarinCRUD
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class UploadActivity : Activity
    {
        ImageView uploadPhoto;
        //Button submitButton;
        byte[] fileBytes;

        readonly string[] permissiongGroup =
        {
            Manifest.Permission.ReadExternalStorage,
            Manifest.Permission.WriteExternalStorage,
            Manifest.Permission.Camera
        };
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.upload);

            uploadPhoto = FindViewById<ImageView>(Resource.Id.UploadPhoto);

            uploadPhoto.Click += UploadPhoto_Click;

        }

        private void UploadPhoto_Click(object sender, EventArgs e)
        {
            Android.Support.V7.App.AlertDialog.Builder photoAlert = new Android.Support.V7.App.AlertDialog.Builder(this);

            photoAlert.SetPositiveButton("UPLOAD PHOTO", (thisalert, args) =>
            {
                SelectPhoto();
            });

            photoAlert.Show();
        }

        async void SelectPhoto()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                Toast.MakeText(this, "Upload not supported", ToastLength.Short).Show();
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                CompressionQuality = 30
            });

            if (file == null)
            {
                return;
            }

            byte[] imageArray = System.IO.File.ReadAllBytes(file.Path);
            fileBytes = imageArray;
            Bitmap bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
            uploadPhoto.SetImageBitmap(bitmap);
        }
    }
}