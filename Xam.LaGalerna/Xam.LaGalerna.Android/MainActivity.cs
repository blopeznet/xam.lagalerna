using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;

namespace Xam.LaGalerna.Droid
{
    [Activity(Label = "@string/ApplicationName", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]

    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            global::Xamarin.Forms.Forms.SetFlags(new[] { "CollectionView_Experimental" });
            Xam.Forms.VideoPlayer.Android.VideoPlayerRenderer.Init();
            global::Xamarin.Forms.Forms.Init(this, bundle);
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);
            LoadApplication(new App());               
            Window.SetStatusBarColor(Android.Graphics.Color.Rgb(239, 238, 237)); //status bar            
        }

     

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

