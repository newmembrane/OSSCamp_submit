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

            for (int i=3;i<lines.Length;i++)
            {
                //정규식을 사용하여 시간 추출
                Regex regex = new Regex(@"[0-9]{2,3}\:[0-9]{2}\.[0-9]{2}");
                MatchCollection resultTime = regex.Matches(lines[i]);
                if (resultTime.Count < 1) break;
                var time = TimeSpan.ParseExact(resultTime[0].Groups[0].ToString(), @"mm\:ss\.ff", CultureInfo.InvariantCulture);
                var lyrStartIndex = 10;

                // 가사 추출
                if (lines[i][10] == ']') lyrStartIndex++;
                var lyrLine = lines[i].Substring(lyrStartIndex).ToString();

                MessageBox.Show(time.TotalMilliseconds.ToString()+"\n"+lyrLine);

                //var splitData = lines[i].Split(']');
                //var time = TimeSpan.ParseExact(splitData[0].Substring(1).Trim(),
                //    @"mm\:ss\.ff", CultureInfo.InvariantCulture);
                //var lyrLine = splitData[1];
                //MessageBox.Show(time.TotalMilliseconds.ToString()+"\n"+lyrLine);
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
