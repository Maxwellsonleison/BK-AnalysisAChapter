using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace bk_analysischapter
{
    /// <summary>
    /// Novel核心类引用方法，此类可继承
    /// </summary>
    public class Novel:VNovels
    {
        private static string Selectpath;
        private static Encoding _Encoding = Encoding.UTF8;
        private static List<string> ContentList= new List<string>();
        private static List<string> TitleList = new List<string>();
        private static readonly Regex reg = new Regex("(第[0-9零|一|二|三|四|五|六|七|八|九|十|百|千|万]+?章)");
        private static readonly Regex _rex = new Regex(@"^-?\d+$ ");
        private  OneChapter nContent;
        
        public Novel(string path)
        {
            if(!string.IsNullOrEmpty(path))
            {
                Selectpath = path;                         
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public Novel(string path,Encoding encoding)
        {
            _Encoding = encoding;
            if (!string.IsNullOrEmpty(path))
            {
                Selectpath = path;
                
            }
            else
            {
                throw new NullReferenceException();
            }
            
        }


        public Novel(List<string> dir,List<string> con)
        {
            if (dir.Count == 0 || con.Count == 0) throw new ArgumentException("dir count cannot be null");
            //if (dir.Count != con.Count) throw new ArgumentException("var dir count not match with var con count");
            _directory = dir;
            _content = con;
        }


        public override int GetCurrentLineOfContent()
        {
            return ContentList.Count;
        }

        public override List<string> GetAllDirectory()
        {
            _directory = TitleList;
            return TitleList;
        }

        public override OneChapter GetChapterFromTitle(string title)
        {
            var _content="";
            if(_directory.Count>0&& _directory.Contains(title))
            {
                
                _content = ContentList[_directory.IndexOf(title)].ToString();
            }
            else
            {
                GetChapterFromTitle(title);
            }
            return new OneChapter(title,_content);
        }


       
        //public OneChapter ChapterContent(string title)
        //{
        //    if (ContentList.Count == 0 && _directory.Count == 0)
        //    {
        //        throw new NullReferenceException("zero content reference");
        //    }
        //    if(_directory.Contains(title))
        //    //int i = _directory.IndexOf(title);
        //    //nContent._Content = ContentList[i].ToString();
        //    //nContent._Title = _directory[i].ToString();
        //    return nContent;
        //}

        /// <summary>
        /// OneChapter File 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public override OneChapter ChapterContent(int index)
        {
            if(ContentList.Count==0&&_directory.Count==0)
            {
                throw new NullReferenceException("zero content reference");
            }
            var cc = ContentList[index].ToString();
            nContent = new OneChapter(_directory[index].ToString(),cc+ContentList.Count.ToString());
            return nContent;
        }


        public static Novel TxtFile(string path)
        {
            //初始化
            Selectpath = path;
            ContentList.Clear();
            
            StringBuilder st = new StringBuilder();
            string LastTitle="";

            foreach (string _line in File.ReadAllLines(Selectpath, _Encoding))
            {
                if (!string.IsNullOrEmpty(_line))
                {
                    if (!(reg.IsMatch(_line) || _line.StartsWith("楔子") || _line.StartsWith("前言") || _line.StartsWith("后记") || _rex.IsMatch(_line)))
                    {
                        st.AppendLine(_line);//追加内容
                    }
                    else
                    {
                        //TitleList.Add(_line);
                        if (TitleList.Count > 0)
                        { LastTitle = TitleList[TitleList.Count - 1]; }
                        //防止重复
                        if (!LastTitle.Replace(" ", "").Contains(_line.Replace(" ", "")))
                        {
                            TitleList.Add(_line);
                        }
                        if (st.Length > 50)//防止空内容匹配
                        {

                            ContentList.Add(st.ToString());
                            st.Clear();
                        }
                    }
                }
            }
            return new Novel(TitleList,ContentList);
        }

        public List<string> test()
        {
            if(ContentList.Count==0)
            {
                throw new NullReferenceException();
            }
            return ContentList;
        }

        /// <summary>
        /// This Method will clear memory.
        /// </summary>
        public override void RemoveOfAll()
        {
            TitleList.Clear();
            ContentList.Clear();
            _content.Clear();
            _directory.Clear();
        }
    }
}
