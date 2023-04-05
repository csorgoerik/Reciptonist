using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reciptonist.Application.Persistance.MongoDb;

public class MongoDbSettings : IMongoDbSettings
{
    public string DatabaseName { get; set; }
    public string ConnectionString { get; set; }
}