using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TechSmith.Interact.DataAccess.SalesDataAccess;

namespace EntityFramework6Examples
{
   public class SalesAsyncExamples : IDisposable
   {
      private readonly ISalesContext _salesContext;

      public SalesAsyncExamples()
         : this( new SalesContext() )
      {
      }

      public SalesAsyncExamples( ISalesContext salesContext )
      {
         if ( salesContext == null )
            throw new ArgumentNullException( "salesContext" );

         _salesContext = salesContext;
      }

      public async Task<IEnumerable<Country>> GetCountriesAsync()
      {
         var countries = await ( from c in _salesContext.Countries
                                 where c.CountryActive
                                 select c ).ToListAsync();


         return countries;
      }

      public async Task<int> GetNumberOfOrdersAsync()
      {
         var count = await ( from o in _salesContext.Ords
                             select o ).CountAsync();

         return count;
      }

      public void Dispose()
      {
         _salesContext.Dispose();
         GC.SuppressFinalize( this );
      }
   }
}
