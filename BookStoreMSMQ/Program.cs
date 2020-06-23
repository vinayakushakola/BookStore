//
// Author    : Vinayak Ushakola
// Date      : 21 June 2020
// Purpose   : It starts the Msmq Listener
//

namespace BookStoreMSMQ
{
    class Program
    {
        static void Main()
        {
            string path = @".\Private$\BookStoreQueue";
            MsmqListener msmqListener = new MsmqListener(path);
            msmqListener.Start();
        }
    }
}
