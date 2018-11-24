using Presto.SDK;
using System;
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
        public LyricsWindow()
        {
            InitializeComponent();

            //파일 읽어오기
            var title = PrestoSDK.PrestoService.Player.CurrentMusic;//재생중인 음악 정보
            var lines = File.ReadAllLines("C:/Users/cbnu/Documents/Presto.Lyrics.Sample/Musics/볼빨간사춘기 - 여행.lrc");
            for(int i=3;i<lines.Length;i++)
            {
                //Regex regex = new Regex("")
                var splitData = lines[i].Split(']');
                var time = TimeSpan.ParseExact(splitData[0].Substring(1).Trim(),
                    @"mm\:ss\.ff", CultureInfo.InvariantCulture);
                MessageBox.Show(time.TotalMilliseconds.ToString());
            }

            // 타이머
            var timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(100)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        // PrestoSDK.PrestoService.Player.Position
        private void Timer_Tick(object sender, EventArgs e)
        {
            textLyrics.Text = PrestoSDK.PrestoService.Player.Position.ToString();
        }
    }
    
}
