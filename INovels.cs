using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bk_analysischapter
{
    interface INovels
    {
        List<string> _directory { get; }

        List<string> _content { get;  }
        /// <summary>
        /// 根据索引获取章节
        /// </summary>
        /// <param name="ChapIndex"></param>
        /// <returns></returns>
        string CastToIndexChap(int ChapIndex);

        /// <summary>
        /// 根据标题获取章节
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        string GetChapterFromTitle(string title);

        /// <summary>
        /// 获取小说所有章节标题
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        List<string> GetAllDirectory();

        /// <summary>
        /// 从匹配内容获取章节
        /// </summary>
        /// <returns></returns>
        int GetCurrentLineOfContent();

        /// <summary>
        /// Warning,not usual
        /// </summary>
        void RemoveOfAll();

        OneChapter ChapterContent(int index);
    }
}
