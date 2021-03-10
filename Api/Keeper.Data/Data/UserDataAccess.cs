using Keeper.Helper.Helpers;
using Keeper.Models.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace Keeper.Data.Data
{
    public class Query : Base
    {
        public Query() : base()
        {

        }

        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            List<DbParameter> parameterList = new List<DbParameter>();

            using (DbDataReader dataReader = base.GetDataReader("sp_GetUsers", parameterList))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        users.Add(new User
                        {
                            Id = dataReader.GetInt32(dataReader.GetOrdinal("Id")),
                            name = dataReader.GetString(dataReader.GetOrdinal("Name")),
                            email = dataReader.GetString(dataReader.GetOrdinal("Email")),
                            password = dataReader.GetString(dataReader.GetOrdinal("Password")).Decrypt(),
                            created = dataReader.IsDBNull(dataReader.GetOrdinal("Created")) ? new DateTime() : dataReader.GetDateTime(dataReader.GetOrdinal("Created")),
                        });
                    }
                }
            }
            return users;
        }

        public User InsertNewUser(User model)
        {
            User user = new User();
            int Id = 0;
            DateTime date = new DateTime();

            List<DbParameter> parameterList = new List<DbParameter>();
            parameterList.Add(new SqlParameter("@Name", SqlDbType.NVarChar) { Value = model.name });
            parameterList.Add(new SqlParameter("@Email", SqlDbType.NVarChar) { Value = model.email });
            parameterList.Add(new SqlParameter("@Password", SqlDbType.NVarChar) { Value = model.password.Encrypt() });

            using (DbDataReader dataReader = base.GetDataReader("sp_InsertNewUser", parameterList))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Id = dataReader.GetInt32(dataReader.GetOrdinal("Id"));
                        date = dataReader.GetDateTime(dataReader.GetOrdinal("Created"));
                    }

                    user = model;
                    user.created = date;
                    user.Id = Id;
                }
            }
            return user;
        }

        public List<Contact> GetContacts(int userId)
        {
            List<Contact> contacts = new List<Contact>();

            List<DbParameter> parameterList = new List<DbParameter>();
            parameterList.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = userId });

            using (DbDataReader dataReader = base.GetDataReader("sp_GetContactsByUserId", parameterList))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        contacts.Add(new Contact
                        {
                            Id = dataReader.GetGuid(dataReader.GetOrdinal("Id")),
                            Name = dataReader.GetString(dataReader.GetOrdinal("Name")),
                            Email = dataReader.GetString(dataReader.GetOrdinal("Email")),
                            Phone = dataReader.GetString(dataReader.GetOrdinal("Phone")),
                            ContactType = (ContactType)Enum.Parse(typeof(ContactType), dataReader.GetString(dataReader.GetOrdinal("ContactType"))),
                            Owner = dataReader.GetInt32(dataReader.GetOrdinal("Owner")),
                            OwnerName = dataReader.GetString(dataReader.GetOrdinal("OwnerName")),
                            OwnerEmail = dataReader.GetString(dataReader.GetOrdinal("OwnerEmail")),
                            Created = dataReader.IsDBNull(dataReader.GetOrdinal("Created")) ? new DateTime() : dataReader.GetDateTime(dataReader.GetOrdinal("Created")),
                        });
                    }
                }
            }
            return contacts;
        }

        public Contact InsertNewContact(Contact model)
        {
            Contact contact = new Contact();

            List<DbParameter> parameterList = new List<DbParameter>();
            parameterList.Add(new SqlParameter("@Name", SqlDbType.NVarChar) { Value = model.Name });
            parameterList.Add(new SqlParameter("@Email", SqlDbType.NVarChar) { Value = model.Email });
            parameterList.Add(new SqlParameter("@Phone", SqlDbType.NVarChar) { Value = model.Phone });
            parameterList.Add(new SqlParameter("@ContactType", SqlDbType.NVarChar) { Value = model.ContactType.ToString() });
            parameterList.Add(new SqlParameter("@Owner", SqlDbType.Int) { Value = model.Owner });

            using (DbDataReader dataReader = base.GetDataReader("sp_InsertNewContact", parameterList))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        contact = new Contact
                        {
                            Id = dataReader.GetGuid(dataReader.GetOrdinal("Id")),
                            Name = dataReader.GetString(dataReader.GetOrdinal("Name")),
                            Email = dataReader.GetString(dataReader.GetOrdinal("Email")),
                            Phone = dataReader.GetString(dataReader.GetOrdinal("Phone")),
                            ContactType = (ContactType)Enum.Parse(typeof(ContactType), dataReader.GetString(dataReader.GetOrdinal("ContactType"))),
                            Owner = dataReader.GetInt32(dataReader.GetOrdinal("Owner")),
                            Created = dataReader.IsDBNull(dataReader.GetOrdinal("Created")) ? new DateTime() : dataReader.GetDateTime(dataReader.GetOrdinal("Created")),
                        };
                    }
                }
            }
            return contact;
        }
    }
}
