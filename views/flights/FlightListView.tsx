import { AxiosResponse } from "axios";
import React, { ChangeEvent, FormEvent, MouseEvent } from "react";
import { Button, Form, Modal } from "react-bootstrap";
import Flight, { AddFlight } from "../../models/flight";
import APIService from "../../services/apiService";

type FlightListViewProps = {

}

type FlightViewState = {
    showAdd: boolean;
    showEdit: boolean;
    flightId: number;
    passengerLimit: string;
    aircraftType: string;
    departureDate: string;
    departureTime: string;
    departureAirport: string;
    arrivalDate: string;
    arrivalTime: string;
    arrivalAirport: string;
    flights: Flight[];
}
export class FlightListView extends React.Component<FlightListViewProps, FlightViewState> {
    constructor(props: FlightListViewProps) {
        super(props);
        this.state = {
            showAdd: false,
            showEdit: false,
            flightId: 0,
            passengerLimit: "",
            aircraftType: "",
            departureDate: "",
            departureTime: "",
            departureAirport: "",
            arrivalDate: "",
            arrivalTime: "",
            arrivalAirport: "",
            flights: []
        }
    }
    componentDidMount() {
        APIService.getFlights()
            .then((response) => {
                this.setState({
                    flights: response.data
                });
            })
            .catch((err: Error) => {
                //console.log(err);
                alert(err);
            });
    }
    componentDidUpdate(pProps: FlightListView, fState: FlightViewState) {
        if (fState.flights.length !== this.state.flights.length) {
            APIService.getFlights()
                .then((response) => {
                    this.setState({
                        flights: response.data
                    });
                })
                .catch((err: Error) => {
                    //console.log(err);
                    alert(err);
                });
        }
    }

    handleClose = () => this.setState({ showAdd: false, showEdit: false });
    handleShow = () => this.setState({ showAdd: true });
    editShow = (flight: Flight) => {
        this.setState({
            showEdit: true,
            flightId: flight.flightId,
            passengerLimit: flight.passengerLimit,
            aircraftType: flight.aircraftType,
            departureDate: flight.departureDate,
            departureTime: flight.departureTime,
            departureAirport: flight.departureAirport,
            arrivalDate: flight.arrivalDate,
            arrivalTime: flight.arrivalTime,
            arrivalAirport: flight.arrivalAirport

        })
    };
    handleUpdates = (event: FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        let flight: Flight = {
            flightId: this.state.flightId,
            passengerLimit: this.state.passengerLimit,
            aircraftType: this.state.aircraftType,
            departureDate: this.state.departureDate,
            departureTime: this.state.departureTime,
            departureAirport: this.state.departureAirport,
            arrivalDate: this.state.arrivalDate,
            arrivalTime: this.state.arrivalTime,
            arrivalAirport: this.state.arrivalAirport
        };
        APIService.updateFlight(flight)
            .then((response: AxiosResponse<Flight>) => {
                let f = [...this.state.flights];
                f = f.filter((item) => item.flightId === flight.flightId);
                f.push(response.data);
                this.setState({
                    flights: f
                });
            })
        this.handleClose();
    }
    handleClickPreventDefault = (e: MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        let flight: Flight = {
            flightId: this.state.flightId,
            passengerLimit: this.state.passengerLimit,
            aircraftType: this.state.aircraftType,
            departureDate: this.state.departureDate,
            departureTime: this.state.departureTime,
            departureAirport: this.state.departureAirport,
            arrivalDate: this.state.arrivalDate,
            arrivalTime: this.state.arrivalTime,
            arrivalAirport: this.state.arrivalAirport
        };
        APIService.updateFlight(flight)
            .then((response: AxiosResponse<Flight>) => {
                let f = [...this.state.flights];
                f = f.filter((item) => item.flightId === flight.flightId);
                f.push(response.data);
                this.setState({
                    flights: f
                });
            })
        this.handleClose();
    }
    handleDepartureDateChange = (event: ChangeEvent<HTMLInputElement>) => this.setState({ departureDate: event.target.value });
    handleDepartureTimeChange = (event: ChangeEvent<HTMLInputElement>) => this.setState({ departureTime: event.target.value });
    handleArrivalDateChange = (event: ChangeEvent<HTMLInputElement>) => this.setState({ arrivalDate: event.target.value });
    handleArrivalTimeChange = (event: ChangeEvent<HTMLInputElement>) => this.setState({ arrivalTime: event.target.value });
    handleDepartureAirportChange = (event: ChangeEvent<HTMLInputElement>) => this.setState({ departureAirport: event.target.value });
    handleArrivalAirportChange = (event: ChangeEvent<HTMLInputElement>) => this.setState({ arrivalAirport: event.target.value });
    handlepassengerLimitChange = (event: ChangeEvent<HTMLInputElement>) => this.setState({ passengerLimit: event.target.value });
    handleAircraftTypeChange = (event: ChangeEvent<HTMLInputElement>) => this.setState({ aircraftType: event.target.value });
    handleDelete(flight: Flight) {
        APIService.deleteFlight(flight)
            .then((response: AxiosResponse<Flight>) => {
                let f = [...this.state.flights];
                f.push(response.data);
                this.setState({
                    flights: f
                });
            })
        this.forceUpdate();
    }
    handleSaveChanges = (event: FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        let flight: AddFlight = {
            passengerLimit: this.state.passengerLimit,
            aircraftType: this.state.aircraftType,
            departureDate: this.state.departureDate,
            departureTime: this.state.departureTime,
            departureAirport: this.state.departureAirport,
            arrivalDate: this.state.arrivalDate,
            arrivalTime: this.state.arrivalTime,
            arrivalAirport: this.state.arrivalAirport
        };

        APIService.createFlight(flight)
            .then((response: AxiosResponse<Flight>) => {
                let f = [...this.state.flights];
                f.push(response.data);
                this.setState({
                    flights: f
                });
            })
        this.forceUpdate();
        this.handleClose();
    }

