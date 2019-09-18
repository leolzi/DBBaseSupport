using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DBBase
{
    public class FileSupport
    {
        public static FileSupport Instance = new FileSupport();

        static string mRoot = "";
        public static string Root
        {
            get
            {
                try
                {
                    if(!string.IsNullOrEmpty(mRoot))
                    {
                        return mRoot;
                    }
                    if (System.Web.HttpRuntime.BinDirectory != null)
                    {
                        mRoot= HttpRuntime.BinDirectory + "/log";
                        return mRoot;
                    }
                        
                    else
                    {
                        mRoot = System.Environment.CurrentDirectory + "/log";
                        return mRoot;
                    }
                }
                catch
                {
                    mRoot= System.Environment.CurrentDirectory + "/log";
                    return mRoot;
                }
            }
        }//= HttpRuntime.BinDirectory + "/log";



        //string mPath = HttpRuntime.BinDirectory + "/log/" + System.DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
        string mPath
        {
            get
            {
               return  mRoot + "/" + System.DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            }
        }

        public bool FileExit()
        {
            if (!Directory.Exists(mRoot))//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(mRoot);
            }
            if (!File.Exists(mPath))
            {
               return false;
            }
            return true;
        }

        public void Write(string msg)
        {
            try
            {
                System.Diagnostics.Debug.Write(msg);

                FileStream fs;
                if (!FileExit())
                {
                    fs = File.Create(mPath);//创建该文件
                }
                else
                {
                    fs = new FileStream(mPath, FileMode.Append);
                }

                StreamWriter sw = new StreamWriter(fs);
                //开始写入
                var addstr = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "    " + msg + "\r\n";
                sw.Write(addstr);
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
                fs.Close();
            }
            catch
            {

            }
        }

    }
}
