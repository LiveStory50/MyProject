using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

//数据访问层
namespace MyProject
{
    //封装访问数据库的通用方法
   static class SqlHelper
    {
        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString);


        /// <summary>
        /// 打开连接
        /// </summary>
    static void Open()
        {
            //如果当前连接对象为打开则关闭连接
            if (con.State == ConnectionState.Closed)
                con.Open();
        }
        /// <summary>
        /// 关闭连接
        /// </summary>
        static void Close()
        {
            //如果当前连接对象为关闭则打开连接
            if (con.State == ConnectionState.Open)
                con.Close();
        }



        /// <summary>
        /// DataSet数据集
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pa"></param>
        /// <returns></returns>
        public static DataTable GetDatatable(string sql, params object[] pa)
        {
            DataTable set = new DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter(sql, con))
            {
                if (pa.Length > 0 && pa != null)
                {
                    for (int i = 0; i < pa.Length; i++)
                    {
                        SqlParameter sp = new SqlParameter("@" + i, pa[i]);
                        da.SelectCommand.Parameters.Add(sp);
                    }
                }
                Open();
                 da.Fill(set); 
                Close();
                return set;
            }
        }

        //返回DataTable
        public static DataTable ExcuteDataTable(string sql, params SqlParameter[] pars)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter sda = new SqlDataAdapter(sql, con))
            {

                if (pars != null)
                {
                    sda.SelectCommand.Parameters.AddRange(pars);
                }
                sda.Fill(dt);
                return dt;
            }
        }


        /// <summary>
        /// 对数据库进行增删改的操作
        /// </summary>
        /// <param name="sql">要执行的sql语句</param>
        /// <param name="commandType">要执行的查询语句类型，如存储过程或者sql文本命令</param>
        /// <param name="parameters">Transact-SQL语句或者存储过程的参数数组</param>
        /// <returns></returns>
        public static int ExecuteNonQueryCommandType(string sql, CommandType commandType, SqlParameter[] parameters)
        {
            Close();
            SqlCommand cmd = new SqlCommand(sql, con) { CommandType = commandType };
            cmd.Parameters.AddRange(parameters);
            Open();
            var count = cmd.ExecuteNonQuery();
            Close();
            return count;
        }
        /// <summary>
        /// 封装一个返回受影响行数的方法
        /// </summary>
        /// <param name="sql">Sql查询文本</param>
        /// <param name="mess">用户执行的操作</param>
        /// <param name="pa"></param>
        public static int ExcuteNonQuery(string sql, params object[] pa)
        {
            Close();
            int k = 0;
            //using代码片段，用using创建的对象可自动销毁(释放内存)，在小括号中创建
            //SqlCommand cmd=con.CreateCommand()创建并返回一个与SqlConnection关联的SqlCommand对象
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = sql;
                if (pa.Length > 0 && pa != null)
                {
                    for (int i = 0; i < pa.Length; i++)
                    {
                        SqlParameter sp = new SqlParameter("@" + i, pa[i]);
                        cmd.Parameters.Add(sp);
                    }

                }
                Open();//调用封装方法打开连接
                k = cmd.ExecuteNonQuery();
                Close();// 调用封装方法关闭连接
            }
            return k;
        }


        public static int BeginTranExcuteNonQuery(List<string> sqllist)
        {
           
            int result = 0,count=0;
            //using代码片段，用using创建的对象可自动销毁(释放内存)，在小括号中创建
            //SqlCommand cmd=con.CreateCommand()创建并返回一个与SqlConnection关联的SqlCommand对象
            using (SqlCommand cmd=con.CreateCommand())
            {
                Open();//调用封装方法打开连接
                cmd.Transaction = con.BeginTransaction("添加成绩");//开始事务处理
                try
                {
                    //遍历SQL命令列表，每个命令执行一次插入操作，如果遇到一个失败，则终止回滚操作
                    foreach (string sql in sqllist)
                    {
                        cmd.CommandText = sql;//要执行的SQL命令文本
                        result = cmd.ExecuteNonQuery();//执行SQL命令，返回受影响行数
                        if(result<=0)//如果执行失败则回滚
                        {
                            cmd.Transaction.Rollback("添加成绩");//回滚事务
                        }
                        count++;//成功次数
                    }
                    cmd.Transaction.Commit();//如果全部成功，提交事务
                    
                }
                catch
                {
                    result = 0;
                    cmd.Transaction.Rollback();
                }
                finally
                {
                    Close();//调用关闭连接的方法
                }
                return count;
            }
        }
        /// <summary>
        /// 执行查询返回阅读器
        /// </summary>
        /// <param name="sql">Sql查询文本</param>
        /// <param name="pa"></param>
        /// <returns></returns>
        public static SqlDataReader GetReader(string sql,params object[] pa)
        {
            SqlDataReader dr;
            
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = sql;
                if (pa.Length > 0 && pa != null)
                {
                    for (int i = 0; i < pa.Length; i++)
                    {
                        SqlParameter sp = new SqlParameter("@" + i, pa[i]);
                        cmd.Parameters.Add(sp);
                    }

                }
                Open();//调用封装方法打开连接
               dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
               // Close();// 调用封装方法关闭连接
               
            }
           return dr;
            
        }
        /// <summary>
        /// 聚合函数查询，返回结果集中第一行，第一列的值
        /// </summary>
        /// <param name="sql">Sql查询文本</param>
        /// <param name="pa"></param>
        /// <returns></returns>
        public static object ExScalar(string sql, params object[] pa)
        {
            object sca=null;

            using (SqlCommand cmd = con.CreateCommand())            
            {
                cmd.CommandText = sql;
                if (pa.Length > 0 && pa != null)
                {
                    for (int i = 0; i < pa.Length; i++)
                    {
                        SqlParameter sp = new SqlParameter("@" + i, pa[i]);
                        cmd.Parameters.Add(sp);
                    }

                }
                Open();//调用封装方法打开连接
                sca = cmd.ExecuteScalar();
                Close();// 调用封装方法关闭连接
                return sca;
            }
            
        }
        /// <summary>
        /// DataSet数据集
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pa"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(string sql, params object[] pa)
        {
            DataSet set = new DataSet();
            using (SqlDataAdapter da = new SqlDataAdapter(sql, con))
            {
                if (pa.Length > 0 && pa != null)
                {
                    for (int i = 0; i < pa.Length; i++)
                    {
                        SqlParameter sp = new SqlParameter("@" + i, pa[i]);
                        da.SelectCommand.Parameters.Add(sp);
                    }
                }
                Open();
                da.Fill(set);
            }
                return set ;
        }

    }

}


