using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using DTcms.DBUtility;
using DTcms.Common;
using MySql.Data.MySqlClient;

namespace DTcms.DAL.MySql
{
    /// <summary>
    /// 数据访问类:系统导航菜单
    /// </summary>
    public partial class navigation
    {
        private string databaseprefix;//数据库表名前缀
        public navigation(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        #region 基本方法================================
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from  " + databaseprefix + "navigation");
            strSql.Append(" where id=@id  ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = id;

            return DbHelperMySql.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.navigation model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder str1 = new StringBuilder();//数据字段
            StringBuilder str2 = new StringBuilder();//数据参数
            //利用反射获得属性的所有公共属性
            PropertyInfo[] pros = model.GetType().GetProperties();
            List<MySqlParameter> paras = new List<MySqlParameter>();
            strSql.Append("insert into  " + databaseprefix + "navigation(");
            foreach (PropertyInfo pi in pros)
            {
                //如果不是主键则追加sql字符串
                if (!pi.Name.Equals("id"))
                {
                    //判断属性值是否为空
                    if (pi.GetValue(model, null) != null)
                    {
                        str1.Append(pi.Name + ",");//拼接字段
                        str2.Append("@" + pi.Name + ",");//声明参数
                        paras.Add(new MySqlParameter("@" + pi.Name, pi.GetValue(model, null)));//对参数赋值
                    }
                }
            }
            strSql.Append(str1.ToString().Trim(','));
            strSql.Append(") values (");
            strSql.Append(str2.ToString().Trim(','));
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY;");
            object obj = DbHelperMySql.GetSingle(strSql.ToString(), paras.ToArray());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.navigation model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder str1 = new StringBuilder();
            //利用反射获得属性的所有公共属性
            PropertyInfo[] pros = model.GetType().GetProperties();
            List<MySqlParameter> paras = new List<MySqlParameter>();
            strSql.Append("update  " + databaseprefix + "navigation set ");
            foreach (PropertyInfo pi in pros)
            {
                //如果不是主键则追加sql字符串
                if (!pi.Name.Equals("id"))
                {
                    //判断属性值是否为空
                    if (pi.GetValue(model, null) != null)
                    {
                        str1.Append(pi.Name + "=@" + pi.Name + ",");//声明参数
                        paras.Add(new MySqlParameter("@" + pi.Name, pi.GetValue(model, null)));//对参数赋值
                    }
                }
            }
            strSql.Append(str1.ToString().Trim(','));
            strSql.Append(" where id=@id ");
            paras.Add(new MySqlParameter("@id", model.id));
            return DbHelperMySql.ExecuteSql(strSql.ToString(), paras.ToArray()) > 0;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "navigation ");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = id;

            return DbHelperMySql.ExecuteSql(strSql.ToString(), parameters) > 0;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.navigation GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder str1 = new StringBuilder();
            Model.navigation model = new Model.navigation();
            //利用反射获得属性的所有公共属性
            PropertyInfo[] pros = model.GetType().GetProperties();
            foreach (PropertyInfo p in pros)
            {
                str1.Append(p.Name + ",");//拼接字段
            }
            strSql.Append("select top 1 " + str1.ToString().Trim(','));
            strSql.Append(" from " + databaseprefix + "navigation");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = id;
            DataTable dt = DbHelperMySql.Query(strSql.ToString(), parameters).Tables[0];

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
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM  " + databaseprefix + "navigation ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperMySql.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM " + databaseprefix + "navigation");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperMySql.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperMySql.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

        #endregion

        #region 扩展方法================================
        /// <summary>
        /// 查询是否存在该记录
        /// </summary>
        public bool Exists(string nav_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "navigation");
            strSql.Append(" where name=@nav_name ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@nav_name", MySqlDbType.VarChar,50)};
            parameters[0].Value = nav_name;

