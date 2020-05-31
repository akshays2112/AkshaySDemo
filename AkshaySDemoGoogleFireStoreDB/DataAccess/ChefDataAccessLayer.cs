﻿using System;
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
        FirestoreDb fireStoreDB;

        public ChefDataAccessLayer()
        {
            FirestoreClientBuilder firestoreClientBuilder = new FirestoreClientBuilder();
            firestoreClientBuilder.JsonCredentials = "{" +
                    "\"type\": \"service_account\", \"project_id\": \"akshaysdemo\"," +
                    "\"private_key_id\": \"4eb4511f3238abf9456cbe93bd1c989e5145cacf\"," +
                    "\"private_key\": \"-----BEGIN PRIVATE KEY-----\nMIIEvAIBADANBgkqhkiG9w0BAQEFAASCBKYwggSiAgEAAoIBAQCl02nIZZhlwu3Z\ngjvkuR+G3zSptc2B1OETI1i5u7Y45SnkflT1wXn4WOmopcQF25YMstalUwwOD8on\nY1Xf7Vs44izXnOoT1gZtXn4KwXTFnFcUxndtESUpTB2DQ05UU7ep/gg0HW4CO4fk\nksFZhkdOhxDdtpbxWXX4igagVr/oRTiPblUMF+zBB1elmHO8mY9PxXwG1jIFS2cM\niRdYjwhULJ4Zc2z/pZlBzh7WlPNAjUb/EBoWCDIAFCHXm+Afldy0fv1jxsAmXQFW\nrEsq1ji2iHCTl0wthBws5cu4jsfOWfAhgo7um2SLCAOWg+Bi9pjR668KuzGGzxME\nZEI4733bAgMBAAECggEADgdeuA3WrY7aLgz7u2C1/BdyDlF+u1ciweQho3gjKGXd\n/DviPPaxtoLyV/3K7m8ihWlNcEGGwygu0AcLwtADAaldmQToM+Yz0P9HgB1xuMex\nmxfw212x9fAMjYhQkVQrzspA/QSB9z2WESfuCs1DYK9bvx+tXjaIg0Hm4ZwbmPvv\nD2RUeOtYlDJeKjmN4Z0XilvIhmllfk/EHkYW4MKWK5K+Nqh04NjMZ8L4yUNZC/M4\nzQDRuh9C/Y45Qld7/KlzavaQz94xv6spsKbvGlzakN6GlCEpbwZCrovL0L7hM9IY\nVeq8ff4uw6RC4bR6IiVWjrhYtwmy/GpGt3fblhcxkQKBgQDpN8uLGpfOw4pfCh9d\nfIaNJ5aa0SqKZRviTaJccFnftPg/+Wk0hQNUOq3TDOSZxgK8hD2D4tNC4Nv9ZO9c\ni+xqBULUFY3CDJWdQqntCWWC6DBokfzDHQr+gDI8+Moocyi2cqjN4cfbMX6/xX3j\nOvRrZ3F9pscXC64Wix7pw7bpRwKBgQC2Bk7HfBUX71pAQith8CaISU+aY+JA+DTf\nibNjZfORpfDH2fSrjh1+M2Nsoi1jsAPnUEAckxbtBAOBOp9w70tmU/riLfdQ1H8F\nj9pDn35ohpP4cYnN2mQqxNY+jL4pI+Qu/SWf63oXvG2AC7X4RSidtI1Q6J3HH+z2\nQu1BNSvQzQKBgEt3cZcOwOb4YZNFfEbNH+EXWWW5n5FvDGnbg1l0RbDdJ6PT+lYz\nYJNl9Y+g0WxtJb+I7zr5MDGo/6bsfYQuBw97qldkrh2H4vYjd0crzjxhFCESdH9S\nq5cVNqyCOTCDqz32tmcA06I7Tu+RYZ4hGqySqafmSvBLKEdFN3ifi1XLAoGAHnZ/\nyLLjdNYB7K8mQ4XFbRmX0ObWfrkLYD3TX9c4JC/5U/kOEYf/N5eyFAQwRHa4sIWl\ntSKIu7HoREBjXqstmzqCykeXFFf7yhqBFMAkj6m2KeYWgfUCvoWitWUojgoLrjF/\nknv+Ouq2CK/tDFfGrF4DKH9FqIWXSr94pWkYpCECgYBWA2Lv2+3uoVROgLuTAJbj\nIhZ0LvK6gBt0OPXMLDEcIXndcaUNSTC9l8+Fl2eNaMcNc/1mKQhfRR1GA17kuXKf\nNqW+rTlxa7mr/0S/DMUaxzccFfoQKr/x7uV1MvJulOaXGxpOXj+MoHVSCNm4lXbi\n6dNL9/RkDlm9am2Ct1RdeA==\n-----END PRIVATE KEY-----\n\"," +
                    "\"client_email\": \"akshaysdemo@appspot.gserviceaccount.com\"," +
                    "\"client_id\": \"117196345125732155646\"," +
                    "\"auth_uri\": \"https://accounts.google.com/o/oauth2/auth\"," +
                    "\"token_uri\": \"https://oauth2.googleapis.com/token\"," +
                    "\"auth_provider_x509_cert_url\": \"https://www.googleapis.com/oauth2/v1/certs\"," +
                    "\"client_x509_cert_url\": \"https://www.googleapis.com/robot/v1/metadata/x509/akshaysdemo%40appspot.gserviceaccount.com\"" +
                    "}";
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
