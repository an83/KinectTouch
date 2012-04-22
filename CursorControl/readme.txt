================================================================================

Cursor Control – READ ME
Copyright © Microsoft Corporation.
This source is subject to the Microsoft Public License (Ms-PL).
Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
All other rights reserved. 
=============================
OVERVIEW  

This sample demonstrates how to use the Kinect skeletal tracking feature to move the mouse cursor with a user's hands.  
- By default, the right hand moves the cursor and the left hand controls the left mouse button.
- Then the left hand is raised, the mouse button is held down.  When the left hand is lowered, the mouse button is released.
- Use the checkbox on the main UI to switch to "left-handed" mode where the hand functions are reversed.

=============================
SAMPLE LANGUAGE IMPLEMENTATIONS     
 
This sample is available in the following language implementations:
     C#
     Visual Basic

=============================
FILES   
- App.xaml: declaration of application level resources
- App.xaml.cs/vb: interaction logic behind app.xaml
- MainWindow.xaml: declaration of layout within main application window
- MainWindow.xaml.cs/vb: NUI initialization, processing
- CursorControl.ico: Application icon used in title bar and task bar

=============================
BUILDING THE SAMPLE   

To build the sample using Visual Studio (preferred method):
-----------------------------------------------------------
1. In Windows Explorer, navigate to the [directgory pathname] directory.
2. Double-click the icon for the .sln (solution) file to open the file in Visual Studio.
3. In the Build menu, select Build Solution. The application will be built in the default \Debug or \Release directory.

=============================
RUNNING THE SAMPLE   
 
To run the sample:
------------------
1. Navigate to the directory that contains the new executable, using the command prompt or Windows Explorer.
2. Type Cursorcontrol at the command line, or double-click the icon for CursorControl to launch it from Windows Explorer.
