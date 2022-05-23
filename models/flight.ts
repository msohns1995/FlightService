import Passenger from "./passenger";
import Ticket from "./ticket";

interface Flight {
    flightId: number;
    passengerLimit: string;
    aircraftType: string;
    departureDate: string;
    departureTime: string;
    departureAirport: string;
    arrivalDate: string;
    arrivalTime: string;    
    arrivalAirport: string;    
    //tickets: Ticket[];
    //totalTickets: number;
    //passengers: Passenger[];
    //totalPassengers: number;    
}
export interface AddFlight {
    passengerLimit: string;
    aircraftType: string;
    departureDate: string;
    departureTime: string;
    departureAirport: string;
    arrivalDate: string;
    arrivalTime: string;    
    arrivalAirport: string;    
    //tickets: Ticket[];
    //totalTickets: number;
    //passengers: Passenger[];
    //totalPassengers: number;    
}

export default Flight;
