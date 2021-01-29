using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.DAL;
using ChinookSystem.Entities;   //for Sql and are internal
using ChinookSystem.ViewModels; //for data class to transfer data from BLL to web app
using System.ComponentModel;    //for ODS wizard
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
    public class CustomerController
    { 
               
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<CustomersOfCountryEmail> Customer_GetCustomersForCountryAndEmail(string country, string email)
        {
            using (var context = new ChinookSystemContext())
            {
                //when you are working in your sandbox of LinqPad, you are using Linq to SQL
                //when you move your query to your controller class, you are using Linq to Entity
                //You therefore have to request the DbSet in your context class
              
                IEnumerable<CustomersOfCountryEmail> results = context.Customers
                    .Where(x => x.Country.Contains(country) && x.Email.Contains(email))
                    .OrderBy(x => x.LastName)
                    .ThenBy(x => x.FirstName)
                    .Select(x => new CustomersOfCountryEmail
                            {
                                Name = x.LastName + " , " + x.FirstName,
                                Email = x.Email,
                                City = x.City,
                                State = x.State,
                                Country = x.Country
                            });
                return results.ToList();
            }
        }
    }
}
