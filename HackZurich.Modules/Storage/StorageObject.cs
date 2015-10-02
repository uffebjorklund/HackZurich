using Microsoft.WindowsAzure.Storage.Table;
using System.Reflection;
using XSockets.Core.Common.Utility.Serialization;
using XSockets.Plugin.Framework;

namespace HackZurich.Modules.Storage
{
    public class StorageObject : TableEntity
    {
        public string Type { get; set; }
        public string JSON { get; set; } 

        public object Deserialize()
        {           
            return Composable.GetExport<IXSocketJsonSerializer>().DeserializeFromString(this.JSON, System.Type.GetType(Type));            
        }
        
        public StorageObject() { }

        public StorageObject(string partitiokKey, string rowKey)
        {
            this.PartitionKey = partitiokKey;
            this.RowKey = rowKey;
        }
        public StorageObject(string partitiokKey, string rowKey, object o)
        {
            this.PartitionKey = partitiokKey;
            this.RowKey = rowKey;
            var t = o.GetType();            
            this.Type = string.Format("{0}, {1}",t.FullName, Assembly.GetAssembly(t).GetName().Name);
            this.JSON = Composable.GetExport<IXSocketJsonSerializer>().SerializeToString(o, o.GetType());
        }
    }    
}
