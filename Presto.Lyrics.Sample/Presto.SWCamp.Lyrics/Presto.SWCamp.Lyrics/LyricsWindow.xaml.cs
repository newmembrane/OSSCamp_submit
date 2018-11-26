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
using Path = System.IO.Path;

namespace Presto.SWCamp.Lyrics
{
    /// <summary>
    /// LyricsWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LyricsWindow : Window
    {
        public Lyrics lyrics = new Lyrics();
        string filepath;
        public LyricsWindow()
        {
            InitializeComponent();
            
            //파싱 확인용
            //string tmp_filename = "supercell - Sayonara Memories";
            //lyrics.InitLyrics("C:\\Users\\Admin\\GitAheadRepos\\OSSCamp_submit\\Presto.Lyrics.Sample\\Musics\\" + tmp_filename + ".lrc");
            //var tmp = lyrics.dic;
            //if (tmp.Count <= 0)   MessageBox.Show("there is empty lyrics file...");
            //else
            //{
            //    foreach (var value in tmp)
            //        MessageBox.Show(value.Key.TotalMilliseconds.ToString() + "\n" + value.Value.ToString());
            //}
            //lyrics.clear();

            MouseLeftButtonDown += LyricsWindow_MouseLeftButtonDown;
            PrestoSDK.PrestoService.Player.StreamChanged += Player_StreamChanged;
        }

        private void LyricsWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
            //throw new NotImplementedException();
        }

        private void Player_StreamChanged(object sender, Common.StreamChangedEventArgs e)
        {
            lyrics.clear();
            //파일 읽어오기
            try
            {
                var musicPath = PrestoSDK.PrestoService.Player.CurrentMusic.Path;
                var lrcFileName = Path.GetFileNameWithoutExtension(musicPath) + ".lrc";
                var dirPath = Path.GetDirectoryName(musicPath);
                var result = Path.Combine(dirPath, lrcFileName);
                lyrics.InitLyrics(result);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("재생중이 아닙니다.");
            }
            //lyrics.InitLyrics("../../../../ Musics / TWICE - Dance The Night Away.lrc");
            if(lyrics.dic.Count == 0)
            {
                lyrics_prev_line.Text = "";
                lyrics_curr_line.Text = "가사가 없습니다.";
                lyrics_next_line.Text = "";
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
            try
            {
                //textLyrics.Text = PrestoSDK.PrestoService.Player.Position.ToString(); //현재 재생중인 음악 경로
                lyrics.SyncCurrentTimeLyrics(PrestoSDK.PrestoService.Player.Position);
                int index = lyrics.CurrIndex;
                lyrics_prev_line.Text = lyrics.prevLine(index);
                lyrics_curr_line.Text = lyrics.currLine(index);
                lyrics_next_line.Text = lyrics.nextLine(index);
            }
            catch (Exception)
            {
                //인덱스 범위 초과에 대한 오류 처리
            }
        }
    }
    
}
