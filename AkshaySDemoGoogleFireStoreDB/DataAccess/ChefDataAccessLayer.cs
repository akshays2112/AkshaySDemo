using System;
using System.Collections.Generic;
using System.Text;
using Google.Cloud.Firestore;
using Newtonsoft.Json;
using AkshaySDemoGoogleFireStoreDB.Models;
using System.Threading.Tasks;
using System.Linq;
using Google.Cloud.Firestore.V1;

namespace AkshaySDemoGoogleFireStoreDB.DataAccess
{
    public class ChefDataAccessLayer
    {
        public static string GoogleApisOAuthJsonString { get; set; }
        FirestoreDb fireStoreDB;

        public ChefDataAccessLayer()
        {
            FirestoreClientBuilder firestoreClientBuilder = new FirestoreClientBuilder();
            firestoreClientBuilder.JsonCredentials = GoogleApisOAuthJsonString;
            FirestoreClient firestoreClient = firestoreClientBuilder.Build();
            fireStoreDB = FirestoreDb.Create("akshaysdemo", firestoreClient);
        }

        public async Task<List<Chef>> GetAllChefs()
        {
            try
            {
                Query chefQuery = fireStoreDB.Collection("chefs");
                QuerySnapshot chefQuerySnapshot = await chefQuery.GetSnapshotAsync();
                List<Chef> lstChefs = new List<Chef>();
                foreach(DocumentSnapshot documentSnapshot in chefQuerySnapshot.Documents)
                {
                    if(documentSnapshot.Exists)
                    {
                        Dictionary<string, object> name = documentSnapshot.ToDictionary();
                        string json = JsonConvert.SerializeObject(name);
                        Chef newChef = JsonConvert.DeserializeObject<Chef>(json);
                        newChef.ID = documentSnapshot.Id;
                        lstChefs.Add(newChef);
                    }
                }
                return lstChefs;
            }
            catch
            { 
                throw;
            }
        }

        public async void AddChef(Chef chef)
        {
            try
            {
                CollectionReference chefsRef = fireStoreDB.Collection("chefs");
                await chefsRef.AddAsync(chef);
            }
            catch
            {
                throw;
            }
        }

        public async void UpdateChef(Chef chef)
        {
            try
            {
                DocumentReference chefRef = fireStoreDB.Collection("chefs").Document(chef.ID.ToString());
                await chefRef.SetAsync(chef, SetOptions.Overwrite);
            }
            catch
            {
                throw;
            }
        }

        public async Task<Chef> GetChefData(int id)
        {
            try
            {
                DocumentReference docRef = fireStoreDB.Collection("chefs").Document(id.ToString());
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

                if (snapshot.Exists)
                {
                    Chef chef = snapshot.ConvertTo<Chef>();
                    chef.ID = snapshot.Id;
                    return chef;
                }
                else
                {
                    return new Chef();
                }
            }
            catch
            {
                throw;
            }
        }

        public async void DeleteChef(int id)
        {
            try
            {
                DocumentReference chefRef = fireStoreDB.Collection("chefs").Document(id.ToString());
                await chefRef.DeleteAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Country>> GetCountryData()
        {
            try
            {
                Query countriesQuery = fireStoreDB.Collection("countries");
                QuerySnapshot countriesQuerySnapshot = await countriesQuery.GetSnapshotAsync();
                List<Country> lstCountries = new List<Country>();

                foreach (DocumentSnapshot documentSnapshot in countriesQuerySnapshot.Documents)
                {
                    if (documentSnapshot.Exists)
                    {
                        Dictionary<string, object> country = documentSnapshot.ToDictionary();
                        string json = JsonConvert.SerializeObject(country);
                        Country newCountry = JsonConvert.DeserializeObject<Country>(json);
                        lstCountries.Add(newCountry);
                    }
                }
                return lstCountries;
            }
            catch
            {
                throw;
            }
        }
    }
}
