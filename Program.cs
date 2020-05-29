using System.Threading;

namespace CHCNetSDK
{
    static class Program
    {

        public static void Main()
        {
            var esdk = new EasySDK("10.0.1.60", 8000, "admin", "A12345678",1);
            while (true)
            {
                Thread.Sleep(100);
                var img = esdk.ReadImage(0);
                img = img;
            }
        }
    }

}