using grafis_aspnet_test.Context;
using grafis_aspnet_test.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace grafis_aspnet_test.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ClientController : ApiController
    {
        public class ClientInfo
        {
            public long id { get; set; }
            public string name { get; set; }
            public string email { get; set; }
        }

        public static ClientInfo getInfo(Client client)
        {
            return new ClientInfo
            {
                id = client.Id,
                name = client.Name,
                email = client.Email
            };
        }

        public List<ClientInfo> Get()
        {
            using (var context = new DatabaseContext())
            {
                return context.Clients.Select(getInfo).ToList();
            }
        }

        public ClientInfo Get(int id)
        {
            using (var context = new DatabaseContext())
            {
                var client = context.Clients.Find(id);
                return getInfo(client);
            }
        }

        public class NewClient
        {
            public string Name { get; set; }
            public string Email { get; set; }
        }

        public IHttpActionResult Post(NewClient data)
        {

            using (var context = new DatabaseContext())
            {
                try
                {
                    data.Email = data.Email.Trim().ToLower();

                    var client = new Client()
                    {
                        Name = data.Name,
                        Email = data.Email
                    };

                    client = context.Clients.Add(client);
                    context.SaveChanges();

                    return Ok<ClientInfo>(getInfo(client));
                }
                catch (DbEntityValidationException validationException)
                {
                    return BadRequest(validationException.EntityValidationErrors.First().ValidationErrors.First().ErrorMessage);
                }
                catch (DbUpdateException e)
                {
                    return BadRequest($"Email {data.Email} já cadastrado.");
                }

            }
        }

    }
}