            return DbHelperMySql.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.navigation GetModel(string nav_name)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder str1 = new StringBuilder();
            Model.navigation model = new Model.navigation();
            //利用反射获得属性的所有公共属性
            PropertyInfo[] pros = model.GetType().GetProperties();
            foreach (PropertyInfo p in pros)
            {
                str1.Append(p.Name + ",");//拼接字段
            }
            strSql.Append("select top 1 " + str1.ToString().Trim(','));
            strSql.Append(" from  " + databaseprefix + "navigation ");
            strSql.Append(" where name=@nav_name");
            MySqlParameter[] parameters = {
					new MySqlParameter("@nav_name", MySqlDbType.VarBinary,50)};
            parameters[0].Value = nav_name;
            DataTable dt = DbHelperMySql.Query(strSql.ToString(), parameters).Tables[0];

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
        /// 得到一个对象实体，带事务
        /// </summary>
        public Model.navigation GetModel(MySqlConnection conn, MySqlTransaction trans, string nav_name)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder str1 = new StringBuilder();
            Model.navigation model = new Model.navigation();
            //利用反射获得属性的所有公共属性
            PropertyInfo[] pros = model.GetType().GetProperties();
            foreach (PropertyInfo p in pros)
            {
                str1.Append(p.Name + ",");//拼接字段
            }
            strSql.Append("select top 1 " + str1.ToString().Trim(','));
            strSql.Append(" from " + databaseprefix + "navigation");
            strSql.Append(" where name=@nav_name");
            MySqlParameter[] parameters = {
					new MySqlParameter("@nav_name", MySqlDbType.VarChar,50)};
            parameters[0].Value = nav_name;
            DataTable dt = DbHelperMySql.Query(conn, trans, strSql.ToString(), parameters).Tables[0];

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
        /// 根据导航的名称查询其ID
        /// </summary>
        public int GetNavId(string nav_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id from " + databaseprefix + "navigation");
            strSql.Append(" where name=@nav_name");
            MySqlParameter[] parameters = {
					new MySqlParameter("@nav_name", MySqlDbType.VarChar,50)};
            parameters[0].Value = nav_name;
            string str = Convert.ToString(DbHelperMySql.GetSingle(strSql.ToString(), parameters));
            return Utils.StrToInt(str, 0);
        }

        /// <summary>
        /// 获取父类下的所有子类ID(含自己本身)
        /// </summary>
        public string GetIds(int parent_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,parent_id from " + databaseprefix + "navigation");
            DataSet ds = DbHelperMySql.Query(strSql.ToString());
            string ids = parent_id.ToString() + ",";
            GetChildIds(ds.Tables[0], parent_id, ref ids);
            return ids.TrimEnd(',');
        }

        /// <summary>
        /// 获取父类下的所有子类ID(含自己本身)
        /// </summary>
        public string GetIds(string nav_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id from " + databaseprefix + "navigation");
            strSql.Append(" where name=@nav_name");
            MySqlParameter[] parameters = {
					new MySqlParameter("@nav_name", MySqlDbType.VarChar,50)};
            parameters[0].Value = nav_name;
            object obj = DbHelperMySql.GetSingle(strSql.ToString(), parameters);
            if (obj != null)
            {
                return GetIds(Convert.ToInt32(obj));
            }
            return string.Empty;
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "navigation set " + strValue);
            strSql.Append(" where id=" + id);
            return DbHelperMySql.ExecuteSql(strSql.ToString()) > 0;
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(string name, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "navigation set " + strValue);
            strSql.Append(" where name='" + name + "'");
            return DbHelperMySql.ExecuteSql(strSql.ToString()) > 0;
        }

