using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBBase
{

    public class ThreadBase
    {
        public delegate void D_ThreadTick();
        public delegate void D_OnThreadFinish();
        public delegate void D_OnThreadStart();

        public D_ThreadTick ThreadTick { get; set; }
        public D_OnThreadFinish ThreadStart { get; set; }
        public D_OnThreadFinish ThreadFinish { get; set; }

        //public ThreadBase(string name)
        //{
        //    Thread.Name = name;
        //    Thread = new System.Threading.Thread(Run);
        //    Thread.Start();
        //    if(ThreadStart !=null)
        //        ThreadStart();
        //}
        public ThreadBase(string name, D_ThreadTick tickMethod)
        {
            Thread = new System.Threading.Thread(Run);
            Thread.Name = name;
            ThreadTick = tickMethod;
            Thread.Start();
            if (ThreadStart != null)
                ThreadStart();
        }


        public void Run()
        {
            try
            {
                if(ThreadTick !=null)
                    ThreadTick();
            }
            catch(Exception ex)
            {
                FileSupport.Instance.Write(ex.ToString());
            }
            if (ThreadFinish != null)
                ThreadFinish();
            FileSupport.Instance.Write("Thread finish name : " + Thread.Name);
     
        }

        public System.Threading.Thread Thread { get; set; }

    }
}
