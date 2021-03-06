﻿using System;
using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using Pampsip.Controls;
using Pampsip.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Pampsip.Models.Menu;

[assembly: ExportRenderer(typeof(ExtendedViewCell), typeof(ExtendedViewCellRenderer))]

namespace Pampsip.Droid.Renderers
{
    public class ExtendedViewCellRenderer : ViewCellRenderer
    {

        private Android.Views.View _cellCore;
        private Drawable _unselectedBackground;
        private bool _selected;

        protected override Android.Views.View GetCellCore(Cell item,
                                                          Android.Views.View convertView,
                                                          ViewGroup parent,
                                                          Context context)
        {
            _cellCore = base.GetCellCore(item, convertView, parent, context);
            try
            {
                _selected = ((Menus)item.BindingContext).isSelected;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            _unselectedBackground = _cellCore.Background;

            if(_selected)
            {
                ExtendedViewCell extendedViewCell = item as ExtendedViewCell;
                _cellCore.SetBackgroundColor(extendedViewCell.SelectedBackgroundColor.ToAndroid());
            }
            else
            {
                ExtendedViewCell extendedViewCell = item as ExtendedViewCell;
                _cellCore.SetBackgroundColor(Color.White.ToAndroid());
            }

            return _cellCore;
        }
    }

}
