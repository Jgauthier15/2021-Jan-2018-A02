<Query Kind="Expression">
  <Connection>
    <ID>64886636-d165-4bdc-b80f-7cc9808d4a44</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>DESKTOP-LAS06NI\SQLEXPRESS</Server>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>Chinook</Database>
  </Connection>
</Query>

//commenting  ctrl-KC
//uncommenting   ctrl-KU

//Method Syntax/Grammar
//Albums
//.Select(AnyRowAtAnyPointInTime => AnyRowAtAnyPointInTime)
//
//Query Syntax
//from x in Albums
//select x

//Filtering
//Where clause in query syntax
//.Where() method in method syntax


//*****Find all albums released in 1990*****
//Query Syntax
//from x in Albums
//where x.ReleaseYear == 1990
//select x

//Method Syntax
//Albums
//	.Where(x => x.ReleaseYear == 1990)
//	.Select(x =>x)


//Find all albums released in the good old 70's
//Query Syntax
//from x in Albums
//where x.ReleaseYear >= 1969
//where x.ReleaseYear <= 1979
//select x

//Method Syntax
//Albums
//	.Where(x => x.ReleaseYear <1980 && x.ReleaseYear >= 1970)
//	.Select(x => x)


//Ordering
//List all albums by ascending year of release
//Query Syntax
//from x in Albums
//orderby x.ReleaseYear
//select x

//Method Syntax
//Albums
//	.OrderBy(x => x.ReleaseYear)
//	.Select(x => x)

//List all albums by descending year of release in alphabetical order of title
//Query Syntax
//from x in Albums
//orderby x.ReleaseYear descending, x.Title
//select x

//Method Syntax
//Albums
//	.OrderByDescending(x => x.ReleaseYear)
//	.ThenBy(x => x.Title)
//	.Select(x => x)


//What about only certain fields (partial entity records or fields from another table)
//List all records from 1970's showing title, artist name and year.
from x in Albums
where x.ReleaseYear < 1980 && x.ReleaseYear >=1970
orderby x.ReleaseYear, x.Title
select new
{
	Title= x.Title,
	Artist = x.Artist.Name,
	Year = x.ReleaseYear
}


Albums
	.Where(x => x.ReleaseYear <1980 && x.ReleaseYear >= 1970)
	.OrderBy(x => x.ReleaseYear)
	.ThenBy(x => x.Title)
	.Select(x => new
				{
					Title= x.Title,
					Artist = x.Artist.Name,
					Year = x.ReleaseYear
	
				})	