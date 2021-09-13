﻿using System;
using System.Windows;
using System.Windows.Controls;
using Toolkit.Widget.Standard;

namespace Toolkit.Widget.Controls
{
    public class CaptionButton : Button
    {
        static CaptionButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CaptionButton), new FrameworkPropertyMetadata(typeof(CaptionButton)));
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            this.owner = Window.GetWindow(this);
            if (this.owner != null)
            {
                this.owner.StateChanged += (sender, args) => this.ChangeVisibility();
                this.ChangeResizeMode();
                this.ChangeVisibility();
            }
        }

        private Window owner;
        private bool isDisplay;

        #region WindowAction 依赖属性

        /// <summary>
        /// 获取或设置用于按钮的窗口操作
        /// </summary>
        public WindowAction WindowAction
        {
            get { return (WindowAction)this.GetValue(WindowActionProperty); }
            set { this.SetValue(WindowActionProperty, value); }
        }

        public static readonly DependencyProperty WindowActionProperty =
            DependencyProperty.Register("WindowAction", typeof(WindowAction), typeof(CaptionButton), new UIPropertyMetadata(WindowAction.None));

        #endregion WindowAction 依赖属性

        #region Mode 依赖属性

        public CaptionButtonMode Mode
        {
            get { return (CaptionButtonMode)this.GetValue(ModeProperty); }
            set { this.SetValue(ModeProperty, value); }
        }

        public static readonly DependencyProperty ModeProperty =
            DependencyProperty.Register("Mode", typeof(CaptionButtonMode), typeof(CaptionButton), new UIPropertyMetadata(CaptionButtonMode.Normal));

        #endregion Mode 依赖属性

        #region IsChecked 依赖属性

        public bool IsChecked
        {
            get { return (bool)this.GetValue(IsCheckedProperty); }
            set { this.SetValue(IsCheckedProperty, value); }
        }

        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register("IsChecked", typeof(bool), typeof(CaptionButton), new UIPropertyMetadata(false));

        #endregion IsChecked 依赖属性

        protected override void OnClick()
        {
            this.WindowAction.Invoke(this);

            if (this.Mode == CaptionButtonMode.Toggle) this.IsChecked = !this.IsChecked;

            base.OnClick();
        }

        private void ChangeResizeMode()
        {
            switch (this.WindowAction)
            {
                case WindowAction.Maximize:
                    isDisplay = this.owner.ResizeMode == ResizeMode.CanResize || this.owner.ResizeMode == ResizeMode.CanResizeWithGrip;
                    break;

                case WindowAction.Normalize:
                    isDisplay = this.owner.ResizeMode == ResizeMode.CanResize || this.owner.ResizeMode == ResizeMode.CanResizeWithGrip;
                    break;

                case WindowAction.Minimize:
                    isDisplay = this.owner.ResizeMode != ResizeMode.NoResize;
                    break;

                case WindowAction.Close:
                    isDisplay = true;
                    break;
            }
        }

        private void ChangeVisibility()
        {
            if (isDisplay)
            {
                switch (this.WindowAction)
                {
                    case WindowAction.Maximize:
                        this.Visibility = this.owner.WindowState != WindowState.Maximized ? Visibility.Visible : Visibility.Collapsed;
                        break;

                    case WindowAction.Minimize:
                        this.Visibility = this.owner.WindowState != WindowState.Minimized ? Visibility.Visible : Visibility.Collapsed;
                        break;

                    case WindowAction.Normalize:
                        this.Visibility = this.owner.WindowState != WindowState.Normal ? Visibility.Visible : Visibility.Collapsed;
                        break;
                }
            }
            else
            {
                this.Visibility = Visibility.Collapsed;
            }
        }
    }
}