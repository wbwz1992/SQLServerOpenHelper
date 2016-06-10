using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace SQLServerOpenHelper
{
    /// <summary>
    /// WebService1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        DBOparition dbOparition = new DBOparition();

        [WebMethod(Description = "获取登录信息")]
        public string[] userLogin(string mId, string mPassword)
        {
            return dbOparition.userLogin(mId, mPassword).ToArray();
        }

        [WebMethod(Description = "获取学生信息")]
        public string[] selectStudentInfo(string mId, string mName, string mSex, string mDepartment, string mMajor, string mClass)
        {
            return dbOparition.selectStudentInfo(mId, mName, mSex, mDepartment, mMajor, mClass).ToArray();
        }

        [WebMethod(Description = "插入学生信息")]
        public bool insertStudentInfo(string mId, string mName, string mSex, string mDepartment, string mMajor, string mClass, string mPosition, string mClassId, int tag)
        {
            return dbOparition.insertStudentInfo(mId, mName, mSex, mDepartment, mMajor, mClass, mPosition, mClassId, tag);
        }
        [WebMethod(Description = "删除学生信息")]
        public bool deleteStudentInfo(string mId)
        {
            return dbOparition.deleteStudentInfo(mId);
        }

        [WebMethod(Description = "获取班级院系专业信息")]
        public string[] selectClassInfo(string value, int tag)
        {
            return dbOparition.selectClassInfo(value, tag).ToArray();
        }

        [WebMethod(Description = "获取班级信息")]
        public string[] selectClassList(string mDepartment, string mMajor, string mClass)
        {
            return dbOparition.selectClassList(mDepartment, mMajor, mClass).ToArray();
        }

        [WebMethod(Description = "插入班级信息")]
        public bool insertClassInfo(string mOldId, string mId, string mDepartment, string mMajor, string mClass, string mCount, int tag)
        {
            return dbOparition.insertClassInfo(mOldId,mId, mDepartment, mMajor, mClass, mCount, tag);
        }
        [WebMethod(Description = "删除班级信息")]
        public bool deleteClassInfo(string mId)
        {
            return dbOparition.deleteClassInfo(mId);
        }
        [WebMethod(Description = "获取课程信息")]
        public string[] selectLessonInfo(string mTeacher, string mName, int mWeekday)
        {
            return dbOparition.selectLessonInfo(mTeacher, mName, mWeekday).ToArray();
        }
        [WebMethod(Description = "插入课程信息")]
        public bool insertLessonInfo(string oldId, string mId, string mName, string mWeekday, string mDaytime, string mStart, string mFinish, string mTeacher, string mClassId, string mClassroom, int tag)
        {
            return dbOparition.insertLessonInfo(oldId, mId, mName, int.Parse(mWeekday), int.Parse(mDaytime), int.Parse(mStart), int.Parse(mFinish), mTeacher, mClassId, mClassroom, tag);
        }
        [WebMethod(Description = "删除课程信息")]
        public bool deleteLessonInfo(string mId, string mWeekday, string mTeacher)
        {
            return dbOparition.deleteLessonInfo(mId, int.Parse(mWeekday), mTeacher);
        }
        [WebMethod(Description = "获取成绩信息")]
        public string[] selectGradeInfo(string mLessonId, string mClassId)
        {
            return dbOparition.selectGradeInfo(mLessonId, mClassId).ToArray();
        }
    }
}
