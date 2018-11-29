using Presto.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Presto.SWCamp.Lyrics
{
    /// <summary>
    /// Window2.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
            MouseLeftButtonDown += LyricsWindow_MouseLeftButtonDown;
        }
        private void LyricsWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
            //throw new NotImplementedException();
        }
        private void svLineUp(object sender, RoutedEventArgs e)
        {
            sv.LineUp();
        }
        private void svLineDown(object sender, RoutedEventArgs e)
        {
            sv.LineDown();
        }
        // 전체가사창 닫기
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
