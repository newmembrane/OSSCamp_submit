using Presto.SDK;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Presto.SWCamp.Lyrics
{
    /// <summary>
    /// LyricsWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LyricsWindow : Window
    {
        public Lyrics lyrics = new Lyrics();
        public string str { get; set; }
        public LyricsWindow()
        {
            InitializeComponent();
            

            //파일 읽어오기
            var title = PrestoSDK.PrestoService.Player.CurrentMusic;//재생중인 음악 정보
            //MessageBox.Show(title.Title);
            
            try
            {
                string filepath = System.IO.Path.GetFileNameWithoutExtension(PrestoSDK.PrestoService.Player.CurrentMusic.Path);
                lyrics.InitLyrics(filepath);
                MessageBox.Show("재생중인 음악경로 : " + filepath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("재생중이 아닙니다.");
            }
            

            // 타이머
            var timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(1000)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        // PrestoSDK.PrestoService.Player.Position
        private void Timer_Tick(object sender, EventArgs e)
        {
            textLyrics.Text = PrestoSDK.PrestoService.Player.Position.ToString();
            lyrics.SyncCurrentTimeLyrics(PrestoSDK.PrestoService.Player.Position);
            data_show.Text = lyrics.CurrLyrLine;
            str = PrestoSDK.PrestoService.Player.Position.ToString();
        }
    }
    
}
