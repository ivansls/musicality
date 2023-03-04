


using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Microsoft.WindowsAPICodePack.Shell.PropertySystem.SystemProperties.System;

namespace Musicaliti
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int c = 0;
        int play_count = 0;
        int volume;
        string put = "";
        int click = 0;
        int repeat_click = 0;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string put = logica.putt[list.SelectedIndex];
            text_song.Text = put.Split("\\").Last().ToString();
            logica.put = put;
            media.Source = new Uri(put);
        }

        private void play_Click(object sender, RoutedEventArgs e)
        {
            
            if (play_count == 0)
            {

                play.Background = new SolidColorBrush(Color.FromArgb(100, 228, 255, 84));
                media.Play();
                play_count = 1;
                Thread thread = new Thread(_ =>
                {
                    timeline.timeli_start(this);
                });
                thread.Start();
            }
            else if (play_count == 1)
            {
                play.Background = new SolidColorBrush(Color.FromArgb(255, 228, 255, 84));
                media.Pause();
                play_count = 0;
               
            }
            
        }

        private void mix_Click(object sender, RoutedEventArgs e)
        {
            if (click == 0)
            {
                mix.Background = new SolidColorBrush(Color.FromArgb(100, 228, 255, 84));
                click = 1;
                logica.count_mix = 1;
                logica.dop_count_mix = 1;
            }
            else
            {
                mix.Background = new SolidColorBrush(Color.FromArgb(255, 228, 255, 84));
                click = 0;
                logica.count_mix = 0;
                logica.dop_count_mix = 0;
            }
        }

        private void audioSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            media.Volume = audioSlider.Value/ 100;
        }

        private void folder_Click(object sender, RoutedEventArgs e)
        {
            logica.show_file(list, text);
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            logica.next_song(list, media, audioSliderr);
        }

        private void previous_Click(object sender, RoutedEventArgs e)
        {
            logica.previous_song(list, media, audioSliderr);
        }



        private void audioSliderr_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            media.Position = new TimeSpan(Convert.ToInt64(audioSliderr.Value));
            start_time.Text = new TimeSpan(Convert.ToInt64(audioSliderr.Value)).ToString(@"mm\:ss");
            

        }

        private void media_MediaOpened(object sender, RoutedEventArgs e)
        {
            
            audioSliderr.Maximum = media.NaturalDuration.TimeSpan.Ticks;
            
            last_time.Text = new TimeSpan(Convert.ToInt64(audioSliderr.Maximum)).ToString(@"mm\:ss");
        }

        private void repeat_Click(object sender, RoutedEventArgs e)
        {
            if (repeat_click == 0)
            {
                repeat.Background = new SolidColorBrush(Color.FromArgb(100, 228, 255, 84));
                repeat_click = 1;
                logica.count_mix = 2;
            }
            else
            {
                repeat.Background = new SolidColorBrush(Color.FromArgb(255, 228, 255, 84));
                repeat_click = 0;
                logica.count_mix = 0;
            }
        }


    }
}
