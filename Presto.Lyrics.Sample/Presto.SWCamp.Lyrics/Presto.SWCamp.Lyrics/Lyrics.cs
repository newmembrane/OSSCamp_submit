using Presto.SDK;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Presto.SWCamp.Lyrics
{
    public class Lyrics
    {
        public Dictionary<TimeSpan, string> dic = new Dictionary<TimeSpan, string>();
        public TimeSpan CurrTime { get; set; }
        public double DoubledCurrTime { get; set; }
        public string CurrLyrLine { get; set; }
        public int CurrIndex { get; set; }
        public void InitLyrics(string filepath) 
        {
            DoubledCurrTime = 0;
            CurrLyrLine = "";
            var lines = File.ReadAllLines(filepath);
            //var lines = File.ReadAllLines("../../../../Musics/TWICE - Dance The Night Away.lrc");
            Regex time_reg = new Regex(@"[0-9]{2,3}\:[0-9]{2}\.[0-9]{2}");
            for (int i = 3; i < lines.Length; i++)
            {
                //정규식을 사용하여 시간 추출
                MatchCollection resultTime = time_reg.Matches(lines[i]);
                if (resultTime.Count < 1) break;
                var time = TimeSpan.ParseExact(resultTime[0].Groups[0].ToString(), @"mm\:ss\.ff", CultureInfo.InvariantCulture);
                var lyrStartIndex = 10;
                // 가사 추출
                if (lines[i][10] == ']') lyrStartIndex++;
                var lyrLine = lines[i].Substring(lyrStartIndex).ToString();
                //MessageBox.Show(time.TotalMilliseconds.ToString()+"\n"+lyrLine);

                // 같은시간대 가사 추가
                if (dic.ContainsKey(time))
                {
                    dic[time] = dic[time].ToString() + "\n" + lyrLine;
                }
                dic.Add(time, lyrLine);
            }
            //foreach (var value in dic)
                //MessageBox.Show(value.Key.TotalMilliseconds.ToString() + "\n" + value.Value.ToString());
        }
        /**
         * 재생되고 있는 음악 시간대를 이용하여 가가운 시간대 의 시간값과 가사 업데이트
         * @params
         * position : 현재 재생되는 음악 시간대
         */
        public void SyncCurrentTimeLyrics(double position)
        {
            int i = dic.Count - 1;
            if (dic.Count > 0)
            {
                for(i=dic.Count-1;i>=0;i--)
                {
                    if (dic.ElementAt(i).Key.TotalMilliseconds < position)
                        break;
                }
                if (i < 0) i = 0;
                CurrIndex = i;
                DoubledCurrTime = dic.ElementAt(i).Key.TotalMilliseconds;
                CurrLyrLine = dic.ElementAt(i).Value;
            }
        }
        public string prevLine(int index)
        {
            if (index > 0)
                return dic.ElementAt(index - 1).Value;
            else
                return "";
        }
        public string nextLine(int index)
        {
            if (index < dic.Count - 1)
                return dic.ElementAt(index + 1).Value;
            else
                return "";
        }
        public string currLine(int index)
        {
            return dic.ElementAt(index).Value;
        }
        public void clear()
        {
            dic.Clear();
        }
    }
    
}
