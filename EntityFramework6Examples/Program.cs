using System;
using System.Collections.Generic;
using System.Linq;
using TechSmith.Interact.DataAccess.SalesDataAccess;

namespace EntityFramework6Examples
{
   class Program
   {
      static void Main( string[] args )
      {
         Console.WriteLine( "Welcome to Entity Framework 6 Examples!\n--------------------------------------\n\n" );

         RunQueries();

         Console.WriteLine( "At Console.ReadLine()\n" );
         Console.ReadLine();
      }

      public async static void RunQueries()
      {
         using ( var context = new SalesContext() )
         using ( var asyncExamples = new SalesAsyncExamples( context ) )
         {
            Console.WriteLine( "Executing GetNumberOfOrdersAsync()\n" );
            await asyncExamples.GetNumberOfOrdersAsync().ContinueWith( t => WriteToConsole( t.Result + "\n\n" ) );

            Console.WriteLine( "Executing GetCountriesAsync()\n" );
            await asyncExamples.GetCountriesAsync().ContinueWith( t => WriteCountryCollectionToCommandPrompt( t.Result ) );
         }
      }

      private static void WriteToConsole<T>( T result )
      {
         Console.WriteLine( result );
      }

      private static void WriteCountryCollectionToCommandPrompt( IEnumerable<Country> countryCollection )
      {
         foreach ( var c in countryCollection.Take( 10 ) )
         {
            Console.WriteLine( "ISO Code: {0} - Name: {1}", c.CountryIsoCode, c.CountryPrintName );
         }
      }
   }
}
