﻿namespace NfcXample
{
    using System;
    using System.Text;

    using Android.App;
    using Android.Nfc;
    using Android.OS;
    using Android.Widget;

    [Activity, IntentFilter(new[] { "android.nfc.action.NDEF_DISCOVERED" },
        DataMimeType = MainActivity.ViewApeMimeType,
        Categories = new[] { "android.intent.category.DEFAULT" })]
    public class DisplayHominidActivity : Activity
    {
        private ImageView _imageView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.DisplayHominid);
            if (Intent == null)
            {
                return;
            }

            var intentType = Intent.Type ?? String.Empty;
            _imageView = FindViewById<ImageView>(Resource.Id.ape_view);

            var button = FindViewById<Button>(Resource.Id.back_to_main_activity);
            button.Click += (sender, args) => Finish();

            if (MainActivity.ViewApeMimeType.Equals(intentType))
            {
                var rawMessages = Intent.GetParcelableArrayExtra(NfcAdapter.ExtraNdefMessages);
                var msg = (NdefMessage)rawMessages[0];
                var apeRecord = msg.GetRecords()[0];
                var apeName = Encoding.ASCII.GetString(apeRecord.GetPayload());
                DisplayHominid(apeName);
            }
        }

        private void DisplayHominid(string name)
        {
            var apeId = 0;

            if ("cornelius".Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                apeId = Resource.Drawable.cornelius;
            }
            if ("dr_zaius".Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                apeId = Resource.Drawable.dr_zaius;
            }
            if ("gorillas".Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                apeId = Resource.Drawable.gorillas;
            }
            if ("heston".Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                apeId = Resource.Drawable.heston;
            }

            if (apeId > 0)
            {
                _imageView.SetImageResource(apeId);
            }
        }
    }
}
