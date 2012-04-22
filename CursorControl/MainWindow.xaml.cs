// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved. 

using System;
using System.Windows;
using System.Windows.Forms;

using Coding4Fun.Kinect.Wpf;

using Microsoft.Kinect;
using Nui = Microsoft.Kinect;

using MessageBox = System.Windows.MessageBox;
using System.Runtime.InteropServices;

[assembly:CLSCompliant(true)]
namespace Microsoft.Kinect.Samples.CursorControl
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private const float ClickThreshold = 0.33f;
		private const float SkeletonMaxX = 0.60f;
		private const float SkeletonMaxY = 0.40f;
		
		
		private NotifyIcon _notifyIcon = new NotifyIcon();


		public MainWindow()
		{
			InitializeComponent();

			// create tray icon
			_notifyIcon.Icon = new System.Drawing.Icon("CursorControl.ico");
			_notifyIcon.Visible = true;
			_notifyIcon.DoubleClick += delegate
			{
				this.Show();
				this.WindowState = WindowState.Normal;
				this.Focus();
			};
		}



		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			kinectSensorChooser.KinectSensorChanged += new DependencyPropertyChangedEventHandler(kinectSensorChooser_KinectSensorChanged);
		}

		private void StopKinect(KinectSensor sensor)
		{
			if (sensor != null)
			{
				if (sensor.IsRunning)
				{
					sensor.Stop();
					sensor.AudioSource.Stop();
				}
			}
		}

		void kinectSensorChooser_KinectSensorChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			KinectSensor old = (KinectSensor)e.OldValue;

			StopKinect(old);

			KinectSensor sensor = (KinectSensor)e.NewValue;

			if (sensor == null)
			{
				return;
			}

			TransformSmoothParameters parameters = new TransformSmoothParameters();
			parameters.Smoothing = 0.7f;
			parameters.Correction = 0.3f;
			parameters.Prediction = 0.4f;
			parameters.JitterRadius = 1.0f;
			parameters.MaxDeviationRadius = 0.5f;

			sensor.SkeletonStream.Enable(parameters);
			sensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
			sensor.DepthStream.Enable(DepthImageFormat.Resolution320x240Fps30);

			sensor.AllFramesReady += new EventHandler<AllFramesReadyEventArgs>(sensor_AllFramesReady);
			
            try
			{
				sensor.Start();
			}
			catch (System.IO.IOException)
			{
				//another app is using Kinect
				kinectSensorChooser.AppConflictOccurred();
			}
		}

		void sensor_AllFramesReady(object sender, AllFramesReadyEventArgs e)
		{
			sensor_DepthFrameReady(e);
			sensor_SkeletonFrameReady(e);
		}


		private void Window_Closed(object sender, EventArgs e)
		{
			// shut down the Kinect device
			_notifyIcon.Visible = false;

			if (kinectSensorChooser.Kinect != null)
			{
				kinectSensorChooser.Kinect.Stop();
			}
			
		}

		private void Window_StateChanged(object sender, EventArgs e)
		{
			if (WindowState == WindowState.Minimized)
			{ 
				this.Hide();
			}
		}

        [DllImport("InjectTouch.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Touch(int x, int y);


		void sensor_SkeletonFrameReady(AllFramesReadyEventArgs e)
		{

			using (SkeletonFrame skeletonFrameData = e.OpenSkeletonFrame())
			{
                if (skeletonFrameData == null)
                {
                    return; 
                }

				Skeleton[] allSkeletons = new Skeleton[skeletonFrameData.SkeletonArrayLength];

				skeletonFrameData.CopySkeletonDataTo(allSkeletons); 

				foreach (Skeleton sd in allSkeletons)
				{
					// the first found/tracked skeleton moves the mouse cursor
					if (sd.TrackingState == SkeletonTrackingState.Tracked)
					{
						// make sure both hands are tracked
						if (sd.Joints[JointType.HandLeft].TrackingState == JointTrackingState.Tracked &&
							sd.Joints[JointType.HandRight].TrackingState == JointTrackingState.Tracked)
						{
							int cursorX, cursorY;

							// get the left and right hand Joints
							Joint jointRight = sd.Joints[JointType.HandRight];
							Joint jointLeft = sd.Joints[JointType.HandLeft];

							// scale those Joints to the primary screen width and height
							Joint scaledRight = jointRight.ScaleTo((int)SystemParameters.PrimaryScreenWidth, (int)SystemParameters.PrimaryScreenHeight, SkeletonMaxX, SkeletonMaxY);
							Joint scaledLeft = jointLeft.ScaleTo((int)SystemParameters.PrimaryScreenWidth, (int)SystemParameters.PrimaryScreenHeight, SkeletonMaxX, SkeletonMaxY);

							// figure out the cursor position based on left/right handedness
							if (LeftHand.IsChecked.GetValueOrDefault())
							{
								cursorX = (int)scaledLeft.Position.X;
								cursorY = (int)scaledLeft.Position.Y;
							}
							else
							{
								cursorX = (int)scaledRight.Position.X;
								cursorY = (int)scaledRight.Position.Y;
							}

							bool leftClick;

							// figure out whether the mouse button is down based on where the opposite hand is
							if ((LeftHand.IsChecked.GetValueOrDefault() && jointRight.Position.Y > ClickThreshold) ||
									(!LeftHand.IsChecked.GetValueOrDefault() && jointLeft.Position.Y > ClickThreshold))
								leftClick = true;
							else
								leftClick = false;

							Status.Text = cursorX + ", " + cursorY + ", " + leftClick;


							NativeMethods.SendMouseInput(cursorX, cursorY, 
                                (int)SystemParameters.PrimaryScreenWidth, 
                                (int)SystemParameters.PrimaryScreenHeight, 
                                false);

                            if (leftClick)
                                Touch(cursorX, cursorY);

							return;
						}
					}
				}
			}


		}

		void sensor_DepthFrameReady(AllFramesReadyEventArgs e)
		{
			// if the window is displayed, show the depth buffer image
			if (this.WindowState == WindowState.Normal)
			{
				using (DepthImageFrame depthFrame = e.OpenDepthImageFrame())
				{
                    if (depthFrame == null)
                    {
                        return; 
                    }

					video.Source = depthFrame.ToBitmapSource();
				}
				
			}
		}
	}
}
