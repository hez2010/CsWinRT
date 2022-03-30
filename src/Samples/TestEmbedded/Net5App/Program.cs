﻿using System;
using Gamma;
using Alpha;
using Beta;
using Windows.Devices.Geolocation;

namespace UseTestLib
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            TestLib testLib = new();
            Console.WriteLine("Expect 10, Got " + testLib.Test1());

            Console.WriteLine("Expect 5, Got " + testLib.Test2());

            (bool, bool) pair = testLib.Test3();
            Console.WriteLine("Expect false, Got " + pair.Item1);
            Console.WriteLine("Expect true, Got " + pair.Item2);

            Console.WriteLine("Expect 5, Got " + testLib.Test4());

            testLib.Test5();
            testLib.Test6();
        }
    }

    class MyAlpha : IAlpha
    {
        public int Five() { return 5; }
    }

    class MyBeta : IBeta
    {
        public int CallFive(Alpha.IAlpha a) { return a.Five(); }
    }

    class MyGreek : IAlpha, IGamma
    {
        public int Five() { return 5; }
        public int Six() { return 6; }
    }

    public class TestLib
    {
        public TestLib()
        {
        }

        internal int Test1_Helper(IAlpha alpha) { return alpha.Five(); }

        public int Test1()
        {
            MyAlpha a = new();
            MyGreek g = new();
            return Test1_Helper(a) + Test1_Helper(g);
        }

        public int Test2()
        {
            MyGreek g = new();
            MyBeta b = new();
            return b.CallFive(g);
        }

        public (bool, bool) Test3()
        {
            IAlpha myAlpha = new MyAlpha();
            MyGreek myGreek = new();
            QIAgent qiAgent = new();
            return (qiAgent.CheckForIGamma(myAlpha), qiAgent.CheckForIGamma(myGreek));
        }

        internal QIAgent GetQIAgent() { return new QIAgent(); }

        public int Test4()
        {
            QIAgent qiAgent = GetQIAgent();
            //var x = qiAgent.IdentityAlpha(new MyAlpha());
            return qiAgent.Run(new MyAlpha());
        }

        async System.Threading.Tasks.Task CallGeoAsyncApi()
        {
            Console.WriteLine("Making a Microsoft.Devices.Geolocation.Geolocator object...");
            Geolocator g = new();
            Console.WriteLine("Setting the Desired Accuracy to Default on the Geolocator object...");
            g.DesiredAccuracy = PositionAccuracy.Default;
            Console.WriteLine("Accessing the Desired Accuracy, shows: " + g.DesiredAccuracy);
            Console.WriteLine("Calling GetGeopositionAsync...");
            Geoposition pos = await g.GetGeopositionAsync();
        }

        public void Test5()
        {
            CallGeoAsyncApi().Wait(1000);
        }

        // Embedded projection using global types
        public void Test6()
        {
            QIAgent qiAgent = new();
            Windows.Media.AudioFrame audioFrame = qiAgent.ReturnsAudioFrame();
            Windows.Media.AudioBuffer buf = audioFrame.LockBuffer(Windows.Media.AudioBufferAccessMode.Read);
            Console.WriteLine("capactiy should be 20, got " + buf.Capacity);
        }
    }


}
