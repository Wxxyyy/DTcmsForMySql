using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.DAL
{
    /// <summary>
    /// ���ݷ�����:��Ա��۸�
    /// </summary>
    public partial class user_group_price
    {
        private string databaseprefix;//���ݿ����ǰ׺
        public user_group_price(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        #region ��������================================
        /// <summary>
        /// ����һ�����ݣ�������
        /// </summary>
        public bool Add(SqlConnection conn, SqlTransaction trans, Model.user_group_price model, int channel_id, int article_id)
        {
            StringBuilder strSql = new StringBuilder(); //SQL�ַ���
            StringBuilder str1 = new StringBuilder(); //���ݿ��ֶ�
            StringBuilder str2 = new StringBuilder(); //��������
            PropertyInfo[] pros = model.GetType().GetProperties();
            List<SqlParameter> paras = new List<SqlParameter>();

            strSql.Append("insert into " + databaseprefix + "user_group_price(");
            foreach (PropertyInfo pi in pros)
            {
                if (!pi.Name.Equals("id"))
                {
                    if (pi.GetValue(model, null) != null)
                    {
                        str1.Append(pi.Name + ",");
                        str2.Append("@" + pi.Name + ",");
                        switch (pi.Name)
                        {
                            case "channel_id":
                                paras.Add(new SqlParameter("@" + pi.Name, channel_id));
                                break;
                            case "article_id":
                                paras.Add(new SqlParameter("@" + pi.Name, article_id));
                                break;
                            default:
                                paras.Add(new SqlParameter("@" + pi.Name, pi.GetValue(model, null)));
                                break;
                        }
                    }
                }
            }
            strSql.Append(str1.ToString().Trim(','));
            strSql.Append(") values (");
            strSql.Append(str2.ToString().Trim(','));
            strSql.Append(")");
            return DbHelperSQL.ExecuteSql(conn, trans, strSql.ToString(), paras.ToArray()) > 0;
        }

        /// <summary>
        /// ɾ��һ�����ݣ�������
        /// </summary>
        public void Delete(SqlConnection conn, SqlTransaction trans, int channel_id, int article_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "user_group_price");
            strSql.Append(" where channel_id=@channel_id and article_id=@article_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@channel_id", SqlDbType.Int,4),
					new SqlParameter("@article_id", SqlDbType.Int,4)};
            parameters[0].Value = channel_id;
            parameters[1].Value = article_id;
            DbHelperSQL.ExecuteSql(conn, trans, strSql.ToString(), parameters);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public Model.user_group_price GetModel(int group_id, int channel_id, int article_id)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder str1 = new StringBuilder();
            Model.user_group_price model = new Model.user_group_price();
            //���÷��������Ե����й�������
            PropertyInfo[] pros = model.GetType().GetProperties();
            foreach (PropertyInfo p in pros)
            {
                str1.Append(p.Name + ",");//ƴ���ֶ�
            }
            strSql.Append("select top 1 " + str1.ToString().Trim(','));
            strSql.Append(" from " + databaseprefix + "user_group_price");
            strSql.Append(" where group_id=@group_id and channel_id=@channel_id and article_id=@article_id");
            SqlParameter[] parameters = {
					new SqlParameter("@group_id", SqlDbType.Int,4),
                    new SqlParameter("@channel_id", SqlDbType.Int,4),
                    new SqlParameter("@article_id", SqlDbType.Int,4)};
            parameters[0].Value = group_id;
            parameters[1].Value = channel_id;
            parameters[2].Value = article_id;
            DataTable dt = DbHelperSQL.Query(strSql.ToString(), parameters).Tables[0];

            if (dt.Rows.Count > 0)
            {
                return DataRowToModel(dt.Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public List<Model.user_group_price> GetList(int channel_id, int article_id)
        {
            List<Model.user_group_price> modelList = new List<Model.user_group_price>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM " + databaseprefix + "user_group_price ");
            strSql.Append(" where channel_id=" + channel_id + " and article_id=" + article_id);
            DataTable dt = DbHelperSQL.Query(strSql.ToString()).Tables[0];

            if (dt.Rows.Count > 0)
            {
                for (int n = 0; n < dt.Rows.Count; n++)
                {
                    modelList.Add(DataRowToModel(dt.Rows[n]));
                }
            }
            return modelList;
        }
        #endregion

        #region ��չ����================================
        /// <summary>
        /// ������ת��ʵ��
        /// </summary>
        public Model.user_group_price DataRowToModel(DataRow row)
        {
            Model.user_group_price model = new Model.user_group_price();
            if (row != null)
            {
                //���÷��������Ե����й�������
                Type modelType = model.GetType();
                for (int i = 0; i < row.Table.Columns.Count; i++)
                {
                    //����ʵ���Ƿ�����б���ͬ�Ĺ�������
                    PropertyInfo proInfo = modelType.GetProperty(row.Table.Columns[i].ColumnName);
                    if (proInfo != null && row[i] != DBNull.Value)
                    {
                        proInfo.SetValue(model, row[i], null);//������ֵ��������ֵ
                    }
                }
            }
            return model;
        }
        #endregion
    }
}