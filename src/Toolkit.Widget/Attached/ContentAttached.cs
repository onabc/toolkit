﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Toolkit.Widget.Attached
{
    public static class ContentAttached
    {
        #region ContentMargin

        public static Thickness GetContentMargin(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(ContentMarginProperty);
        }

        public static void SetContentMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(ContentMarginProperty, value);
        }

        public static readonly DependencyProperty ContentMarginProperty =
            DependencyProperty.RegisterAttached("ContentMargin", typeof(Thickness), typeof(ContentAttached), new PropertyMetadata(new Thickness()));

        #endregion ContentMargin

        #region Orientation

        public static Orientation GetOrientation(DependencyObject obj)
        {
            return (Orientation)obj.GetValue(OrientationProperty);
        }

        public static void SetOrientation(DependencyObject obj, Orientation value)
        {
            obj.SetValue(OrientationProperty, value);
        }

        /// <summary>
        /// path and content children stacked.
        /// </summary>
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.RegisterAttached("Orientation", typeof(Orientation), typeof(ContentAttached), new PropertyMetadata(Orientation.Vertical));

        #endregion Orientation

        #region Path

        public static Geometry GetPath(DependencyObject obj)
        {
            return (Geometry)obj.GetValue(PathProperty);
        }

        public static void SetPath(DependencyObject obj, Geometry value)
        {
            obj.SetValue(PathProperty, value);
        }

        /// <summary>
        /// path geometry for content part.
        /// </summary>
        public static readonly DependencyProperty PathProperty =
            DependencyProperty.RegisterAttached("Path", typeof(Geometry), typeof(ContentAttached), new PropertyMetadata());

        #endregion Path

        #region CheckedPath

        public static Geometry GetCheckedPath(DependencyObject obj)
        {
            return (Geometry)obj.GetValue(CheckedPathProperty);
        }

        public static void SetCheckedPath(DependencyObject obj, Geometry value)
        {
            obj.SetValue(CheckedPathProperty, value);
        }

        public static readonly DependencyProperty CheckedPathProperty =
           DependencyProperty.RegisterAttached("CheckedPath", typeof(Geometry), typeof(ContentAttached), new PropertyMetadata());

        #endregion CheckedPath

        #region PathMargin

        public static Thickness GetPathMargin(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(PathMarginProperty);
        }

        public static void SetPathMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(PathMarginProperty, value);
        }

        public static readonly DependencyProperty PathMarginProperty =
            DependencyProperty.RegisterAttached("PathMargin", typeof(Thickness), typeof(ContentAttached), new PropertyMetadata(new Thickness()));

        #endregion PathMargin

        #region PathHeight

        public static double GetPathHeight(DependencyObject obj)
        {
            return (double)obj.GetValue(PathHeightProperty);
        }

        public static void SetPathHeight(DependencyObject obj, double value)
        {
            obj.SetValue(PathHeightProperty, value);
        }

        public static readonly DependencyProperty PathHeightProperty =
            DependencyProperty.RegisterAttached("PathHeight", typeof(double), typeof(ContentAttached), new PropertyMetadata(Double.NaN));

        #endregion PathHeight

        #region PathWidth

        public static double GetPathWidth(DependencyObject obj)
        {
            return (double)obj.GetValue(PathWidthProperty);
        }

        public static void SetPathWidth(DependencyObject obj, double value)
        {
            obj.SetValue(PathWidthProperty, value);
        }

        public static readonly DependencyProperty PathWidthProperty =
            DependencyProperty.RegisterAttached("PathWidth", typeof(double), typeof(ContentAttached), new PropertyMetadata(Double.NaN));

        #endregion PathWidth

        #region PathFill

        public static Brush GetPathFill(DependencyObject obj)
        {
            return (Brush)obj.GetValue(PathFillProperty);
        }

        public static void SetPathFill(DependencyObject obj, Brush value)
        {
            obj.SetValue(PathFillProperty, value);
        }

        public static readonly DependencyProperty PathFillProperty =
            DependencyProperty.RegisterAttached("PathFill", typeof(Brush), typeof(ContentAttached), new PropertyMetadata());

        #endregion PathFill

        #region PathHorizontalAlignment

        public static HorizontalAlignment GetPathHorizontalAlignment(DependencyObject obj)
        {
            return (HorizontalAlignment)obj.GetValue(PathHorizontalAlignmentProperty);
        }

        public static void SetPathHorizontalAlignment(DependencyObject obj, HorizontalAlignment value)
        {
            obj.SetValue(PathHorizontalAlignmentProperty, value);
        }

        public static readonly DependencyProperty PathHorizontalAlignmentProperty =
            DependencyProperty.RegisterAttached("PathHorizontalAlignment", typeof(HorizontalAlignment), typeof(ContentAttached), new PropertyMetadata(HorizontalAlignment.Stretch));

        #endregion PathHorizontalAlignment

        #region PathVerticalAlignment

        public static VerticalAlignment GetPathVerticalAlignment(DependencyObject obj)
        {
            return (VerticalAlignment)obj.GetValue(PathVerticalAlignmentProperty);
        }

        public static void SetPathVerticalAlignment(DependencyObject obj, VerticalAlignment value)
        {
            obj.SetValue(PathVerticalAlignmentProperty, value);
        }

        public static readonly DependencyProperty PathVerticalAlignmentProperty =
            DependencyProperty.RegisterAttached("PathVerticalAlignment", typeof(VerticalAlignment), typeof(ContentAttached), new PropertyMetadata(VerticalAlignment.Stretch));

        #endregion PathVerticalAlignment

        #region PathStretch

        public static Stretch GetPathStretch(DependencyObject obj)
        {
            return (Stretch)obj.GetValue(PathStretchProperty);
        }

        public static void SetPathStretch(DependencyObject obj, Stretch value)
        {
            obj.SetValue(PathStretchProperty, value);
        }

        public static readonly DependencyProperty PathStretchProperty =
            DependencyProperty.RegisterAttached("PathStretch", typeof(Stretch), typeof(ContentAttached), new PropertyMetadata(Stretch.Uniform));

        #endregion PathStretch

        #region AtachedContent

        public static object GetAtachedContent(DependencyObject obj)
        {
            return obj.GetValue(AtachedContentProperty);
        }

        public static void SetAtachedContent(DependencyObject obj, object value)
        {
            obj.SetValue(AtachedContentProperty, value);
        }

        public static readonly DependencyProperty AtachedContentProperty =
            DependencyProperty.RegisterAttached("AtachedContent", typeof(object), typeof(ContentAttached), new PropertyMetadata());

        public static string GetAtachedContentStringFormat(DependencyObject obj)
        {
            return (string)obj.GetValue(AtachedContentStringFormatProperty);
        }

        public static void SetAtachedContentStringFormat(DependencyObject obj, string value)
        {
            obj.SetValue(AtachedContentStringFormatProperty, value);
        }

        public static readonly DependencyProperty AtachedContentStringFormatProperty =
            DependencyProperty.RegisterAttached("AtachedContentStringFormat", typeof(string), typeof(ContentAttached), new PropertyMetadata());

        public static DataTemplate GetAtachedContentTemplate(DependencyObject obj)
        {
            return (DataTemplate)obj.GetValue(AtachedContentTemplateProperty);
        }

        public static void SetAtachedContentTemplate(DependencyObject obj, DataTemplate value)
        {
            obj.SetValue(AtachedContentTemplateProperty, value);
        }

        public static readonly DependencyProperty AtachedContentTemplateProperty =
            DependencyProperty.RegisterAttached("AtachedContentTemplate", typeof(DataTemplate), typeof(ContentAttached), new PropertyMetadata());

        public static DataTemplateSelector GetAtachedContentTemplateSelector(DependencyObject obj)
        {
            return (DataTemplateSelector)obj.GetValue(AtachedContentTemplateSelectorProperty);
        }

        public static void SetAtachedContentTemplateSelector(DependencyObject obj, DataTemplateSelector value)
        {
            obj.SetValue(AtachedContentTemplateSelectorProperty, value);
        }

        public static readonly DependencyProperty AtachedContentTemplateSelectorProperty =
            DependencyProperty.RegisterAttached("AtachedContentTemplateSelector", typeof(DataTemplateSelector), typeof(ContentAttached), new PropertyMetadata());

        #endregion AtachedContent

        #region CheckedContent

        public static object GetCheckedContent(DependencyObject obj)
        {
            return obj.GetValue(CheckedContentProperty);
        }

        public static void SetCheckedContent(DependencyObject obj, object value)
        {
            obj.SetValue(CheckedContentProperty, value);
        }

        public static readonly DependencyProperty CheckedContentProperty =
            DependencyProperty.RegisterAttached("CheckedContent", typeof(object), typeof(ContentAttached), new PropertyMetadata());

        #endregion CheckedContent

        #region NormalImage

        public static ImageSource GetNormalImage(DependencyObject obj)
        {
            return (ImageSource)obj.GetValue(NormalImageProperty);
        }

        public static void SetNormalImage(DependencyObject obj, ImageSource value)
        {
            obj.SetValue(NormalImageProperty, value);
        }

        public static readonly DependencyProperty NormalImageProperty =
            DependencyProperty.RegisterAttached("NormalImage", typeof(ImageSource), typeof(ContentAttached), new PropertyMetadata());

        #endregion NormalImage

        #region CheckedImage

        public static ImageSource GetCheckedImage(DependencyObject obj)
        {
            return (ImageSource)obj.GetValue(CheckedImageProperty);
        }

        public static void SetCheckedImage(DependencyObject obj, ImageSource value)
        {
            obj.SetValue(CheckedImageProperty, value);
        }

        public static readonly DependencyProperty CheckedImageProperty =
            DependencyProperty.RegisterAttached("CheckedImage", typeof(ImageSource), typeof(ContentAttached), new PropertyMetadata());

        #endregion CheckedImage
    }
}