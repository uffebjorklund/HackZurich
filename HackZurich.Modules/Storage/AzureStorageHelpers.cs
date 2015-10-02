using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace HackZurich.Modules.Storage
{
    public static class StorageTableHelpers
    {
        private static string connString;
        private static CloudStorageAccount storageAccount;
        private static CloudTableClient tableClient;
        static StorageTableHelpers()
        {
            connString = ConfigurationManager.AppSettings["StorageConnectionString"];
            // Retrieve the storage account from the connection string.
            storageAccount = CloudStorageAccount.Parse(connString);
            // Create the table client.
            tableClient = storageAccount.CreateCloudTableClient();
        }

        /// <summary>
        /// Generic helper to save to an entity to a Azure Storage Table.        
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">Any object that inherits TableEntity</param>
        /// <param name="updateAction">Perform update on properties, if null no updates will be made and an overwrite will be made if the entity exists</param>
        /// <param name="tableName">If not passed in the table will be named after the entity T</param>
        public static async Task SaveEntity<T>(this T entity, Func<T, T> updateAction = null, string tableName = null) where T : TableEntity
        {
            if (tableName == null)
            {
                tableName = typeof(T).Name;
            }

            var t = tableName.EnsureTable();

            //Check for current rows...
            TableOperation retrieveOperation = TableOperation.Retrieve<T>(entity.PartitionKey, entity.RowKey);

            // Execute the operation.
            TableResult retrievedResult = await t.ExecuteAsync(retrieveOperation);

            // Assign the result to T.
            T updateEntity = (T)retrievedResult.Result;

            if (updateEntity != null)
            {                
                if (updateAction != null)
                {
                    //Update memebers from delegate
                    updateEntity = updateAction(updateEntity);
                    TableOperation insertOrReplaceOperation = TableOperation.InsertOrReplace(updateEntity);
                    await t.ExecuteAsync(insertOrReplaceOperation);
                }
                else
                {
                    //merge
                    TableOperation replaceOperation = TableOperation.InsertOrMerge(entity);
                    await t.ExecuteAsync(replaceOperation);
                }                
            }
            else
            {
                //Add new entity
                TableOperation insertOperation = TableOperation.Insert(entity);
                await t.ExecuteAsync(insertOperation);
            }
        }

        public static async Task<T> GetEntity<T>(this T entity, string tableName = null) where T : TableEntity
        {
            if (tableName == null)
            {
                tableName = typeof(T).Name;
            }

            var t = tableName.EnsureTable();

            //Check for current rows...
            var partitionKey = entity.PartitionKey;
            var rowKey = entity.RowKey;
            TableOperation retrieveOperation = TableOperation.Retrieve<T>(partitionKey, rowKey);

            // Execute the operation.
            TableResult retrievedResult = await t.ExecuteAsync(retrieveOperation);

            return (T)retrievedResult.Result;
        }

        public static async Task<T> DeleteEntity<T>(this T entity, string tableName = null) where T : TableEntity
        {
            if (tableName == null)
            {
                tableName = typeof(T).Name;
            }

            var t = tableName.EnsureTable();
            if(string.IsNullOrEmpty(entity.ETag))
                entity.ETag = "*";
            TableOperation deleteOperation = TableOperation.Delete(entity);

            // Execute the operation.
            TableResult deleteResult = await t.ExecuteAsync(deleteOperation);

            return (T)deleteResult.Result;
        }

        private static CloudTable EnsureTable(this string tableName)
        {
            CloudTable table = tableClient.GetTableReference(tableName);

            table.CreateIfNotExists();

            return table;
        }

        public static void DeleteTable(this string tableName)
        {
            // Create the CloudTable object that represents the "people" table.
            CloudTable table = tableClient.GetTableReference(tableName);

            table.Delete();
        }
    }
}