    render(): React.ReactNode {
        return (
            <div className="App container">
                <div className="jumbotron">
                    <h2>Flights</h2>
                </div>
                <table className="table table-striped table-bordered table-hover table-highlight">
                    <thead>
                        <tr>
                            <th>Flight Number</th>
                            <th>Passenger Limit</th>
                            <th>Aircraft Type</th>
                            <th>Departure Date</th>
                            <th>Departure Time</th>
                            <th>Departure Airport</th>
                            <th>Arrival Date</th>
                            <th>Arrival Time</th>
                            <th>Arrival Airport</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.state.flights.map((flight: Flight) => (
                            <React.Fragment key={flight.flightId}>
                                <tr id={"flight-" + flight.flightId}>
                                    <td>{flight.flightId}</td>
                                    <td>{flight.passengerLimit}</td>
                                    <td>{flight.aircraftType}</td>
                                    <td>{flight.departureDate}</td>
                                    <td>{flight.departureTime}</td>
                                    <td>{flight.departureAirport}</td>
                                    <td>{flight.arrivalDate}</td>
                                    <td>{flight.arrivalTime}</td>
                                    <td>{flight.arrivalAirport}</td>
                                    <td>
                                        <div className="btn-group">
                                            <Button variant="outline-primary" onClick={() => this.editShow(flight)}>
                                                Edit
                                            </Button>
                                            <Button variant="outline-secondary" onClick={() => this.handleDelete(flight)}>
                                                Delete
                                            </Button>
                                        </div>
                                    </td>
                                </tr>
                            </React.Fragment>
                        ))};
                    </tbody>
                </table>
                <>
                    <Button variant="outline-primary" onClick={this.handleShow}>
                        Add Flight
                    </Button>
                    <Modal show={this.state.showAdd} onHide={this.handleClose}>
                        <Modal.Header closeButton>
                            <Modal.Title>New Flight</Modal.Title>
                        </Modal.Header>
                        <Modal.Body>
                            <Form onSubmit={this.handleSaveChanges}>
                                <Form.Group className="mb-3" controlId="passengerLimit">
                                    <Form.Label>Passenger Limit</Form.Label>
                                    <Form.Control type="text" pattern="[0-9]*" value={this.state.passengerLimit} onChange={this.handlepassengerLimitChange} />
                                </Form.Group>
                                <Form.Group className="mb-3" controlId="aircraftType">
                                    <Form.Label>Aircraft Type</Form.Label>
                                    <Form.Control type="text" value={this.state.aircraftType} onChange={this.handleAircraftTypeChange} required />
                                </Form.Group>
                                <Form.Group className="mb-3" controlId="departureDate">
                                    <Form.Label>Departure Date</Form.Label>
                                    <Form.Control type="date" value={this.state.departureDate} onChange={this.handleDepartureDateChange} required />
                                </Form.Group>
                                <Form.Group className="mb-3" controlId="departureTime">
                                    <Form.Label>Departure Time</Form.Label>
                                    <Form.Control type="time" value={this.state.departureTime} onChange={this.handleDepartureTimeChange} required />
                                </Form.Group>
                                <Form.Group className="mb-3" controlId="departureAirport">
                                    <Form.Label>Departure Airport</Form.Label>
                                    <Form.Control type="text" value={this.state.departureAirport} onChange={this.handleDepartureAirportChange} required />
                                </Form.Group>
                                <Form.Group className="mb-3" controlId="arrivalDate">
                                    <Form.Label>Arrival Date</Form.Label>
                                    <Form.Control type="date" value={this.state.arrivalDate} onChange={this.handleArrivalDateChange} required />
                                </Form.Group>
                                <Form.Group className="mb-3" controlId="arrivalTime">
                                    <Form.Label>Arrival Time</Form.Label>
                                    <Form.Control type="time" value={this.state.arrivalTime} onChange={this.handleArrivalTimeChange} required />
                                </Form.Group>
                                <Form.Group className="mb-3" controlId="arrivalAirport">
                                    <Form.Label>Arrival Airport</Form.Label>
                                    <Form.Control type="text" value={this.state.arrivalAirport} onChange={this.handleArrivalAirportChange} required />
                                </Form.Group>
                                <div className="btn-group">
                                    <Button variant="secondary" onClick={this.handleClose}>
                                        Close
                                    </Button>
                                    <Button variant="primary" type="submit">
                                        Confirm Flight
                                    </Button>
                                </div>
                            </Form>
                        </Modal.Body>
                    </Modal>
                    <Modal show={this.state.showEdit} onHide={this.handleClose}>
                        <Modal.Header closeButton>
                            <Modal.Title>Update Flight #{this.state.flightId}</Modal.Title>
                        </Modal.Header>
                        <Modal.Body>
                            <Form onSubmit={this.handleUpdates}>
                                <Form.Group className="mb-3" controlId="passengerLimit">
                                    <Form.Label>Passenger Limit</Form.Label>
                                    <Form.Control type="text" pattern="[0-9]*" value={this.state.passengerLimit} onChange={this.handlepassengerLimitChange} required/>
                                </Form.Group>
                                <Form.Group className="mb-3" controlId="aircraftType">
                                    <Form.Label>Aircraft Type</Form.Label>
                                    <Form.Control type="text" value={this.state.aircraftType} onChange={this.handleAircraftTypeChange} required/>
                                </Form.Group>
                                <Form.Group className="mb-3" controlId="departureDate">
                                    <Form.Label>Departure Date</Form.Label>
                                    <Form.Control type="date" value={this.state.departureDate} onChange={this.handleDepartureDateChange} required/>
                                </Form.Group>
                                <Form.Group className="mb-3" controlId="departureTime">
                                    <Form.Label>Departure Time</Form.Label>
                                    <Form.Control type="time" value={this.state.departureTime} onChange={this.handleDepartureTimeChange} required/>
                                </Form.Group>
                                <Form.Group className="mb-3" controlId="departureAirport">
                                    <Form.Label>Departure Airport</Form.Label>
                                    <Form.Control type="text" value={this.state.departureAirport} onChange={this.handleDepartureAirportChange} required/>
                                </Form.Group>
                                <Form.Group className="mb-3" controlId="arrivalDate">
                                    <Form.Label>Arrival Date</Form.Label>
                                    <Form.Control type="date" value={this.state.arrivalDate} onChange={this.handleArrivalDateChange} required/>
                                </Form.Group>
                                <Form.Group className="mb-3" controlId="arrivalTime">
                                    <Form.Label>Arrival Time</Form.Label>
                                    <Form.Control type="time" value={this.state.arrivalTime} onChange={this.handleArrivalTimeChange} required/>
                                </Form.Group>
                                <Form.Group className="mb-3" controlId="arrivalAirport">
                                    <Form.Label>Arrival Airport</Form.Label>
                                    <Form.Control type="text" value={this.state.arrivalAirport} onChange={this.handleArrivalAirportChange} required/>
                                </Form.Group>
                                <div className="btn-group">
                                    <Button variant="secondary" onClick={this.handleClose}>
                                        Close
                                    </Button>
                                    <Button variant="primary" type="submit">
                                        Save Changes
                                    </Button>
                                </div>
                            </Form>
                        </Modal.Body>
                    </Modal>
                </>
            </div>
        );
    }
}

export default FlightListView;