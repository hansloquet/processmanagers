using System;
using Newtonsoft.Json;

namespace ProcessManagers
{
    internal class MyPrinter : IHandle<Message>
    {
        public void Handle(Message message)
        {
            Console.WriteLine(message.GetType().Name);
            Console.WriteLine(JsonConvert.SerializeObject(message));
        }
    }
}