using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows.Documents;
using System.Windows.Controls;
using static Microsoft.WindowsAPICodePack.Shell.PropertySystem.SystemProperties.System;
using System.Windows;

namespace Musicaliti
{
    internal class logica
    {
        public static List<string> putt = new List<string>();
        public static int count_mix = 0;
        public static int dop_count_mix = 0;
        public static string put = "";
        public static void show_file(ListBox list, TextBlock text)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog { IsFolderPicker = true};
            var result = dialog.ShowDialog();
            text.Text = dialog.FileName;
            List<string> file_mus = new List<string>();
            if (result == CommonFileDialogResult.Ok)
            {
                foreach (string s in Directory.GetFiles(dialog.FileName).Where(x => x.EndsWith(".mp3") || x.EndsWith(".flac")))
                {
                   // if (s.EndsWith(".mp3")||)
                    putt.Add(s);
                    file_mus.Add(s.Split("\\").Last());
                }
                list.ItemsSource = file_mus;
            }
        }

        public static void next_song(ListBox list, MediaElement media, Slider audioSliderr) 
        {
            if (count_mix == 0 & dop_count_mix == 0)
            {

                try
                {
                    put = putt[list.SelectedIndex + 1].ToString();
                    media.Source = new Uri(put);
                    list.SelectedIndex += 1;
                }
                catch (Exception ex)
                {
                    put = putt[0].ToString();
                    media.Source = new Uri(put);
                    list.SelectedIndex = 0;
                }
            }
            else if (count_mix == 1 & dop_count_mix == 1 | count_mix == 0 & dop_count_mix == 1)
            {
                mix(list, media);
            }
            else if (count_mix == 2)
            {
                repeat(media);
            }
            audioSliderr.Value = 0;
        }

        public static void previous_song(ListBox list, MediaElement media, Slider audioSliderr) 
        {
            try
            {
                put = putt[list.SelectedIndex - 1].ToString();
                media.Source = new Uri(put);
                list.SelectedIndex -= 1;
            }
            catch (Exception ex)
            {
                put = putt[0].ToString();
                media.Source = new Uri(put);
                list.SelectedIndex = list.Items.Count - 1;
            }
            audioSliderr.Value = 0;
        }

        private static void mix(ListBox list, MediaElement media)
        {
            Random rand = new Random();
            bool flag = true;
            string ranndd = putt[rand.Next(0, putt.Count)].ToString();
            while (flag)
            {
                if (put.ToString() == ranndd)
                {
                    ranndd = putt[rand.Next(0, putt.Count)].ToString();
                }
                else if (put.ToString() != ranndd) 
                {
                    media.Source = new Uri(ranndd);
                    list.SelectedIndex = putt.IndexOf(ranndd);
                    flag = false;
                }
            }
        }
        private static void repeat(MediaElement media)
        {
            media.Stop();
            media.Play();
        }
    }
}
