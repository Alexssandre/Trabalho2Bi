using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diary.Rabbit
{
    public class ExecutionRabbit
    {
        ConnectionFactory factory = new ConnectionFactory
        {
            HostName = "localhost"
        };

       

    }
}
