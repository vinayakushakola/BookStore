//
// Author   : Vinayak Ushakola
// Date     : 21 June 2020
// Purpose  : Add data to the Queue
//

using Experimental.System.Messaging;
using System;

namespace BookStoreCommonLayer.MSMQSender
{
    public class MsmqSender
    {
        /// <summary>
        /// Send data to the Queue
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="token">token</param>
        public static void SendToMsmq(string email, string token)
        {
            try
            {
                string path = @".\Private$\BookStoreQueue";
                MessageQueue messageQueue = null;
                if (MessageQueue.Exists(path))
                {
                    messageQueue = new MessageQueue(path);
                }
                else
                {
                    messageQueue = MessageQueue.Create(path);
                }
                messageQueue.Label = "Book Store Mail Sending";

                Message message = new Message(token)
                {
                    Formatter = new BinaryMessageFormatter()
                };
                messageQueue.Send(message, email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
