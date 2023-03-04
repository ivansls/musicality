using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Threading;
using MathNet.Numerics;
using Microsoft.WindowsAPICodePack.Shell.Interop;
using static Microsoft.WindowsAPICodePack.Shell.PropertySystem.SystemProperties.System;

namespace Musicaliti
{
    public class timeline
    {
       

        public static void timeli_start(MainWindow window)
        {
            bool flag = true;
            while (flag)
            {
                window.Dispatcher.Invoke(() => { 
                    window.audioSliderr.Value = System.Convert.ToDouble(window.media.Position.Ticks);
                    if (System.Convert.ToDouble(window.media.Position.Ticks) == window.audioSliderr.Maximum)
                    {
                        logica.next_song(window.list, window.media, window.audioSliderr);
                    }
                });
                Thread.Sleep(1000);
            }
        }
        
    }
}
