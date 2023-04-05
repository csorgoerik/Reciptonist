using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reciptonist.Application.Persistance.MongoDb;
public interface IMongoDbSettings
{
    string DatabaseName { get; set; }
    string ConnectionString { get; set; }
}
