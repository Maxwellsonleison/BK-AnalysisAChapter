namespace bk_analysischapter
{
    /// <summary>
    /// Based On VoneChapter,this class are show one chapter content;
    /// </summary>
    public class OneChapter:VOneChapter
    {
        public OneChapter(string title,string content)
        {
            _Title = title;
            _Content = content;
        }

        public new string _Title { get; internal set; }

        public new string _Content { get; internal set; }
    }
}
