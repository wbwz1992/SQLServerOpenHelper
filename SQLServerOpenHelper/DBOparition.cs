using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;

namespace SQLServerOpenHelper
{
    public class DBOparition
    {
        public static SqlConnection sqlCon;

        private string ConSeverStr = @"Data Source=DESKTOP-NOTL3M1;Initial Catalog=Management;Integrated Security=True";

        public DBOparition()
        {
            if (sqlCon == null)
            {
                sqlCon = new SqlConnection();
                sqlCon.ConnectionString = ConSeverStr;
                sqlCon.Open();
            }
        }

        public void Dispose()
        {
            if (sqlCon != null)
            {
                sqlCon.Close();
                sqlCon = null;
            }
        }

        public List<string> userLogin(string mId, string mPassword)
        {
            string sql = "SELECT * FROM Account WHERE _id='" + mId + "'";
            List<string> list = new List<string>();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        list.Add(reader[0].ToString());
                        list.Add(reader[1].ToString());
                        list.Add(reader[2].ToString());
                        list.Add(reader[3].ToString());
                    }
                    if (mPassword != list[2])
                    {
                        list.Clear();
                        list.Add("false");
                    }
                }
                else
                {
                    list.Add("false");
                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {
                list.Clear();
                list.Add("false");
            }
            return list;

        }

