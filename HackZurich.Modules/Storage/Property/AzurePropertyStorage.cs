using System;
using System.Threading.Tasks;
using XSockets.Core.XSocket;

namespace HackZurich.Modules.Storage.Property
{
    public class AzurePropertyStorage : PersistentPropertyStorage
    {
        public override async Task ReadFromPropertyStorage<T>(T controller)
        {
            foreach (var pi in this.GetPersistentProperties(controller.GetType()))
            {
                var e = new StorageObject(string.Format("{0}.{1}", controller.Alias, pi.Name), controller.PersistentId.ToString());
                var so = await e.GetEntity();
                if (so == null) continue;
                pi.SetValue(controller, so.Deserialize(), null);
            }
        }

        public override async Task WriteToPropertyStorage<T>(T controller)
        {
            foreach (var pi in this.GetPersistentProperties(controller.GetType()))
            {

                var v = pi.GetValue(controller, null);
                if (v == GetDefault(pi.PropertyType)) continue;
                //User controller.property as partition key, otherwise you cant use teh same property name on several controllers
                await new StorageObject(string.Format("{0}.{1}", controller.Alias, pi.Name), controller.PersistentId.ToString(), v).SaveEntity();
            }
        }

        public static object GetDefault(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }
    }
}