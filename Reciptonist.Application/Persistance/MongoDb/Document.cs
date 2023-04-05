using MongoDB.Bson;
using Reciptonist.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reciptonist.Application.Persistance.MongoDb;
public abstract class Document : AuditableEntity, IDocument
{
    public ObjectId Id { get; set; }

    public override DateTime Created => Id.CreationTime;
}