        public List<String> selectStudentInfo(string mId, string mName, string mSex, string mDepartment, string mMajor, string mClass)
        {
            string sql1 = "SELECT * FROM Student WHERE _id='" + mId + "'";
            string sql2 = "SELECT * FROM Student WHERE name LIKE '%" + mName + "%'" +
                    "AND department LIKE '%" + mDepartment + "%'" +
                    "AND major LIKE '%" + mMajor + "%'" +
                    "AND class LIKE '%" + mClass + "%'" +
                    "AND sex LIKE '%" + mSex + "%'";
            string sql;
            if (mId != "")
            {
                sql = sql1;
            }
            else
            {
                sql = sql2;
            }
            List<string> list = new List<string>();

            try
            {
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());
                    list.Add(reader[2].ToString());
                    list.Add(reader[3].ToString());
                    list.Add(reader[4].ToString());
                    list.Add(reader[5].ToString());
                    list.Add(reader[6].ToString());
                    list.Add(reader[7].ToString());
                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }
            return list;
        }

        public bool insertStudentInfo(string mId, string mName, string mSex, string mDepartment, string mMajor, string mClass, string mPosition, string mClassId, int tag)
        {
            try
            {
                string sql = "";
                if (tag == 0)
                {
                    sql = "INSERT INTO Student (_id,name,sex,department,major,class,position,class_id) " +
                      "VALUES ('" + mId + "','" + mName + "','" + mSex + "','" + mDepartment + "','" + mMajor + "','" + mClass + "','" + mPosition + "','" + mClassId + "')";
                }
                else
                {
                    sql = "UPDATE Student SET name='" 
                        + mName + "',sex='" + mSex + "',department='" + mDepartment + "',major='" + mMajor + "',class='" 
                        + mClass + "',position='" + mPosition + "',class_id = '" + mClassId + "' WHERE _id='" + mId + "'";
                }
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool deleteStudentInfo(string mId)
        {
            try
            {
                string sql = "DELETE FROM Student WHERE _id='" + mId + "'";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<String> selectClassInfo(string value, int tag)
        {
            string sql1 = "SELECT DISTINCT department FROM Class";
            string sql2 = "SELECT DISTINCT major FROM Class WHERE department='" + value + "'";
            string sql3 = "SELECT _id,class FROM Class WHERE major='" + value + "' ";
            string[] sql = { sql1, sql2, sql3 };
            List<string> list = new List<string>();

            try
            {
                SqlCommand cmd = new SqlCommand(sql[tag], sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                if (tag != 2)
                {
                    while (reader.Read())
                    {
                        list.Add(reader[0].ToString());
                        //list.Add(reader[1].ToString());
                        //list.Add(reader[2].ToString());
                        //list.Add(reader[3].ToString());
                        //list.Add(reader[4].ToString());
                    }
                }
                else
                {
                    while (reader.Read())
                    {
                        list.Add(reader[0].ToString());
                        list.Add(reader[1].ToString());
                        //list.Add(reader[2].ToString());
                        //list.Add(reader[3].ToString());
                        //list.Add(reader[4].ToString());
                    }
                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }
            return list;
        }

        public List<String> selectClassList(string mDepartment, string mMajor, string mClass)
        {
           
            List<string> list = new List<string>();
            try
            {
                string sql = "SELECT * FROM Class WHERE department LIKE'%" + mDepartment + "%' AND major LIKE'%" + mMajor + "%' AND class LIKE'%" + mClass + "%'";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());
                    list.Add(reader[2].ToString());
                    list.Add(reader[3].ToString());
                    list.Add(reader[4].ToString());
                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }
            return list;
        }

        public bool insertClassInfo(string oldId, string Id, string Department, string Major, string Class, string Count, int tag)
        {
            try
            {
                string sql = "";
                if (tag == 0)
                {
                    sql = "INSERT INTO Class (_id,department,major,class,count) VALUES ('" + Id + "','" + Department + "','" + Major + "','" + Class + "','" + Count + "')";
                }
                else
                {
                    sql = "UPDATE Class SET _id='" 
                        + Id + "',department='" + Department + "',major='" + Major + "',class='" + Class + "',count='" + Count + "'WHERE _id='" + oldId + "'";
                }
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool deleteClassInfo(string Id)
        {
            try
            {
                string sql = "DELETE FROM Class WHERE _id='" + Id + "' DELETE FROM Student WHERE class_id='" + Id + "'";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<String> selectLessonInfo(string mTeacher, string mName, int mWeekday)
        {
            List<string> list = new List<string>();
            string sql = "";
            if (mWeekday == 8)
            {
                sql = "SELECT DISTINCT Lesson._id,Lesson.name,Lesson.class_id,Class.department,Class.major,Class.class "
                    + "FROM lesson,Class where Lesson.teacher='" + mTeacher + "' AND Lesson.class_id=Class._id";
                try
                {
                    SqlCommand cmd = new SqlCommand(sql, sqlCon);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(reader[0].ToString());
                        list.Add(reader[1].ToString());
                        list.Add(reader[2].ToString());
                        list.Add(reader[3].ToString());
                        list.Add(reader[4].ToString());
                        list.Add(reader[5].ToString());
                    }
                    reader.Close();
                    cmd.Dispose();
                }
                catch (Exception)
                {

                }
            }
            else
            {
                int count = mTeacher.Length;
                if (count == 12)
                {
                    sql = "SELECT * FROM Lesson WHERE teacher='" + mTeacher + "'"
                    + "AND name LIKE'%" + mName + "%'";
                }
                else
                {
                    sql = "SELECT * FROM Lesson WHERE teacher IN (SELECT _id FROM Account WHERE name Like'%"
                        + mTeacher + "%')" + "AND name LIKE'%" + mName + "%'";
                }
                if (mWeekday != 0)
                {
                    sql = sql + "AND weekday=" + mWeekday + "";
                }
                try
                {
                    SqlCommand cmd = new SqlCommand(sql, sqlCon);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(reader[0].ToString());
                        list.Add(reader[1].ToString());
                        list.Add(reader[2].ToString());
                        list.Add(reader[3].ToString());
                        list.Add(reader[4].ToString());
                        list.Add(reader[5].ToString());
                        list.Add(reader[6].ToString());
                        list.Add(reader[7].ToString());
                        list.Add(reader[8].ToString());
                    }
                    reader.Close();
                    cmd.Dispose();
                }
                catch (Exception)
                {

                }
            }
            return list;
        }
        public bool insertLessonInfo(string oldId, string mId, string mName, int mWeekday, int mDaytime, int mStart, int mFinish, string mTeacher, string mClassId, string mClassroom, int tag)
        {
            try
            {
                string sql = "";
                if (tag == 0)
                {
                    sql = "INSERT INTO Lesson (_id,name,weekday,daytime,start,finish,teacher,class_id,classroom) VALUES ('" 
                        + mId + "','" + mName + "'," + mWeekday + "," + mDaytime + "," + mStart + "," + mFinish + ",'" + mTeacher + "','" + mClassId + "','" + mClassroom + "')";
                }
                else
                {
                    sql = "UPDATE Lesson SET _id='"
                        + mId + "',name='" + mName + "',weekday=" + mWeekday + ",daytime=" + mDaytime + ",start=" + mStart + ",finish=" + mFinish + ",teacher='" + mTeacher + "',class_id='" + mClassId + "',classroom='" + mClassroom + "'WHERE _id='" + oldId + "'";
                }
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool deleteLessonInfo(string Id, int mWeekday, string mTeacher)
        {
            try
            {
                string sql = "DELETE FROM Lesson WHERE _id='" + Id + "' AND weekday=" + mWeekday + " And teacher='" + mTeacher + "'";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<String> selectGradeInfo(string mLessonId, string mClassId)
        {

            List<string> list = new List<string>();
            try
            {
                string sql = "SELECT Grade.score,Student._id,Student.name FROM Student,Grade "
                    + "WHERE Grade.student_id = Student._id AND Grade.lesson_id = '" + mLessonId + "' AND Student.class_id = '" + mClassId + "'";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());
                    list.Add(reader[2].ToString());
                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }
            return list;
        }
    }
}