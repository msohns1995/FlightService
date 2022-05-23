using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightServiceEF
{
    public class DBInitializer
    {
        public static void Initialize(FlightServiceContext flightServiceContext)
        {
            flightServiceContext.Database.EnsureCreated();
            if (!flightServiceContext.Passengers.Any())
            {
                var passengers = new Passenger[]
                {
                    new Passenger {
                    ConfirmationNumber = 1998,
                    FirstName = "Jonas",
                    LastName = "Castillo",
                    Job = "Magna Et Ipsum Incorporated",
                    Email = "vitae.sodales@google.org",
                    Age = 46
                    },
                    new Passenger {
                        ConfirmationNumber = 1813,
                        FirstName = "Nola",
                        LastName = "Estes",
                        Job = "Luctus Felis Industries",
                        Email = "morbi.non.sapien@outlook.ca",
                        Age = 32
                    },
                    new Passenger {
                        ConfirmationNumber = 1881,
                        FirstName = "Oleg",
                        LastName = "Riley",
                        Job = "Et Lacinia LLC",
                        Email = "orci.adipiscing@hotmail.org",
                        Age = 37
                    },
                    new Passenger {
                        ConfirmationNumber = 1291,
                        FirstName = "Idola",
                        LastName = "Workman",
                        Job = "Amet Consectetuer Adipiscing PC",
                        Email = "neque.non.quam@aol.com",
                        Age = 55
                    },
                    new Passenger {
                        ConfirmationNumber = 1821,
                        FirstName = "Martina",
                        LastName = "Carney",
                        Job = "Pede Nunc Corp.",
                        Email = "praesent.eu@icloud.net",
                        Age = 61
                    },
                    new Passenger {
                        ConfirmationNumber = 1658,
                        FirstName = "Lilah",
                        LastName = "Cunningham",
                        Job = "Consectetuer Adipiscing LLC",
                        Email = "enim.nisl.elementum@google.edu",
                        Age = 26
                    },
                    new Passenger {
                        ConfirmationNumber = 1250,
                        FirstName = "Dante",
                        LastName = "Harding",
                        Job = "Senectus Et LLC",
                        Email = "sed.pede@outlook.couk",
                        Age = 60
                    },
                    new Passenger {
                        ConfirmationNumber = 1659,
                        FirstName = "Kermit",
                        LastName = "Burgess",
                        Job = "Aliquam Tincidunt Nunc Ltd",
                        Email = "turpis.in@protonmail.edu",
                        Age = 39
                    },
                    new Passenger {
                        ConfirmationNumber = 1125,
                        FirstName = "Evangeline",
                        LastName = "Slater",
                        Job = "Ornare Limited",
                        Email = "neque@aol.ca",
                        Age = 44
                    },
                    new Passenger {
                        ConfirmationNumber = 1584,
                        FirstName = "Porter",
                        LastName = "Pickett",
                        Job = "Blandit Mattis Foundation",
                        Email = "arcu.ac@outlook.org",
                        Age = 65
                    },
                    new Passenger {
                        ConfirmationNumber = 1364,
                        FirstName = "Daniel",
                        LastName = "Langley",
                        Job = "Luctus Industries",
                        Email = "dolor.quisque@icloud.couk",
                        Age = 45
                    },
                    new Passenger {
                        ConfirmationNumber = 1575,
                        FirstName = "Jermaine",
                        LastName = "Burton",
                        Job = "Dolor Fusce Foundation",
                        Email = "nunc.risus@hotmail.ca",
                        Age = 23
                    },
                    new Passenger {
                        ConfirmationNumber = 1395,
                        FirstName = "Stone",
                        LastName = "Justice",
                        Job = "Lectus Convallis Est Foundation",
                        Email = "vel.quam@outlook.couk",
                        Age = 41
                    },
                    new Passenger {
                        ConfirmationNumber = 1352,
                        FirstName = "Reuben",
                        LastName = "Tyson",
                        Job = "Rutrum Urna Incorporated",
                        Email = "vitae.diam.proin@protonmail.com",
                        Age = 63
                    }
                };
            
                foreach (Passenger passenger in passengers)
                {
                    flightServiceContext.Add(passenger);
                }
            }
            if (!flightServiceContext.Seats.Any())
            {
                var seats = new Seat[]
                {
                    new Seat
                    {
                        SeatNumber = 1
                    },
                    new Seat
                    {
                        SeatNumber = 2
                    },
                    new Seat
                    {
                        SeatNumber = 3
                    },
                    new Seat
                    {
                        SeatNumber = 4
                    },
                    new Seat
                    {
                        SeatNumber = 5
                    },

                    new Seat
                    {
                        SeatNumber = 6
                    },
                    new Seat
                    {
                        SeatNumber = 7
                    },
                    new Seat
                    {
                        SeatNumber = 8
                    },
                    new Seat
                    {
                        SeatNumber = 9
                    },
                    new Seat
                    {
                        SeatNumber = 10
                    }
                };
                foreach (Seat seat in seats)
                {
                    flightServiceContext.Add(seat);
                }
            }
            if (!flightServiceContext.Aircrafts.Any())
            {
                var aircrafts = new Aircraft[]
                {
                    new Aircraft {
                        SerialNumber = 4276,
                        AircraftType = "Concorde",
                        PassengerLimit = 10,
                        SeatClass = "Economy"
                    },
                    new Aircraft {
                        SerialNumber = 2452,
                        AircraftType = "Federline",
                        PassengerLimit = 11,
                        SeatClass = "Economy"
                    },
                    new Aircraft {
                        SerialNumber = 3632,
                        AircraftType = "Regional",
                        PassengerLimit = 18,
                        SeatClass = "Business"
                    },
                    new Aircraft {
                        SerialNumber = 4438,
                        AircraftType = "Commuter",
                        PassengerLimit = 12,
                        SeatClass = "Economy"
                    },
                    new Aircraft {
                        SerialNumber = 3342,
                        AircraftType = "Concorde",
                        PassengerLimit = 9,
                        SeatClass = "Business"
                    }
            };
                foreach (Aircraft aircraft in aircrafts)
                {
                    flightServiceContext.Add(aircraft);
                }
            }
            if (!flightServiceContext.Flights.Any())
            {
                //var flights = new Flight[]
                //{
                //    new Flight {
                //    FlightNumber = 3578,
                //    DepartureDate = "Nov 5, 2022",
                //    DepartureTime = "12:50 AM",
                //    ArrivalDate = "Aug 26, 2021",
                //    ArrivalTime = "8:00 PM",
                //    DepartureAirport = "amet",
                //    ArrivalAirport = "at"
                //},
                //    new Flight{
                //        FlightNumber = 4332,
                //        DepartureDate = "Dec 11, 2021",
                //        DepartureTime = "1:19 PM",
                //        ArrivalDate = "Oct 10, 2021",
                //        ArrivalTime = "7:57 PM",
                //        DepartureAirport = "condimentum.",
                //        ArrivalAirport = "lacinia"
                //},
                //    new Flight {
                //        FlightNumber = 4454,
                //        DepartureDate = "Oct 2, 2022",
                //        DepartureTime = "2:56 AM",
                //        ArrivalDate = "Jul 17, 2021",
                //        ArrivalTime = "10:20 AM",
                //        DepartureAirport = "ornare",
                //        ArrivalAirport = "Aliquam"
                //},
                //    new Flight {
                //        FlightNumber = 3746,
                //        DepartureDate = "Jun 23, 2021",
                //        DepartureTime = "11:57 AM",
                //        ArrivalDate = "Oct 23, 2021",
                //        ArrivalTime = "5:18 PM",
                //        DepartureAirport = "ornare",
                //        ArrivalAirport = "pellentesque"
                //},
                //    new Flight {
                //        FlightNumber = 2285,
                //        DepartureDate = "Dec 21, 2022",
                //        DepartureTime = "11:41 AM",
                //        ArrivalDate = "Nov 16, 2021",
                //        ArrivalTime = "10:27 PM",
                //        DepartureAirport = "lorem",
                //        ArrivalAirport = "ipsum"
                //}
                //};
                //foreach (Flight flight in flights)
                //{
                //    flightServiceContext.Add(flight);
                //}
            }

        }
    }
}
