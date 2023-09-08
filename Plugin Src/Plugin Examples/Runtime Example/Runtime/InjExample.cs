using System;

namespace KI_Plugin_Examples.Runtime
{
    // We Put all are Logic inside This class
    internal class InjExample
    {
        public static void WindowsPopup()
        {
            bool Example = true;

            if (Example == true){
                System.Windows.Forms.MessageBox.Show("A True Popup");
            }
            else{
                Environment.Exit(0);
            }

        }
    }


}
