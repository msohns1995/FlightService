using FlightServiceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightServiceAPI.Data
{
    public static class FSInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var flightServiceContext = new FSContext(serviceProvider.GetRequiredService<DbContextOptions<FSContext>>()))
            {
                //flightServiceContext.Database.EnsureDeleted();
                flightServiceContext.Database.EnsureCreated();


                if (!flightServiceContext.Passengers.Any())
                {
                    var passengers = new Passenger[]
                    {
                    new Passenger {
                        Name = "Jonas Castillo",
                        Job = "Magna Et Ipsum Incorporated",
                        Email = "vitae.sodales@google.org",
                        Age = 46
                        },
                    new Passenger {
                        Name = "Nola",
                        
                        Job = "Luctus Felis Industries",
                        Email = "morbi.non.sapien@outlook.ca",
                        Age = 32
                    },
                    new Passenger {
                        Name = "Oleg",
                        
                        Job = "Et Lacinia LLC",
                        Email = "orci.adipiscing@hotmail.org",
                        Age = 37
                    },
                    new Passenger {
                        Name = "Idola",
                        
                        Job = "Amet Consectetuer Adipiscing PC",
                        Email = "neque.non.quam@aol.com",
                        Age = 55
                    },
                    new Passenger {
                        Name = "Martina",
                        
                        Job = "Pede Nunc Corp.",
                        Email = "praesent.eu@icloud.net",
                        Age = 61
                    },
                    new Passenger {
                        Name = "Lilah",
                        
                        Job = "Consectetuer Adipiscing LLC",
                        Email = "enim.nisl.elementum@google.edu",
                        Age = 26
                    },
                    new Passenger {
                        Name = "Dante",
                        
                        Job = "Senectus Et LLC",
                        Email = "sed.pede@outlook.couk",
                        Age = 60
                    },
                    new Passenger {
                        Name = "Kermit",
                        
                        Job = "Aliquam Tincidunt Nunc Ltd",
                        Email = "turpis.in@protonmail.edu",
                        Age = 39
                    },
                    new Passenger {
                        Name = "Evangeline",
                        
                        Job = "Ornare Limited",
                        Email = "neque@aol.ca",
                        Age = 44
                    },
                    new Passenger {
                        Name = "Porter",
                        
                        Job = "Blandit Mattis Foundation",
                        Email = "arcu.ac@outlook.org",
                        Age = 65
                    },
                    new Passenger {
                        Name = "Daniel",
                        
                        Job = "Luctus Industries",
                        Email = "dolor.quisque@icloud.couk",
                        Age = 45
                    },
                    new Passenger {
                        Name = "Jermaine",
                        
                        Job = "Dolor Fusce Foundation",
                        Email = "nunc.risus@hotmail.ca",
                        Age = 23
                    },
                    new Passenger {
                        Name = "Stone",
                        
                        Job = "Lectus Convallis Est Foundation",
                        Email = "vel.quam@outlook.couk",
                        Age = 41
                    },
                    new Passenger {
                        Name = "Reuben",
                        
                        Job = "Rutrum Urna Incorporated",
                        Email = "vitae.diam.proin@protonmail.com",
                        Age = 63
                    }
                    };

                    foreach (Passenger passenger in passengers)
                    {
                        flightServiceContext.Add(passenger);
                    }
                    flightServiceContext.SaveChanges();
                }

                if (!flightServiceContext.Flights.Any())
                {
                    var flights = new Flight[]
                    {
                    new Flight {
                        DepartureDate = "Dec 21, 2022 11:41 AM",
                        ArrivalDate = "Nov 16, 2021 10:27 PM",
                        DepartureAirport = "amet",
                        ArrivalAirport = "at"
                },
                    new Flight{
                        DepartureDate = "Dec 21, 2022 11:41 AM",
                        ArrivalDate = "Nov 16, 2021 10:27 PM",
                        DepartureAirport = "condimentum.",
                        ArrivalAirport = "lacinia"
                }
                    };
                    foreach (Flight flight in flights)
                    {
                        flightServiceContext.Add(flight);
                    }
                    flightServiceContext.SaveChanges();
                }
            }
        }
    }
}
