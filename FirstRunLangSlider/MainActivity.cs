using Android.App;
using Android.Widget;
using Android.OS;

namespace FirstRunLangSlider
{
    [Activity(Label = "FirstRunLangSlider")]
    public class MainActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
             base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
        }
    }
}

