using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.NetworkInformation;

namespace NetTest
{
    public class PingTest:ITest
    {
        private bool isPing = true;
        private string ip;
        private string CommandText;

        public PingTest(string p_ip, string p_CommandText)
        {
            this.ip = p_ip;
            this.CommandText = p_CommandText;
        }
        public bool Test()
        {
            AutoResetEvent waiter = new AutoResetEvent(false);

            Ping pingSender = new Ping();

            // When the PingCompleted event is raised,
            // the PingCompletedCallback method is called.
            pingSender.PingCompleted += new PingCompletedEventHandler(PingCompletedCallback);

            // Create a buffer of 32 bytes of CommandText to be transmitted.
            byte[] buffer = Encoding.ASCII.GetBytes(CommandText);

            // Wait 12 seconds for a reply.
            int timeout = 20;

            // Set options for transmission:
            // The CommandText can go through 64 gateways or routers
            // before it is destroyed, and the CommandText packet
            // cannot be fragmented.
            PingOptions options = new PingOptions(64, true);

            //Console.WriteLine("Time to live: {0}", options.Ttl);
            //Console.WriteLine("Don't fragment: {0}", options.DontFragment);

            // Send the ping asynchronously.
            // Use the waiter as the user token.
            // When the callback completes, it can wake up this thread.
            pingSender.SendAsync(ip, timeout, buffer, options, waiter.Set());

            // Prevent this example application from ending.
            // A real application should do something useful
            // when possible.
            waiter.WaitOne();
            Console.WriteLine("Ping example completed.");
            //Console.Read();
            return isPing;
        }

        private void PingCompletedCallback(object sender, PingCompletedEventArgs e)
        {
            // If the operation was canceled, display a message to the user.
            if (e.Cancelled)
            {
                isPing = false;
                //Console.WriteLine("Ping canceled.");
                // Let the main thread resume. 
                // UserToken is the AutoResetEvent object that the main thread 
                // is waiting for.
                ((AutoResetEvent)e.UserState).Set();
            }

            // If an error occurred, display the exception to the user.
            if (e.Error != null)
            {
                isPing = false;
                //Console.WriteLine("Ping failed:");
                //Console.WriteLine(e.Error.ToString());

                // Let the main thread resume. 
                ((AutoResetEvent)e.UserState).Set();
            }

            PingReply reply = e.Reply;

            DisplayReply(reply);

            // Let the main thread resume.
            //((AutoResetEvent)e.UserState).Set();
        }

        public void DisplayReply(PingReply reply)
        {
            if (reply == null)
            {
                Console.WriteLine("Status: {0}", reply.Status.ToString());
                return;
            }
            //Console.WriteLine("ping status: {0}", reply.Status);
            if (reply.Status == IPStatus.Success)
            {
                Console.WriteLine("Address: {0}", reply.Address.ToString());
                Console.WriteLine("Status: {0}", reply.Status.ToString());
               // Console.WriteLine("RoundTrip time: {0}", reply.RoundtripTime);
              //  Console.WriteLine("Time to live: {0}", reply.Options.Ttl);
                //Console.WriteLine("Don't fragment: {0}", reply.Options.DontFragment);
                //Console.WriteLine("Buffer size: {0}", reply.Buffer.Length);
            }
        }
    }
}
