using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.Content;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.IO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Content.Res;
using Com.Lilarcor.Cheeseknife;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Graphics;
using Android.Graphics.Drawables;
using Java.Lang;
using Java.Lang.Reflect;
using String = System.String;

namespace FirstRunLangSlider
{
    [Activity(Label = "LangSelector", MainLauncher = true)]
    public class LangSelector : BaseActivity
    {
        [InjectView(Resource.Id.picker)] public NumberPicker picker;
        [InjectView(Resource.Id.fab)] public FloatingActionButton fab;
        String TAG = "SetUpWizard";
        [InjectView(Resource.Id.cStart)] public CoordinatorLayout cStart;
        [InjectView(Resource.Id.toolbar)] private SupportToolbar toolbar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LangSelecting);
            // Create your application here
            Cheeseknife.Inject(this);
            
            SetSupportActionBar(toolbar);

            String[] social = { "English (United Kingdom)", "English (United States)", "German", "Chinese" };
            picker.MinValue = 0;
            picker.MaxValue = 2;

            picker.Value = 1;//display initial selected value


            picker.DescendantFocusability = DescendantFocusability.BlockDescendants;

            picker.SetDisplayedValues(social);

            SetNumberPickerTextColor(picker, Color.White);
            SetDividerColor(picker, Color.White);            
        }

        [InjectOnClick(Resource.Id.fab)]
        public void onFabClicked(object sender, EventArgs e)
        {
            switch (picker.Value)
            {
                case 0:
                    Log.Debug(TAG, "onClick: 0 ");
                    Snack("English (United Kingdom)");
                    break;
                case 1:
                    Log.Debug(TAG, "onClick: 1 ");
                    Snack("English (United States)");
                    break;
                case 2:
                    Log.Debug(TAG, "onClick: 2 ");
                    Snack("German");
                    break;
                case 3:
                    Log.Debug(TAG, "onClick: 3 ");
                    Snack("Chinese");
                    break;
            }
        }

        public void Snack(String message)
        {
            Snackbar.Make(cStart, message, Snackbar.LengthLong).Show();
        }

        //setting number picker text color
        private bool SetNumberPickerTextColor(NumberPicker numberPicker, Color color)
        {
            int count = numberPicker.ChildCount;
            for (int i = 0; i < count; i++)
            {
                View child = numberPicker.GetChildAt(i);
                if (child is EditText)
                {
                    try
                    {
                        Field selectorWheelPaintField = numberPicker.Class.GetDeclaredField("mSelectorWheelPaint");
                        selectorWheelPaintField.Accessible = true;
                        ((Paint)selectorWheelPaintField.Get(numberPicker)).Color = color;
                        ((EditText)child).SetTextColor(color);
                        numberPicker.Invalidate();
                        return true;
                    }
                    catch (NoSuchFieldException e)
                    {

                    }
                    catch (IllegalAccessException e)
                    {
                    }
                    catch (IllegalArgumentException e)
                    {
                    }
                }
            }
            return false;
        }

        //setting divider color
        private void SetDividerColor(NumberPicker picker, Color color)
        {

            Field[] pickerFields = picker.Class.GetDeclaredFields();
            foreach (Field pf in pickerFields) {
                if (pf.Name.Equals("mSelectionDivider")) {
                    pf.Accessible = true;
                    try {
                        ColorDrawable colorDrawable = new ColorDrawable(color);
                        pf.Set(picker, colorDrawable);
                    } catch (IllegalArgumentException e)
                    {
                        throw e;
                    } catch (Resources.NotFoundException e) {
                        throw e;
                    }
                    catch (IllegalAccessException e) {
                       throw e;
                    }
                    break;
                }
            }
        }
    }
}