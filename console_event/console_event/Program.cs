using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace console_event
{
    /// <summary>
    /// MSDN建議這樣寫 返回值為空(void),帶兩個參數如下
    /// https://www.youtube.com/watch?v=_PunoSCnsQU&index=3&list=PLTstZD3AK3S_UL-d16UJZmUcFqc5lyUFI
    /// </summary>
    public delegate void ChangedEventHandler(object sender,EventArgs e);

    class Program
    {
        
        static void Main(string[] args)
        {
            //ex1
            var e = new EventTest(5);
            e.SetValue(100);
            //綁定一個delegate事件觸發
            e.ChangeNum+=new EventTest.NumManipilationHander(EventTest.EventFired);
            //再次觸發
            e.SetValue(200);


            //ex2
            I i = new MyClass();
            i.MyEvent+=new MyDelegate(f);
            i.FireAway();
            Console.ReadLine();
        }

        private  static void f() 
        {
            Console.WriteLine("This is called when the event fired");
        }
    }

    /// <summary>
    ///做一個class來實現事件觸發 
    /// </summary>
    class EventTest 
    {
        private int value;
        public delegate void NumManipilationHander();
        public event NumManipilationHander ChangeNum;

        public EventTest(int n) 
        {
            SetValue(n);
        }

        public static void EventFired() 
        {
            Console.WriteLine("Bruce Event fired");
        }

        protected virtual void OnNumChange() 
        {
            if (ChangeNum != null)
            {
                ChangeNum();
            }
            else 
            {
                Console.WriteLine("event fired");
            }
        }

        public void SetValue(int n) 
        {
            if (value != n) 
            {
                value = n;
                OnNumChange();
            }
        }
     }

    public delegate void MyDelegate();
    

    /// <summary>
    /// event利用interface來實現
    /// </summary>
    public interface I 
    {
        event MyDelegate MyEvent;

        //最好是聲明成 EventHandler 這種格式的
        //event EventHandler MyGuidlineEvent;

        void FireAway();
    }

    public class MyClass : I 
    {
        public event MyDelegate MyEvent;
        public void FireAway() 
        {
            if (MyEvent != null)
            {
                MyEvent();
            }
        }
    }
}
