import Ticket, { AddTicket } from '../models/ticket';
import axios from "axios";
import Passenger, { AddPassenger } from '../models/passenger';
import Flight, { AddFlight } from '../models/flight';


const http = axios.create({
    baseURL: "https://localhost:7066",
    headers: {
        'Content-Type': 'application/json'
    }
});

//Ticket Controller Links
const getTickets = () => {
    return http.get<Array<Ticket>>("/api/Tickets");
};

const getTicket = (confirmationNumber: number) => {
    return http.get<Ticket>(`api/Tickets/${confirmationNumber}`);
}

const createTicket = (ticket: AddTicket) => {
    return http.post<Ticket>("api/Tickets", ticket);
};

const updateTicket = (ticket: Ticket) => {
    return http.put<Ticket>(`api/Tickets/${ticket.confirmationNumber}`, ticket);
};

const deleteTicket = (ticket: Ticket) => {
    return http.delete<Ticket>(`api/Tickets/${ticket.confirmationNumber}`);
};

//Passenger Controller Links
const getPassengers = () => {
    return http.get<Array<Passenger>>("/api/Passengers");
};

const getPassenger = (PassengerId: number) => {
    return http.get<Passenger>(`api/Passengers/${PassengerId}`);
}

const createPassenger = (passenger: AddPassenger) => {
    return http.post<Passenger>("api/Passengers", passenger);
};

const updatePassenger = (passenger: Passenger) => {
    return http.put<Passenger>(`api/Passengers/${passenger.passengerId}`, passenger);
};

const deletePassenger = (passenger: Passenger) => {
    return http.delete<Passenger>(`api/Passengers/${passenger.passengerId}`);
};

const addPassengerToFlight = (PassengerId: number, FlightId: number) => {
    let ticket: AddTicket = {
        ticketClass: "First",
        ticketPrice: "$100.00",
        passengerId: PassengerId,
        flightId: FlightId
    };
    //http.put<Passenger>(`api/Passengers/${PassengerId}/${FlightId}`);
    return http.post<Ticket>("api/Tickets", ticket);
};

//Flight Controller Links
const getFlights = () => {
    return http.get<Array<Flight>>("/api/Flights");
};

const getFlight = (FlightId: number) => {
    return http.get<Flight>(`api/Flights/${FlightId}`);
}

const createFlight = (flight: AddFlight) => {
    return http.post<Flight>("api/Flights", flight);
};

const updateFlight = (flight: Flight) => {
    return http.put<Flight>(`api/Flights/${flight.flightId}`, flight);
};

const deleteFlight = (flight: Flight) => {
    return http.delete<Flight>(`api/Flights/${flight.flightId}`);
};


const APIService = {
    getTickets,
    getTicket,
    createTicket,
    updateTicket,
    deleteTicket,

    getPassengers,
    getPassenger,
    createPassenger,
    updatePassenger,
    deletePassenger,

    addPassengerToFlight,

    getFlights,
    getFlight,
    createFlight,
    updateFlight,
    deleteFlight
};

export default APIService;