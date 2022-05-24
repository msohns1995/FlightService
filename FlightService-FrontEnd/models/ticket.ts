interface Ticket {
    confirmationNumber: number;
    seatNumber: string;
    gateNumber: string;
    ticketClass: string;
    ticketPrice: string;
    passengerId: string;
    flightId: string; 
    // passengerId: number;
    // flightId: number; 
}

export interface AddTicket {
    ticketClass: string;
    ticketPrice: string;
    passengerId: number;
    flightId: number; 
    //passengerId: number;
    //flightId: number; 
}

export default Ticket;
