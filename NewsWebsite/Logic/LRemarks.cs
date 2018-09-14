using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class LRemarks
    {
        /// <summary>
        /// 获取文章评论
        /// </summary>
        /// <param name="inArticleID"></param>
        /// <returns></returns>
        public static List<Model.MRemarks> GetRemarks(long inArticleID)
        {
            return Data.DRemarks.GetRemarks(inArticleID);
        }

        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="inModel"></param>
        /// <returns></returns>
        public static int InsertRemark(Model.MRemarks inModel)
        {
            return Data.DRemarks.InsertRemark(inModel);
        }

        #region Reply
        /// <summary>
        /// 获取评论回复列表
        /// </summary>
        /// <param name="inRID"></param>
        /// <returns></returns>
        public static List<Model.MReplies> GetReplies(int inRID)
        {
            return Data.DRemarks.GetReplies(inRID);
        }

        /// <summary>
        /// 获取回复数
        /// </summary>
        /// <param name="inRID"></param>
        /// <returns></returns>
        public static int GetReplyTotal(int inRID)
        {
            return Data.DRemarks.GetReplyTotal(inRID);
        }
        #endregion
    }
}
