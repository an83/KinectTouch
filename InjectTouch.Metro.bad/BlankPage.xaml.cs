using System;
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
        public static extern void Touch(int x, int y);

        [DllImport("InjectTouch.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Hold(int x, int y);

        private void Tap_Click(object sender, RoutedEventArgs e)
        {
            Touch(10, 10);
        }

        private void Hold_Click(object sender, RoutedEventArgs e)
        {
            Hold(10, 10);
        }
    }
}
