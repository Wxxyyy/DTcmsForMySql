using System;
using System.Data;
using System.Collections.Generic;
using DTcms.Common;

namespace DTcms.BLL
{
    /// <summary>
    /// ��������
    /// </summary>
    public partial class article
    {
        private readonly Model.sysconfig sysConfig = new BLL.sysconfig().loadConfig();//���ϵͳ������Ϣ
        private readonly DAL.MySql.article dal;

        public article()
        {
            dal = new DAL.MySql.article(sysConfig.sysdatabaseprefix);
        }

        #region ��������================================
        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int channel_id, int article_id)
        {
            string channelName = new BLL.site_channel().GetChannelName(channel_id);//��ѯƵ������
            if (string.IsNullOrEmpty(channelName))
            {
                return false;
            }
            return dal.Exists(channelName, article_id);
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int channel_id, string call_index)
        {
            if (string.IsNullOrEmpty(call_index))
            {
                return false;
            }
            string channelName = new BLL.site_channel().GetChannelName(channel_id);//��ѯƵ������
            if (string.IsNullOrEmpty(channelName))
            {
                return false;
            }
            return dal.Exists(channelName, call_index);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(Model.article model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public bool Update(Model.article model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public bool Delete(int channel_id, int article_id)
        {
            string channelName = new BLL.site_channel().GetChannelName(channel_id);//��ѯƵ������
            if (string.IsNullOrEmpty(channelName))
            {
                return false;
            }
            string content = dal.GetContent(channelName, article_id);//��ȡ��Ϣ����
            bool result = dal.Delete(channelName, channel_id, article_id);//ɾ������
            if (result && !string.IsNullOrEmpty(content))
            {
                FileHelper.DeleteContentPic(content, sysConfig.webpath + sysConfig.filepath); //ɾ������ͼƬ
            }
            return result;
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public Model.article GetModel(int channel_id, int article_id)
        {
            string channelName = new BLL.site_channel().GetChannelName(channel_id);//��ѯƵ������
            if (string.IsNullOrEmpty(channelName))
            {
                return null;
            }
            return dal.GetModel(channelName, article_id);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public Model.article GetModel(int channel_id, string call_index)
        {
            string channelName = new BLL.site_channel().GetChannelName(channel_id);//��ѯƵ������
            if (string.IsNullOrEmpty(channelName))
            {
                return null;
            }
            return dal.GetModel(channelName, call_index);
        }

        /// <summary>
        /// ���ǰ��������
        /// </summary>
        public DataSet GetList(int channel_id, int Top, string strWhere, string filedOrder)
        {
            string channelName = new BLL.site_channel().GetChannelName(channel_id);//��ѯƵ������
            if (string.IsNullOrEmpty(channelName))
            {
                return new DataSet();
            }
            return dal.GetList(channelName, Top, strWhere, filedOrder);
        }
        
        /// <summary>
        /// ��ò�ѯ��ҳ����
        /// </summary>
        public DataSet GetList(int channel_id, int category_id, int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            string channelName = new BLL.site_channel().GetChannelName(channel_id);//��ѯƵ������
            if (string.IsNullOrEmpty(channelName))
            {
                recordCount = 0;
                return new DataSet();
            }
            return dal.GetList(channelName, category_id, pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        #endregion

        #region ��չ����================================
        /// <summary>
        /// �Ƿ���ڱ���
        /// </summary>
        public bool ExistsTitle(int channel_id, string title)
        {
            string channelName = new BLL.site_channel().GetChannelName(channel_id);//��ѯƵ������
            if (string.IsNullOrEmpty(channelName))
            {
                return false;
            }
            return dal.ExistsTitle(channelName, title);
        }
        /// <summary>
        /// �Ƿ���ڱ���
        /// </summary>
        public bool ExistsTitle(int channel_id, int category_id, string title)
        {
            string channelName = new BLL.site_channel().GetChannelName(channel_id);//��ѯƵ������
            if (string.IsNullOrEmpty(channelName))
            {
                return false;
            }
            return dal.ExistsTitle(channelName, category_id, title);
        }

        /// <summary>
        /// ������Ϣ����
        /// </summary>
        public string GetTitle(int channel_id, int article_id)
        {
            string channelName = new BLL.site_channel().GetChannelName(channel_id);//��ѯƵ������
            if (string.IsNullOrEmpty(channelName))
            {
                return string.Empty;
            }
            return dal.GetTitle(channelName, article_id);
        }

        /// <summary>
        /// ������Ϣ����
        /// </summary>
        public string GetContent(string channel_name, int article_id)
        {
            if (string.IsNullOrEmpty(channel_name))
            {
                return string.Empty;
            }
            return dal.GetContent(channel_name, article_id);
        }

        /// <summary>
        /// ������Ϣ����
        /// </summary>
        public string GetContent(string channel_name, string call_index)
        {
            return dal.GetContent(channel_name, call_index);
        }

        /// <summary>
        /// ������Ϣ����ͼ
        /// </summary>
        public string GetImgUrl(int channel_id, int article_id)
        {
            string channelName = new BLL.site_channel().GetChannelName(channel_id);//��ѯƵ������
            if (string.IsNullOrEmpty(channelName))
            {
                return string.Empty;
            }
            return dal.GetImgUrl(channelName, article_id);
        }

        /// <summary>
        /// ��ȡ�Ķ�����
        /// </summary>
        public int GetClick(int channel_id, int article_id)
        {
            string channelName = new BLL.site_channel().GetChannelName(channel_id);//��ѯƵ������
            if (string.IsNullOrEmpty(channelName))
            {
                return 0;
            }
            return dal.GetClick(channelName, article_id);
        }

        /// <summary>
        /// ������������
        /// </summary>
        public int GetCount(int channel_id, string strWhere)
        {
            string channelName = new BLL.site_channel().GetChannelName(channel_id);//��ѯƵ������
            if (string.IsNullOrEmpty(channelName))
            {
                return 0;
            }
            return dal.GetCount(channelName, strWhere);
        }

        /// <summary>
        /// ������Ʒ�������
        /// </summary>
        public int GetStockQuantity(int channel_id, int article_id)
        {
            string channelName = new BLL.site_channel().GetChannelName(channel_id);//��ѯƵ������
            if (string.IsNullOrEmpty(channelName))
            {
                return 0;
            }
            return dal.GetStockQuantity(channelName, article_id);
        }

        /// <summary>
        /// �޸�һ������
        /// </summary>
        public bool UpdateField(int channel_id, int article_id, string strValue)
        {
            string channelName = new BLL.site_channel().GetChannelName(channel_id);//��ѯƵ������
            if (string.IsNullOrEmpty(channelName))
            {
                return false;
            }
            return dal.UpdateField(channelName, article_id, strValue);
        }

        /// <summary>
        /// ��ȡ΢������ʵ��
        /// </summary>
        public Model.article GetWXModel(int channel_id, int article_id)
        {
            string channelName = new BLL.site_channel().GetChannelName(channel_id);//��ѯƵ������
            if (string.IsNullOrEmpty(channelName))
            {
                return null;
            }
            DataTable dt = dal.GetList(channelName, 1, "id=" + article_id, "id desc").Tables[0];
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            Model.article model = new Model.article();
            model.id = int.Parse(dt.Rows[0]["id"].ToString());
            model.title = dt.Rows[0]["title"].ToString();
            model.img_url = dt.Rows[0]["img_url"].ToString();
            model.zhaiyao = dt.Rows[0]["zhaiyao"].ToString();
            model.content = dt.Rows[0]["content"].ToString();
            return model;
        }
        #endregion

        #region ǰ̨ģ����÷���========================
        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool ArticleExists(string channel_name, int article_id)
        {
            Dictionary<int, string> dic = new BLL.site_channel().GetListAll();
            if (!dic.ContainsValue(channel_name))
            {
                return false;
            }
            return dal.Exists(channel_name, article_id);
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool ArticleExists(string channel_name, string call_index)
        {
            Dictionary<int, string> dic = new BLL.site_channel().GetListAll();
            if (!dic.ContainsValue(channel_name))
            {
                return false;
            }
            return dal.Exists(channel_name, call_index);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public Model.article ArticleModel(string channel_name, int article_id)
        {
            Dictionary<int, string> dic = new BLL.site_channel().GetListAll();
            if (!dic.ContainsValue(channel_name))
            {
                return null;
            }
            return dal.GetModel(channel_name, article_id);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public Model.article ArticleModel(string channel_name, string call_index)
        {
            Dictionary<int, string> dic = new BLL.site_channel().GetListAll();
            if (!dic.ContainsValue(channel_name))
            {
                return null;
            }
            return dal.GetModel(channel_name, call_index);
        }

        /// <summary>
        /// ����Ƶ�����ƻ�ȡ�ܼ�¼��
        /// </summary>
        public int ArticleCount(string channel_name, int category_id, string strWhere)
        {
            Dictionary<int, string> dic = new BLL.site_channel().GetListAll();
            if (!dic.ContainsValue(channel_name))
            {
                return 0;
            }
            return dal.ArticleCount(channel_name, category_id, strWhere);
        }

        /// <summary>
        /// ����Ƶ��������ʾǰ��������
        /// </summary>
        public DataSet ArticleList(string channel_name, int Top, string strWhere, string filedOrder)
        {
            Dictionary<int, string> dic = new BLL.site_channel().GetListAll();
            if (!dic.ContainsValue(channel_name))
            {
                return new DataSet();
            }
            return dal.ArticleList(channel_name, Top, strWhere, filedOrder);
        }

        /// <summary>
        /// ����Ƶ��������ʾǰ��������
        /// </summary>
        public DataSet ArticleList(string channel_name, int category_id, int Top, string strWhere, string filedOrder)
        {
            Dictionary<int, string> dic = new BLL.site_channel().GetListAll();
            if (!dic.ContainsValue(channel_name))
            {
                return new DataSet();
            }
            return dal.ArticleList(channel_name, category_id, Top, strWhere, filedOrder);
        }

        /// <summary>
        /// ����Ƶ�����ƻ�ò�ѯ��ҳ����
        /// </summary>
        public DataSet ArticleList(string channel_name, int category_id, int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            Dictionary<int, string> dic = new BLL.site_channel().GetListAll();
            if (!dic.ContainsValue(channel_name))
            {
                recordCount = 0;
                return new DataSet();
            }
            return dal.ArticleList(channel_name, category_id, pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        /// <summary>
        /// ����Ƶ�����Ƽ�����ѯ��ҳ����
        /// </summary>
        public DataSet ArticleList(string channel_name, int category_id, Dictionary<string, string> dicSpecIds, int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.ArticleList(channel_name, category_id, dicSpecIds, pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }
        
        /// <summary>
        /// ��ùؽ��ֲ�ѯ��ҳ����(�����õ�)
        /// </summary>
        public DataSet ArticleSearch(int site_id, int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.ArticleSearch(site_id, pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        /// <summary>
        /// ���Tags��ѯ��ҳ����(�����õ�)
        /// </summary>
        public DataSet ArticleSearch(int site_id, string tags, int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.ArticleSearch(site_id, tags, pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }
        #endregion
    }
}