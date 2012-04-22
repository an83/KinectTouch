﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace InjectTouch.Metro
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BlankPage : Page
    {
        public BlankPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        [DllImport("InjectTouch.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool InjectTouch(int x, int y, POINTER_FLAGS pointerFlags);

        [DllImport("InjectTouch.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Drag(int x, int y);


        private void Drag_Click(object sender, RoutedEventArgs e)
        {
            var ret = InjectTouch(10, 10,
                POINTER_FLAGS.POINTER_FLAG_DOWN | POINTER_FLAGS.POINTER_FLAG_INRANGE | POINTER_FLAGS.POINTER_FLAG_INCONTACT);

            if (ret)
                InjectTouch(10, 10, POINTER_FLAGS.POINTER_FLAG_UP);

            //Drag(300, 300);

        }

        #region Native Touch Enum

        public enum TOUCH_MASK : uint
        {
            TOUCH_MASK_NONE = 0x00000000,
            TOUCH_MASK_CONTACTAREA = 0x00000001,
            TOUCH_MASK_ORIENTATION = 0x00000002,
            TOUCH_MASK_PRESSURE = 0x00000004
        }
        public enum POINTER_INPUT_TYPE : uint
        {
            PT_POINTER = 0x00000001,
            PT_TOUCH = 0x00000002,
            PT_PEN = 0x00000003,
            PT_MOUSE = 0x00000004
        }

        public enum POINTER_FLAGS : uint
        {
            POINTER_FLAG_NONE = 0x00000000,
            POINTER_FLAG_NEW = 0x00000001,
            POINTER_FLAG_INRANGE = 0x00000002,
            POINTER_FLAG_INCONTACT = 0x00000004,
            POINTER_FLAG_FIRSTBUTTON = 0x00000010,
            POINTER_FLAG_SECONDBUTTON = 0x00000020,
            POINTER_FLAG_THIRDBUTTON = 0x00000040,
            POINTER_FLAG_OTHERBUTTON = 0x00000080,
            POINTER_FLAG_PRIMARY = 0x00000100,
            POINTER_FLAG_CONFIDENCE = 0x00000200,
            POINTER_FLAG_CANCELLED = 0x00000400,
            POINTER_FLAG_DOWN = 0x00010000,
            POINTER_FLAG_UPDATE = 0x00020000,
            POINTER_FLAG_UP = 0x00040000,
            POINTER_FLAG_WHEEL = 0x00080000,
            POINTER_FLAG_HWHEEL = 0x00100000
        }
        public enum TOUCH_FEEDBACK : uint
        {
            TOUCH_FEEDBACK_DEFAULT = 0x1,
            TOUCH_FEEDBACK_INDIRECT = 0x2,
            TOUCH_FEEDBACK_NONE = 0x3
        } 
        #endregion
    }
}
