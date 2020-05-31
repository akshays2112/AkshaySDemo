using System;
using Google.Cloud.Firestore;

namespace AkshaySDemoGoogleFireStoreDB
{
    [FirestoreData]
    public class Country
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
