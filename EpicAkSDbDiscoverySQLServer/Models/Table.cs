using System;
using System.Security;
using System.Runtime.InteropServices;

namespace EpicAkSDbDiscoverySQLServer.Models
{
    public class Table
    {
        private SecureString tableName = null;
        public string TableName
        {
            get
            {
                IntPtr umgdTableName = IntPtr.Zero;
                try
                {
                    umgdTableName = Marshal.SecureStringToGlobalAllocUnicode(tableName);
                    return Marshal.PtrToStringUni(umgdTableName);
                } 
                finally
                {
                    Marshal.ZeroFreeGlobalAllocUnicode(umgdTableName);
                }
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if(this.tableName != null)
                    {
                        this.tableName.Clear();
                        this.tableName.Dispose();
                    }
                    this.tableName = new SecureString();
                    foreach (char c in value)
                    {
                        this.tableName.AppendChar(c);
                    }
                }
            }
        }

        public void DeleteTableName()
        {
            tableName.Clear();
            tableName.Dispose();
        }
    }
}