        /// <summary>
        /// 修改一条记录，带事务
        /// </summary>
        public bool Update(MySqlConnection conn, MySqlTransaction trans, string old_name, string new_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "navigation set name=@new_name");
            strSql.Append(" where name=@old_name");
            MySqlParameter[] parameters = {
					new MySqlParameter("@new_name", MySqlDbType.VarChar,50),
					new MySqlParameter("@old_name", MySqlDbType.VarChar,50)};
            parameters[0].Value = new_name;
            parameters[1].Value = old_name;
            return DbHelperMySql.ExecuteSql(conn, trans, strSql.ToString(), parameters) > 0;
        }

        /// <summary>
        /// 修改一条记录，带事务
        /// </summary>
        public bool Update(MySqlConnection conn, MySqlTransaction trans, string old_name, string new_name, string title, int sort_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "navigation set");
            strSql.Append(" name=@new_name,");
            strSql.Append(" title=@title,");
            strSql.Append(" sort_id=@sort_id");
            strSql.Append(" where name=@old_name");
            MySqlParameter[] parameters = {
					new MySqlParameter("@new_name", MySqlDbType.VarChar,50),
                    new MySqlParameter("@title", MySqlDbType.VarChar,100),
                    new MySqlParameter("@sort_id", MySqlDbType.Int32,4),
                    new MySqlParameter("@old_name", MySqlDbType.VarChar,50)};
            parameters[0].Value = new_name;
            parameters[1].Value = title;
            parameters[2].Value = sort_id;
            parameters[3].Value = old_name;
            return DbHelperMySql.ExecuteSql(conn, trans, strSql.ToString(), parameters) > 0;
        }

        /// <summary>
        /// 修改一条记录，带事务
        /// </summary>
        public bool Update(MySqlConnection conn, MySqlTransaction trans, string old_name, int parent_id, string nav_name, string title, int sort_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "navigation set");
            strSql.Append(" parent_id=@parent_id,");
            strSql.Append(" name=@name,");
            strSql.Append(" title=@title,");
            strSql.Append(" sort_id=@sort_id");
            strSql.Append(" where name=@old_name");
            MySqlParameter[] parameters = {
					new MySqlParameter("@parent_id", MySqlDbType.Int32,4),
					new MySqlParameter("@name", MySqlDbType.VarChar,50),
                    new MySqlParameter("@title", MySqlDbType.VarChar,100),
                    new MySqlParameter("@sort_id", MySqlDbType.Int32,4),
                    new MySqlParameter("@old_name", MySqlDbType.VarChar,50)};
            parameters[0].Value = parent_id;
            parameters[1].Value = nav_name;
            parameters[2].Value = title;
            parameters[3].Value = sort_id;
            parameters[4].Value = old_name;
            return DbHelperMySql.ExecuteSql(conn, trans, strSql.ToString(), parameters) > 0;
        }

        /// <summary>
        /// 快捷添加系统默认导航
        /// </summary>
        public int Add(string parent_name, string nav_name, string title, string link_url, int sort_id, int channel_id, string action_type)
        {
            //先根据名称查询该父ID
            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("select top 1 id from " + databaseprefix + "navigation");
            strSql1.Append(" where name=@parent_name");
            MySqlParameter[] parameters1 = {
					new MySqlParameter("@parent_name", MySqlDbType.VarChar,50)};
            parameters1[0].Value = parent_name;
            object obj1 = DbHelperMySql.GetSingle(strSql1.ToString(), parameters1);
            if (obj1 == null)
            {
                return 0;
            }
            int parent_id = Convert.ToInt32(obj1);

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "navigation(");
            strSql.Append("parent_id,channel_id,nav_type,name,title,link_url,sort_id,action_type,is_lock,is_sys)");
            strSql.Append(" values (");
            strSql.Append("@parent_id,@channel_id,@nav_type,@name,@title,@link_url,@sort_id,@action_type,@is_lock,@is_sys)");
            strSql.Append(";select @@IDENTITY");
            MySqlParameter[] parameters = {
					new MySqlParameter("@parent_id", MySqlDbType.Int32,4),
					new MySqlParameter("@channel_id", MySqlDbType.Int32,4),
					new MySqlParameter("@nav_type", MySqlDbType.VarChar,50),
					new MySqlParameter("@name", MySqlDbType.VarChar,50),
					new MySqlParameter("@title", MySqlDbType.VarChar,100),
					new MySqlParameter("@link_url", MySqlDbType.VarChar,255),
					new MySqlParameter("@sort_id", MySqlDbType.Int32,4),
					new MySqlParameter("@action_type", MySqlDbType.VarChar,500),
                    new MySqlParameter("@is_lock", MySqlDbType.TinyText,1),
					new MySqlParameter("@is_sys", MySqlDbType.TinyText,1)};
            parameters[0].Value = parent_id;
            parameters[1].Value = channel_id;
            parameters[2].Value = DTEnums.NavigationEnum.System.ToString();
            parameters[3].Value = nav_name;
            parameters[4].Value = title;
            parameters[5].Value = link_url;
            parameters[6].Value = sort_id;
            parameters[7].Value = action_type;
            parameters[8].Value = 0;
            parameters[9].Value = 1;
            object obj2 = DbHelperMySql.GetSingle(strSql.ToString(), parameters);
            return Convert.ToInt32(obj2);
        }

        /// <summary>
        /// 快捷添加系统默认导航，带事务
        /// </summary>
        public int Add(MySqlConnection conn, MySqlTransaction trans, string parent_name, string nav_name, string title, string link_url, int sort_id, int channel_id, string action_type)
        {
            //先根据名称查询该父ID
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id from " + databaseprefix + "navigation");
            strSql.Append(" where name=@parent_name");
            MySqlParameter[] parameters = {
					new MySqlParameter("@parent_name", MySqlDbType.VarChar,50)};
            parameters[0].Value = parent_name;
            object obj = DbHelperMySql.GetSingle(conn, trans, strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            string parent_id = obj.ToString();

            return Add(conn, trans, parent_id, nav_name, title, link_url, sort_id, channel_id, action_type);
        }

        /// <summary>
        /// 快捷添加系统默认导航，带事务
        /// </summary>
        public int Add(MySqlConnection conn, MySqlTransaction trans, int parent_id, string nav_name, string title, string link_url, int sort_id, int channel_id, string action_type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "navigation(");
            strSql.Append("parent_id,channel_id,nav_type,name,title,link_url,sort_id,action_type,is_lock,is_sys)");
            strSql.Append(" values (");
            strSql.Append("@parent_id,@channel_id,@nav_type,@name,@title,@link_url,@sort_id,@action_type,@is_lock,@is_sys)");
            strSql.Append(";select @@IDENTITY");
            MySqlParameter[] parameters = {
					new MySqlParameter("@parent_id", MySqlDbType.Int32,4),
					new MySqlParameter("@channel_id", MySqlDbType.Int32,4),
					new MySqlParameter("@nav_type", MySqlDbType.VarChar,50),
					new MySqlParameter("@name", MySqlDbType.VarChar,50),
					new MySqlParameter("@title", MySqlDbType.VarChar,100),
					new MySqlParameter("@link_url", MySqlDbType.VarChar,255),
					new MySqlParameter("@sort_id", MySqlDbType.Int32,4),
					new MySqlParameter("@action_type", MySqlDbType.VarChar,500),
                    new MySqlParameter("@is_lock", MySqlDbType.TinyText,1),
					new MySqlParameter("@is_sys", MySqlDbType.TinyText,1)};
            parameters[0].Value = parent_id;
            parameters[1].Value = channel_id;
            parameters[2].Value = DTEnums.NavigationEnum.System.ToString();
            parameters[3].Value = nav_name;
            parameters[4].Value = title;
            parameters[5].Value = link_url;
            parameters[6].Value = sort_id;
            parameters[7].Value = action_type;
            parameters[8].Value = 0;
            parameters[9].Value = 1;
            object obj = DbHelperMySql.GetSingle(conn, trans, strSql.ToString(), parameters);
            return Convert.ToInt32(obj);
        }

        /// <summary>
        /// 删除一条数据，带事务
        /// </summary>
        public bool Delete(MySqlConnection conn, MySqlTransaction trans, string nav_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "navigation");
            strSql.Append(" where name=@name");
            MySqlParameter[] parameters = {
					new MySqlParameter("@name", MySqlDbType.VarChar,50)};
            parameters[0].Value = nav_name;

            return DbHelperMySql.ExecuteSql(conn, trans, strSql.ToString(), parameters) > 0;
        }

        /// <summary>
        /// 根据父节点ID获取导航列表
        /// </summary>
        public DataTable GetList(int parent_id, string nav_type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM " + databaseprefix + "navigation");
            strSql.Append(" where nav_type='" + nav_type + "'");
            strSql.Append(" order by sort_id asc,id desc");
            DataSet ds = DbHelperMySql.Query(strSql.ToString());
            //重组列表
            DataTable oldData = ds.Tables[0] as DataTable;
            if (oldData == null)
            {
                return null;
            }
            //创建一个新的DataTable增加一个深度字段
            DataTable newData = oldData.Clone();
            newData.Columns.Add("class_layer", typeof(int));
            //调用迭代组合成DAGATABLE
            GetChilds(oldData, newData, parent_id, 0);
            return newData;
        }

        /// <summary>
        /// 将对象转换实体
        /// </summary>
        public Model.navigation DataRowToModel(DataRow row)
        {
            Model.navigation model = new Model.navigation();
            if (row != null)
            {
                //利用反射获得属性的所有公共属性
                Type modelType = model.GetType();
                for (int i = 0; i < row.Table.Columns.Count; i++)
                {
                    PropertyInfo proInfo = modelType.GetProperty(row.Table.Columns[i].ColumnName);
                    if (proInfo != null && row[i] != DBNull.Value)
                    {
                        proInfo.SetValue(model, row[i], null);//用索引值设置属性值
                    }
                }
            }
            return model;
        }
        #endregion

        #region 私有方法================================
        /// <summary>
        /// 从内存中取得所有下级类别列表（自身迭代）
        /// </summary>
        private void GetChilds(DataTable oldData, DataTable newData, int parent_id, int class_layer)
        {
            class_layer++;
            DataRow[] dr = oldData.Select("parent_id=" + parent_id);
            for (int i = 0; i < dr.Length; i++)
            {
                DataRow row = newData.NewRow();//创建新行
                //循环查找列数量赋值
                for (int j = 0; j < dr[i].Table.Columns.Count; j++)
                {
                    row[dr[i].Table.Columns[j].ColumnName] = dr[i][dr[i].Table.Columns[j].ColumnName];
                }
                row["class_layer"] = class_layer;//赋值深度字段
                newData.Rows.Add(row);//添加新行
                //调用自身迭代
                this.GetChilds(oldData, newData, int.Parse(dr[i]["id"].ToString()), class_layer);
            }
        }

        /// <summary>
        /// 获取父类下的所有子类ID
        /// </summary>
        private void GetChildIds(DataTable dt, int parent_id, ref string ids)
        {
            DataRow[] dr = dt.Select("parent_id=" + parent_id);
            for (int i = 0; i < dr.Length; i++)
            {
                ids += dr[i]["id"].ToString() + ",";
                //调用自身迭代
                this.GetChildIds(dt, int.Parse(dr[i]["id"].ToString()), ref ids);
            }
        }
        #endregion
    }
}