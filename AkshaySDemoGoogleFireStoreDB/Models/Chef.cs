using System;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;

namespace AkshaySDemoGoogleFireStoreDB.Models
{
    [FirestoreData]
    public class Chef
    {
        public string ID { get; set; }
        [FirestoreProperty]
        public string FirstName { get; set; }
        [FirestoreProperty]
        public string LastName { get; set; }
        [FirestoreProperty]
        public string Name { get; set; }
    }
}
