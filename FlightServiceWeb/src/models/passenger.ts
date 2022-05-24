import Flight from "./flight";
import Ticket from "./ticket";

interface Passenger {
    passengerId: number;
    name: string;
    job: string;
    email: string;
    age: string;
    //flightId: string;
}

export interface AddPassenger {
    name: string;
    job: string;
    email: string;
    age: string;
    //flightId: string;
}

export default Passenger;
